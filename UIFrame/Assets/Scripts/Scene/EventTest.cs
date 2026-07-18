using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystems;
using System;
namespace Test
{
    public class EventTest : MonoBehaviour
    {
        /*
        string eventName = "TestEvent1";
        string eventName2 = "TestEvent2";
        int eventId1;
        int eventId2;
        //int tagA = 100;
        //int tagB = 200;
        private void Start()
        {
            eventId1 = StringId.Instance.GetId(eventName);
            eventId2 = StringId.Instance.GetId(eventName2);*/
        /*
        // register with tags
        EventBus.AddEventListener<int>(eventId1, OnTestEvent1, tagA);
        EventBus.AddEventListener(eventId2, OnTestEvent2, tagB);
        // add additional listeners without tags
        EventBus.AddEventListener<int>(eventId1, OnTestEvent1);
        EventBus.AddEventListener(eventId2, OnTestEvent2);

        Debug.Log("--- before removals: trigger both ---");
        TriggerEvents();

        // remove events that have tagA
        Debug.Log("Removing events by tagA");
        EventBus.RemoveEventsByTag(tagA);
        Debug.Log("--- after RemoveEventsByTag(tagA): trigger both ---");
        TriggerEvents();

        // now remove events not containing tagB (should remove events not tagged with tagB)
        Debug.Log("Removing events not containing tagB");
        EventBus.RemoveEventsNotContainingTag(tagB);
        Debug.Log("--- after RemoveEventsNotContainingTag(tagB): trigger both ---");
        TriggerEvents();
        */
        /*
        EventBus.AddEventListener<int, int>(eventId1, Add);
    }
    public void TriggerEvents()
    {
        EventBus.EventTrigger(eventId1, 42, 58);
        EventBus.RemoveEventListener<int, int>(eventId1, Add);
        EventBus.EventTrigger(eventName2);

    }

    private void OnTestEvent2()
    {
        Debug.Log("TestEvent2 triggered");
    }

    private void OnTestEvent1(int obj)
    {
        Debug.Log($"TestEvent1 triggered with value: {obj}");
    }
    private void Add(int a , int b)
    {
        Debug.Log($"Add: {a} + {b} = {a + b}");
    }*/
        void Awake()
        {
            EventBus.AddEventListener("TestEvent", Handle);
            EventBus.AddEventListener("TestEvent", Handle2);
        }
        public void TriggerEvent()
        {
            EventBus.EventTrigger("TestEvent");
        }
        public void Handle()
        {
            Debug.Log("EventTest TriggerEvent");
            EventBus.RemoveEventListener("TestEvent", Handle2);
        }
        public void Handle2()
        {
            Debug.Log("EventTest Handle2");
        }
    }

}
