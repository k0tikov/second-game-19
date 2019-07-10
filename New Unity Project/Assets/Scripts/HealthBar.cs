using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


    EquipmentManager equipmentManager;
    PlayerStats playerStats;

    public Slider healthSlider;



    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onUpdateHealthChanged += UpdateHealth;

        playerStats = PlayerStats.instance;
        healthSlider.maxValue = playerStats.health.baseValue;
        healthSlider.value = playerStats.health.GetValue();



        Debug.Log(playerStats.health.GetValue());
    }

    void UpdateHealth(int healthChanging)
    {
        if (healthSlider.value + healthChanging >= healthSlider.maxValue)
        {
            healthSlider.value = healthSlider.maxValue;
            Debug.Log("MAX VALUE");
        }
        else
        {
            healthSlider.value += healthChanging;
            Debug.Log("CURRENT VALUE");
        }
        
    }
}
