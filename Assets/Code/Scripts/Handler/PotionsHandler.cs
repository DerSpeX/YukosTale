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
    private CharacterStatHandler _characterCharacterStatHandler;
    
    #endregion
    #region Unity Functions
    private void Start()
    {
        _characterCharacterStatHandler = FindObjectOfType<CharacterStatHandler>();
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
                    _characterCharacterStatHandler.TakeInstantHealth(amount);
                    break;
                }
                case Operation.IncreaseOverTime:
                {
                    _characterCharacterStatHandler.TakeHealOverTime(amount, duration);
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
                    _characterCharacterStatHandler.TakeInstantDamage(amount, penetration);
                    break;
                }
                case Operation.DecreaseOverTime:
                {
                    _characterCharacterStatHandler.TakeDamageOverTime(amount, penetration, duration);
                    break;
                }
            }
        }
    }

    #endregion
}
