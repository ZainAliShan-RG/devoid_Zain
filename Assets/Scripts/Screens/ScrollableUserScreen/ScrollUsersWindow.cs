using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using GeneralControllers;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.ScrollableUserScreen
{
    // Class to hold properties for a user screen window
    [Serializable]
    public class UserScreenProperties : WindowProperties
    {
        public Dictionary<string, UserData> AllUsers = new Dictionary<string, UserData>();  // Dictionary to store all user data
    }

    // Controller for the scrollable users window
    public class ScrollUsersWindow : AWindowController<UserScreenProperties>
    {
        public GameObject userPrefab;  // Prefab for user items
        public Transform content;  // Content transform to hold user items
        [SerializeField] private string userDataUrl = "";  // URL to fetch user data
        private readonly List<User> userActiveListCells = new List<User>();  // List to hold instantiated user items (active user list)
        private readonly List<User> userPool = new List<User>();  // Object pool for user objects
        [SerializeField] private Scrollbar userListScrollbar;  // Scrollbar for the user list
        private AssetController.AssetController assetController;  // Asset controller instance
        private UIFrame uiFrame;  // UI frame instance

        private void Start()
        {
            // Get instances of required services
            assetController = ServiceLocator.Instance.Get<AssetController.AssetController>();
            uiFrame = ServiceLocator.Instance.Get<UIFrame>();
            StartCoroutine(FetchUsersDataCoroutine());  // Start coroutine to fetch user data
        }

        // Coroutine to fetch user data
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator FetchUsersDataCoroutine()
        {
            NetworkController requestManager = ServiceLocator.Instance.Get<NetworkController>();
            yield return requestManager.GetCoroutine<UsersData>(userDataUrl, HandleUserData, HandleNetworkError);  // Get user data using coroutine
        }

        // Callback to handle user data
        private void HandleUserData(UsersData userData)
        {
            foreach (FinalUser userResult in userData.results)
            {
                // Add each user data to the AllUsers dictionary
                Properties.AllUsers.Add(userResult.email, new UserData(userResult.name.first, userResult.name.last, userResult.email, userResult.gender, userResult.phone, userResult.dob.age, userResult.picture.large, userResult.picture.thumbnail));
            }
            OnPropertiesSet();  // Call method to update UI
        }

        // Callback to handle network errors
        private void HandleNetworkError(string error)
        {
            Debug.LogError("Network Error: " + error);
        }

        // Method to get the users data
        public UserScreenProperties GetUsersData()
        {
            return Properties;
        }

        // Method to show user detail screen
        public void ShowUserDetailScreen(string email)
        {
            // Create an instance of SpriteSendProperties to send sprite request
            SpriteSendProperties spriteRequestProperties = new SpriteSendProperties { Email = email };
            assetController.FetchSprite(Properties.AllUsers[email].imageUrl, spriteRequestProperties, ProfilePictureCallBack);
        }

        // Callback method to handle profile picture fetching
        private void ProfilePictureCallBack(ISpriteProperties spriteProperties)
        {
            if (spriteProperties is SpriteSendProperties data)
            {
                // Set the fetched sprite to the user data
                Properties.AllUsers[data.Email].image = data.Sprite;
                Debug.Log("ProfilePictureCallBack called with sprite: " + data.Sprite);
                uiFrame.OpenWindow("UserDetailsScreen", Properties.AllUsers[data.Email]);  // Open user detail screen with updated user data
            }
            else
            {
                Debug.LogError("Invalid ISpriteProperties type: " + spriteProperties.GetType());
            }
        }

        // Method called when properties are set, to initialize the user list
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnPropertiesSet()
        {
            if (Properties == null)
            {
                Debug.LogError("Null: Properties");
                return;
            }

            if (Properties.AllUsers == null)
            {
                Debug.LogError("Null: Properties(AllUsers)");
                return;
            }

            // Instantiate user items and initialize them
            foreach (var userPair in Properties.AllUsers)
            {
                UserData user = userPair.Value;
                User userInfo = GetUserObject();
                userActiveListCells.Add(userInfo);
                userInfo.Initialize(user, ShowUserDetailScreen);
            }
        }

        // Method to handle back button click
        public void CloseScrollUsersWindowBtn()
        {
            CleanUpScrollWindow();  // Clean up the user list
            UI_Close();
        }

        // Method to clean up the user list
        private void CleanUpScrollWindow()
        {
            ReturnAllUserObjectsToPool();  // Return all user objects to the pool
            userActiveListCells.Clear();  // Clear the cells list (Clears active users)
            Properties.AllUsers.Clear();  // Clear the AllUsers dictionary
            userListScrollbar.value = 1f;  // Reset scrollbar value
        }

        // Method to get a user object from the pool or create a new one if none are available
        private User GetUserObject()
        {
            foreach (User user in userPool)
            {
                if (!user.gameObject.activeSelf)
                {
                    user.gameObject.SetActive(true);
                    return user;
                }
            }

            // Instantiate a new user object if none are available in the pool
            GameObject userObject = Instantiate(userPrefab, content);
            User newUser = userObject.GetComponent<User>();
            userPool.Add(newUser);
            return newUser;
        }

        // Method to return all user objects to the pool
        private void ReturnAllUserObjectsToPool()
        {
            foreach (User user in userPool)
            {
                user.gameObject.SetActive(false);
            }
        }
    }
}

/*
This script defines two classes: UserScreenProperties and ScrollUsersWindow.

UserScreenProperties is a class that holds the properties for a user screen window, which consists of a dictionary to store all user data.

ScrollUsersWindow is a controller for the scrollable users window. This class handles fetching user data from a given URL, handling the user data, and managing user objects both in use and in a pool for reuse. It also handles the UI interactions such as showing a user detail screen when a user item is clicked, and cleaning up the user list when the back button is clicked.

The script includes methods for:
- Fetching user data from a network
- Handling the fetched user data
- Handling network errors
- Showing a user detail screen
- Handling the fetching of profile pictures
- Initializing the user list when properties are set
- Handling back button click to close the scrollable users window
- Cleaning up the user list
- Getting a user object from the pool or creating a new one if none are available
- Returning all user objects to the pool for reuse
*/
