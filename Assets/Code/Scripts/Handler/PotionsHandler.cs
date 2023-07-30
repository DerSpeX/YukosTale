using UnityEngine;

public class PotionsHandler : MonoBehaviour
{
    #region Variables
    //||ENUMS||\\
    //PotionTypes
    [SerializeField] private PotionType potionType;
    [SerializeField] private enum PotionType
    {
        HealthPotion,
        StaminaPotion,
        DamagePotion
    }

    [SerializeField] private Operation operation;

    [SerializeField]
    private enum Operation
    {
        IncreaseInstant,
        IncreaseOverTime,
        DecreaseInstant,
        DecreaseOverTime
    }
    [SerializeField] private float amount;
    [SerializeField] private float duration;
    [SerializeField] private float penetration;
    
    //references
    private SceneManager _sceneManager;
    private PlayerStatHandler _playerStatHandler;
    
    #endregion
    #region Unity Functions
    private void Start()
    {
        _playerStatHandler = FindObjectOfType<PlayerStatHandler>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Consume();
        }
    }
    #endregion
    #region Custom Functions
    
    private void Consume()
    {
        if (potionType == PotionType.HealthPotion)
        {
            switch (operation)
            {
                case Operation.IncreaseInstant:
                {
                    _playerStatHandler.TakeInstantHealth(amount);
                    break;
                }
                case Operation.IncreaseOverTime:
                {
                    _playerStatHandler.TakeHealOverTime(amount, duration);
                    break;
                }
            }
        }
        else if (potionType == PotionType.DamagePotion)
        {
            switch (operation)
            {
                case Operation.DecreaseInstant:
                {
                    _playerStatHandler.TakeInstantDamage(amount, penetration);
                    break;
                }
                case Operation.DecreaseOverTime:
                {
                    _playerStatHandler.TakeDamageOverTime(amount, penetration, duration);
                    break;
                }
            }
        }
    }

    #endregion
}
