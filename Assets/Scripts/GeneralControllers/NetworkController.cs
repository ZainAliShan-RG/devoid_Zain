using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GeneralControllers
{
    public class NetworkController : MonoBehaviour, IRegisterableService
    {
        // Static HttpClient instance for making HTTP requests
        private static readonly HttpClient HttpClientInstance = new();

        // Coroutine method for making GET requests
        public IEnumerator GetCoroutine<TProps>(string url, Action<TProps> onSuccess, Action<string> onError) where TProps : class
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();  // Send the GET request

                if (webRequest.result == UnityWebRequest.Result.Success)  // Check if the request succeeded
                {
                    string responseContent = webRequest.downloadHandler.text;
                    TProps data = JsonUtility.FromJson<TProps>(responseContent);  // Parse the response content
                    onSuccess?.Invoke(data);  // Invoke the onSuccess callback if provided
                }
                else
                {
                    onError?.Invoke(webRequest.error);  // Invoke the onError callback if provided
                }
            }
        }

        // Method to make asynchronous GET requests
        public async Task<TProps> Get<TProps>(string url, CancellationToken cancellationToken = default) where TProps : class, new()
        {
            HttpResponseMessage response = await HttpClientInstance.GetAsync(url, cancellationToken);  // Send the GET request
            TProps result = await FromJson<TProps>(response);  // Parse the response content
            return result;
        }

        // Method to make asynchronous POST requests
        public async Task<TProps> Post<TProps, TDProps>(string url, TDProps data, CancellationToken cancellationToken = default) where TProps : class, new()
        {
            HttpContent content = ToJson(data);  // Convert data to JSON
            HttpResponseMessage response = await HttpClientInstance.PostAsync(url, content, cancellationToken);  // Send the POST request
            TProps result = await FromJson<TProps>(response);  // Parse the response content
            return result;
        }

        // Method to make asynchronous PUT requests
        public async Task<TProps> Put<TProps, TDProps>(string url, TDProps data, CancellationToken cancellationToken = default) where TProps : class, new()
        {
            HttpContent content = ToJson(data);  // Convert data to JSON
            HttpResponseMessage response = await HttpClientInstance.PutAsync(url, content, cancellationToken);  // Send the PUT request
            TProps result = await FromJson<TProps>(response);  // Parse the response content
            return result;
        }

        // Method to make asynchronous DELETE requests
        public async Task<TProps> Delete<TProps>(string url, CancellationToken cancellationToken = default) where TProps : class, new()
        {
            HttpResponseMessage response = await HttpClientInstance.DeleteAsync(url, cancellationToken);  // Send the DELETE request
            TProps result = await FromJson<TProps>(response);  // Parse the response content
            return result;
        }

        // Method to initiate a coroutine for fetching a sprite
        public void GetSprite(string url, ISpriteProperties spriteProperties, Action<ISpriteProperties> callback)
        {
            StartCoroutine(GetSpriteCoroutine(url, spriteProperties, callback));
        }

        // Coroutine method for fetching a sprite from a URL
        private IEnumerator GetSpriteCoroutine(string url, ISpriteProperties spriteProperties, Action<ISpriteProperties> callback)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();  // Send the request

                Sprite sprite = null;
                if (request.result == UnityWebRequest.Result.Success)  // Check if the request succeeded
                {
                    Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }

                spriteProperties.Sprite = sprite;  // Set the sprite in the properties object
                callback(spriteProperties);  // Invoke the callback with the sprite properties
            }
        }

        // Method to convert data to JSON content for HTTP requests
        private HttpContent ToJson<TData>(TData data)
        {
            string jsonContent = JsonUtility.ToJson(data);
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return content;
        }

        // Method to parse JSON content from HTTP responses
        private async Task<TProps> FromJson<TProps>(HttpResponseMessage response) where TProps : class, new()
        {
            if (response.IsSuccessStatusCode)  // Check if the response indicates success
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                TProps data = JsonUtility.FromJson<TProps>(responseContent);
                return data;
            }
            else
            {
                Debug.LogError($"Failed to fetch data: {response.ReasonPhrase}");  // Log error on failure
                return default(TProps);
            }
        }
    }
}

/*
 This script defines a NetworkController class that provides methods for making HTTP requests 
 and fetching a sprite from a URL using Unity's UnityWebRequest and HttpClient classes.

 It exposes methods for sending GET, POST, PUT, and DELETE requests, both synchronously and asynchronously.
 Additionally, it provides a method to initiate a coroutine for fetching a sprite from a URL,
 which is useful for loading image assets at runtime.

 The script uses JsonUtility for serializing data to JSON for requests and deserializing data from JSON for responses.
 It also uses a static HttpClient instance for making HTTP requests,
 and Unity's Debug class to log an error message if a request fails.
*/
