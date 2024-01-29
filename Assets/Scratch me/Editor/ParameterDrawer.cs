using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ScratchMe
{
    [CustomPropertyDrawer(typeof(Parameter))]
    public class ParameterDrawer : PropertyDrawer
    {
        private void CheckIsInRange(SerializedProperty property, SerializedProperty minValue, SerializedProperty maxValue)
        {
            property.floatValue = Mathf.Clamp(property.floatValue, minValue.floatValue, maxValue.floatValue);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var totalWidth = position.width;

            var min = property.FindPropertyRelative("min");
            var max = property.FindPropertyRelative("max");

            var minRange = property.FindPropertyRelative("minRange");
            var maxRange = property.FindPropertyRelative("maxRange");

            EditorGUI.LabelField(position, label);

            var labelWidth = 70.0f;
            position.x += labelWidth;
            position.width -= labelWidth;

            var parametersWidth = 180.0f;
            position.width -= parametersWidth;

            var sliderPosition = position;
            var singleParameterWidth = parametersWidth / 2.0f;
            position.x += position.width;
            position.width = singleParameterWidth;

            EditorGUI.PropertyField(position, min, GUIContent.none);
            position.x += singleParameterWidth;
            EditorGUI.PropertyField(position, max, GUIContent.none);

            var minValue = min.floatValue;
            var maxValue = max.floatValue;

            if (totalWidth >= 300)
            {
                EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, minRange.floatValue, maxRange.floatValue);

                min.floatValue = minValue;
                max.floatValue = maxValue;
            }

            CheckIsInRange(min, minRange, maxRange);
            CheckIsInRange(max, minRange, maxRange);

            if (min.floatValue > max.floatValue)
                max.floatValue = min.floatValue;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}