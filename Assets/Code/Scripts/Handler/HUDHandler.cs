using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.MultiplayerModels;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour
{
    #region Variables
    //HOD Component References
    public Slider HealthBar_Slider; 
    public Slider ExperienceBar_Slider;
    public TMP_Text HealtBarText_Text_TMP;
    public TMP_Text ExperienceBarText_Text_TMP;
    
    
    private PlayerStatHandler _playerStatHandler;
    #endregion

    #region Unity Functions

    private void Awake() 
    {
        
        /// <summary>
        /// Muss erstmal gehardcodet, werden... hier muss später eine effektivere Lösung her, wie er sich die passenden Slider und co grabbt
        /// </summary>
        /*HealthBar_Slider = GetComponentInChildren<Slider>(); //maybe simplifizieren
        if (HealthBar_Slider != null && HealthBar_Slider.name == "HealthBar_Slider")
        {
           Debug.Log("HealthBar_Slider wurde gefunden!");
        }
        HealtBarText_Text_TMP = GetComponentInChildren<TMP_Text>();

        ExperienceBar_Slider = GetComponentInChildren<Slider>();
        if (ExperienceBar_Slider != null && ExperienceBar_Slider.name == "ExperienceBar_Slider")
        {
            Debug.Log("ExperienceBar_Slider wurde gefunden!");
        }

        ExperienceBarText_Text_TMP = GetComponentInChildren<TMP_Text>();*/


    }

    private void Start()
    {
        _playerStatHandler = FindObjectOfType<PlayerStatHandler>();
    }
    #endregion

    #region Custom Functions

    //Update the Users Health Data for HUD
    public void UpdatePlayersHealthStats(float minHealth, float maxHealth, float currentHealth)
    {
        HealthBar_Slider.minValue = minHealth;
        HealthBar_Slider.maxValue = maxHealth;
        HealthBar_Slider.value = currentHealth;

        float roundedMaxHealth = Mathf.Round(maxHealth);
        string formattedMaxHealth = roundedMaxHealth.ToString("F1");
        
        float roundedCurrentHealth = Mathf.Round(currentHealth);
        string formattedCurrentHealth = roundedCurrentHealth.ToString("F1");
        
        //Calculate the currentHealth Percentage from maxHealth
        float healthPercentage = (currentHealth - minHealth) / (maxHealth - minHealth) * 100f;
        string formattedHealthPercentage = healthPercentage.ToString("F1");
        
        HealtBarText_Text_TMP.text = $"{formattedHealthPercentage}" + " % " + $" | {formattedCurrentHealth} / {formattedMaxHealth}";

        if (healthPercentage >= 75)
        {
            HealthBar_Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color32(0, 105, 0, 255);
        }
        else if (healthPercentage < 75 && healthPercentage >= 50)
        {
            HealthBar_Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color32(235, 185, 0, 255);;
        }
        else if (healthPercentage < 50 && healthPercentage >= 25)
        {
            HealthBar_Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color32(200, 105, 0, 255);
        }
        else if (healthPercentage < 25 && healthPercentage >= 10)
        {
            HealthBar_Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color32(180, 0, 10, 255);
        }
        else if (healthPercentage < 25)
        {
            HealthBar_Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color32(110, 0, 10, 255);
        }
    }

    public void UpdatePlayerLevelAndExperienceStats(float level, float minExperience, float maxExperience, float currentExperience)
    {
        ExperienceBar_Slider.minValue = minExperience;
        ExperienceBar_Slider.maxValue = maxExperience;
        ExperienceBar_Slider.value = currentExperience;
        
        float roundedMaxExperience = Mathf.Round(maxExperience);
        string formattedMaxExperience = roundedMaxExperience.ToString("F1");
        
        float roundedCurrentExperience = Mathf.Round(currentExperience);
        string formattedCurrentExperience = roundedCurrentExperience.ToString("F1");
        
        //Calculate the currentExperience Percentage from maxExperience
        float experiencePercentage = (currentExperience - minExperience) / (maxExperience - minExperience) * 100f;
        string formattedExperiencePercentage = experiencePercentage.ToString("F1");

        ExperienceBarText_Text_TMP.text = $"LVL: {level.ToString()} | EXP: {formattedCurrentExperience} / {formattedMaxExperience}";

    }
                    
    #endregion
}
