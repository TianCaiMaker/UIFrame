using System;
namespace EventSystems
{
    /// <summary>
    /// 事件系统管理器
    /// </summary>
    public static partial class EventBus
    {
        private static EventModule eventModule;
        static EventBus()
        {
            eventModule = new EventModule();
        }
        public static void Init()
        {
            eventModule.ClearAll();
        }

        #region 添加事件的监听，你想要关心某个事件，当这个事件触时，会执行你传递过来的Action
        /// <summary>
        /// 添加无参事件
        /// </summary>
        public static void AddEventListener(int eventId, Action action)
        {
            eventModule.AddEventListener(eventId, action);
        }

        /// <summary>
        /// 添加无参事件并附带单个 tag
        /// </summary>
        public static void AddEventListener(int eventId, Action action, int tag)
        {
            eventModule.AddEventListener(eventId, action, tag);
        }

        /// <summary>
        /// 添加无参事件并附带多个 tags
        /// </summary>
        public static void AddEventListener(int eventId, Action action, System.Collections.Generic.List<int> tags)
        {
            eventModule.AddEventListener(eventId, action, tags);
        }

        /// <summary>
        /// 添加1个参数事件
        /// </summary>
        public static void AddEventListener<T>(int eventId, Action<T> action)
        {
            eventModule.AddEventListener<Action<T>>(eventId, action);
        }

        /// <summary>
        /// 添加带单个 tag 的多参事件监听
        /// </summary>
        public static void AddEventListener<T>(int eventId, Action<T> action, int tag)
        {
            eventModule.AddEventListener<Action<T>>(eventId, action, tag);
        }

        /// <summary>
        /// 添加带多个 tags 的多参事件监听
        /// </summary>
        public static void AddEventListener<T>(int eventId, Action<T> action, System.Collections.Generic.List<int> tags)
        {
            eventModule.AddEventListener<Action<T>>(eventId, action, tags);
        }
        /// <summary>
        /// 添加2个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1>(int eventId, Action<T0, T1> action)
        {
            eventModule.AddEventListener<Action<T0, T1>>(eventId, action);
        }
        /// <summary>
        /// 添加3个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2>(int eventId, Action<T0, T1, T2> action)
        {
            eventModule.AddEventListener<Action<T0, T1, T2>>(eventId, action);
        }
        /// <summary>
        /// 添加4个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3>(int eventId, Action<T0, T1, T2, T3> action)
        {
            eventModule.AddEventListener<Action<T0, T1, T2, T3>>(eventId, action);
        }
        /// <summary>
        /// 添加5个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4>(int eventId, Action<T0, T1, T2, T3, T4> action)
        {
            eventModule.AddEventListener<Action<T0, T1, T2, T3, T4>>(eventId, action);
        }
        /// <summary>
        /// 添加6个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(int eventId, Action<T0, T1, T2, T3, T4, T5> action)
        {
            eventModule.AddEventListener<Action<T0, T1, T2, T3, T4, T5>>(eventId, action);
        }
        /// <summary>
        /// 添加7个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            eventModule.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6>>(eventId, action);
        }
        /// <summary>
        /// 添加8个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            eventModule.AddEventListener<Action<T0, T1, T2, T3, T4, T5, T6, T7>>(eventId, action);
        }
        /// <summary>
        /// 添加9个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加10个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加11个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加12个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加13个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加14个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加15个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        /// <summary>
        /// 添加16个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            eventModule.AddEventListener(eventId, action);
        }
        #endregion

        #region 触发事件
        /// <summary>
        /// 触发无参的事件
        /// </summary>
        public static void EventTrigger(int eventId)
        {
            eventModule.EventTrigger(eventId);
        }
        /// <summary>
        /// 触发1个参数的事件
        /// </summary>
        public static void EventTrigger<T>(int eventId, T arg)
        {
            eventModule.EventTrigger<T>(eventId, arg);
        }
        /// <summary>
        /// 触发2个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1>(int eventId, T0 arg0, T1 arg1)
        {
            eventModule.EventTrigger<T0, T1>(eventId, arg0, arg1);
        }
        /// <summary>
        /// 触发3个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2>(int eventId, T0 arg0, T1 arg1, T2 arg2)
        {
            eventModule.EventTrigger<T0, T1, T2>(eventId, arg0, arg1, arg2);
        }
        /// <summary>
        /// 触发4个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            eventModule.EventTrigger<T0, T1, T2, T3>(eventId, arg0, arg1, arg2, arg3);
        }
        /// <summary>
        /// 触发5个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4>(eventId, arg0, arg1, arg2, arg3, arg4);
        }
        /// <summary>
        /// 触发6个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5>(eventId, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// 触发7个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// 触发8个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// 触发9个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        /// <summary>
        /// 触发10个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// 触发11个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }


        /// <summary>
        /// 触发12个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// 触发13个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// 触发14个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// 触发15个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary>
        /// 触发16个参数的事件
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        #endregion

        #region 取消事件的监听
        /// <summary>
        /// 移除无参的事件监听
        /// </summary>
        public static void RemoveEventListener(int eventId, Action action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除1个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T>(int eventId, Action<T> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除2个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1>(int eventId, Action<T0, T1> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除3个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2>(int eventId, Action<T0, T1, T2> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除4个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3>(int eventId, Action<T0, T1, T2, T3> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除5个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4>(int eventId, Action<T0, T1, T2, T3, T4> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除6个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5>(int eventId, Action<T0, T1, T2, T3, T4, T5> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除7个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除8个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除9个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除10个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除11个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除12个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除13个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除14个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除15个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        /// <summary>
        /// 移除16个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int eventId, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            eventModule.RemoveEventListener(eventId, action);
        }
        #endregion

        #region 移除事件
        /// <summary>
        /// 移除/删除一个事件
        /// </summary>
        public static void RemoveEvent(int eventId)
        {
            eventModule.RemoveEvent(eventId);
        }

        /// <summary>
        /// 根据单个 tag 清空所有带该 tag 的事件
        /// </summary>
        public static void RemoveEventsByTag(int tag)
        {
            eventModule.RemoveEventsByTag(tag);
        }

        /// <summary>
        /// 根据 tag 列表，移除包含任意一个 tag 的所有事件
        /// </summary>
        public static void RemoveEventsByTags(System.Collections.Generic.List<int> tags)
        {
            eventModule.RemoveEventsByTags(tags);
        }

        /// <summary>
        /// 移除所有不包含指定 tag 的事件（单个 tag）
        /// </summary>
        public static void RemoveEventsNotContainingTag(int tag)
        {
            eventModule.RemoveEventsNotContainingTag(tag);
        }

        /// <summary>
        /// 移除所有不包含输入 tag 列表中任一 tag 的事件
        /// </summary>
        public static void RemoveEventsNotContainingTags(System.Collections.Generic.List<int> tags)
        {
            eventModule.RemoveEventsNotContainingTags(tags);
        }

        /// <summary>
        /// 清空事件中心
        /// </summary>
        public static void Clear()
        {
            eventModule.ClearAll();
        }

        #endregion
/*
        #region 类型事件
        /// <summary>
        /// 添加类型事件的监听
        /// 本质上是以T的名称作为事件名称
        /// </summary>
        /// <typeparam name="T">参数类型,建议为struct类型</typeparam>
        /// <param name="action">回调函数</param>
        public static void AddTypeEventListener<T>(Action<T> action)
        {
            AddEventListener<T>(typeof(T).Name, action);
        }

        /// <summary>
        /// 移除/删除一个类型事件
        /// </summary>
        /// <typeparam name="T">事件的参数类型</typeparam>
        public static void RemoveTypeEvent<T>()
        {
            RemoveEvent(typeof(T).Name);
        }

        /// <summary>
        /// 移除类型事件的监听
        /// </summary>
        public static void RemoveTypeEventListener<T>(Action<T> action)
        {
            eventModule.RemoveEventListener(typeof(T).Name, action);
        }

        /// <summary>
        /// 触发类型事件
        /// </summary>
        public static void TypeEventTrigger<T>(T arg)
        {
            EventTrigger(typeof(T).Name, arg);
        }
        #endregion
        */
    }
}
