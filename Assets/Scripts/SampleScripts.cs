using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SampleScripts : MonoBehaviour
{
   
   const string GET_USERS = "https://reqres.in/api/users/";
   const string PUT_USER_2 = "https://reqres.in/api/users/2";

   [SerializeField]private RawImage imagetoChange;
   private const string IMAGE_URL = "https://reqres.in/img/faces/3-image.jpg";
   
   public void Start()
   {
      StartCoroutine(UnityGetRequest(GET_USERS));
      StartCoroutine(DownloadImage(IMAGE_URL, (texture) =>
      {
         if (imagetoChange != null)
         {
            imagetoChange.texture = texture;
         }
      }));
   }

   private static IEnumerator UnityGetRequest(string url, Action<bool, string> callback = null)
   {
      var request = UnityWebRequest.Get(url);

      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
         callback?.Invoke(false, request.error);
      }
      else
      {
         Debug.Log(request.downloadHandler.text);
         callback?.Invoke(true, request.downloadHandler.text);

      }
   }


   private static IEnumerator UnityPostRequest(string url, string jsonData, Action<bool, string> callback = null)
   {
      jsonData = "{\"name\":\"John\", \"job\":\"Developer\"}";

      var request = UnityWebRequest.Post(url, jsonData, "application/json");
      /*
      var request = new UnityWebRequest(url, "POST");
      byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
      request.uploadHandler = new UploadHandlerRaw(bodyRaw);
      request.downloadHandler = new DownloadHandlerBuffer();
      request.SetRequestHeader("Content-Type", "application/json");
      */

      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
         callback?.Invoke(false, request.error);
      }
      else
      {
         Debug.Log(request.downloadHandler.text);
         callback?.Invoke(true, request.downloadHandler.text);
      }
   }


   private IEnumerator UnityPutRequest(string url, string jsonData, Action<bool, string> callback = null)
   {
      jsonData = "{\"name\":\"John\", \"job\":\"Senior Developer\"}";

      var request = UnityWebRequest.Put(url, jsonData);

      /*
       var request = new UnityWebRequest(url, "PUT");
      byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
      request.uploadHandler = new UploadHandlerRaw(bodyRaw);
      request.downloadHandler = new DownloadHandlerBuffer();
      request.SetRequestHeader("Content-Type", "application/json");
      */

      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
         callback?.Invoke(false, request.error);
      }
      else
      {
         Debug.Log(request.downloadHandler.text);
         callback?.Invoke(true, request.downloadHandler.text);
      }
   }

   private IEnumerator UnityPatchRequest(string url, string jsonData, Action<bool, string> callback = null)
   {
      jsonData = "{\"name\":\"James\"}";
      UnityWebRequest request = new UnityWebRequest(url, "PATCH");
      byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
      request.uploadHandler = new UploadHandlerRaw(bodyRaw);
      request.downloadHandler = new DownloadHandlerBuffer();
      request.SetRequestHeader("Content-Type", "application/json");
      
      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
         callback?.Invoke(false, request.error);
      }
      else
      {
         Debug.Log(request.downloadHandler.text);
         callback?.Invoke(true, request.downloadHandler.text);
      }
   }
   
   private IEnumerator UnityDeleteRequest(string url, Action<bool, string> callback = null)
   {
      var request = UnityWebRequest.Delete(url);

      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
         callback?.Invoke(false, request.error);
      }
      else
      {
         Debug.Log("User Deleted");
         callback?.Invoke(true, "");
      }
   }

   private IEnumerator DownloadImage(string imageUrl, Action<Texture2D> Callback)
   {
      var request = UnityWebRequestTexture.GetTexture(imageUrl);
      
      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
      }
      else
      {
         Debug.Log("Texture Found");
         var texture = DownloadHandlerTexture.GetContent(request);
         Callback?.Invoke(texture);
      }
   }

   private IEnumerator GetWithAuthRequest(string url, string authToken)
   {
      var request = UnityWebRequest.Get(url);
      request.SetRequestHeader("Authorization", "Bearer " + authToken);
      
      yield return request.SendWebRequest();

      if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
      {
         Debug.LogError(request.error);
      }
      else
      {
         Debug.Log(request.downloadHandler.text);
      }
   }
}
