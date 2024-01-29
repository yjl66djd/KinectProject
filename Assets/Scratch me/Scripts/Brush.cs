using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649

namespace ScratchMe
{
    [System.Serializable]
    public class Parameter
    {
        [SerializeField]
        private float min;

        [SerializeField]
        private float max;

        [SerializeField]
        private float minRange;

        [SerializeField]
        private float maxRange;

        public float Value
        {
            get
            {
                return Random.Range(min, max);
            }
        }

        public void Set(float first, float second, float minRange, float maxRange)
        {
            min = Mathf.Min(first, second);
            max = Mathf.Max(first, second);

            this.minRange = Mathf.Min(minRange, minRange);
            this.maxRange = Mathf.Max(minRange, maxRange);
        }

        public void Set(float first, float second)
        {
            min = Mathf.Min(first, second);
            max = Mathf.Max(first, second);
        }

        public Parameter(float value, float minRange, float maxRange)
        {
            Set(value, value, minRange, maxRange);
        }

        public Parameter(float first, float second, float minRange, float maxRange)
        {
            Set(first, second, minRange, maxRange);
        }
    }

    [System.Serializable]
    public class Brush
    {
        private static Texture defaultBrush;

        [SerializeField]
        private Texture brushTexture;

        public Texture BrushTexture
        {
            get
            {
                if (brushTexture == null)
                {
                    if (defaultBrush == null)
                        defaultBrush = Resources.Load<Texture2D>("Scratch/Brushes/Default brush");

                    return defaultBrush;
                }

                return brushTexture;
            }

            set
            {
                brushTexture = value;
            }
        }

        [SerializeField]
        public Parameter Size = new Parameter(40.0f, 60.0f, 1.0f, 500.0f);

        [SerializeField]
        public Parameter Spacing = new Parameter(1.0f, 3.0f, 0.0f, 500.0f);

        [SerializeField]
        public Parameter Rotation = new Parameter(0.0f, 360.0f, -360.0f, 360.0f);

        [SerializeField]
        public bool AlignWithDirection = true;

        private Material material;

        public Material Material
        {
            get
            {
                if (material == null)
                    material = new Material(Resources.Load<Shader>("Scratch/Shaders/Brush"));

                material.mainTexture = BrushTexture;

                return material;
            }
        }
    }
}