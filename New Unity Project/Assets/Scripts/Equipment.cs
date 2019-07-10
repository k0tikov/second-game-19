using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorMod;
    public int damageMod;

    public override void Use (PlayerStats playerStats)
    {
        //base.Use();
        // Equip the item
        //EquipmentManager.instance.Equip(this);
        // Remove it from the inventory
    }
    
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }