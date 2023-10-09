using System;
using System.Collections.Generic;
using GeneralControllers;
using UnityEngine;

namespace AssetController
{
    public class AssetController : MonoBehaviour, IRegisterableService
    {
        private Dictionary<string, Sprite> _spriteCache = new Dictionary<string, Sprite>();  // Cache to store loaded sprites by URL
        private Dictionary<string, bool> _loadingSprites = new Dictionary<string, bool>();  // Tracks which sprites are currently being loaded
        private Dictionary<string, List<ISpriteProperties>> _pendingRequests = new Dictionary<string, List<ISpriteProperties>>();  // Holds pending requests for sprites that are being loaded

        // Method to fetch a sprite from a URL or cache, and provide it to a callback
        // ReSharper disable Unity.PerformanceAnalysis
        public void FetchSprite(string url, ISpriteProperties data, Action<ISpriteProperties> callback)
        {
            data.Url = url;
            data.Callback = callback;

            // If sprite is in cache, use it
            if (_spriteCache.TryGetValue(url, out var value))
            {
                data.Sprite = value;
                callback(data);
            }
            // If sprite is currently being loaded, add request to pendingRequests
            else if (_loadingSprites.ContainsKey(url))
            {
                if (!_pendingRequests.ContainsKey(url))
                {
                    _pendingRequests[url] = new List<ISpriteProperties>();
                }
                _pendingRequests[url].Add(data);
            }
            // Otherwise, start loading the sprite
            else
            {
                var networkManager = ServiceLocator.Instance.Get<NetworkController>();
                _loadingSprites.Add(url, true);
                networkManager.GetSprite(url, data, SpriteFromNetwork);
            }
        }

        // Callback method to handle sprite data from network
        private void SpriteFromNetwork(ISpriteProperties data)
        {
            _spriteCache.Add(data.Url, data.Sprite);  // Cache the loaded sprite
            data.Callback(data);  // Call the original callback
            _loadingSprites.Remove(data.Url);  // Remove sprite from loadingSprites

            // Handle any pending requests for this sprite
            if (_pendingRequests.ContainsKey(data.Url))
            {
                foreach (var request in _pendingRequests[data.Url])
                {
                    request.Sprite = data.Sprite;
                    request.Callback(request);
                }

                _pendingRequests.Remove(data.Url);  // Remove handled requests from pendingRequests
            }
        }
    }
}

/*
This script, AssetController, is designed to manage the fetching and caching of sprite assets in a Unity project.
It implements the IRegisterableService interface, allowing it to be registered and accessed through a service locator pattern.

The FetchSprite method is the primary public interface for requesting a sprite from a given URL. 
This method checks if the sprite is already cached, and if so, immediately returns it via the provided callback.
If the sprite is currently being loaded, the request is added to a list of pending requests for that sprite.
If the sprite is neither cached nor being loaded, a new network request is initiated to load the sprite.

The SpriteFromNetwork method is the callback for handling the loaded sprite data from the network.
It adds the loaded sprite to the cache, calls the original callback with the loaded sprite, 
handles any pending requests for that sprite, and cleans up the tracking dictionaries.

This setup ensures that each sprite is only loaded once from the network, even if multiple requests for the same sprite are made in quick succession.
*/
