using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystems;
using System;
namespace Test
{
    public class EventTest : MonoBehaviour
    {
        string eventName = "TestEvent1";
        string eventName2 = "TestEvent2";
        int eventId1;
        int eventId2;
        private void Start()
        {
            eventId1 = Tools.StringId.Instance.GetId(eventName);
            eventId2 = Tools.StringId.Instance.GetId(eventName2);
            EventSystem.AddEventListener<int>(eventId1, OnTestEvent1);
            EventSystem.AddEventListener(eventId2, OnTestEvent2);
            EventSystem.AddEventListener<int>(eventId1, OnTestEvent1);
            EventSystem.AddEventListener(eventId2, OnTestEvent2);
        }
        public void TriggerEvents()
        {
            EventSystem.EventTrigger(eventId1, 42);
            EventSystem.EventTrigger(eventId2);
        }

        private void OnTestEvent2()
        {
            Debug.Log("TestEvent2 triggered");
        }

        private void OnTestEvent1(int obj)
        {
            Debug.Log($"TestEvent1 triggered with value: {obj}");
        }
    }

}
