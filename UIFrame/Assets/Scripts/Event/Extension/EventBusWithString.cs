using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystems
{
    public static partial class EventBus
    {
        #region AddEventListener (string overloads)
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

        public static void EventTrigger<T0, T1, T2, T3, T4, T5>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(StringId.Instance.GetId(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }
        #endregion
    }
}