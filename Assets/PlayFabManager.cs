using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;


public class PlayFabManager : MonoBehaviour
{
    public string customID;
    public string characterName;
    public string characterID;
    public string playFabID;

    #region Variables

    public CharacterData characterData;
    [System.Serializable]
    public class CharacterData
    {
        //Player Informations
        [Header("Names")]
        [Space(10)]
        public string foreName;
        public string lastName;
        [Space(25)]
        
        [Header("Age")]
        [Space(10)]
        public int currentAge;
        [Space(25)]
        
        [Header("Location")]
        [Space(10)]
        public float currentPosX;
        public  float currentPosY;
        public float currentPosZ;
        
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
        
        //Level Informations
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
        [Space(25)]
        
        //Combat Informations
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
        public float minTacticalDefense;
        public float maxTacticalDefense;
        [Space(10)]
        public float currentTacticalDefense;
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
        public float minTacticalDamage;
        public float maxTacticalDamage;
        [Space(10)]
        public float currentTacticalDamage;
        [Space(25)]
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCharacter();
        }
    }

    //Login
    public void LogIn()
    {
        var request = new LoginWithCustomIDRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.CustomId = customID;

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucceed, OnLoginFailed);
    }
    //RESULT CALLBACKS FOR LOGIN
    private void OnLoginSucceed(LoginResult result)
    {
        Debug.Log("You're logged in NOW!");
        playFabID = result.PlayFabId;
    }

    private void OnLoginFailed(PlayFabError result)
    {
        Debug.Log("Login Failed!");
        Debug.Log(result.Error.ToString());
    }
    
    //Create Character
    public void CreateCharacter()
    {
        var request = new GrantCharacterToUserRequest();
        request.CharacterName = characterName;
        request.ItemId = "CharacterID";
        request.CatalogVersion = "Characters";

        PlayFabClientAPI.GrantCharacterToUser(request, OnGrantCharacterToUserSucceed, OnGrantCharacterToUserFailed);
    }
    //RESULT CALLBACKS FOR CHARACTER CREATION
    private void OnGrantCharacterToUserSucceed(GrantCharacterToUserResult result)
    {
        Debug.Log("Grant Character to User Succeed!");
        characterID = result.CharacterId;
        if (characterData.foreName != null)
        {
            CreateDataStructureForPlayFabCharacterBlock1();
            Invoke("CreateDataStructureForPlayFabCharacterBlock2",2);
            Invoke("CreateDataStructureForPlayFabCharacterBlock3",3);
            Invoke("CreateDataStructureForPlayFabCharacterBlock4",4);
            Invoke("CreateDataStructureForPlayFabCharacterBlock5",5);
            Invoke("CreateDataStructureForPlayFabCharacterBlock6",6);
        }
        else
        {
            Debug.Log("Es kamen keine Daten im PlayFab Manager an, Forename == null!");
        }
    }
    private void OnGrantCharacterToUserFailed(PlayFabError result)
    {
        Debug.Log("Grant Character to User Failed!");
        Debug.Log(result.Error.ToString());
    }
    
    //Get all the Character Data from PlayerStatHandler
    public void GetAllCharacterData(string foreName, string lastName, int currentAge,float currentPosX, float currentPosY, float currentPosZ,string currencyName1, int currentCurrency1, 
            string currencyName2, int currentCurrency2, string currencyName3, int currentCurrency3, int minLevel, int maxLevel, int currentLevel, float minExperiencePoints, 
            float maxExperiencePoints, float currentExperiencePoints, float leftExperiencePoints, int minAttributePoints, int maxAttributePoints, int currentAttributePoints, int spentAttributePoints,
            int attributeRefundPoints, float minHealth, float maxHealth, float currentHealth, float minStamina, float maxStamina, float currentStamina, 
            float minArmour, float maxArmour, float currentArmour, float minPhysicalDefense, float maxPhysicalDefense, float currentPhysicalDefense, float minTacticalDefense,
            float maxTacticalDefense, float currentTacticalDefense, float minPhysicalDamage, float maxPhysicalDamage,float currentPhysicalDamage, float minTacticalDamage, float maxTacticalDamage, float currentTacticalDamage, 
            float minPhysicalPenetration, float maxPhysicalPenetration, float currentPhysicalPenetration, float minTacticalPenetration,
            float maxTacticalPenetration, float currentTacticalPenetration, float minCriticalStrikeDamage, float maxCriticalStrikeDamage,
            float currentCriticalStrikeDamage, float minCriticalStrikeChance, float maxCriticalStrikeChance, float currentCriticalStrikeChance,
            float minCriticalStrikeChanceMultiplier, float maxCriticalStrikeChanceMultiplier, float currentCriticalStrikeChanceMultiplier
            )

    {

        //Player Informations
        characterData.foreName = foreName;
        characterData.lastName = lastName;
        characterData.currentAge = currentAge;
        characterData.currentPosX = currentPosX;
        characterData.currentPosY = currentPosY;
        characterData.currentPosZ = currentPosZ;
        
        //Currency Informations
        characterData.currencyName1 = currencyName1;
        characterData.currentCurrency1 = currentCurrency1;
        characterData.currencyName2 = currencyName2;
        characterData.currentCurrency2 = currentCurrency2;
        characterData.currencyName3 = currencyName3;
        characterData.currentCurrency3 = currentCurrency3;
        
        //Level Informations
        characterData.minLevel = minLevel;
        characterData.maxLevel = maxLevel;
        characterData.currentLevel = currentLevel;
        characterData.minExperiencePoints = minExperiencePoints;
        characterData.maxExperiencePoints = maxExperiencePoints;
        characterData.currentExperiencePoints = currentExperiencePoints;
        characterData.leftExperiencePoints = leftExperiencePoints;
        characterData.minAttributePoints = minAttributePoints;
        characterData.maxAttributePoints = maxAttributePoints;
        characterData.currentAttributePoints = currentAttributePoints;
        characterData.spentAttributePoints = spentAttributePoints;
        characterData.attributeRefundPoints = attributeRefundPoints;

        //Combat Informations
        characterData.minStamina = minStamina;
        characterData.maxStamina = maxStamina;
        characterData.currentStamina = currentStamina;
        characterData.minHealth = minHealth;
        characterData.maxHealth = maxHealth;
        characterData.currentHealth = currentHealth;
        characterData.minArmour = minArmour;
        characterData.maxArmour = maxArmour;
        characterData.currentArmour = currentArmour;
        characterData.minPhysicalDefense = minPhysicalDefense;
        characterData.maxPhysicalDefense = maxPhysicalDefense;
        characterData.currentPhysicalDefense = currentPhysicalDefense;
        characterData.minTacticalDefense = minTacticalDefense;
        characterData.maxTacticalDefense = maxTacticalDefense;
        characterData.currentTacticalDefense = currentTacticalDefense;
        characterData.minPhysicalDamage = minPhysicalDamage;
        characterData.maxPhysicalDamage = maxPhysicalDamage;
        characterData.currentPhysicalDamage = currentPhysicalDamage;
        characterData.minTacticalDamage = minTacticalDamage;
        characterData.maxTacticalDamage = maxTacticalDamage;
        characterData.currentTacticalDamage = currentTacticalDamage;
        characterData.minPhysicalPenetration = minPhysicalPenetration;
        characterData.maxPhysicalPenetration = maxPhysicalPenetration;
        characterData.currentPhysicalPenetration = currentPhysicalPenetration;
        characterData.minTacticalPenetration = minTacticalPenetration;
        characterData.maxTacticalPenetration = maxTacticalPenetration;
        characterData.currentTacticalPenetration = currentTacticalPenetration;
        characterData.minCriticalStrikeDamage = minCriticalStrikeDamage;
        characterData.maxCriticalStrikeDamage = maxCriticalStrikeDamage;
        characterData.currentCriticalStrikeDamage = currentCriticalStrikeDamage;
        characterData.minCriticalStrikeChance = minCriticalStrikeChance;
        characterData.maxCriticalStrikeChance = maxCriticalStrikeChance;
        characterData.currentCriticalStrikeChance = currentCriticalStrikeChance;
        characterData.minCriticalStrikeChanceMultiplier = minCriticalStrikeChanceMultiplier;
        characterData.maxCriticalStrikeChanceMultiplier = maxCriticalStrikeChanceMultiplier;
        characterData.currentCriticalStrikeChanceMultiplier = currentCriticalStrikeChanceMultiplier;
        
        //Konvertierung des neu erstellten CharacterData obj. in JSON
        //characterDataJSON = JsonUtility.ToJson(characterData);
        Debug.Log("Alle Daten des Charakters wurden erfolgreich an den PlayFab Manager übermittelt!");
    }

    public void CreateDataStructureForPlayFabCharacterBlock1()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Data = new Dictionary<string, string>()
        {
            {"Forename", characterData.foreName},
            {"Lastname", characterData.lastName},
            { "Age", characterData.currentAge.ToString() },
            { "Current Position X", characterData.currentPosX.ToString() },
            { "Current Position Y", characterData.currentPosY.ToString() },
            { "Current Position Z", characterData.currentPosZ.ToString() },
            { "Currency Gold", characterData.currentCurrency1.ToString() },
            { "Currency Silver", characterData.currentCurrency2.ToString() },
            { "Currency Copper", characterData.currentCurrency3.ToString() },
            { "Minimal Level", characterData.minLevel.ToString() },
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void CreateDataStructureForPlayFabCharacterBlock2()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Data = new Dictionary<string, string>()
        {
            { "Maximal Level", characterData.maxLevel.ToString() },
            { "Current Level", characterData.currentLevel.ToString() }, 
            { "Minimal Experience Points", characterData.minExperiencePoints.ToString() },
            { "Maximum Experience Points", characterData.maxExperiencePoints.ToString() },
            { "Current Experience Points", characterData.currentExperiencePoints.ToString() },
            { "Left Experience Points", characterData.leftExperiencePoints.ToString() },
            { "Minimum Attribute Points", characterData.minAttributePoints.ToString() },
            { "Maximum Attribute Points", characterData.maxAttributePoints.ToString() },
            { "Current Attribute Points", characterData.currentAttributePoints.ToString() },
            { "Spent Attribute Points", characterData.spentAttributePoints.ToString() },
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void CreateDataStructureForPlayFabCharacterBlock3()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Data = new Dictionary<string, string>()
        {
            { "Attribute Refund Points", characterData.attributeRefundPoints.ToString() },
            { "Minimum Stamina", characterData.minStamina.ToString() },
            { "Maximum Stamina", characterData.maxStamina.ToString() },
            { "Current Stamina", characterData.currentStamina.ToString() },
            { "Minimum Health", characterData.minHealth.ToString() },
            { "Maximum Health", characterData.maxHealth.ToString() },
            { "Current Health", characterData.currentHealth.ToString() },
            { "Minimum Armour", characterData.minArmour.ToString() },
            { "Maximum Armour", characterData.maxArmour.ToString() },
            { "Current Armour", characterData.currentArmour.ToString() },
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void CreateDataStructureForPlayFabCharacterBlock4()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Physical Defense", characterData.minPhysicalDefense.ToString()},
            {"Maximum Physical Defense", characterData.maxPhysicalDefense.ToString()},
            {"Current Physical Defense", characterData.currentPhysicalDefense.ToString()},
            {"Minimum Tactical Defense", characterData.minTacticalDefense.ToString()},
            {"Maximum Tactical Defense", characterData.maxTacticalDefense.ToString()},
            {"Current Tactical Defense", characterData.currentTacticalDefense.ToString()},
            {"Minimum Physical Damage", characterData.minPhysicalDamage.ToString()},
            {"Maximum Physical Damage", characterData.maxPhysicalDamage.ToString()},
            {"Current Physical Damage", characterData.currentPhysicalDamage.ToString()},
            {"Minimum Tactical Damage", characterData.minTacticalDamage.ToString()},
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void CreateDataStructureForPlayFabCharacterBlock5()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Data = new Dictionary<string, string>()
        {
            {"Maximum Tactical Damage", characterData.maxTacticalDamage.ToString()},
            {"Current Tactical Damage", characterData.currentTacticalDamage.ToString()},
            {"Minimum Physical Penetration", characterData.minPhysicalPenetration.ToString()},
            {"Maximum Physical Penetration", characterData.maxPhysicalPenetration.ToString()},
            {"Current Physical Penetration", characterData.currentPhysicalPenetration.ToString()},
            {"Minimum Tactical Penetration", characterData.minTacticalPenetration.ToString()},
            {"Maximum Tactical Penetration", characterData.maxTacticalPenetration.ToString()},
            {"Current Tactical Penetration", characterData.currentTacticalPenetration.ToString()},
            {"Minimum Critical Strike Damage", characterData.minCriticalStrikeDamage.ToString()},
            {"Maximum Critical Strike Damage", characterData.maxCriticalStrikeDamage.ToString()},
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void CreateDataStructureForPlayFabCharacterBlock6()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Data = new Dictionary<string, string>()
        {
            {"Current Critical Strike Damage", characterData.currentCriticalStrikeDamage.ToString()},
            {"Minimum Critical Strike Chance", characterData.minCriticalStrikeChance.ToString()},
            {"Maximum Critical Strike Chance", characterData.maxCriticalStrikeChance.ToString()},
            {"Current Critical Strike Chance", characterData.currentCriticalStrikeChance.ToString()},
            {"Minimum Critical Strike Chance Multiplier", characterData.minCriticalStrikeChanceMultiplier.ToString()},
            {"Maximum Critical Strike Chance Multiplier", characterData.maxCriticalStrikeChanceMultiplier.ToString()},
            {"Current Critical Strike Chance Multiplier", characterData.currentCriticalStrikeChanceMultiplier.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    
    private void OnUpdateCharacterDataSucceed(UpdateCharacterDataResult result)
    {
        Debug.Log("Parameter für Datenbankerstellung wurden erfolgreich übergeben und eingetragen!");
    }

    private void OnUpdateCharacterDataFailed(PlayFabError result)
    {
        Debug.Log(result.ErrorDetails);
    }

    IEnumerator Waiter()
    {
        for (int i = 0; i < 6; i++)
        {
            
        }
        yield return new WaitForSeconds(1f);
    }
}
