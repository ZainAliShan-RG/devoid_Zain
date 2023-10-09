using System;
using deVoid.UIFramework;
using UnityEngine;

namespace Screens.ScrollableUserScreen
{
    // Class definition for User Data with serialization support
    [Serializable]
    public class UserData : WindowProperties
    {
        public string firstName;    // User's first name
        public string lastName;     // User's last name
        public string email;        // User's email address
        public string gender;       // User's gender
        public string phone;        // User's phone number
        public int age;             // User's age
        public string imageUrl;     // URL of the user's image
        public Sprite image = null; // User's image as a Sprite
        public string thumbnailUrl = null; // URL of the user's thumbnail image

        // Constructor to initialize UserData with provided values
        public UserData(string firstName, string lastName, string email, string gender, string phone, int age, string imageUrl, string thumbnailUrl)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.gender = gender;
            this.phone = phone;
            this.age = age;
            this.imageUrl = imageUrl;
            this.thumbnailUrl = thumbnailUrl;
        }
    }
}

/*
 This script defines a serializable UserData class inheriting from WindowProperties, intended to hold individual user data.
 It defines various public fields to hold user information such as name, email, gender, phone number, age, image URL, 
 and the user's image as a Sprite object. A constructor is provided to initialize a UserData object with specified values.
*/