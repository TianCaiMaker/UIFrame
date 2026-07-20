using System;
using System.Collections.Generic;

namespace EventSystems
{
    public class EventModule
    {

        private Dictionary<int, IEventInfo> eventInfoDic = new Dictionary<int, IEventInfo>();
        #region 内部接口、内部类

        private interface IEventInfo { void Destory(); HashSet<int> EventTags { get; } }

        /// <summary>
        /// 无参-事件信息
        /// </summary>
        private class EventInfo : IEventInfo
        {
            public Action action;
            private HashSet<int> eventTags = new HashSet<int>();
            public HashSet<int> EventTags => eventTags;
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
            private HashSet<int> eventTags = new HashSet<int>();
            public HashSet<int> EventTags => eventTags;
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

        /// <summary>
        /// 添加无参事件并附带单个 tag
        /// </summary>
        public void AddEventListener(int eventId, Action action, int tag)
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo info))
            {
                (info as EventInfo).action += action;
                info.EventTags.Add(tag);
            }
            else
            {
                EventInfo eventInfo = new EventInfo();
                eventInfo.Init(action);
                eventInfo.EventTags.Add(tag);
                eventInfoDic.Add(eventId, eventInfo);
            }
        }

        /// <summary>
        /// 添加无参事件并附带多个 tags
        /// </summary>
        public void AddEventListener(int eventId, Action action, List<int> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                AddEventListener(eventId, action);
                return;
            }
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo info))
            {
                (info as EventInfo).action += action;
                info.EventTags.UnionWith(tags);
            }
            else
            {
                EventInfo eventInfo = new EventInfo();
                eventInfo.Init(action);
                eventInfo.EventTags.UnionWith(tags);
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

        /// <summary>
        /// 添加带单个 tag 的多参事件监听
        /// </summary>
        public void AddEventListener<TAction>(int eventId, TAction action, int tag) where TAction : MulticastDelegate
        {
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo))
            {
                MultipleParameterEventInfo<TAction> info = (MultipleParameterEventInfo<TAction>)eventInfo;
                info.action = (TAction)Delegate.Combine(info.action, action);
                info.EventTags.Add(tag);
            }
            else
            {
                AddMultipleParameterEventInfo(eventId, action);
                eventInfoDic[eventId].EventTags.Add(tag);
            }
        }

        /// <summary>
        /// 添加带多个 tags 的多参事件监听
        /// </summary>
        public void AddEventListener<TAction>(int eventId, TAction action, List<int> tags) where TAction : MulticastDelegate
        {
            if (tags == null || tags.Count == 0)
            {
                AddEventListener<TAction>(eventId, action);
                return;
            }
            if (eventInfoDic.TryGetValue(eventId, out IEventInfo eventInfo))
            {
                MultipleParameterEventInfo<TAction> info = (MultipleParameterEventInfo<TAction>)eventInfo;
                info.action = (TAction)Delegate.Combine(info.action, action);
                info.EventTags.UnionWith(tags);
            }
            else
            {
                AddMultipleParameterEventInfo(eventId, action);
                eventInfoDic[eventId].EventTags.UnionWith(tags);
            }
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
        /// 根据单个 tag 清空所有带该 tag 的事件（检查每个事件的 EventTags 列表）
        /// </summary>
        public void RemoveEventsByTag(int tag)
        {
            var keys = new List<int>(eventInfoDic.Keys);
            foreach (var id in keys)
            {
                if (eventInfoDic.TryGetValue(id, out IEventInfo info))
                {
                    var tags = info.EventTags;
                    if (tags != null && tags.Contains(tag))
                    {
                        RemoveEvent(id);
                    }
                }
            }
        }

        /// <summary>
        /// 根据 tag 列表，移除包含任意一个 tag 的所有事件
        /// </summary>
        public void RemoveEventsByTags(System.Collections.Generic.List<int> tags)
        {
            if (tags == null || tags.Count == 0) return;
            var keys = new List<int>(eventInfoDic.Keys);
            foreach (var id in keys)
            {
                if (eventInfoDic.TryGetValue(id, out IEventInfo info))
                {
                    var eventTags = info.EventTags;
                    if (eventTags == null || eventTags.Count == 0) continue;
                    for (int i = 0; i < tags.Count; i++)
                    {
                        if (eventTags.Contains(tags[i]))
                        {
                            RemoveEvent(id);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 移除所有不包含指定 tag 的事件（单个 tag）
        /// </summary>
        public void RemoveEventsNotContainingTag(int tag)
        {
            var keys = new List<int>(eventInfoDic.Keys);
            foreach (var id in keys)
            {
                if (eventInfoDic.TryGetValue(id, out IEventInfo info))
                {
                    var eventTags = info.EventTags;
                    // 若没有任何 tag 或者不包含指定 tag，则移除
                    if (eventTags == null || eventTags.Count == 0 || !eventTags.Contains(tag))
                    {
                        RemoveEvent(id);
                    }
                }
            }
        }

        /// <summary>
        /// 移除所有不包含输入 tag 列表中任一 tag 的事件（即事件 tags 与输入 tags 无交集时移除）
        /// </summary>
        public void RemoveEventsNotContainingTags(System.Collections.Generic.List<int> tags)
        {
            if (tags == null || tags.Count == 0) return;
            var keys = new List<int>(eventInfoDic.Keys);
            foreach (var id in keys)
            {
                if (eventInfoDic.TryGetValue(id, out IEventInfo info))
                {
                    var eventTags = info.EventTags;
                    // 若无 tags 则视为不包含，移除
                    if (eventTags == null || eventTags.Count == 0)
                    {
                        RemoveEvent(id);
                        continue;
                    }
                    bool has = false;
                    for (int i = 0; i < tags.Count; i++)
                    {
                        if (eventTags.Contains(tags[i]))
                        {
                            has = true;
                            break;
                        }
                    }
                    if (!has) RemoveEvent(id);
                }
            }
        }

        /// <summary>
        /// 清空事件中心所有事件
        /// </summary>
        public void ClearAll()
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
