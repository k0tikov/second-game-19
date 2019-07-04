using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlot : InventorySlot
{
    public EquipmentSlot equipmentSlot;

    private void OnValidate () => gameObject.name = equipmentSlot.ToString() + " Slot";

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
        {
            return true;
        }

        Equipment equipment = item as Equipment;
        return equipment != null && equipment.equipSlot == equipmentSlot;
    }
}
