using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace ScratchMe
{
    [CustomEditor(typeof(ScratchImage))]
    public class ScratchImageEditor : ImageEditor
    {
        private void DrawProperty(string name, bool includeChilds = false)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(name), includeChilds);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            DrawProperty("brush", true);

            EditorGUILayout.Space();

            DrawProperty("fillTextureDownscaleFactor");

            DrawProperty("targetScratchFactor");

            DrawProperty("clearOnCompleteScratch");

            EditorGUILayout.Space();

            DrawProperty("onStartScratching");
            DrawProperty("onScratchAmountChanged");
            DrawProperty("onScratchComplete");

            serializedObject.ApplyModifiedProperties();
        }
    }
}