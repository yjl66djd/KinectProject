using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

#pragma warning disable CS0649

namespace ScratchMe
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScratchSprite : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private static MeshBuilderPool buildersPool = new MeshBuilderPool();

        private MaterialPropertyBlock propertyBlock;

        private SpriteRenderer spriteRenderer;

        private RenderTexture maskTarget;

        private CommandBuffer drawingBuffer;

        [SerializeField]
        private ScratchAmountChanged onScratchAmountChanged;

        [SerializeField]
        private UnityEvent onScratchComplete;

        [SerializeField]
        private UnityEvent onStartScratching;

        [SerializeField]
        private Brush brush;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float fillTextureDownscaleFactor = 0.1f;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float targetScratchFactor = 0.25f;

        [SerializeField]
        private bool clearOnCompleteScratch = false;

        private ScratchState scratchState;

        private float currentScratchAmount = 1.0f;

        private Texture2D fillTexture;

        private Dictionary<int, MeshBuilder> builders = new Dictionary<int, MeshBuilder>();

        private float PixelsPerUnit
        {
            get
            {
                return spriteRenderer.sprite == null ? 100 : spriteRenderer.sprite.pixelsPerUnit;
            }
        }

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
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            drawingBuffer = new CommandBuffer();
        }

        private void OnDestroy()
        {
            DestroyImmediate(maskTarget);
            DestroyImmediate(fillTexture);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        private Matrix4x4 GenerateProjectionMatrix()
        {
            var projection = Matrix4x4.identity;
            projection.m00 = 2.0f / maskTarget.width;
            projection.m11 = 2.0f / maskTarget.height;

            return projection;
        }

        private Vector3? GetLocalPosition(PointerEventData eventData)
        {
            var size = Vector2.zero;
            var center = Vector2.zero;
            if (!GetDimentions(out center, out size))
                return Vector2.zero;

            var pixelShift = Vector2.Scale(center, size);

            if (!eventData.pointerCurrentRaycast.isValid)
                return null;

            var local = (Vector2)transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            local += pixelShift;

            var resultingPosition = local * PixelsPerUnit;
            return (Vector3)(resultingPosition - size * PixelsPerUnit * 0.5f) + Vector3.forward;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (builders.ContainsKey(eventData.pointerId))
                return;

            var builder =  buildersPool.Allocate();
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

            var builder = builders[eventData.pointerId]; 

            var localPosition = GetLocalPosition(eventData);
            builder.ClearPrevious(localPosition == null);
            if (localPosition == null)
                return;

            builder.Append(Color.black, 1.0f, localPosition.Value);
            Draw(builder);
        }

        private void Draw(MeshBuilder builder)
        {
            if (drawingBuffer == null)
                drawingBuffer = new CommandBuffer();

            drawingBuffer.SetRenderTarget(maskTarget);
            drawingBuffer.SetViewProjectionMatrices(GenerateProjectionMatrix(), Matrix4x4.identity);
            drawingBuffer.DrawMesh(builder.Mesh, Matrix4x4.identity, brush.Material);

            Graphics.ExecuteCommandBuffer(drawingBuffer);

            drawingBuffer.Clear();

            var center = Vector2.zero;
            var downscaledSize = Vector2.zero;
            if (!GetDimentions(out center, out downscaledSize))
                return;

            downscaledSize *= PixelsPerUnit;
            downscaledSize *= fillTextureDownscaleFactor;

            if (fillTexture == null ||
                fillTexture.width != (int)downscaledSize.x ||
                fillTexture.height != (int)downscaledSize.y)
            {
                DestroyImmediate(fillTexture);
                fillTexture = new Texture2D((int)downscaledSize.x, (int)downscaledSize.y, TextureFormat.RGBA32, false);
            }

            var newFill = FillCalculationUtility.CalculateFillAmount(downscaledSize, fillTexture, maskTarget);
            if (newFill == currentScratchAmount)
                return;

            if (scratchState == ScratchState.Initial)
            {
                onStartScratching.Invoke();
                scratchState = ScratchState.Scratched;
            }

            onScratchAmountChanged.Invoke(newFill);

            var targetFill = Mathf.Max(0.05f, targetScratchFactor);
            if (newFill <= targetFill && scratchState != ScratchState.Complete)
            {
                if (clearOnCompleteScratch)
                    Graphics.Blit(Texture2D.blackTexture, maskTarget);

                scratchState = ScratchState.Complete;
                onScratchComplete.Invoke();
            }
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

        private bool GetDimentions(out Vector2 center, out Vector2 size)
        {
            var sprite = spriteRenderer.sprite;
            if (sprite == null)
            {
                center = Vector2.zero;
                size = Vector2.zero;
                return false;
            }

            size = spriteRenderer.drawMode == SpriteDrawMode.Simple ? sprite.rect.size : spriteRenderer.size;
            if (spriteRenderer.drawMode == SpriteDrawMode.Simple)
                size /= PixelsPerUnit;

            size = Vector2.Scale(size, transform.localScale);

            center = sprite.pivot;
            var spriteSize = sprite.rect.size;
            center.x /= spriteSize.x;
            center.y /= spriteSize.y;

            return true;
        }

        private void Update()
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

            var size = Vector2.zero;
            var center = Vector2.zero;
            if (!GetDimentions(out center, out size))
                return;

            if (maskTarget == null || maskTarget == null ||
                maskTarget.width != (int)(size.x * PixelsPerUnit) ||
                maskTarget.height != (int)(size.y * PixelsPerUnit))
            {
                var previousTarget = maskTarget;
                maskTarget = new RenderTexture((int)(size.x * PixelsPerUnit), (int)(size.y * PixelsPerUnit), 0, RenderTextureFormat.ARGB32);

                Graphics.Blit(Texture2D.whiteTexture, maskTarget);
                if (previousTarget != null)
                {
                    var widthRatio = (float)maskTarget.width / (float)previousTarget.width;
                    var heightRatio = (float)maskTarget.height / (float)previousTarget.height;
                    Graphics.Blit(previousTarget, maskTarget, new Vector2(widthRatio, heightRatio), Vector2.zero);
                }

                DestroyImmediate(previousTarget);
            }

            spriteRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetTexture(PropertyIds.Mask, maskTarget);

            propertyBlock.SetVector(PropertyIds.Rect, new Vector4(center.x, center.y, size.x, size.y));
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}