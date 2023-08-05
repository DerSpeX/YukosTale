using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStatHandler : MonoBehaviour //PlayerStatHandler ist eigentlich eine CharacterStatHandler und PLayerStatHandler ist eigentlich ein UserStatHandler
{
    #region Variables
    public ScriptReferences scriptReferences;
    [System.Serializable]
    public class ScriptReferences
    {
        public EquipmentHandler equipmentHandler;
        public InventoryHandler inventoryHandler;
        public HUDHandler hudHandler;
        public PlayFabManager playFabManager;
        //Space for more Handlers
    }
    
    public CharacterInformations characterInformations;
    [System.Serializable]
    public class CharacterInformations
    {
        //Character Information
        [Header("Names")]
        [Space(10)]
        public string foreName;
        public string lastName;
        [Space(25)]
        
        [Header("Age")]
        [Space(10)]
        public int currentAge;
        [Space(25)] 
        
        [Header("Class")] 
        [Space(10)]
        public Classes classes;
        public enum Classes
        {
            Archer, Guardian, Minstrel
        }
        [Space(25)]
        
        [Header("Race")] 
        [Space(10)]
        public Races races;
        public enum Races
        {
            Human, Elf, Dwarf
        }
    }
    
    public LocationInformations locationInformations;
    [System.Serializable]
    public class LocationInformations
    {
        //Location Information
        [Header("Location")]
        [Space(10)]
        public float currentPosX;
        public  float currentPosY;
        public float currentPosZ;
        [Space(25)] 
        
        [Header("Region")] 
        [Space(10)]
        public string currentRegion;
    }
    
    public CurrencyInformations currencyInformations;
    [System.Serializable]
    public class CurrencyInformations
    {
        //Currency Informations
        [Header("Currencies")]
        [Space(10)]
        public string currencyName1;
        [Space(10)] 
        public int currentCurrency1;
        [Space(25)]
        public string currencyName2;
        [Space(10)] 
        public int currentCurrency2;
        [Space(25)]
        public string currencyName3;
        [Space(10)] 
        public int currentCurrency3;
    }
    
    public LevelStatistics levelStatistics;
    [System.Serializable]
    public class LevelStatistics
    {
        //Level Informations Level_Experience
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
        [Space(10)]
        public float leftExperiencePoints;
        [Space(25)]

        //Level Informations Attribute_Points
        [Header("Attribute Points")]
        [Space(10)]
        public int minAttributePoints;
        public int maxAttributePoints;
        [Space(10)]
        public int currentAttributePoints;
        [Space(10)]
        public int spentAttributePoints;
        [Space(10)]
        public int attributeRefundPoints;
    }
    
    public CombatStatistics combatStatistics;
    [System.Serializable]
    public class CombatStatistics
    {
        //Combat Informations Health_Shield_Armour
        [Header("Defensives")]
        [Space(10)]
        public float minHealth;
        public float maxHealth;
        [Space(10)]
        public float currentHealth;
        [Space(25)]
        
        public float minShield;
        public float maxShield;
        [Space(10)]
        public float currentShield;
        [Space (25)]
        
        public float minArmour;
        public float maxArmour;
        [Space(10)]
        public float currentArmour;

        //Combat Informations Phys_Tact_Defense
        [Space(25)]
        public float minPhysicalDefense;
        public float maxPhysicalDefense;
        [Space(10)]
        public float currentPhysicalDefense;
        [Space(25)]
        public float minTacticalDefense;
        public float maxTacticalDefense;
        [Space(10)]
        public float currentTacticalDefense;
        [Space(25)]
        
        //Combat Informations Defensives_Regeneration
        [Header(" Defensives Regeneration")]
        [Space(10)]
        public float minHealthReg;
        public float maxHealthReg;
        [Space(10)]
        public float currentHealthReg;
        [Space(25)]
        
        public float minShieldReg;
        public float maxShieldReg;
        [Space(10)]
        public float currentShieldReg;
        [Space (25)]
        
        //Combat Informations Stam_Mana_Wrath
        [Header("Neutrals")]
        [Space(10)]
        public float minStamina;
        public float maxStamina;
        [Space(10)]
        public float currentStamina;
        [Space(25)]
        
        public float minMana;
        public float maxMana;
        [Space(10)]
        public float currentMana;
        [Space(25)]
        
        public float minWrath;
        public float maxWrath;
        [Space(10)]
        public float currentWrath;
        [Space(25)]

        //Combat Informations Neutrals_Regenerations
        [Header(" Neutrals Regenerations")]
        [Space(10)]
        public float minStaminaReg;
        public float maxStaminaReg;
        [Space(10)]
        public float currentStaminaReg;
        [Space(25)]
        
        public float minManaReg;
        public float maxManaReg;
        [Space(10)]
        public float currentManaReg;
        [Space (25)]
        
        public float minWrathReg;
        public float maxWrathReg;
        [Space(10)]
        public float currentWrathReg;
        [Space (25)]
        
        //Combat Informations Phys_Tact_Damage
        [Header("Offensives")]
        [Space(10)]
        public float minPhysicalDamage;
        public float maxPhysicalDamage;
        [Space(10)]
        public float currentPhysicalDamage;
        [Space(25)]
        public float minTacticalDamage;
        public float maxTacticalDamage;
        [Space(10)]
        public float currentTacticalDamage;
        [Space(25)]
        
        //Combat Informations Phys_Tact_Penetration
        public float minPhysicalPenetration;
        public float maxPhysicalPenetration;
        [Space(10)]
        public float currentPhysicalPenetration;
        [Space(25)]
        public float minTacticalPenetration;
        public float maxTacticalPenetration;
        [Space(10)]
        public float currentTacticalPenetration;
        [Space(25)]
        
        //Combat Informations Crit_Strike_D_C_M
        public float minCriticalStrikeDamage;
        public float maxCriticalStrikeDamage;
        [Space(10)]
        public float currentCriticalStrikeDamage;
        [Space(25)]
        public float minCriticalStrikeChance;
        public float maxCriticalStrikeChance;
        [Space(10)] 
        public float currentCriticalStrikeChance;

    }
    #endregion
    #region Unity Functions
    private void Awake()
    {
        combatStatistics.currentHealth = combatStatistics.maxHealth; //Later, combatStatistics.currentHealth = cloud.currentHealth... -> Sync Local with cloud!
        scriptReferences.playFabManager = PlayFabManager.PlayFabManagerInstance;
    }
    private void Start()
    {
        //Möglicherweise SPäter zu einem GameManager auslagern und dort erstmalige Updates etc. durchlaufen lassen
        UpdateHUDHealthStats();
        Debug.Log("Health Stats wurden geupdated!");
        UpdateHUDLevelAndExperienceStats();
        Debug.Log("Level Stats wurden geupdatedet!");
    }
    private void Update()
    {
        locationInformations.currentPosX = transform.position.x;
        locationInformations.currentPosY = transform.position.y;
        locationInformations.currentPosZ = transform.position.z;
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            scriptReferences.playFabManager.playFabHandler.GetAllCharacterData(characterInformations.foreName, characterInformations.lastName, characterInformations.currentAge, characterInformations.classes.ToString(), characterInformations.races.ToString(),locationInformations.currentPosX, locationInformations.currentPosY, locationInformations.currentPosZ,locationInformations.currentRegion ,currencyInformations.currencyName1, currencyInformations.currentCurrency1, currencyInformations.currencyName2, currencyInformations.currentCurrency2, currencyInformations.currencyName3, currencyInformations.currentCurrency3, levelStatistics.minLevel, levelStatistics.maxLevel, levelStatistics.currentLevel, levelStatistics.minExperiencePoints, levelStatistics.maxExperiencePoints, levelStatistics.currentExperiencePoints, levelStatistics.leftExperiencePoints , levelStatistics.minAttributePoints, levelStatistics.maxAttributePoints, levelStatistics.currentAttributePoints, levelStatistics.spentAttributePoints, levelStatistics.attributeRefundPoints, combatStatistics.minHealth, combatStatistics.maxHealth, combatStatistics.currentHealth, combatStatistics.minShield, combatStatistics.maxShield, combatStatistics.currentShield, combatStatistics.minArmour, combatStatistics.maxArmour, combatStatistics.currentArmour, combatStatistics.minStamina, combatStatistics.maxStamina, combatStatistics.currentStamina, combatStatistics.minMana, combatStatistics.maxMana, combatStatistics.currentMana, combatStatistics.minWrath, combatStatistics.maxWrath, combatStatistics.currentWrath, combatStatistics.minPhysicalDefense, combatStatistics.maxPhysicalDefense, combatStatistics.currentPhysicalDefense, combatStatistics.minTacticalDefense, combatStatistics.maxTacticalDefense, combatStatistics.currentTacticalDefense, combatStatistics.minPhysicalDamage, combatStatistics.maxPhysicalDamage, combatStatistics.currentPhysicalDamage, combatStatistics.minTacticalDamage, combatStatistics.maxTacticalDamage, combatStatistics.currentTacticalDamage, combatStatistics.minPhysicalPenetration, combatStatistics.maxPhysicalPenetration, combatStatistics.currentPhysicalPenetration, combatStatistics.minTacticalPenetration, combatStatistics.maxTacticalPenetration, combatStatistics.currentTacticalPenetration, combatStatistics.minCriticalStrikeDamage, combatStatistics.maxCriticalStrikeDamage, combatStatistics.currentCriticalStrikeDamage, combatStatistics.minCriticalStrikeChance, combatStatistics.maxCriticalStrikeChance, combatStatistics.currentCriticalStrikeChance, combatStatistics.minHealthReg, combatStatistics.maxHealthReg, combatStatistics.currentHealthReg, combatStatistics.minShieldReg, combatStatistics.maxShieldReg, combatStatistics.currentShieldReg, combatStatistics.minStaminaReg, combatStatistics.maxStaminaReg, combatStatistics.currentStaminaReg, combatStatistics.minManaReg, combatStatistics.maxManaReg, combatStatistics.currentManaReg, combatStatistics.minWrathReg, combatStatistics.maxWrathReg, combatStatistics.currentWrathReg);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Mid_Level");
        }
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
        // Berechnung der fehlenden Erfahrunspunkte zum näcshten level Up 
        levelStatistics.leftExperiencePoints = levelStatistics.maxExperiencePoints - levelStatistics.currentExperiencePoints;
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