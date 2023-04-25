using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStatHandler : MonoBehaviour
{
    #region Variables
    public PlayerInformations playerInformations;
    [System.Serializable]
    public class PlayerInformations
    {
        [Header("Names")]
        [Space(10)]
        public string foreName;
        public string lastName;
        [Space(25)]
        
        [Header("Age")]
        [Space(10)]
        public int minAge;
        public int maxAge;
        [Space(10)]
        public int currentAge;
    }
    
    public CurrencyInformations currencyInformations;
    [System.Serializable]
    public class CurrencyInformations
    {
        [Header("Currencies")]
        [Space(10)]
        public string currencyName1;
        public int minCurrency1;
        public int maxCurrency1;
        [Space(10)] 
        public int currentCurrency1;
        [Space(25)]
        public string currencyName2;
        public int minCurrency2;
        public int maxCurrency2;
        [Space(10)] 
        public int currentCurrency2;
        [Space(25)]
        public string currencyName3;
        public int minCurrency3;
        public int maxCurrency3;
        [Space(10)] 
        public int currentCurrency_003;

    }
    
    public LevelStatistics levelStatistics;
    [System.Serializable]
    public class LevelStatistics
    {
        [Header("Level")]
        [Space(10)]
        public int minLevel;
        public int maxLevel;
        [Space(10)]
        public int currentLevel;
        [Space(25)]
        
        [Header("Experience")]
        [Space(10)]
        public int minExperiencePoints;
        public int maxExperiencePoints;
        [Space(10)]
        public int currentExperiencePoints;
        
    }
    
    public CombatStatistics combatStatistics;
    [System.Serializable]
    public class CombatStatistics
    {
        [Header("Defensives")]
        [Space(10)]
        public float minHealth;
        public float maxHealth;
        [Space(10)]
        public float currentHealth;
        [Space(25)]
        public float minArmour;
        public float maxArmour;
        [Space(10)]
        public float currentArmour;
        [Space(25)]
        public float minPhysicalDefense;
        public float maxPhysicalDefense;
        [Space(10)]
        public float currentPhysicalDefense;
        [Space(25)]
        
        [Header("Neutrals")]
        [Space(10)]
        public float minStamina;
        public float maxStamina;
        [Space(10)]
        public float currentStamina;
        [Space(25)]
        
        [Header("Offensives")]
        [Space(10)]
        public float minPhysicalDamage;
        public float maxPhysicalDamage;
        [Space(10)]
        public float currentPhysicalDamage;
        [Space(25)]
        public float minPhysicalPenetration;
        public float maxPhysicalPenetration;
        [Space(10)]
        public float currentPhysicalPenetration;
        [Space(25)]
        public float minCriticalStrikeDamage;
        public float maxCriticalStrikeDamage;
        [Space(10)]
        public float currentCriticalStrikeDamage;
        [Space(25)]
        public float minCriticalStrikeChance;
        public float maxCriticalStrikeChance;
        [Space(10)]
        public float currentCriticalStrikeChance;
        [Space(25)]
        public float minCriticalStrikeChanceMultiplier;
        public float maxCriticalStrikeChanceMultiplier;
        [Space(10)]
        public float currentCriticalStrikeChanceMultiplier;
    }
    #endregion
    #region Unity Functions
    private void Update()
    {
        // Maybe currentHealth <= minHealth = Die();... but, it also happens in TakeDamage Functions
        // if ESCAPE Pressed -> open Ingame Menu
    }
    #endregion
    #region Custom Functions
    //Take Damage - Werteberechnung erfolgt aus dem min und max
    public void TakeInstantDamage(float physicalDamage, float physicalPenetration)
    {
        float physicalDefense =  GetPhysicalDefense();
        
         if (physicalPenetration >= physicalDefense)
        {
            if (combatStatistics.currentHealth - physicalDamage <= combatStatistics.minHealth)
            {
                combatStatistics.currentHealth = combatStatistics.minHealth;
                Die();
            }
            combatStatistics.currentHealth -= physicalDamage;
        }
        else if (physicalPenetration < physicalDefense)
        {
            // No Penetration = No Damage... Bruh
        }
    }

    public void TakeDamageOverTime(float physicalDamage, float physicalPenetration, float duration)
    {
        float physicalDefense =  GetPhysicalDefense();

        if (physicalPenetration >= physicalDefense)
        {
            StartCoroutine(DamageOverTime(physicalDamage, duration));
        }
        else if (physicalPenetration < physicalDefense)
        {
            // No Penetration = No Damage... Bruh
        }
    }
    public void TakeInstantHealth(float healthAmount)
    {
        if (combatStatistics.currentHealth < combatStatistics.maxHealth &&
            combatStatistics.currentHealth > combatStatistics.minHealth)
        {
            if (combatStatistics.currentHealth + healthAmount >= combatStatistics.maxHealth)
            {
                combatStatistics.currentHealth = combatStatistics.maxHealth;
            }
            else
            {
                combatStatistics.currentHealth += healthAmount;
            }
        }
    }
    public void TakeHealOverTime(float healthAmount, float duration)
    {
        if (combatStatistics.currentHealth < combatStatistics.maxHealth &&
            combatStatistics.currentHealth > combatStatistics.minHealth)
        {
            StartCoroutine(HealthOverTime( healthAmount, duration));
        }
    }
    public IEnumerator HealthOverTime(float healthAmount, float duration)
    {
        float timer = 0f;
        float healthAmountPerSecond = healthAmount / duration;
        while (timer < duration)
        {
            if (combatStatistics.currentHealth + healthAmountPerSecond < combatStatistics.maxHealth)
            {
                // Leben schrittweise erhöhen
                float amountToAdd = healthAmountPerSecond * Time.deltaTime;
                combatStatistics.currentHealth += amountToAdd;

                timer += Time.deltaTime;
            }
            else if (combatStatistics.currentHealth + healthAmountPerSecond >= combatStatistics.maxHealth)
            {
                combatStatistics.currentHealth = combatStatistics.maxHealth;
                duration = 0;
            }
            yield return null;
        }
    }
    public IEnumerator DamageOverTime(float physicalDamage, float duration)
    {
        float timer = 0f;
        float damageAmountPerSecond = physicalDamage / duration;
        while (timer < duration)
        {
            if (combatStatistics.currentHealth - damageAmountPerSecond > combatStatistics.minHealth)
            {
                float amountToReduce = damageAmountPerSecond * Time.deltaTime;
                combatStatistics.currentHealth -= amountToReduce;

                timer += Time.deltaTime;
            }
            else if (combatStatistics.currentHealth - damageAmountPerSecond <= combatStatistics.minHealth)
            {
                combatStatistics.currentHealth = combatStatistics.minHealth;
                Die();
            }
            yield return null;
        }
    }
    //WerteBerechnung
    private float GetPhysicalDefense()
    {
        combatStatistics.currentPhysicalDefense = Random.Range(combatStatistics.minPhysicalDefense, combatStatistics.maxPhysicalDefense);
        return combatStatistics.currentPhysicalDefense;
    } //Maybe call it CalculatePhysicalDefense

    public void SaveStats()
    {
        //Save Stats... Call PlayFab Stat Save Script
    }
    public void Die()
    {
        //Player Died. 
        //Destroy(gameObject);
        //Load Death Screen
        //Maybe SaveStats(); here
    }
    #endregion
}
