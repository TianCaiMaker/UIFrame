using System.Collections.Generic;
namespace FactMachines
{

    public class FactMachine<TFact> where TFact : struct
    {
        private HashSet<TFact> factSet = new ();
        private Dictionary<TFact, IFactContext> contextDict = new();
        private Dictionary<TFact, List<IFactListener<TFact>>> listenerDict = new();
        private int notifyDepth = 0;
        private Dictionary<IFactListener<TFact>, int> pendingDict = new();
        public void RegisterFact(TFact fact, IFactContext context)
        {
            if (!contextDict.ContainsKey(fact))
            {
                contextDict.Add(fact, context);
            }
        }
        public void UnregisterFact(TFact fact)
        {
            if (contextDict.ContainsKey(fact))
            {
                contextDict.Remove(fact);
            }
        }
        public void RegisterListener(IFactListener<TFact> listener)
        {
            if (notifyDepth > 0)
            {
                if(pendingDict.TryGetValue(listener, out var count))
                {
                    pendingDict[listener] = count + 1;
                }
                else
                {
                    pendingDict[listener] = 1;
                }
                return;
            }
            RegisterListenerImmediate(listener);
        }
        private void RegisterListenerImmediate(IFactListener<TFact> listener)
        {
            foreach (var fact in listener.Facts)
            {
                if (!listenerDict.TryGetValue(fact, out var listeners))
                {
                    listeners = new List<IFactListener<TFact>>();
                    listenerDict[fact] = listeners;
                }
                listeners.Add(listener);
            }
        }
        public void UnregisterListener(IFactListener<TFact> listener)
        {
            if (notifyDepth > 0)
            {
                if(pendingDict.TryGetValue(listener, out var count))
                {
                    pendingDict[listener] = count - 1;
                }
                else
                {
                    pendingDict[listener] = -1;
                }
                return;
            }
            UnregisterListenerImmediate(listener);
        }
        private void UnregisterListenerImmediate(IFactListener<TFact> listener)
        {
            foreach (var fact in listener.Facts)
            {
                if (listenerDict.TryGetValue(fact, out var listeners))
                {
                    listeners.Remove(listener);
                    if (listeners.Count == 0)
                    {
                        listenerDict.Remove(fact);
                    }
                }
            }
        }
        public void AddFact(object source, TFact fact)
        {
            if (contextDict.TryGetValue(fact, out var context))
            {
                if (factSet.Add(fact))
                {
                    NotifyListeners(source, fact);
                    if (context.IsOneShoot)
                    {
                        factSet.Remove(fact);
                    }
                }
            }
        }
        public void RemoveFact(object source, TFact fact)
        {
            if (factSet.Remove(fact))
            {
                NotifyListeners(source, fact);
            }
        }
        private void NotifyListeners(object source, TFact fact)
        {
            notifyDepth++;
            if (listenerDict.TryGetValue(fact, out var listeners))
            {
                foreach (var listener in listeners)
                {
                    listener.OnFactTrigger(source, fact);
                }
            }
            notifyDepth--;
            FlushListenerChanges();
        }
        public IFactContext GetContext(TFact fact)
        {
            if (contextDict.ContainsKey(fact))
            {
                return contextDict[fact];
            }
            else
            {
                return null;
            }
        }
        public bool HasFact(TFact fact)
        {
            return factSet.Contains(fact);
        }
        private void FlushListenerChanges()
        {
            if (notifyDepth > 0)
                return;
            foreach (var kvp in pendingDict)
            {
                IFactListener<TFact> listener = kvp.Key;
                int count = kvp.Value;
                if (count > 0)
                {
                    RegisterListenerImmediate(listener);
                }
                else if (count < 0)
                {
                    UnregisterListenerImmediate(listener);
                }
            }
            pendingDict.Clear();
        }
        public IFactContext GetFactContext(TFact fact)
        {
            if (contextDict.TryGetValue(fact, out var context))
            {
                return context;
            }
            return null;
        }
        public void Clear()
        {
            factSet.Clear();
            contextDict.Clear();
            listenerDict.Clear();
            pendingDict.Clear();
        }
    }
}
