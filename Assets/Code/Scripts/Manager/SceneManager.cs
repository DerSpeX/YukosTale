using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Variables
    //references
    [Header("Player References")]
    public GameObject player;
    private CharacterStatHandler _characterStatHandler;
    private PlayerController _playerController;
    
    [Header("Other")]
    #endregion
    #region Singleton
    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
#if UNITY_EDITOR
            if (instance == null)
            {
                Debug.LogWarning(
                    "[SingletonWarning]: No instance of Session found. Make sure there is one in your scene");
            }
#endif
            return instance;
        }
    }
    private void InitSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
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
        
        //||PLAYER||\\
        //Get Player by Tag Player
        player = GameObject.FindGameObjectWithTag("Player");
        //Get PlayerStatHandler
        _characterStatHandler = player.GetComponent<CharacterStatHandler>();
        //GetPlayerController
        _playerController = player.GetComponent<PlayerController>();
    }
    #endregion
}

