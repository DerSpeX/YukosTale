using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UserDataPermission = PlayFab.ClientModels.UserDataPermission;
using Newtonsoft.Json;
using PlayFab.AdminModels;
using UnityEngine.TextCore.Text;
using GetUserDataRequest = PlayFab.ClientModels.GetUserDataRequest;
using GetUserDataResult = PlayFab.ClientModels.GetUserDataResult;


public class PlayFabHandler : MonoBehaviour
{
    //Müssen noch zugeordnet werden!
    public string customID;
    public string characterName;
    public string characterID;
    public  string playFabID;
    

    #region Variables
    public CharacterData characterData;
    [System.Serializable]
    public class CharacterData
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
        public string cClass;
        [Space(25)] 
        
        [Header("Race")] 
        [Space(10)]
        public string cRace;
        [Space(25)]
        
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
        [Space(25)] 

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
        [Space(25)]
        
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
        [Space(25)]
        
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
        [Space(25)]

        //Combat Informations Phys_Tact_Defense
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
        [Header(" Defensive Regenerations")]
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
        [Header(" Neutral Regenerations")]
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
      private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                CreateCharacter();
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                UpdateAllCharacterData();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                LoadAllCharacterData();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadCharacters();
            }
        }
    #endregion
    #region Custom Functions
    
    //Register User
    public void RegisterUser(string regMailAddress, string regDisplayname, string regUsername, string regPassword)
    {
        var request = new RegisterPlayFabUserRequest();
        
        request.TitleId = PlayFabSettings.TitleId; //Alternativ: A1A0B
        request.Email = regMailAddress;
        request.DisplayName = regDisplayname;
        request.Username = regUsername;
        request.Password = regPassword;

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserSucceed, OnRegisterUserFailed);
    }
    private void OnRegisterUserSucceed(RegisterPlayFabUserResult result)
    {
        //Grant Item to User... Triggered by Automation Rule Playstream Event
        Debug.Log("Register User was sucessfull!");

    }
    private void OnRegisterUserFailed(PlayFabError result)
    {
        Debug.Log("Register User Failed!");
        Debug.Log(result.GenerateErrorReport());
        //Fehlermeldung auf Display des Spielers anzeigen im HUD
    }
    //Login User
    public void LogIn(string loginUsername, string loginPassword)
    {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = loginUsername;
        request.Password = loginPassword;

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSucceed, OnLoginFailed);
    }
    //RESULT CALLBACKS FOR LOGIN
    private void OnLoginSucceed(LoginResult result)
    {
        Debug.Log("You're logged in NOW!");
        playFabID = result.PlayFabId;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Prototyping_Level");
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
            UpdateAll_Character_Informations();
            UpdateAll_Currency_Informations();
            UpdateAll_Location_Informations();
            Invoke("UpdateAll_Level_Informations_Level_Experience",1);
            Invoke("UpdateAll_Level_Informations_Attribute_Points",1);
            Invoke("UpdateAll_Combat_Informations_Health_Shield_Armour",1);
            Invoke("UpdateAll_Combat_Informations_Defensives_Regeneration",2);
            Invoke("UpdateAll_Combat_Informations_Stam_Mana_Wrath",2);
            Invoke("UpdateAll_Combat_Informations_Neutrals_Regeneration",2);
            Invoke("UpdateAll_Combat_Informations_Phys_Tact_Defense",3);
            Invoke("UpdateAll_Combat_Informations_Phys_Tact_Damage",3);
            Invoke("UpdateAll_Combat_Informations_Phys_Tact_Penetration",3);
            Invoke("UpdateAll_Combat_Informations_Crit_Strike_D_C",3);
            Debug.Log("User Data are now synced with PlayFab Cloud!");
            //Liste muss hier weg und später in die Charaktererstellung mit Loop, und dort Charaktere via CLoud zugewiesen kriegen
            List<Character> characters = new List<Character>();
        }
        else
        {
            Debug.Log("Es kamen keine Daten im PlayFab Manager an, Forename == null!");
        }
    }
    private void OnGrantCharacterToUserFailed(PlayFabError result)
    {
        Debug.Log("Grant Character to User Failed!");
        Debug.Log(result.GenerateErrorReport());
    }
    
    //Get all the Character Data from PlayerStatHandler
    public void GetAllCharacterData(string foreName, string lastName, int currentAge, string cClass, string cRace, float currentPosX, float currentPosY, float currentPosZ, string currentRegion, string currencyName1, int currentCurrency1, 
            string currencyName2, int currentCurrency2, string currencyName3, int currentCurrency3, int minLevel, int maxLevel, int currentLevel, float minExperiencePoints, 
            float maxExperiencePoints, float currentExperiencePoints, float leftExperiencePoints, int minAttributePoints, int maxAttributePoints, int currentAttributePoints, int spentAttributePoints,
            int attributeRefundPoints, float minHealth, float maxHealth, float currentHealth,float minShield, float maxShield, float currentShield, float minArmour, float maxArmour, float currentArmour, float minStamina, float maxStamina,
            float currentStamina, float minMana, float maxMana, float currentMana, float minWrath, float maxWrath, float currentWrath,
            float minPhysicalDefense, float maxPhysicalDefense, float currentPhysicalDefense, float minTacticalDefense,
            float maxTacticalDefense, float currentTacticalDefense, float minPhysicalDamage, float maxPhysicalDamage,float currentPhysicalDamage, float minTacticalDamage, float maxTacticalDamage, float currentTacticalDamage, 
            float minPhysicalPenetration, float maxPhysicalPenetration, float currentPhysicalPenetration, float minTacticalPenetration,
            float maxTacticalPenetration, float currentTacticalPenetration, float minCriticalStrikeDamage, float maxCriticalStrikeDamage,
            float currentCriticalStrikeDamage, float minCriticalStrikeChance, float maxCriticalStrikeChance, float currentCriticalStrikeChance, float minHealthReg, float maxHealthReg, float currentHealthReg, float minShieldReg, float maxShieldReg,
            float currentShieldReg, float minStaminaReg, float maxStaminaReg, float currentStaminaReg, float minManaReg, float maxManaReg, float currentManaReg, float minWrathReg, float maxWrathReg, float currentWrathReg
    )

    {

        //Player Informations
        characterData.foreName = foreName;
        characterData.lastName = lastName;
        characterData.currentAge = currentAge;
        characterData.cClass = cClass;
        characterData.cRace = cRace;

        //Location Informations
        characterData.currentPosX = currentPosX;
        characterData.currentPosY = currentPosY;
        characterData.currentPosZ = currentPosZ;
        characterData.currentRegion = currentRegion;
        
        //Currency Informations
        characterData.currencyName1 = currencyName1;
        characterData.currentCurrency1 = currentCurrency1;
        characterData.currencyName2 = currencyName2;
        characterData.currentCurrency2 = currentCurrency2;
        characterData.currencyName3 = currencyName3;
        characterData.currentCurrency3 = currentCurrency3;
        
        //Level Informations Level_Experience
        characterData.minLevel = minLevel;
        characterData.maxLevel = maxLevel;
        characterData.currentLevel = currentLevel;
        
        characterData.minExperiencePoints = minExperiencePoints;
        characterData.maxExperiencePoints = maxExperiencePoints;
        characterData.currentExperiencePoints = currentExperiencePoints;
        characterData.leftExperiencePoints = leftExperiencePoints;
        
        //Level Informations Attribute_Points
        characterData.minAttributePoints = minAttributePoints;
        characterData.maxAttributePoints = maxAttributePoints;
        characterData.currentAttributePoints = currentAttributePoints;
        characterData.spentAttributePoints = spentAttributePoints;
        characterData.attributeRefundPoints = attributeRefundPoints;

        //Combat Informations Health_Shield_Armour
        characterData.minHealth = minHealth;
        characterData.maxHealth = maxHealth;
        characterData.currentHealth = currentHealth;
        characterData.minShield = minShield;
        characterData.maxShield = maxShield;
        characterData.currentShield = currentShield;
        characterData.minArmour = minArmour;
        characterData.maxArmour = maxArmour;
        characterData.currentArmour = currentArmour;
        
        //Combat Informations Stam_Mana_Wrath
        characterData.minStamina = minStamina;
        characterData.maxStamina = maxStamina;
        characterData.currentStamina = currentStamina;
        characterData.minMana = minMana;
        characterData.maxMana = maxMana;
        characterData.currentMana = currentMana;
        characterData.minWrath = minWrath;
        characterData.maxWrath = maxWrath;
        characterData.currentWrath = currentWrath;
        
        //Combat Informations Phys_Tact_Defense
        characterData.minPhysicalDefense = minPhysicalDefense;
        characterData.maxPhysicalDefense = maxPhysicalDefense;
        characterData.currentPhysicalDefense = currentPhysicalDefense;
        characterData.minTacticalDefense = minTacticalDefense;
        characterData.maxTacticalDefense = maxTacticalDefense;
        characterData.currentTacticalDefense = currentTacticalDefense;
        
        //Combat Informations Def_Regenerations
        characterData.minHealthReg = minHealthReg;
        characterData.maxHealth = maxHealthReg;
        characterData.currentHealthReg = currentHealthReg;
        characterData.minShieldReg = minShieldReg;
        characterData.maxShieldReg = maxShieldReg;
        characterData.currentShieldReg = currentShieldReg;
        
        //Combat Informations Neutrals_Regeneration
        characterData.minStaminaReg = minStaminaReg;
        characterData.maxStaminaReg = maxStaminaReg;
        characterData.currentStaminaReg = currentStaminaReg;
        characterData.minManaReg = minManaReg;
        characterData.maxManaReg = maxManaReg;
        characterData.currentManaReg = currentManaReg;
        characterData.minWrathReg = minWrathReg;
        characterData.maxWrathReg = maxWrathReg;
        characterData.currentWrathReg = currentWrathReg;
        
        //Combat Informations Phys_Tact_Damage
        characterData.minPhysicalDamage = minPhysicalDamage;
        characterData.maxPhysicalDamage = maxPhysicalDamage;
        characterData.currentPhysicalDamage = currentPhysicalDamage;
        characterData.minTacticalDamage = minTacticalDamage;
        characterData.maxTacticalDamage = maxTacticalDamage;
        characterData.currentTacticalDamage = currentTacticalDamage;
        
        //Combat Informations Phys_Tact_Penetration
        characterData.minPhysicalPenetration = minPhysicalPenetration;
        characterData.maxPhysicalPenetration = maxPhysicalPenetration;
        characterData.currentPhysicalPenetration = currentPhysicalPenetration;
        characterData.minTacticalPenetration = minTacticalPenetration;
        characterData.maxTacticalPenetration = maxTacticalPenetration;
        characterData.currentTacticalPenetration = currentTacticalPenetration;
        
        //Combat Informations Crit_Strike_D_C
        characterData.minCriticalStrikeDamage = minCriticalStrikeDamage;
        characterData.maxCriticalStrikeDamage = maxCriticalStrikeDamage;
        characterData.currentCriticalStrikeDamage = currentCriticalStrikeDamage;
        characterData.minCriticalStrikeChance = minCriticalStrikeChance;
        characterData.maxCriticalStrikeChance = maxCriticalStrikeChance;
        characterData.currentCriticalStrikeChance = currentCriticalStrikeChance;

        //Konvertierung des neu erstellten CharacterData obj. in JSON
        //characterDataJSON = JsonUtility.ToJson(characterData);
        Debug.Log("Alle Daten des Charakters wurden erfolgreich an den PlayFab Manager übermittelt!");
    }
    
    //Update Character Data Functions
    public void UpdateAll_Character_Informations()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Forename", characterData.foreName},
            {"Lastname", characterData.lastName},
            {"Age", characterData.currentAge.ToString() },
            {"Class", characterData.cClass},
            {"Race", characterData.cRace}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Location_Informations()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Current Position X", characterData.currentPosX.ToString()},
            {"Current Position Y", characterData.currentPosY.ToString()},
            {"Current Position Z", characterData.currentPosZ.ToString()},
            {"Current Region", characterData.currentRegion}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Currency_Informations()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {characterData.currencyName1, characterData.currentCurrency1.ToString()},
            {characterData.currencyName2, characterData.currentCurrency2.ToString()},
            {characterData.currencyName3, characterData.currentCurrency3.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Level_Informations_Level_Experience()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;

        request.Data = new Dictionary<string, string>()
        {
            {"Minimal Level", characterData.minLevel.ToString()},
            {"Maximal Level", characterData.maxLevel.ToString()},
            {"Current Level", characterData.currentLevel.ToString()}, 
            {"Minimal Experience Points", characterData.minExperiencePoints.ToString()},
            {"Maximum Experience Points", characterData.maxExperiencePoints.ToString()},
            {"Current Experience Points", characterData.currentExperiencePoints.ToString()},
            {"Left Experience Points", characterData.leftExperiencePoints.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Level_Informations_Attribute_Points()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Attribute Points", characterData.minAttributePoints.ToString()},
            {"Maximum Attribute Points", characterData.maxAttributePoints.ToString()},
            {"Current Attribute Points", characterData.currentAttributePoints.ToString()},
            {"Spent Attribute Points", characterData.spentAttributePoints.ToString()},
            {"Attribute Refund Points", characterData.attributeRefundPoints.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Health_Shield_Armour()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Health", characterData.minHealth.ToString()},
            {"Maximum Health", characterData.maxHealth.ToString()},
            {"Current Health", characterData.currentHealth.ToString()},
            {"Minimum Shield", characterData.minShield.ToString()},
            {"Maximum Shield", characterData.maxShield.ToString()},
            {"Current Shield", characterData.currentShield.ToString()},
            {"Minimum Armour", characterData.minArmour.ToString()},
            {"Maximum Armour", characterData.maxArmour.ToString()},
            {"Current Armour", characterData.currentArmour.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Defensives_Regeneration()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Health Regeneration", characterData.minHealthReg.ToString()},
            {"Maximum Health Regeneration", characterData.maxHealthReg.ToString()},
            {"Current Health Regeneration", characterData.currentHealthReg.ToString()},
            {"Minimum Shield Regeneration", characterData.minShieldReg.ToString()},
            {"Maximum Shield Regeneration", characterData.maxShieldReg.ToString()},
            {"Current Shield Regeneration", characterData.currentShieldReg.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Stam_Mana_Wrath()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Stamina", characterData.minStamina.ToString()},
            {"Maximum Stamina", characterData.maxStamina.ToString()},
            {"Current Stamina", characterData.currentStamina.ToString()},
            {"Minimum Mana", characterData.minMana.ToString()},
            {"Maximum Mana", characterData.maxMana.ToString()},
            {"Current Mana", characterData.currentMana.ToString()},
            {"Minimum Wrath", characterData.minWrath.ToString()},
            {"Maximum Wrath", characterData.maxWrath.ToString()},
            {"Current Wrath", characterData.currentWrath.ToString()}
        };
            
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Neutrals_Regeneration()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Stamina Regeneration", characterData.minStaminaReg.ToString()},
            {"Maximum Stamina Regeneration", characterData.maxStaminaReg.ToString()},
            {"Current Stamina Regeneration", characterData.currentStaminaReg.ToString()},
            {"Minimum Mana Regeneration", characterData.minManaReg.ToString()},
            {"Maximum Mana Regeneration", characterData.maxManaReg.ToString()},
            {"Current Mana Regeneration", characterData.currentManaReg.ToString()},
            {"Minimum Wrath Regeneration", characterData.minWrathReg.ToString()},
            {"Maximum Wrath Regeneration", characterData.maxWrathReg.ToString()},
            {"Current Wrath Regeneration", characterData.currentWrathReg.ToString()}
        };
            
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Phys_Tact_Defense()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Physical Defense", characterData.minPhysicalDefense.ToString()},
            {"Maximum Physical Defense", characterData.maxPhysicalDefense.ToString()},
            {"Current Physical Defense", characterData.currentPhysicalDefense.ToString()},
            {"Minimum Tactical Defense", characterData.minTacticalDefense.ToString()},
            {"Maximum Tactical Defense", characterData.maxTacticalDefense.ToString()},
            {"Current Tactical Defense", characterData.currentTacticalDefense.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Phys_Tact_Damage()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        { 
            {"Minimum Physical Damage", characterData.minPhysicalDamage.ToString()},
            {"Maximum Physical Damage", characterData.maxPhysicalDamage.ToString()},
            {"Current Physical Damage", characterData.currentPhysicalDamage.ToString()},
            {"Minimum Tactical Damage", characterData.minTacticalDamage.ToString()},
            {"Maximum Tactical Damage", characterData.maxTacticalDamage.ToString()},
            {"Current Tactical Damage", characterData.currentTacticalDamage.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Phys_Tact_Penetration()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Physical Penetration", characterData.minPhysicalPenetration.ToString()},
            {"Maximum Physical Penetration", characterData.maxPhysicalPenetration.ToString()},
            {"Current Physical Penetration", characterData.currentPhysicalPenetration.ToString()},
            {"Minimum Tactical Penetration", characterData.minTacticalPenetration.ToString()},
            {"Maximum Tactical Penetration", characterData.maxTacticalPenetration.ToString()},
            {"Current Tactical Penetration", characterData.currentTacticalPenetration.ToString()}
        };
        
        PlayFabClientAPI.UpdateCharacterData(request, OnUpdateCharacterDataSucceed, OnUpdateCharacterDataFailed);
    }
    public void UpdateAll_Combat_Informations_Crit_Strike_D_C()
    {
        var request = new UpdateCharacterDataRequest();
        request.CharacterId = characterID;
        request.Permission = UserDataPermission.Public;
        request.Data = new Dictionary<string, string>()
        {
            {"Minimum Critical Strike Damage", characterData.minCriticalStrikeDamage.ToString()},
            {"Maximum Critical Strike Damage", characterData.maxCriticalStrikeDamage.ToString()},
            {"Current Critical Strike Damage", characterData.currentCriticalStrikeDamage.ToString()},
            {"Minimum Critical Strike Chance", characterData.minCriticalStrikeChance.ToString()},
            {"Maximum Critical Strike Chance", characterData.maxCriticalStrikeChance.ToString()},
            {"Current Critical Strike Chance", characterData.currentCriticalStrikeChance.ToString()}
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
    // Update Character Stats
    public void UpdateAllCharacterData()
    {
        UpdateAll_Character_Informations();
        UpdateAll_Currency_Informations();
        UpdateAll_Location_Informations();
        Invoke("UpdateAll_Level_Informations_Level_Experience",1);
        Invoke("UpdateAll_Level_Informations_Attribute_Points",1);
        Invoke("UpdateAll_Combat_Informations_Health_Shield_Armour",1);
        Invoke("UpdateAll_Combat_Informations_Defensives_Regeneration",2);
        Invoke("UpdateAll_Combat_Informations_Stam_Mana_Wrath",2);
        Invoke("UpdateAll_Combat_Informations_Neutrals_Regeneration",2);
        Invoke("UpdateAll_Combat_Informations_Phys_Tact_Defense",3);
        Invoke("UpdateAll_Combat_Informations_Phys_Tact_Damage",3);
        Invoke("UpdateAll_Combat_Informations_Phys_Tact_Penetration",3);
        Invoke("UpdateAll_Combat_Informations_Crit_Strike_D_C",3);
        Debug.Log("User Data are now synced with PlayFab Cloud!");
    }

    public void LoadAllCharacterData()
    {
        var request = new GetCharacterDataRequest();
        request.PlayFabId = playFabID;
        request.CharacterId = characterID;
        PlayFabClientAPI.GetCharacterData(request, OnLoadCharacterDataSucceed, OnLoadCharacterDataFailed);
    }
    private void OnLoadCharacterDataSucceed(GetCharacterDataResult result)
    {
        Debug.Log("Character Data was successfully load from PlayFab Cloud!");
        if (result.Data == null || result.Data.ContainsKey("Forename") == false)
        {
            Debug.Log("No Forename");
            return;
        }
        Debug.Log(result.Data["Forename"].Value);
        //Beispiel zum Konvertieren der Werte und Vorlage zum übermitteln an den Player!!!!
        characterData.foreName = result.Data["Forename"].Value;
        characterData.currentAge = int.Parse(result.Data["Current Age"].Value.ToString());

    }
    private void OnLoadCharacterDataFailed(PlayFabError result)
    {
        Debug.Log(result.GenerateErrorReport());
    }

    public void LoadCharacters()
    {
        var request = new GetUserDataRequest();
        request.PlayFabId = playFabID;
        
        PlayFabClientAPI.GetUserData(request, OnLoadUserCharactersSucceed, OnLoadUserCharactersFailed);
    }
    
    private void OnLoadUserCharactersSucceed(GetUserDataResult result)
    {
        Debug.Log("Received Characters from User!");
        if (result.Data != null && result.Data.ContainsKey("Characters"))
        {
            //List<UserCharacters> userCharacters =
             //  JsonConvert.DeserializeObject<List<UserCharacters>>(result.Data["Characters"].Value);
           
              Debug.Log(result.Data["Characters"].Value.ToString());
        }
    }
    
    private void OnLoadUserCharactersFailed(PlayFabError result)
    {
        Debug.Log(result.GenerateErrorReport());
    }

    #endregion
}
