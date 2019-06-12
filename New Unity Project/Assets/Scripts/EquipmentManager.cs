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

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    Equipment[] currentEquipment;

    Inventory inventory;

    public Transform equipmentParent;
    EquipSlot[] equipSlots;

    StatPanel statPanel;

    private void Start()
    {
        inventory = Inventory.instance;
        statPanel = StatPanel.instance;
        // CСчитаем сколько элементов жквипа в энумераторе
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        // Инициализируем массив длиной в кол-во элементов энумератора
        currentEquipment = new Equipment[numSlots];

        equipSlots = equipmentParent.GetComponentsInChildren<EquipSlot>();

        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].onInventorySlotChangedCallback += UnequipFromItemPanel;
            
        }
    }
    // Если итем - эквип, то вызываем метод Equip
    public void EquipFromTheInventory(Item item)
    {
        Debug.Log("item name is " + item.name);

        if (item is Equipment)
        {
            // удаляем итем из инвентаря
            inventory.Remove(item);
            //Debug.Log("item removed" + item);
 
            // вызываем метод добавления в эквип панель
            Equip((Equipment)item);
        }
    }


    public void Equip (Equipment newItem)
    {
        // Чекаем, какого типа является итем (из енумки). Приводим к инт
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        // меняем иконку, но это надо перепроверить. вроде лишнее
        equipSlots[slotIndex].icon.sprite = newItem.icon;
        equipSlots[slotIndex].item = newItem;

        // если в слоте есть итем - добавляем его в инвентарь
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
            Debug.Log("DELEGAT APPEARED");
        }
            // Помещаем итем в массив на позицию, соответствующую числовому индексу в энумераторе
        currentEquipment[slotIndex] = newItem;

    }

    public void UnequipFromItemPanel(Item item)
    {
        //Debug.Log("item name is " + item.name);

        if (item is Equipment)
        {
            Unquip((Equipment)item);
        }
    }

    public void Unquip(Equipment item)
    {
        //чекаем, смогли ли мы добавить эквип в сумму (есть ли место)
        if (inventory.Add(item))
        {
            Equipment oldItem = item;
            int slotIndex = (int)item.equipSlot;
            // если смогли - удаляем ее из панели и массива
            currentEquipment[slotIndex] = null;

            // удаляем иконки
            equipSlots[slotIndex].icon.sprite = null;
            equipSlots[slotIndex].item = null;
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

        }
        else
        {
            Debug.Log("МЕСТА В СУМКЕ НЕТ");

        }
        //Debug.Log("not enough room");

    }
}
