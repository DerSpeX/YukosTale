using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Variables
    //references
    [Header("Player References")]
    public GameObject player;
    public CharacterStatHandler _characterStatHandler;
    public PlayerController _playerController;
    #endregion
    #region Singleton
    private static SceneManager sceneManagerInstance;
    public static SceneManager SceneManagerInstance
    {
        get
        {
#if UNITY_EDITOR
            if (sceneManagerInstance == null)
            {
                Debug.LogWarning(
                    "[SingletonWarning]: No sceneManangerInstance of Session found. Make sure there is one in your scene");
            }
#endif
            return sceneManagerInstance;
        }
    }
    private void InitSingleton()
    {
        if (sceneManagerInstance != null && sceneManagerInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            sceneManagerInstance = this;
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
        //Get CharacterStatHandler by Player
        _characterStatHandler = player.GetComponent<CharacterStatHandler>();
        //Get PlayerController by Player
        _playerController = player.GetComponent<PlayerController>();
    }
    #endregion
}

