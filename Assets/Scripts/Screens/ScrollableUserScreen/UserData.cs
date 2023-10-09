using System;
using deVoid.UIFramework;
using UnityEngine;

namespace Screens.ScrollableUserScreen
{
    [Serializable]
    public class UserData : WindowProperties
    {
        public string first;
        public string last;
        public string email;
        public string gender;
        public string phone;
        public int age;
        public string imageUrl;
        public Sprite image = null;
        public string thumbnailUrl = null;

        public UserData(string fname, string lname, string email, string gender, string phone, int age, string imageUrl, string thumbnailUrl)
        {
            first = fname;
            last = lname;
            this.email = email;
            this.gender = gender;
            this.phone = phone;
            this.age = age;
            this.imageUrl = imageUrl;
            this.thumbnailUrl = thumbnailUrl;
        }
    }
}