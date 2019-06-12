﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    #region Singleton

    public static PlayerStats instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    StatPanel statPanel;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        statPanel = StatPanel.instance;

        // определяем типы статов
        statPanel.SetStats(damage, armor);
        statPanel.UpdateStatValues();
    }

    public void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        
        if (newItem != null)
        {
            Debug.Log("ПРРОВЕРОЧКА");
            damage.AddModifier(newItem.damageMod);
            armor.AddModifier(newItem.armorMod);
        }
        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageMod);
            armor.RemoveModifier(oldItem.armorMod);
        }

        // обновляем панель со статами при каждом изменнии модифаера
        statPanel.UpdateStatValues();
    }
}