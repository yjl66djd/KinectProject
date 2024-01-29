using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
 

namespace ScratchMe
{
    [ExecuteInEditMode]
    public class ScratchImage : Image, IPointerDownHandler, IPointerUpHandler, IDragHandler , InteractionListenerInterface
    {
        private static MeshBuilderPool buildersPool = new MeshBuilderPool();

        [SerializeField]
        private ScratchAmountChanged onScratchAmountChanged;

        [SerializeField]
        private UnityEvent onScratchComplete;

        [SerializeField]
        private UnityEvent onStartScratching;

        [SerializeField]
        private Brush brush;


        #region kinectParameter

        [Tooltip("Material used to outline the object when selected.")]
        public Material selectedObjectMaterial;

        [Tooltip("Smooth factor used for object rotation.")]
        public float smoothFactor = 3.0f;

        [Tooltip("Camera used for screen ray-casting. This is usually the main camera.")]
        public Camera screenCamera;

        [Tooltip("UI-Text used to display information messages.")]
        public UnityEngine.UI.Text infoGuiText;

        [Tooltip("Interaction manager instance, used to detect hand interactions. If left empty, the component will try to find a proper interaction manager in the scene.")]
        private InteractionManager interactionManager;

        [Tooltip("Index of the player, tracked by the respective InteractionManager. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
        public int playerIndex = 0;

        [Tooltip("Whether the left hand interaction is allowed by the respective InteractionManager.")]
        public bool leftHandInteraction = true;

        [Tooltip("Whether the right hand interaction is allowed by the respective InteractionManager.")]
        public bool rightHandInteraction = true;


        //private bool isLeftHandDrag = false;
        private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;
        private Vector3 screenNormalPos = Vector3.zero;

        private GameObject selectedObject;
        private Material savedObjectMaterial;
        private Animator animator;
         
        //private bool m_isLeftHand = false;
        private bool m_leftHandGrip = false;
        private bool m_rightHandGrip = false;
        private Vector3 m_handCursorPos = Vector3.zero;
        private Vector2 m_lastCursorPos = Vector2.zero;
        private PointerEventData.FramePressState m_framePressState = PointerEventData.FramePressState.NotChanged;
        public Transform target;
        public float splitRange;
        public Vector3 startPosition;
        private bool canMOVE = true;
        public bool CanScrach;
        #endregion



        public Brush Brush
        {
            get
            {
                return brush; 
            }

            set
            {
                if (value == null)
                    throw new ArgumentException("Null brush is not allowed");

                brush = value;
            }
        }

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float fillTextureDownscaleFactor = 0.1f;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float targetScratchFactor = 0.05f;

        [SerializeField]
        private bool clearOnCompleteScratch = false;

        [SerializeField]
        private RawImage tempImage;

        private ScratchState scratchState;

        private RenderTexture maskTarget;

        private CommandBuffer drawingBuffer;

        private Texture2D fillTexture;

        private float currentScratchAmount = 1.0f;

        private Dictionary<int, MeshBuilder> builders = new Dictionary<int, MeshBuilder>();

        public float CurrentScratchAmount
        {
            get
            {
                return currentScratchAmount;
            }
        }

        public ScratchAmountChanged OnScratchAmountChanged
        {
            get
            {
                return onScratchAmountChanged;
            }
        }

        public UnityEvent OnScratchComplete
        {
            get
            {
                return onScratchComplete;
            }
        }

        public UnityEvent OnStartScratching
        {
            get
            {
                return onStartScratching;
            }
        }

        protected override void Start()
        {
            base.Start();
             
            m_Material = new Material(Resources.Load<Shader>("Scratch/Shaders/ScratchImage"));

            if (sprite == null)
                return;

            drawingBuffer = new CommandBuffer();
            CreateRenderTarget();

             
            //Vector3 randomSeedPos = startPosition + new Vector3(startPosition.x + Random.Range(2, splitRange), startPosition.y + Random.Range(2, splitRange),0);


            // by default set the main-camera to be screen-camera
            if (screenCamera == null)
            {
                screenCamera = Camera.main;
            }
            // get the interaction manager instance
            if (interactionManager == null)
            {
                //interactionManager = InteractionManager.Instance;
                interactionManager = GetInteractionManager();
            }
            interactionManager.interactionListeners.Add(this);
        }

         
#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            m_Material = new Material(Resources.Load<Shader>("Scratch/Shaders/ScratchImage"));
        }
#endif

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (RenderTexture.active == maskTarget)
                RenderTexture.active = null;

            DestroyImmediate(maskTarget);
            DestroyImmediate(fillTexture);

            if (drawingBuffer != null)
            {
                drawingBuffer.Dispose();
                drawingBuffer = null;
            }
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();

            CreateRenderTarget();
        }

        private void CreateRenderTarget()
        {
            var adjustRect = GetPixelAdjustedRect();
            if (adjustRect.width < 1.0f || adjustRect.height < 1.0f)
                return;

            var newImage = new RenderTexture((int)adjustRect.width, (int)adjustRect.height, 32, RenderTextureFormat.Default);
            if (maskTarget != null)
                DestroyImmediate(fillTexture);

            var previousTarget = maskTarget;
            maskTarget = newImage;

            Graphics.Blit(Texture2D.whiteTexture, maskTarget);
            if (previousTarget != null)
            {
                var widthRatio = (float)maskTarget.width / (float)previousTarget.width;
                var heightRatio = (float)maskTarget.height / (float)previousTarget.height;
                Graphics.Blit(previousTarget, maskTarget, new Vector2(widthRatio, heightRatio), Vector2.zero);
            }

            material.SetTexture(PropertyIds.Mask, maskTarget);

            if (tempImage != null)
                tempImage.texture = maskTarget;

            DestroyImmediate(previousTarget);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (builders.ContainsKey(eventData.pointerId))
                return;

            var builder = buildersPool.Allocate();
            builders.Add(eventData.pointerId, builder);
            builder.Brush = brush;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!builders.ContainsKey(eventData.pointerId))
                return;

            buildersPool.Release(builders[eventData.pointerId]);
            builders.Remove(eventData.pointerId);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!builders.ContainsKey(eventData.pointerId))
                return;
            //Debug.Log(eventData.position);
            var builder = builders[eventData.pointerId];

            builder.ClearPrevious();
            Debug.Log(GetLocalPosition(eventData));
            builder.Append(Color.black, 1.0f, GetLocalPosition(eventData));
            Draw(builder);
        }

        public void Clear()
        {
            if (scratchState == ScratchState.Initial)
                return;

            if (maskTarget != null)
                Graphics.Blit(Texture2D.whiteTexture, maskTarget);

            scratchState = ScratchState.Initial;
            onScratchAmountChanged.Invoke(1.0f);
        }

        private void Draw(MeshBuilder builder)
        {
            if (material == null)
                return;

            if (drawingBuffer == null)
                drawingBuffer = new CommandBuffer();

            drawingBuffer.SetRenderTarget(maskTarget);

            drawingBuffer.SetViewMatrix(Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1)) * Matrix4x4.LookAt(Vector3.zero, Vector3.forward, Vector3.up).inverse);

            drawingBuffer.SetProjectionMatrix(Matrix4x4.Ortho(-(float)maskTarget.width * 0.5f, (float)maskTarget.width * 0.5f, -(float)maskTarget.height * 0.5f, (float)maskTarget.height * 0.5f, 0.01f, 3000.0f));

            drawingBuffer.DrawMesh(builder.Mesh, Matrix4x4.identity, brush.Material);

            Graphics.ExecuteCommandBuffer(drawingBuffer);

            drawingBuffer.Clear();

            var requiredTextureSize = GetPixelAdjustedRect();
            requiredTextureSize.width *= fillTextureDownscaleFactor;
            requiredTextureSize.height *= fillTextureDownscaleFactor;

            if (fillTexture == null ||
                fillTexture.width != (int)requiredTextureSize.width ||
                fillTexture.height != (int)requiredTextureSize.height)
            {
                DestroyImmediate(fillTexture);
                fillTexture = new Texture2D((int)requiredTextureSize.width, (int)requiredTextureSize.height, TextureFormat.RGBA32, false);
            }

            var newFill = FillCalculationUtility.CalculateFillAmount(requiredTextureSize.size, fillTexture, maskTarget);
            if (newFill == currentScratchAmount)
                return;

            if (scratchState == ScratchState.Initial)
            {
                onStartScratching.Invoke();
                scratchState = ScratchState.Scratched;
            }

            var targetFill = Mathf.Max(0.05f, targetScratchFactor);
            currentScratchAmount = newFill;
            onScratchAmountChanged.Invoke(currentScratchAmount);
            if (newFill <= targetFill && scratchState != ScratchState.Complete)
            {
                if (clearOnCompleteScratch)
                    Graphics.Blit(Texture2D.blackTexture, maskTarget);

                scratchState = ScratchState.Complete;
                GameRoot.Instance.interactionManager.interactionListeners.Remove(this);
                onScratchComplete.Invoke();
            }
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            base.OnPopulateMesh(toFill);

            var size = GetPixelAdjustedRect();
            var scale = new Vector3(1.0f / size.width, 1.0f / size.height, 1.0f);
            var halfVector = Vector3.one * 0.5f;

            var verteciesCount = toFill.currentVertCount;
            for (var index = 0; index < verteciesCount; index++)
            {
                UIVertex vertex = new UIVertex();
                toFill.PopulateUIVertex(ref vertex, index);
                vertex.uv1 = (Vector2)(Vector3.Scale(vertex.position, scale) + halfVector);
                toFill.SetUIVertex(vertex, index);
            }
        }

        private Vector3 GetLocalPosition(PointerEventData eventData)
        {
            var local = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out local);
            return (Vector3)local + Vector3.forward;
        }
        private Vector3 GetLocalPosition(Vector3 Data)
        {
            var local = Vector2.zero;
            Vector2 screenpos = new Vector2(Mathf.Lerp(0, 1920, Data.x), Mathf.Lerp(0, 1080, Data.y));
            //Debug.Log(screenpos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), screenpos, null, out local);
            return (Vector3)local + Vector3.forward;
        }

        private void Update()
        {
            var currentCanvas = canvas;
            if (currentCanvas == null)
                return;

            currentCanvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
            if (interactionManager != null && interactionManager.IsInteractionInited() && canMOVE)
            {
                Vector3 screenPixelPos = Vector3.zero;

                // no object is currently selected or dragged.
                bool bHandIntAllowed = (leftHandInteraction && interactionManager.IsLeftHandPrimary()) || (rightHandInteraction && interactionManager.IsRightHandPrimary());

                // check if there is an underlying object to be selected
                if (lastHandEvent == InteractionManager.HandEventType.Grip && bHandIntAllowed)
                {
                    // convert the normalized screen pos to pixel pos
                    screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

                    screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
                    screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
                    //Ray ray = screenCamera ? screenCamera.ScreenPointToRay(screenPixelPos) : new Ray();
                    //// check for underlying objects
                    //RaycastHit hit;
                    //if (Physics.Raycast(ray, out hit))
                    //{
                    //    if (hit.collider.gameObject == gameObject)
                    //    {
                    //        selectedObject = gameObject;

                    //        //savedObjectMaterial = selectedObject.GetComponent<Renderer>().material;
                    //        selectedObject.GetComponent<Renderer>().material = selectedObjectMaterial;
                    //    }
                    //}
                }
                if (bHandIntAllowed)
                {
                    // continue dragging the object
                    screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

                    //float angleArounfY = screenNormalPos.x * 360f;  // horizontal rotation
                    //float angleArounfX = screenNormalPos.y * 360f;  // vertical rotation

                    //Vector3 ScrabPos = ViewPointToWorldPoint(new Vector2(screenNormalPos.x, screenNormalPos.y), selectedObject.transform.position.z);
                    //Vector3 movepos = Vector3.Lerp(selectedObject.transform.position, ScrabPos, 1f);
                    //Debug.Log(screenNormalPos);
                    //selectedObject.transform.position = new Vector3(ScrabPos.x, ScrabPos.y, selectedObject.transform.position.z);

                    //Vector3 vObjectRotation = new Vector3(-angleArounfX, -angleArounfY, 180f);
                    //Quaternion qObjectRotation = screenCamera ? screenCamera.transform.rotation * Quaternion.Euler(vObjectRotation) : Quaternion.Euler(vObjectRotation); 
                    // check if the object (hand grip) was released

                    if (!builders.ContainsKey(1))
                        return;

                    var builder = builders[1];
                    //Debug.Log(GetLocalPosition(screenNormalPos));
                    builder.ClearPrevious();
                    builder.Append(Color.black, 1.0f, GetLocalPosition(screenNormalPos));
                    Draw(builder);
                    //Debug.Log("拖拽1");
                    //Debug.Log(screenNormalPos);
                    bool isReleased = lastHandEvent == InteractionManager.HandEventType.Release;

                    if (isReleased)
                    {
                        Debug.Log("拖拽2");
                        // restore the object's material and stop dragging the object
                        //selectedObject.GetComponent<Renderer>().material = savedObjectMaterial;
                        //selectedObject = null;
                    }
                }


            }
        }
        private InteractionManager GetInteractionManager()
        {
            // find the proper interaction manager
            MonoBehaviour[] monoScripts = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];

            foreach (MonoBehaviour monoScript in monoScripts)
            {
                if ((monoScript is InteractionManager) && monoScript.enabled)
                {
                    InteractionManager manager = (InteractionManager)monoScript;

                    if (manager.playerIndex == playerIndex && manager.leftHandInteraction == leftHandInteraction && manager.rightHandInteraction == rightHandInteraction)
                    {
                        return manager;
                    }
                }
            }

            // not found
            return null;
        }
        public void HandGripDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
        {
            Debug.Log("Detected");
            Debug.Log(screenNormalPos);
            if (!CanScrach)
            {
                return;
            }
            if (userIndex != playerIndex || !isHandInteracting)
                return;

            bool bHandValid = (leftHandInteraction && !isRightHand) || (rightHandInteraction && isRightHand);
            if (!bHandValid)
                return;

            //Debug.Log("HandGripDetected");

            m_framePressState = PointerEventData.FramePressState.Pressed;
            //m_isLeftHand = !isRightHand;
            m_handCursorPos = handScreenPos;

            if (!isRightHand)
                m_leftHandGrip = true;
            else
                m_rightHandGrip = true;
            screenNormalPos = handScreenPos;


            if (builders.ContainsKey(1))
                return;

            var builder = buildersPool.Allocate();
            builders.Add(1, builder);
            builder.Brush = brush;


        }

        public void HandReleaseDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
        {
            Debug.Log("Release");
            Debug.Log(screenNormalPos);
            if (!CanScrach)
            {
                return;
            }
            if (userIndex != playerIndex || !isHandInteracting)
                return;

            bool bHandValid = (leftHandInteraction && !isRightHand) || (rightHandInteraction && isRightHand);
            if (!bHandValid)
                return;

            //Debug.Log("HandReleaseDetected");

            m_framePressState = PointerEventData.FramePressState.Released;
            //m_isLeftHand = !isRightHand;
            m_handCursorPos = handScreenPos;

            if (!isRightHand)
                m_leftHandGrip = false;
            else
                m_rightHandGrip = false;
            screenNormalPos = handScreenPos;

            if (!builders.ContainsKey(1))
                return;

            buildersPool.Release(builders[1]);
            builders.Remove(1);
        }

        public bool HandClickDetected(long userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
        {
            Debug.Log("Click");
            if (userIndex != playerIndex)
                return false;

            bool bHandValid = (leftHandInteraction && !isRightHand) || (rightHandInteraction && isRightHand);
            if (!bHandValid)
                return false;
            return true;
        }
        // UI 坐标转换为屏幕坐标
        public Vector2 UIPointToScreenPoint(Vector3 worldPoint)
        {
            // RectTransform：target
            // worldPoint = target.position; 

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(screenCamera, worldPoint);
            return screenPoint;
        }

        public Vector3 ViewPointToWorldPoint(Vector2 screenPoint, float planeZ)
        {
            // Camera.main 世界摄像机
            Vector3 position = new Vector3(screenPoint.x, screenPoint.y, planeZ);
            Vector3 worldPoint = screenCamera.ViewportToWorldPoint(position);
            return worldPoint;
        }
    }
   
}
 