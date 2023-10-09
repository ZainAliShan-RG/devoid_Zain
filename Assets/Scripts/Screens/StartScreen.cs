using System.Collections;
using deVoid.UIFramework;
using GeneralControllers;
using Screens.ScrollableUserScreen;
using UnityEngine;

namespace Screens
{
    // This class controls the behavior of the Start Screen in the UI.
    public class StartScreen : AWindowController
    {
        // Method to handle the logic when the Start button is clicked.
        public void OnStartButtonClicked()
        {
            // Start a coroutine to fetch user data and open the UsersScreen window.
            StartCoroutine(FetchUserDataAndOpenUsersScreen());
        }

        // Coroutine to fetch user data and open the UsersScreen window.
        private IEnumerator FetchUserDataAndOpenUsersScreen()
        {
            UIFrame uiFrameManager = ServiceLocator.Instance.Get<UIFrame>();  // Get the UIFrame instance to manage UI windows.
            ScrollUsersWindow usersScreenController = (ScrollUsersWindow)uiFrameManager.FindWindow("ScrollUsers");  // Find the UsersScreen window to manage user data.

            // Start the coroutine to fetch user data.
            yield return usersScreenController.FetchUsersDataCoroutine();

            // Get the user data.
            UserScreenProperties usersData = usersScreenController.GetUsersData();

            // Open the UsersScreen window with the fetched user data.
            uiFrameManager.OpenWindow("ScrollUsers", usersData);
        }
    }
}

/*
 This script controls the Start Screen's behavior in the UI.
 When the Start button is clicked, it starts a coroutine to fetch user data,
 gets the UIFrame instance to manage UI windows, finds the UsersScreen window to manage user data,
 waits for the user data to be fetched, gets the user data, and opens the UsersScreen window
 with the fetched user data.
*/