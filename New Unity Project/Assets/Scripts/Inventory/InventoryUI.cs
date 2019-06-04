using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    EquipmentManager equipmentManager;

    InventorySlot[] slots;

    //public event Action<Item> OnItemRightClickedEvent;


    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        equipmentManager = EquipmentManager.instance;


        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].onInventorySlotChangedCallback += equipmentManager.EquipFromTheInventory;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

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
