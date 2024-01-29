using System;
using System.Collections;
using UnityEngine;

namespace CustomFrame
{
    public static class FrameExtension
    {
        #region 通用  
        /// <summary>
        /// 数组相等对比
        /// </summary>
        public static bool ArraryEquals(this object[] objs, object[] other)
        {
            if (other == null || objs.GetType() != other.GetType())
            {
                return false;
            }
            if (objs.Length == other.Length)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!objs[i].Equals(other[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        #endregion
        #region GameObject
        public static bool IsNull(this GameObject obj)
        {
            return ReferenceEquals(obj, null);
        }
        #endregion
        #region Mono

        /// <summary>
        /// 添加Update监听
        /// </summary>
        public static void AddUpdate(this object obj, Action action)
        {
            MonoSystem.AddUpdateListener(action);
        }
        /// <summary>
        /// 移除Update监听
        /// </summary>
        public static void RemoveUpdate(this object obj, Action action)
        {
            MonoSystem.RemoveUpdateListener(action);
        }

        /// <summary>
        /// 添加LateUpdate监听
        /// </summary>
        public static void AddLateUpdate(this object obj, Action action)
        {
            MonoSystem.AddLateUpdateListener(action);
        }
        /// <summary>
        /// 移除LateUpdate监听
        /// </summary>
        public static void RemoveLateUpdate(this object obj, Action action)
        {
            MonoSystem.RemoveLateUpdateListener(action);
        }

        /// <summary>
        /// 添加FixedUpdate监听
        /// </summary>
        public static void AddFixedUpdate(this object obj, Action action)
        {
            MonoSystem.AddFixedUpdateListener(action);
        }
        /// <summary>
        /// 移除Update监听
        /// </summary>
        public static void RemoveFixedUpdate(this object obj, Action action)
        {
            MonoSystem.RemoveFixedUpdateListener(action);
        }
        public static Coroutine StartCoroutine(this object obj, IEnumerator routine)
        {
            return MonoSystem.Start_Coroutine(obj, routine);
        }

        public static void StopCoroutine(this object obj, Coroutine routine)
        {
            MonoSystem.Stop_Coroutine(obj, routine);
        }
         

        #endregion
    }
}