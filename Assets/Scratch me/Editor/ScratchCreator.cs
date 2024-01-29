using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace ScratchMe
{
    public class ScratchCreator
    {
        private static GameObject CreateGO(MenuCommand context, string name, List<Type> componentsToAdd)
        {
            var go = new GameObject(name);

            foreach (var component in componentsToAdd)
                go.AddComponent(component);

            GameObjectUtility.SetParentAndAlign(go, context.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeGameObject = go;

            return go;
        }

        [MenuItem("GameObject/2D Object/Scratch sprite")]
        private static void CreateSprite(MenuCommand context)
        {
            var spriteGO = CreateGO(context, "Scratch sprite", new List<Type> { typeof(SpriteRenderer), typeof(ScratchSprite) });
            var spriteComponent = spriteGO.GetComponent<SpriteRenderer>();
            spriteComponent.material = Resources.Load<Material>("Scratch/Materials/Scratch sprite");
        }

        [MenuItem("GameObject/UI/Scratch image")]
        private static void CreateImage(MenuCommand context)
        {
            CreateGO(context, "Scratch image", new List<Type> { typeof(RectTransform), typeof(ScratchImage) });
        }
    }
}