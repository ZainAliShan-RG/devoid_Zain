using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralControllers
{
    public class ServiceLocator : MonoBehaviour
    {
        // Dictionary to store registered services
        private readonly Dictionary<Type, IRegisterableService> registeredServices = new Dictionary<Type, IRegisterableService>();

        // Singleton instance of the ServiceLocator
        public static ServiceLocator Instance;

        public void Initialize()
        {
            // Ensure only one instance of ServiceLocator exists
            if(Instance != null)
                Destroy(gameObject);
            Instance = this;

            // Iterate through all children of the GameObject this script is attached to
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform childTransform = gameObject.transform.GetChild(i);
                // Attempt to get an IRegisterableService component from the child GameObject
                if (childTransform.gameObject.TryGetComponent<IRegisterableService>(out IRegisterableService service))
                {
                    Debug.Log("Registered service: " + service.GetType().Name); 
                    // Add the service to the registeredServices dictionary
                    registeredServices.Add(service.GetType(), service);
                }
            }   
        }

        // Method to retrieve a registered service of a specified type
        public TService Get<TService>()
        {
            // Attempt to get the service of the specified type from the registeredServices dictionary
            if(registeredServices.TryGetValue(typeof(TService), out IRegisterableService service))
            {
                // Cast the service to the specified type and return it
                return (TService)service;
            }
            // Return the default value for the specified type if the service is not found
            return default(TService);
        }
    }
}

/*
 This script defines a ServiceLocator class which acts as a centralized registry for services within the application.
 It provides a method for other scripts to retrieve registered services of a specified type.
 
 The Initialize method is called to register all services that are children of the GameObject this script is attached to.
 Each child GameObject is checked for a component that implements the IRegisterableService interface, and if found, 
 the service is registered in the registeredServices dictionary using its type as the key.
 
 The Get method allows other scripts to retrieve a registered service of a specified type.
 If a service of the specified type is found in the registeredServices dictionary, it is returned. 
 Otherwise, the default value for the specified type is returned.
*/
