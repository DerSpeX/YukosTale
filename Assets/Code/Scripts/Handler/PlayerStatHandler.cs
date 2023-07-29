
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class PlayerStatHandler : MonoBehaviour
{
    #region Variables
    public ScriptReferences scriptReferences;
    [System.Serializable]
    public class ScriptReferences
    {
        public EquipmentHandler equipmentHandler;
        public InventoryHandler inventoryHandler;
        public HUDHandler hudHandler;
        //Space for more Handlers
    }
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
        public int currentCurrency3;

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
        public float minExperiencePoints;
        public float maxExperiencePoints;
        [Space(10)]
        public float currentExperiencePoints;
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

    private void Awake()
    {
        combatStatistics.currentHealth = combatStatistics.maxHealth; //Later, combatStatistics.currentHealth = cloud.currentHealth... -> Sync Local with cloud!
    }

    private void Start()
    {
        UpdateHUDHealthStats();
        UpdateHUDLevelAndExperienceStats();
    }

    #endregion
    #region Custom Functions
    /// <summary>
    /// Combat Area
    /// </summary>
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
            UpdateHUDHealthStats();
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
                UpdateHUDHealthStats();
            }
            else
            {
                combatStatistics.currentHealth += healthAmount;
                UpdateHUDHealthStats();
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
                UpdateHUDHealthStats();
            }
            else if (combatStatistics.currentHealth + healthAmountPerSecond >= combatStatistics.maxHealth)
            {
                combatStatistics.currentHealth = combatStatistics.maxHealth;
                duration = 0;
                UpdateHUDHealthStats();
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
                UpdateHUDHealthStats();
            }
            else if (combatStatistics.currentHealth - damageAmountPerSecond <= combatStatistics.minHealth)
            {
                combatStatistics.currentHealth = combatStatistics.minHealth;
                UpdateHUDHealthStats();
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
    
    /// <summary>
    /// Leveling Area
    /// </summary>
    // Methode, um Erfahrungspunkte hinzuzufügen und LevelUp zu überprüfen
    public void AddExperience(float experiencePoints)
    {
        // Hinzufügen der neuen Erfahrungspunkte
        levelStatistics.currentExperiencePoints += experiencePoints;
        UpdateHUDLevelAndExperienceStats();

        if (levelStatistics.currentLevel < levelStatistics.maxLevel)
        {
            // Schleife zum Überprüfen, ob mehrere LevelUps auf einmal stattfinden
            while (levelStatistics.currentExperiencePoints >= levelStatistics.maxExperiencePoints)
            {
                // Verbleibende Erfahrungspunkte für das nächste Level merken
                float remainingExperience = levelStatistics.currentExperiencePoints - levelStatistics.maxExperiencePoints;

                // Aufstieg auf das nächste Level
                LevelUp();

                // Aktualisieren der MaxExperiencePoints für das nächste Level
                levelStatistics.maxExperiencePoints *= 1.2f; //Maybe Random betwen factor x and X

                // Verbleibende Erfahrungspunkte setzen, um den Fortschritt zum nächsten Level zu berücksichtigen
                levelStatistics.currentExperiencePoints = remainingExperience;
                UpdateHUDLevelAndExperienceStats();
            }
        }
        else if (levelStatistics.currentLevel >= levelStatistics.maxLevel)
        {
            if (levelStatistics.currentExperiencePoints + experiencePoints >= levelStatistics.maxExperiencePoints)
            {
                levelStatistics.currentExperiencePoints = levelStatistics.maxExperiencePoints;
                UpdateHUDLevelAndExperienceStats();
            }
            else
            {
                levelStatistics.currentExperiencePoints += experiencePoints;
                UpdateHUDLevelAndExperienceStats();
            }
            UpdateHUDLevelAndExperienceStats();
        }
    }

    // Methode zum Aufsteigen auf das nächste Level
    private void LevelUp()
    {
        if (levelStatistics.currentLevel < levelStatistics.maxLevel)
        {
            levelStatistics.currentLevel++;
            //Stuff what happens beim Level Up
            UpdateHUDLevelAndExperienceStats();
        }
        else if (levelStatistics.currentLevel == levelStatistics.maxLevel && levelStatistics.currentExperiencePoints >= levelStatistics.maxExperiencePoints)
        {
            levelStatistics.currentExperiencePoints = levelStatistics.maxExperiencePoints;
            UpdateHUDLevelAndExperienceStats();
        }
    }

    // Methode, um den Fortschritt in Prozent zwischen minLevel und maxLevel zu erhalten
    public float GetLevelProgress()
    {
        if (levelStatistics.currentLevel == levelStatistics.maxLevel)
        {
            return 1f;
        }
        else
        {
            float currentLevelPoints = levelStatistics.currentExperiencePoints - levelStatistics.minExperiencePoints;
            float nextLevelPoints = levelStatistics.maxExperiencePoints - levelStatistics.minExperiencePoints;
            return currentLevelPoints / nextLevelPoints;
        }
    } //ATM Unnecessary

    /// <summary>
    /// Noch nicht zugeordnete Funktionen
    /// </summary>
    //Initiate the Stat Saves in the Cloud from PlayFab
    public void SaveStats()
    {
        //Save Stats... Call PlayFab Stat Save Script
    }
    public void Die()
    {
        Debug.Log("You died!");
        //Player Died. 
        //Destroy(gameObject);
        //Load Death Screen
        //Maybe SaveStats(); here
    }
    
    //Initiate the Visual Update for Health Stats in the UI
    private void UpdateHUDHealthStats()
    {
        scriptReferences.hudHandler.UpdatePlayersHealthStats(combatStatistics.minHealth, combatStatistics.maxHealth, combatStatistics.currentHealth);
    }
    private void UpdateHUDLevelAndExperienceStats()
    {
        scriptReferences.hudHandler.UpdatePlayerLevelAndExperienceStats(levelStatistics.currentLevel, levelStatistics.minExperiencePoints, levelStatistics.maxExperiencePoints, levelStatistics.currentExperiencePoints);
    }
    

    #endregion
}
