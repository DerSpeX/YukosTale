using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    #region Variables
    //references
    public PlayFabHandler playFabHandler;
    
    #endregion
    #region Singleton
    private static PlayFabManager playFabManagerInstance;
    public static PlayFabManager PlayFabManagerInstance
    {
        get
        {
#if UNITY_EDITOR
            if (playFabManagerInstance == null)
            {
                Debug.LogWarning(
                    "[SingletonWarning]: No playFabManagerInstance of Session found. Make sure there is one in your scene");
            }
#endif
            return playFabManagerInstance;
        }
    }
    private void InitSingleton()
    {
        if (playFabManagerInstance != null && playFabManagerInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            playFabManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    #region Unity Functions
    private void Awake()
    {
        //Init the Singleton
        InitSingleton();
        //Cache references to all desired variables\\
        playFabHandler = FindObjectOfType<PlayFabHandler>();
    }
    #endregion
}
