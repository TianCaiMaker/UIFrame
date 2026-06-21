using System;
using System.Collections.Generic;

namespace EventSystems
{
    public class EventModule
    {

        private Dictionary<int, IEventInfo> eventInfoDic = new Dictionary<int, IEventInfo>();
        #region 内部接口、内部类

        private interface IEventInfo { void Destory(); }

        /// <summary>
        /// 无参-事件信息
        /// </summary>
        private class EventInfo : IEventInfo
        {
            public Action action;
            public void Init(Action action) { this.action = action; }
            public void Destory()
            {
                action = null;
            }
        }

        /// <summary>
        /// 多参Action事件信息
        /// </summary>
        private class MultipleParameterEventInfo<TAction> : IEventInfo where TAction : MulticastDelegate
        {
            public TAction action;
            public void Init(TAction action) { this.action = action; }
            public void Destory()
            {
                action = null;
            }
        };
        #endregion
        #region 添加事件的监听，你想要关心某个事件，当这个事件触时，会执行你传递过来的Action
        /// <summary>
        /// 添加无参事件
        /// </summary>
        public void AddEventListener(int eventId, Action action)
        {
            // 有没有对应的事件可以监听
            if (eventInfoDic.ContainsKey(eventId))
            {
                (eventInfoDic[eventId] as EventInfo).action += action;
            }
            // 没有的话，需要新增 到字典中，并添加对应的Action
            else
            {
                EventInfo eventInfo = new EventInfo();
                eventInfo.Init(action);
                eventInfoDic.Add(eventId, eventInfo);
            }
        }


        // <summary>
        // 添加1参事件监听
        // </summary>
        public void AddEventListener<TAction>(int eventId, TAction action) where TAction : MulticastDelegate
        {
            // 有没有对应的事件可以监听
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo))
            {
                MultipleParameterEventInfo<TAction> info = (MultipleParameterEventInfo<TAction>)eventInfo;
                info.action = (TAction)Delegate.Combine(info.action, action);
            }
            else AddMultipleParameterEventInfo(eventId, action);
        }

        private void AddMultipleParameterEventInfo<TAction>(int eventId, TAction action) where TAction : MulticastDelegate
        {
            MultipleParameterEventInfo<TAction> newEventInfo = new MultipleParameterEventInfo<TAction>();
            newEventInfo.Init(action);
            eventInfoDic.Add(eventId, newEventInfo);
        }
        #endregion

        #region 触发无返回值事件，之所以这么多函数，是避免使用params产生数组GC、装箱问题
        /// <summary>
        /// 触发无参的事件
        /// </summary>
        public void EventTrigger(int eventId)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo))
            {
                ((EventInfo)eventInfo).action?.Invoke();
            }
        }
        /// <summary>
        /// 触发1个参数的事件
        /// </summary>
        public void EventTrigger<T>(int eventId, T arg)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T>>)eventInfo).action?.Invoke(arg);
        }
        /// <summary>
        /// 触发2个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1>(int eventId, T0 arg0, T1 arg1)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1>>)eventInfo).action?.Invoke(arg0, arg1);
        }
        /// <summary>
        /// 触发3个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2>(int eventId, T0 arg0, T1 arg1, T2 arg2)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2>>)eventInfo).action?.Invoke(arg0, arg1, arg2);
        }
        /// <summary>
        /// 触发4个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3);
        }
        /// <summary>
        /// 触发5个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4);
        }
        /// <summary>
        /// 触发6个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// 触发7个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// 触发8个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// 触发9个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>>)eventInfo).action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        /// <summary>
        /// 触发10个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// 触发11个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// 触发12个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// 触发13个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// 触发14个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// 触发15个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary>
        /// 触发16个参数的事件
        /// </summary>
        public void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int eventId, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo)) ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>)eventInfo).action?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        #endregion

        #region 取消事件的监听
        /// <summary>
        /// 移除无参的事件监听
        /// </summary>
        public void RemoveEventListener(int eventId, Action action)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo))
            {
                ((EventInfo)eventInfo).action -= action;
            }
        }
        /// <summary>
        /// 移除有参数的事件监听
        /// </summary>
        public void RemoveEventListener<TAction>(int eventId, TAction action) where TAction : MulticastDelegate
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo))
            {
                MultipleParameterEventInfo<TAction> info = (MultipleParameterEventInfo<TAction>)eventInfo;
                info.action = (TAction)Delegate.Remove(info.action, action);
            }
        }
        #endregion

        #region 移除事件
        /// <summary>
        /// 移除/删除一个事件
        /// </summary>
        public void RemoveEvent(int eventId)
        {
            if (eventInfoDic.Remove(eventId, out IEventInfo eventInfo))
            {
                eventInfo.Destory();
            }
        }

        /// <summary>
        /// 清空事件中心
        /// </summary>
        public void Clear()
        {
            foreach (int eventId in eventInfoDic.Keys)
            {
                eventInfoDic[eventId].Destory();
            }
            eventInfoDic.Clear();
        }

        #endregion
    }
}
