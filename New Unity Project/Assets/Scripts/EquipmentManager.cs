using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        // CСчитаем сколько элементов жквипа в энумераторе
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        // Инициализируем массив длиной в кол-во элементов энумератора
        currentEquipment = new Equipment[numSlots];
       

    }
    // Если итем - эквип, то вызываем метод Equip
    public void EquipFromTheInventory(Item item)
    {
        Debug.Log("item name is " + item.name);

        if (item is Equipment)
        {
            inventory.Remove(item);
            Debug.Log("item removed" + item);
            Equip((Equipment)item);

        }
    }


    public void Equip (Equipment newItem)
    {
        // Чекаем, какого типа является итем (из енумки). Приводим к интовому значению
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        // Помещаем итем в массив на позицию, соответствующую числовому индексу в энумераторе
        currentEquipment[slotIndex] = newItem;
        //Debug.Log("ITEEEEEEEEEEEEEEM IS " + newItem.name);
        //Debug.Log("state equip");
    }
}
