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

        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(id, action, tags);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(id, action);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, int tag)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(id, action, tag);
        }
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, List<int> tags)
        {
            int id = StringId.Instance.GetId(eventName);
            module.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(id, action, tags);
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

        /// <summary>
        /// 移除6个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除7个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除8个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除9个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除10个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除11个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除12个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除13个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除14个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除15个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            int id = StringId.Instance.GetId(eventName);
            module.RemoveEventListener(id, action);
        }

        /// <summary>
        /// 移除16个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this EventModule module, string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
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

        // 触发六参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5>(id, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        // 触发七参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        // 触发八参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        // 触发九参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        // 触发十参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        // 触发十一参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        // 触发十二参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        // 触发十三参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        // 触发十四参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        // 触发十五参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        // 触发十六参数事件（字符串名）
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this EventModule module, string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            int id = StringId.Instance.GetId(eventName);
            module.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(id, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        #endregion
    }
}