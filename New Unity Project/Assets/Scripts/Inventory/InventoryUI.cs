using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;

    //public Transform equipmentParent;
    EquipmentManager equipment;
    //EquipSlot[] equipSlots;


    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();


        equipment = EquipmentManager.instance;
        //equipment.onChangedCallback += UpdateEquipPanel;

        //equipSlots = equipmentParent.GetComponentsInChildren<EquipSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].onInventorySlotChangedCallback += equipment.EquipFromTheInventory;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    //void UpdateEquipPanel(Equipment item)
    //{
    //   for(int i = 0; i < slots.Length; i++)
    //   {
    //        equipSlots[i].item = item;
    //   }


    //}




    void UpdateUI()
    {
        //Debug.Log("UPDATING");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) // если есть итемы для добавления
            {
                slots[i].AddItem(inventory.items[i]); //добавляем
            }
            else //если нет
            {
                // очищаем слоты
                slots[i].ClearSlot();
            }
        }
    }
}
