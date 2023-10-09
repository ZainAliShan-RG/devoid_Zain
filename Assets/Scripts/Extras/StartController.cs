using deVoid.UIFramework;
using GeneralControllers;
using UnityEngine;

namespace Extras
{
    public class StartController : MonoBehaviour
    {
        public UISettings uiSettings = null;  // Reference to the UI settings
        public ServiceLocator serviceLocator;  // Reference to the Service Locator
        
        void Awake()
        {
            // Check if uiSettings is assigned in the inspector
            if (uiSettings == null)
            {
                Debug.LogError("UISettings is not assigned!");
            }

            // Check if serviceLocator is assigned in the inspector
            if (serviceLocator == null)
            {
                Debug.LogError("ServiceLocator is not assigned!");
            }

            // Create an instance of the UI using the settings provided
            uiSettings.CreateUIInstance();
            
            // Initialize the Service Locator to register services
            serviceLocator.Initialize();
            
            // Get the UIFrame instance from the Service Locator
            UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
            
            // Check if the UIFrame instance was retrieved successfully
            if (uiFrame == null)
            {
                Debug.LogError("UIFrame is null!");
            }
            else
            {
                // Open the StartScreen window
                uiFrame.OpenWindow("StartScreen");
            }
        }
    }
}

/*
 This script defines a StartController class that is responsible for initializing the application at startup.
 It's attached to a GameObject in the scene, and references to the UISettings and ServiceLocator are set in the inspector.

 The Awake method is called when the script is first loaded. It performs several initialization steps:
 - Checks if references to the UISettings and ServiceLocator have been set.
 - Calls CreateUIInstance on the UISettings to create an instance of the UI.
 - Calls Initialize on the Service Locator to register all services.
 - Retrieves the UIFrame instance from the Service Locator.
 - Checks if the UIFrame instance was retrieved successfully.
 - If the UIFrame instance is valid, it opens the StartScreen window to begin the user interface flow.

 This script acts as the entry point for the application, ensuring that all necessary initialization is performed
 before the user interface is presented to the user.
*/
