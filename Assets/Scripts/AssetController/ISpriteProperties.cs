using System;
using UnityEngine;

public interface ISpriteProperties
{
    Sprite Sprite { get; set; }
    string Url { get; set; }
    Action<ISpriteProperties> Callback { get; set; }
}