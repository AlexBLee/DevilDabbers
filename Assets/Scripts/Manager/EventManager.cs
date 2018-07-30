using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq.Expressions;

public class EventManager : MonoBehaviour
{
    // Definition of custom event class.
    [Serializable]
    public class IntResponseEvent : UnityEvent<int> { };

    // Generic event list
    private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    // Event list of this type of event (IntResponseEvent)
    private Dictionary<string, IntResponseEvent> intEventDictionary = new Dictionary<string, IntResponseEvent>();

    public void AddListener(string eventName, UnityAction listener)
    {
        UnityEvent gameEvent = null;
        // Try to get the value from the dictionary
        if (!eventDictionary.TryGetValue(eventName,out gameEvent))
        {
            // If not, that means the event has not been registered.
            // So, create it.

            gameEvent = new UnityEvent();
            eventDictionary.Add(eventName, gameEvent);
        }

        // Check for existence of gameEvent
        if(gameEvent != null)
        {
            gameEvent.AddListener(listener);
        }
    }

    public void AddListener(string eventName, UnityAction<int> listener)
    {
        IntResponseEvent gameEvent = null;
        // Try to get the value from the dictionary
        if (!intEventDictionary.TryGetValue(eventName, out gameEvent))
        {
            // If not, that means the event has not been registered.
            // So, create it.

            gameEvent = new IntResponseEvent();
            intEventDictionary.Add(eventName, gameEvent);
        }

        // Check for existence of gameEvent
        if (gameEvent != null)
        {
            gameEvent.AddListener(listener);
        }
    }

    public bool RemoveEvent(string eventName)
    {
        return false;
    }

    public void TriggerEvent(string eventName)
    {
        if(string.IsNullOrEmpty(eventName))
        {
            return;
        }

        UnityEvent gameEvent = null;
        if(eventDictionary.TryGetValue(eventName, out gameEvent))
        {
            if(gameEvent != null)
            {
                gameEvent.Invoke();
            }
        }
    }

    public void TriggerIntEvent(string eventName, int val)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            return;
        }

        IntResponseEvent gameEvent = null;
        if (intEventDictionary.TryGetValue(eventName, out gameEvent))
        {
            if (gameEvent != null)
            {
                gameEvent.Invoke(val);
            }
        }
    }
}
