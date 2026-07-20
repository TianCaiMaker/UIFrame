using System;
using System.Collections.Generic;
using Tools;

namespace EventSystems
{
    public static class EventModuleExtension
    {
        #region 注册事件
        // 注册无参事件（字符串名）
        public static void AddEventListener(this EventModule module, string eventName, Action action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener(id, action);
        }

        // 注册无参事件并附带单个 tag
        public static void AddEventListener(this EventModule module, string eventName, Action action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener(id, action, tag);
        }

        // 注册无参事件并附带多个 tags
        public static void AddEventListener(this EventModule module, string eventName, Action action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener(id, action, tags);
        }

        // 注册一参事件（字符串名）
        public static void AddEventListener<T>(this EventModule module, string eventName, Action<T> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T>>(id, action);
        }

        // 注册一参事件并附带单个 tag
        public static void AddEventListener<T>(this EventModule module, string eventName, Action<T> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T>>(id, action, tag);
        }

        // 注册一参事件并附带多个 tags
        public static void AddEventListener<T>(this EventModule module, string eventName, Action<T> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T>>(id, action, tags);
        }
        // 注册两参事件（字符串名）
        public static void AddEventListener<T0, T1>(this EventModule module, string eventName, Action<T0, T1> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1>>(id, action);
        }

        // 注册两参事件并附带单个 tag
        public static void AddEventListener<T0, T1>(this EventModule module, string eventName, Action<T0, T1> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1>>(id, action, tag);
        }

        // 注册两参事件并附带多个 tags
        public static void AddEventListener<T0, T1>(this EventModule module, string eventName, Action<T0, T1> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1>>(id, action, tags);
        }

        // 注册三参事件（字符串名）
        public static void AddEventListener<T0, T1, T2>(this EventModule module, string eventName, Action<T0, T1, T2> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2>>(id, action);
        }

        // 注册三参事件并附带单个 tag
        public static void AddEventListener<T0, T1, T2>(this EventModule module, string eventName, Action<T0, T1, T2> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2>>(id, action, tag);
        }

        // 注册三参事件并附带多个 tags
        public static void AddEventListener<T0, T1, T2>(this EventModule module, string eventName, Action<T0, T1, T2> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2>>(id, action, tags);
        }

        // 注册四参及以上的事件重载（到16个参数）
        public static void AddEventListener<T0, T1, T2, T3>(this EventModule module, string eventName, Action<T0, T1, T2, T3> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3>(this EventModule module, string eventName, Action<T0, T1, T2, T3> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3>(this EventModule module, string eventName, Action<T0, T1, T2, T3> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4>>(id, action, tags);
        }
        #endregion
        #region 取消事件的监听（字符串名重载）
        /// <summary>
        /// 移除无参的事件监听
        /// </summary>
        public static void RemoveEventListener(this EventModule module, string eventName, Action action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除1个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T>(this EventModule module, string eventName, Action<T> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除2个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1>(this EventModule module, string eventName, Action<T0, T1> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除3个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2>(this EventModule module, string eventName, Action<T0, T1, T2> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除4个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3>(this EventModule module, string eventName, Action<T0, T1, T2, T3> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除5个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }
        #endregion
        #region 触发事件
        // 触发无参事件（字符串名）
        public static void EventTrigger(this EventModule module, string eventName)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger(id);
        }

        // 触发一参事件（字符串名）
        public static void EventTrigger<T>(this EventModule module, string eventName, T arg)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T>(id, arg);
        }
        // 触发两参数事件（字符串名）
        public static void EventTrigger<T0, T1>(this EventModule module, string eventName, T0 arg0, T1 arg1)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1>(id, arg0, arg1);
        }

        // 触发三参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2>(id, arg0, arg1, arg2);
        }

        // 触发四参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3>(id, arg0, arg1, arg2, arg3);
        }

        // 触发五参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4>(id, arg0, arg1, arg2, arg3, arg4);
        }
        #endregion
    }
}