using System;
using TMPro;
using UnityEngine;

public class Registration_Login_Menu_Handler : MonoBehaviour
{
    #region Variables
    //Register Panel UI References
    public TMP_InputField regEmailAddress;
    public TMP_InputField regDisplayName;
    public TMP_InputField regUsername;
    public TMP_InputField regPassword;

    //Login Panel UI References
    public TMP_InputField loginUsername;
    public TMP_InputField loginPassword;
    
    public GameObject registrationPanel;
    public GameObject loginPanel;
    public GameObject mainPanel;
    
    public PlayFabManager playFabManager;

    #endregion
    #region Unity Functions
    private void Awake()
    {
        playFabManager = FindObjectOfType<PlayFabManager>();
    }

    private void Start()
    {
        registrationPanel.SetActive(false);
        loginPanel.SetActive(false);
    }

    #endregion
    #region Custom Functions

    public void RegisterUser()
    {
        //Call the PlayFab Register User Function and Transmit the Parameters from the Inputfields in this Scene
        playFabManager.playFabHandler.RegisterUser(regEmailAddress.text, regDisplayName.text, regUsername.text, regPassword.text); 
    }

    public void LoginUser()
    {
        playFabManager.playFabHandler.LogIn(loginUsername.text, loginPassword.text);
    }
    #endregion
    
    //kann simplfiziert werden mit switch oä. Wenn eines dann das andere nicht usw. 
    public void ShowLoginPanel()
    {
        mainPanel.SetActive(false);
        registrationPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
    public void ShowRegisterPanel()
    {
        mainPanel.SetActive(false);
        loginPanel.SetActive(false);
        registrationPanel.SetActive(true);
    }
    public void ShowMainPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
