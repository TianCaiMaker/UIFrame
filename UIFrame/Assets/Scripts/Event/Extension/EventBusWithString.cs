using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystems
{
    public static partial class EventBus
    {
        #region AddEventListener (string overloads)

        //注册无参事件(字符串名)
        public static void AddEventListener(string eventName, Action action)
        {
            AddEventListener(StringId.Instance.GetId(eventName), action);
        }

        public static void AddEventListener(string eventName, Action action, int tag)
        {
            AddEventListener(StringId.Instance.GetId(eventName), action, tag);
        }

        public static void AddEventListener(string eventName, Action action, List<int> tags)
        {
            AddEventListener(StringId.Instance.GetId(eventName), action, tags);
        }
        //注册一参事件(字符串名)

        public static void AddEventListener<T>(string eventName, Action<T> action)
        {
            AddEventListener<T>(StringId.Instance.GetId(eventName), action);
        }

        public static void AddEventListener<T>(string eventName, Action<T> action, int tag)
        {
            AddEventListener<T>(StringId.Instance.GetId(eventName), action, tag);
        }

        public static void AddEventListener<T>(string eventName, Action<T> action, List<int> tags)
        {
            AddEventListener<T>(StringId.Instance.GetId(eventName), action, tags);
        }
        //注册两参事件(字符串名)
        public static void AddEventListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            AddEventListener<T0, T1>(StringId.Instance.GetId(eventName), action);
        }
        public static void AddEventListener<T0, T1>(string eventName, Action<T0, T1> action, int tag)
        {
            AddEventListener<T0, T1>(StringId.Instance.GetId(eventName), action, tag);
        }
        public static void AddEventListener<T0, T1>(string eventName, Action<T0, T1> action, List<int> tags)
        {
            AddEventListener<T0, T1>(StringId.Instance.GetId(eventName), action, tags);
        }
        //注册三参事件(字符串名)
        public static void AddEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            AddEventListener<T0, T1, T2>(StringId.Instance.GetId(eventName), action);
        }
        public static void AddEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action, int tag)
        {
            AddEventListener<T0, T1, T2>(StringId.Instance.GetId(eventName), action, tag);
        }
        public static void AddEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action, List<int> tags)
        {
            AddEventListener<T0, T1, T2>(StringId.Instance.GetId(eventName), action, tags);
        }
        //注册四参事件(字符串名)
        public static void AddEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            AddEventListener<T0, T1, T2, T3>(StringId.Instance.GetId(eventName), action);
        }
        public static void AddEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action, int tag)
        {
            AddEventListener<T0, T1, T2, T3>(StringId.Instance.GetId(eventName), action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action, List<int> tags)
        {
            AddEventListener<T0, T1, T2, T3>(StringId.Instance.GetId(eventName), action, tags);
        }
        //注册五参事件(字符串名)
        public static void AddEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            AddEventListener<T0, T1, T2, T3, T4>(StringId.Instance.GetId(eventName), action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action, int tag)
        {
            AddEventListener<T0, T1, T2, T3, T4>(StringId.Instance.GetId(eventName), action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action, List<int> tags)
        {
            AddEventListener<T0, T1, T2, T3, T4>(StringId.Instance.GetId(eventName), action, tags);
        }
        #endregion

        #region EventTrigger (string overloads)
        public static void EventTrigger(string eventName)
        {
            EventTrigger(StringId.Instance.GetId(eventName));
        }

        public static void EventTrigger<T>(string eventName, T arg)
        {
            EventTrigger<T>(StringId.Instance.GetId(eventName), arg);
        }

        public static void EventTrigger<T0, T1>(string eventName, T0 arg0, T1 arg1)
        {
            EventTrigger<T0, T1>(StringId.Instance.GetId(eventName), arg0, arg1);
        }

        public static void EventTrigger<T0, T1, T2>(string eventName, T0 arg0, T1 arg1, T2 arg2)
        {
            EventTrigger<T0, T1, T2>(StringId.Instance.GetId(eventName), arg0, arg1, arg2);
        }

        public static void EventTrigger<T0, T1, T2, T3>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            EventTrigger<T0, T1, T2, T3>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            EventTrigger<T0, T1, T2, T3, T4>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4);
        }
        #endregion
        #region 取消事件的监听
        /// <summary>
        /// 移除无参的事件监听
        /// </summary>
        public static void RemoveEventListener(string eventName, Action action)
        {
            eventModule.RemoveEventListener(StringId.Instance.GetId(eventName), action);
        }
        /// <summary>
        /// 移除1个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T>(string eventName, Action<T> action)
        {
            eventModule.RemoveEventListener(StringId.Instance.GetId(eventName), action);
        }
        /// <summary>
        /// 移除2个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            eventModule.RemoveEventListener(StringId.Instance.GetId(eventName), action);
        }
        /// <summary>
        /// 移除3个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            eventModule.RemoveEventListener(StringId.Instance.GetId(eventName), action);
        }
        /// <summary>
        /// 移除4个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            eventModule.RemoveEventListener(StringId.Instance.GetId(eventName), action);
        }
        /// <summary>
        /// 移除5个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            eventModule.RemoveEventListener(StringId.Instance.GetId(eventName), action);
        }
        
        #endregion
    }
}