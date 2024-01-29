using UnityEditor;
using UnityEngine;

namespace ScratchMe
{
    [CustomEditor(typeof(ScratchSprite))]
    public class ScratchSpriteEditor : Editor
    {
        private void DrawProperty(string name, bool includeChilds = false)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(name), includeChilds);
        }

        public override void OnInspectorGUI()
        {
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