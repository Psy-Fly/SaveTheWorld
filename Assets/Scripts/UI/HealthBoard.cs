using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBoard : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    public void SetHealth(int amountScore)
    {
        healthText.text = amountScore.ToString();
    }
}
