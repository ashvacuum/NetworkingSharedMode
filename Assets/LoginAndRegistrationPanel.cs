using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoginAndRegistrationPanel : MonoBehaviour
{
    [FormerlySerializedAs("RegPass")] [SerializeField]private TMP_InputField regPass;
    [FormerlySerializedAs("RegPassRepeat")] [SerializeField]private TMP_InputField regPassRepeat;
    [FormerlySerializedAs("RegUser")] [SerializeField]private TMP_InputField regUser;
    [SerializeField] private Button regBtn;
    
    [FormerlySerializedAs("LogUser")] [SerializeField]private TMP_InputField logUser;
    [FormerlySerializedAs("LogPass")] [SerializeField]private TMP_InputField logPass;
    [SerializeField] private Button logBtn;

    private Dictionary<string, string> _userContent = new Dictionary<string, string>();s
    private string UserKey = "USERS";

    private void Awake()
    {
        regBtn.transform.parent.gameObject.SetActive(true);
        logBtn.transform.parent.gameObject.SetActive(false);
        logBtn.onClick.AddListener(OnLoginPressed);
        regBtn.onClick.AddListener(OnRegisterPressed);
        if (PlayerPrefs.HasKey(UserKey))
        {
            _userContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(PlayerPrefs.GetString(UserKey));
        }
        else
        {
            _userContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(PlayerPrefs.GetString(UserKey));
        }
        
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString(UserKey, JsonConvert.SerializeObject(_userContent));
    }

    private void OnLoginPressed()
    {
        if (_userContent.ContainsKey(logUser.text))
        {
            if (_userContent[logUser.text] == logPass.text)
            {
                Debug.Log("Successfully Logged in");
                logUser.text = "";
                logPass.text = "";
                return;
            }
        }
        Debug.Log("Incorrect User or Password");
    }

    private void OnRegisterPressed()
    {
        regUser.text = "";
        regPassRepeat.text = "";
        regPass.text = "";
        if (_userContent.ContainsKey(regUser.text))
        {
            Debug.Log("Username is taken");
            return;
            
        }
        
        if (regPass.text == regPassRepeat.text)
        {
            _userContent.Add(regUser.text, regPass.text);
            Debug.Log("Successfully Registered");
            regUser.text = "";
            regPassRepeat.text = "";
            regPass.text = "";
        }
        else
        {
            Debug.Log("Passwords do not match");
        }
    }

}
