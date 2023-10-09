using System;
using UnityEngine;

namespace Screens.ScrollableUserScreen
{
    public class SpriteSendProperties : ISpriteProperties
    {
        public Sprite Sprite { get; set; }
        public string Url { get; set; }
        public Action<ISpriteProperties> Callback { get; set; }
        public string Email;
    }
}