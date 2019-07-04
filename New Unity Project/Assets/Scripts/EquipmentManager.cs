using System;
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

    public event Action<InventorySlot> OnPointerEnterEvent;
    public event Action<InventorySlot> OnPointerExitEvent;
    public event Action<InventorySlot> OnRightClickEvent;
    public event Action<InventorySlot> OnBeginDragEvent;
    public event Action<InventorySlot> OnEndDragEvent;
    public event Action<InventorySlot> OnDragEvent;
    public event Action<InventorySlot> OnDropEvent;

    Inventory inventory;
    StatPanel statPanel;
    ItemTooltip itemTooltip;
    public Transform equipmentParent;

    EquipSlot[] equipSlots;
    public Equipment[] currentEquipment;

    public void Start()
    {
        inventory = Inventory.instance;
        statPanel = StatPanel.instance;
        itemTooltip = ItemTooltip.instance;
        // Cчитаем сколько элементов эквипа в энумераторе
        int numSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        // Инициализируем массив длиной в кол-во элементов энумератора
        currentEquipment = new Equipment[numSlots];

        equipSlots = equipmentParent.GetComponentsInChildren<EquipSlot>();

        // Right click
        OnRightClickEvent += Unequip;
        // Pointer enter
        OnPointerEnterEvent += itemTooltip.ShowTooltip;
        // Pointer exit
        OnPointerExitEvent += itemTooltip.HideTooltip;
        // Begin drag
        OnBeginDragEvent += inventory.OnBeginDrag;
        // End drag
        OnEndDragEvent += inventory.OnEndDrag;
        // Drag
        OnDragEvent += inventory.Drag;
        // Drop
        OnDropEvent += inventory.Drop;

        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipSlots[i].OnDragEvent += OnDragEvent;
            equipSlots[i].OnDropEvent += OnDropEvent;
        }
    }
    // перегруженный метод, если передаем слот, а не итем
    public void Equip(InventorySlot slot)
    {
        Equipment equipment = slot.item as Equipment;
        if (equipment != null)
        {    
            Equip(equipment);
        }
    }
    public void Equip (Equipment newItem)
    {

        // удаляем перетаскиваемый итем из инвентаря
        inventory.Remove(newItem);
        // Чекаем, какого типа является итем (из енумки). Приводим к инт
        int slotIndex = (int)newItem.equipSlot;
        
        Equipment oldItem = null;
        // меняем иконку у слота эквипа, в который бросаем
        equipSlots[slotIndex].AddItem(newItem);


        //если в слоте эквипа есть итем
        if (currentEquipment[slotIndex] != null)
        {
            // храним его как старый 
            oldItem = currentEquipment[slotIndex];
            // добавляем его в инвентарь
            inventory.Add(oldItem);
        }
        // вызываем делегат для обновлении стат
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
            // Debug.Log("DELEGAT APPEARED");
        }
        // Помещаем итем в массив на позицию, соответствующую числовому индексу в энумераторе
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(InventorySlot slot)
    {
        Equipment equipment = slot.item as Equipment;
        if (equipment != null)
        {
            Unequip(equipment);
        }
    }
    public void Unequip(Equipment item)
    {
        //чекаем, смогли ли мы добавить эквип в сумму (есть ли место)
        if (inventory.Add(item))
        {
            Equipment oldItem = item;
            int slotIndex = (int)item.equipSlot;
            // если смогли - удаляем ее из панели и массива
            currentEquipment[slotIndex] = null;

            // удаляем иконки и дизейблим фон картинки
            equipSlots[slotIndex].ClearSlot();

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);
        }
        else
        {
            Debug.Log("МЕСТА В СУМКЕ НЕТ");
        }
    }
}
