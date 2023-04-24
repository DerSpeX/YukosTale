using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerStatHandler_Archer : MonoBehaviour
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

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TakeInstantDamage(14f, 213);
        }
    }

    #endregion

    #region Custom Functions
    //Take Damage - Werteberechnung erfolgt aus dem min und max
    public void TakeInstantDamage(float physicalDamage, float physicalPenetration)
    {
        //First Generate the Random Value for Stat via Call the Method to Generate and get the Result
        float physicalDefense =  GetPhysicalDefense();
        
        if (physicalPenetration < physicalDefense)
        {
            combatStatistics.currentHealth -= 0f;
        }
        else if (physicalPenetration >= physicalDefense)
        {
            combatStatistics.currentHealth -= physicalDamage;
        }
    }
    //WerteBerechnung
    private float GetPhysicalDefense()
    {
        combatStatistics.currentPhysicalDefense = Random.Range(combatStatistics.minPhysicalDefense, combatStatistics.maxPhysicalDefense);
        return combatStatistics.currentPhysicalDefense;
    }
    #endregion
}
