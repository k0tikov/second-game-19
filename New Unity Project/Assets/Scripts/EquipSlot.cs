using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlot : InventorySlot, IPointerClickHandler
{

    public EquipmentSlot equipmentSlot;

    private void OnValidate () => gameObject.name = equipmentSlot.ToString() + " Slot";


}
