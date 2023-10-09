using System;
using GeneralControllers;
using Screens.ScrollableUserScreen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // This class represents a User in the UI and handles the displaying of user information and image.
    public class User : MonoBehaviour
    {
        public TextMeshProUGUI usernameText;          // Text field for displaying the user's name.
        public TextMeshProUGUI emailText;             // Text field for displaying the user's email.
        public TextMeshProUGUI phoneText;             // Text field for displaying the user's phone number.
        public Button viewDetailsButton;              // Button for viewing more details of the user.
        private Action<string> _onDetailButtonClick;  // Action to be triggered on detail button click.
        public string thumbnailUrl;                   // URL of the user's thumbnail image.
        public Image thumbnailImage;                  // Image UI element for displaying the user's thumbnail.
        private AssetController.AssetController _assetController;     // Controller for handling asset fetching.

        // Initializes user UI elements and fetches the user's thumbnail image.
        public void Initialize(UserData userData, Action<string> onDetailButtonClick)
        {
            _assetController = ServiceLocator.Instance.Get<AssetController.AssetController>();  // Get the AssetController instance.
            usernameText.text = userData.first + " " + userData.last;          // Set the username text.
            emailText.text = userData.email;                                   // Set the email text.
            phoneText.text = userData.phone;                                   // Set the phone text.
            _onDetailButtonClick = onDetailButtonClick;                        // Set the action to be triggered on detail button click.
            thumbnailUrl = userData.thumbnailUrl;                              // Set the thumbnail URL.
            ISpriteProperties spriteProperties = new SpriteSendProperties();   // Create a new sprite properties object.
            _assetController.FetchSprite(thumbnailUrl, spriteProperties, ThumbnailCallback);  // Fetch the sprite for the thumbnail.
        }

        // Callback for setting the fetched sprite to the thumbnail Image UI element.
        private void ThumbnailCallback(ISpriteProperties spriteProperties)
        {
            thumbnailImage.sprite = spriteProperties.Sprite;  // Set the fetched sprite to the thumbnail image.
            Debug.Log("ThumbnailCallback called with sprite: " + spriteProperties.Sprite);  // Log the callback.
        }

        // Method triggered on the View Details button click.
        public void OnViewDetailsClick()
        {
            _onDetailButtonClick(emailText.text);  // Trigger the action on detail button click, passing the email text.
        }
    }
}

/*
 This script handles the display of user information in the UI, including the user's name, email, phone number, and thumbnail image.
 It provides methods to initialize the display with user data, to handle the action triggered when the View Details button is clicked, and to set the fetched sprite to the thumbnail Image UI element.
*/
