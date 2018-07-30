using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator 
{
    // Hold reference to all found services
    // Key = Type
    // Value = Concrete object
    static Dictionary<object, object> serviceContainer = null;

    /// <summary>
    /// Finds a service/script in the current scene and returns a reference of it.
    /// Note: Will still find the service even if it is inactive. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="createObjectIfNotFound"></param>
    /// <returns></returns>

    public static T GetService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        if (serviceContainer == null) serviceContainer = new Dictionary<object, object>();

        try
        {
            if(serviceContainer.ContainsKey(typeof(T)))
            {
                // Found an entry
                T service = (T)serviceContainer[typeof(T)];
                if(service != null)
                {
                    return service;
                }
                else
                {
                    // Associated object is null, remove and attempt to find
                    serviceContainer.Remove(typeof(T));
                    return FindService<T>(createObjectIfNotFound);
                }
            }
            else
            {
                // No entry exists, attempt to find.
                return FindService<T>(createObjectIfNotFound);
            }
        }
        catch(System.Exception ex)
        {
            throw new System.NotImplementedException("Can't find requested service, and create new one is set to " + createObjectIfNotFound);
        }
    }

    public static void RegisterService<T>(T service, bool replaceOriginal = false) where T : Object
    {
        if (serviceContainer == null) serviceContainer = new Dictionary<object, object>();

        if(serviceContainer.ContainsKey(service.GetType()))
        {
            if(replaceOriginal)
            {
                serviceContainer.Remove(service.GetType());
                serviceContainer.Add(service.GetType(), service);
            }
            else
            {
                Debug.LogWarning("Attempting to register service: " + service.GetType().Name + " but service already exists. Destroying Copy.");
                GameObject.Destroy(service);
            }
        }

        else // Container did not contain service
        {
            serviceContainer.Add(service.GetType(), service);
            Debug.Log("Added typeof: " + service.GetType().Name + " Object: " + service.name);
        }
    }

    public static void UnregisterService<T>(T service)
    {
        if(serviceContainer.ContainsKey(service.GetType()))
        {
            serviceContainer.Remove(service.GetType());
        }
    }

    /// <summary>
    /// Looks for a game object with the required type, add to container and return a reference to it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="createObjectIfNotFound"></param>
    /// <returns></returns>
    private static T FindService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        T type = GameObject.FindObjectOfType<T>();
        if (type != null)
        {
            // If found, add it to the dictionary.
            serviceContainer.Add(typeof(T), type);
        }
        else if(createObjectIfNotFound)
        {
            // Create a new gameobject and add the type to it.
            GameObject go = new GameObject(typeof(T).Name, typeof(T));
            serviceContainer.Add(typeof(T), go.GetComponent<T>());
        }
        return (T)serviceContainer[typeof(T)];
    }

}
