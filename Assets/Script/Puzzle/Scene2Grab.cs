using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scene2Grab : MonoBehaviour, InteractionListenerInterface
{
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

    public Transform target;
    public float splitRange;
    public Vector3 startPosition;
    private bool canMOVE =true;

    public GameObject TimeLine2;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; 
        transform.position = target.position;

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

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(transform.position , startPosition) <= 1.5f && selectedObject == null && canMOVE )
        {
            transform.position = startPosition;
            canMOVE = false;
            TimeLine2.SetActive(true);
            GameRoot.Instance.interactionManager.interactionListeners.Remove(this);
        }
       
        if (interactionManager != null && interactionManager.IsInteractionInited()&& canMOVE)
        {
            Vector3 screenPixelPos = Vector3.zero; 
            if (selectedObject == null)
            { 
                // no object is currently selected or dragged.
                bool bHandIntAllowed = (leftHandInteraction && interactionManager.IsLeftHandPrimary()) || (rightHandInteraction && interactionManager.IsRightHandPrimary());

                // check if there is an underlying object to be selected
                if (lastHandEvent == InteractionManager.HandEventType.Grip && bHandIntAllowed)
                {
                    // convert the normalized screen pos to pixel pos
                    screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

                    screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
                    screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
                    Ray ray = screenCamera ? screenCamera.ScreenPointToRay(screenPixelPos) : new Ray(); 
                    // check for underlying objects
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            selectedObject = gameObject;
                            Debug.Log(111);
                            savedObjectMaterial = selectedObject.GetComponent<SpriteRenderer>().material;
                            selectedObject.GetComponent<SpriteRenderer>().material = selectedObjectMaterial;
                        }
                    }
                }
            }
            else
            {
                bool bHandIntAllowed = (leftHandInteraction && interactionManager.IsLeftHandPrimary()) || (rightHandInteraction && interactionManager.IsRightHandPrimary());

                if (bHandIntAllowed)
                {
                    // continue dragging the object
                    screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

                    //float angleArounfY = screenNormalPos.x * 360f;  // horizontal rotation
                    //float angleArounfX = screenNormalPos.y * 360f;  // vertical rotation
                    
                    Vector3 ScrabPos = ViewPointToWorldPoint(new Vector2(screenNormalPos.x , screenNormalPos.y), selectedObject.transform.position.z);
                    Vector3 movepos = Vector3.Lerp(selectedObject.transform.position, ScrabPos, 1f);
                    Debug.Log(screenNormalPos);
                    selectedObject.transform.position = new Vector3(ScrabPos.x, ScrabPos.y, selectedObject.transform.position.z);
                     
                    //Vector3 vObjectRotation = new Vector3(-angleArounfX, -angleArounfY, 180f);
                    //Quaternion qObjectRotation = screenCamera ? screenCamera.transform.rotation * Quaternion.Euler(vObjectRotation) : Quaternion.Euler(vObjectRotation); 
                    // check if the object (hand grip) was released

                    bool isReleased = lastHandEvent == InteractionManager.HandEventType.Release;

                    if (isReleased)
                    {
                        // restore the object's material and stop dragging the object
                        selectedObject.GetComponent<SpriteRenderer>().material = savedObjectMaterial;
                        selectedObject = null;
                    }
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
        if (!isHandInteracting || !interactionManager)
            return;
        if (userId != interactionManager.GetUserID())
            return;

        lastHandEvent = InteractionManager.HandEventType.Grip;
        //isLeftHandDrag = !isRightHand;
        screenNormalPos = handScreenPos;
    }

    public void HandReleaseDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
        if (!isHandInteracting || !interactionManager)
            return;
        if (userId != interactionManager.GetUserID())
            return;

        lastHandEvent = InteractionManager.HandEventType.Release;
        //isLeftHandDrag = !isRightHand;
        screenNormalPos = handScreenPos;
    }

    public bool HandClickDetected(long userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
    {
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

    public  Vector3 ViewPointToWorldPoint(Vector2 screenPoint, float planeZ)
    {
        // Camera.main 世界摄像机
        Vector3 position = new Vector3(screenPoint.x, screenPoint.y, planeZ);
        Vector3 worldPoint = screenCamera.ViewportToWorldPoint(position);
        return worldPoint;
    } 
}
