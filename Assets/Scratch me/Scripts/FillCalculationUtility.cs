using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScratchMe
{
    public static class FillCalculationUtility
    {
        public static float CalculateFillAmount(Vector2 reqiredTextureSize, Texture2D fillTexture, RenderTexture maskTarget)
        {
            var downscaledRT = RenderTexture.GetTemporary((int)reqiredTextureSize.x, (int)reqiredTextureSize.y, 0, RenderTextureFormat.ARGB32);

            Graphics.Blit(maskTarget, downscaledRT);

            var previousTarget = RenderTexture.active;
            RenderTexture.active = downscaledRT;

            fillTexture.ReadPixels(new Rect(0, 0, reqiredTextureSize.x, reqiredTextureSize.y), 0, 0);

            fillTexture.Apply();

            RenderTexture.active = previousTarget;

            RenderTexture.ReleaseTemporary(downscaledRT);

            var data = fillTexture.GetRawTextureData<Color32>();

            var summ = 0L;
            var dataLength = data.Length;
            for (var index = 0; index < dataLength; index++)
            {
                var color = data[index];
                summ += color.r;
            }

            return (float)summ / (dataLength * 255);
        }
    }
}