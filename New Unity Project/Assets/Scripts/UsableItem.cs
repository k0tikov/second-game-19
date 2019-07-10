using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/UsableItem")]
public class UsableItem : Item
{

    public bool isConsumable;

    public int healthRestore;

    //void Start()
    //{
    //    EquipmentManager.instance.OnUpdateHealthChanged += Use;
    //}

    public override void Use(PlayerStats playerStats)
    {
        // Ресаем хп, на сумму, сколько было назначено на поушне
        //playerStats.health.baseValue += healthRestore;
        Debug.Log("Health restored");
    }

}
