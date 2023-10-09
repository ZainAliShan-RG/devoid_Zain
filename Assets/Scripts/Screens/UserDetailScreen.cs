using System;
using deVoid.UIFramework;
using GeneralControllers;
using Screens.ScrollableUserScreen;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Screens
{
    // Class to represent the User Detail Screen in the UI
    [Serializable]
    public class UserDetailScreen : AWindowController<UserData>
    {
        public TextMeshProUGUI userNameDisplay;  // Text field for displaying the user's name
        public TextMeshProUGUI userEmailDisplay;  // Text field for displaying the user's email
        public Image userProfileImage;  // Image component for displaying the user's profile picture
        private UIFrame uiFrameHandler;  // Instance to manage UI frame operations

        private void Start()
        {
            // Acquire the UIFrame instance to manage UI windows
            uiFrameHandler = ServiceLocator.Instance.Get<UIFrame>();
        }

        // Method triggered when properties are set, to update the UI fields
        protected override void OnPropertiesSet()
        {
            // Check if Properties is not null before accessing its members
            if (Properties != null)
            {
                // Format and set the user's name and email in the UI text fields
                userNameDisplay.text = $"{Properties.firstName} {Properties.lastName}";
                userEmailDisplay.text = Properties.email;

                // Check if the user's image and UI Image component are not null before setting the sprite
                if (Properties.image != null && userProfileImage != null)
                {
                    userProfileImage.sprite = Properties.image;
                }
            }
            else
            {
                // Log an error if Properties is null
                Debug.LogError("Properties is null. Unable to set user details.");
            }
        }

        // Method to handle the logic when the Go Back button is clicked
        public void CloseDetailsWindowBtn()
        {
            // Check if uiFrameHandler is not null before attempting to close the window
            if (uiFrameHandler != null)
            {
                uiFrameHandler.CloseWindow("UserDetailsScreen");
            }
            else
            {
                // Log an error if uiFrameHandler is null
                Debug.LogError("UIFrame handler is null. Unable to close window.");
            }
        }
    }
}

/*
This script defines the behavior of the User Detail Screen in the UI.
It inherits from AWindowController with a generic type parameter of UserData, 
which encapsulates the data of a single user.

Upon starting, it fetches the UIFrame instance to manage UI windows.

The OnPropertiesSet method is overridden to update the UI fields with the user's data.
It sets the user's name and email in the UI text fields and sets the user's profile picture 
in the UI image component if the picture is available.

The OnGoBackClick method handles the logic when the Go Back button is clicked,
closing the UserDetailScreen window and returning to the previous screen.
*/
