using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PrototypingUIHandler : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text InputReminder;
    private string InputReminderString;
    


    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        InputReminder.text = playerController.GetInputReminderString(); 
    }
}
