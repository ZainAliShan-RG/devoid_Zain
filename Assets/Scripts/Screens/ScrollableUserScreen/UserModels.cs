using System;
using System.Collections.Generic;

namespace Screens.ScrollableUserScreen
{
    [Serializable]
    public class UserName
    {
        public string title = "";  // Title of the user (e.g., Mr., Mrs., Dr., etc.)
        public string first = "";  // First name of the user
        public string last = "";   // Last name of the user
    }

    [Serializable]
    public class BirthDate
    {
        public string date;  // Date of birth in string format
        public int age;      // Age of the user
    }

    [Serializable]
    public class UserPicture
    {
        public string large;      // URL of the large version of the user's picture
        public string medium;     // URL of the medium version of the user's picture
        public string thumbnail;  // URL of the thumbnail version of the user's picture
    }

    [Serializable]
    public class FinalUser
    {
        public UserName name;        // Object containing the user's name information
        public BirthDate dob;        // Object containing the user's date of birth information
        public UserPicture picture;  // Object containing the URLs of the user's pictures
        public string email = "";    // Email address of the user
        public string gender = "";   // Gender of the user
        public string phone;         // Phone number of the user
    }

    [Serializable]
    public class Info
    {
        public string seed = "";     // Seed used to generate the user data
        public int results;          // Number of results returned
        public int page;             // Page number of the results
        public string version = "";  // Version of the API
    }

    [Serializable]
    public class UsersData
    {
        public List<FinalUser> results;  // List of user data results
        public Info info;             // Information about the results
    }
}

/*
 This script defines several classes that map to the structure of the user data returned by an API.
 The classes are marked as [Serializable] so they can be used with Unity's JSON serialization methods.

 - UserName: Represents the name of a user, with fields for title, first name, and last name.
 - BirthDate: Represents the date of birth of a user, with fields for the date and age.
 - UserPicture: Contains URLs for different sizes of the user's picture.
 - Result: Represents a single user's data, with fields for name, date of birth, picture, email, gender, and phone.
 - Info: Contains metadata about the API response, like the seed used to generate the data, the number of results, the page number, and the version of the API.
 - UsersData: Represents the full set of user data returned by the API, with fields for a list of Result objects and an Info object.
*/
