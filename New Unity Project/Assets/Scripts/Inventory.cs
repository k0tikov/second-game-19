#pragma warning disable 649
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
	#region Singleton
	
	public static Inventory instance;
	
	void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found!");
            return;


        }
		
		instance = this;
	}
	
	#endregion
	
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

    public event Action<InventorySlot> OnPointerEnterEvent;
    public event Action<InventorySlot> OnPointerExitEvent;
    public event Action<InventorySlot> OnRightClickEvent;
    public event Action<InventorySlot> OnBeginDragEvent;
    public event Action<InventorySlot> OnEndDragEvent;
    public event Action<InventorySlot> OnDragEvent;
    public event Action<InventorySlot> OnDropEvent;

    //[SerializeField] int space = 20;

    public List<Item> items = new List<Item>();

    EquipmentManager equipmentManager;
    ItemTooltip itemTooltip;

    [SerializeField] Transform itemsParent;
    [SerializeField] GameObject inventoryUI;

    InventorySlot[] slots;

    [SerializeField] Image draggableItem;
    [SerializeField] InventorySlot draggedSlot;

    public void Start()
    {
        equipmentManager = EquipmentManager.instance;
        itemTooltip = ItemTooltip.instance;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        // Right click
        OnRightClickEvent += equipmentManager.InventoryRightClick;
        // Pointer enter
        OnPointerEnterEvent += itemTooltip.ShowTooltip;
        // Pointer exit
        OnPointerExitEvent += itemTooltip.HideTooltip;
        // Begin drag
        OnBeginDragEvent += OnBeginDrag;
        // End drag
        OnEndDragEvent += OnEndDrag;
        // Drag
        OnDragEvent += Drag;
        // Drop
        OnDropEvent += Drop;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            slots[i].OnPointerExitEvent += OnPointerExitEvent;
            slots[i].OnRightClickEvent += OnRightClickEvent;
            slots[i].OnBeginDragEvent += OnBeginDragEvent;
            slots[i].OnEndDragEvent += OnEndDragEvent;
            slots[i].OnDragEvent += OnDragEvent;
            slots[i].OnDropEvent += OnDropEvent;
        }
        // устанавливаем стартовый инвентарь
        SetStartingInventory();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    //add item to list
    public bool Add (Item item)
	{
        if (IsFull())
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(item);
                    return true;
                }
            }
        }
        return false;
	}
	
	public bool Remove(Item item)
	{
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                // очищаем слот
                slots[i].ClearSlot();
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }
        return false;
    }

    void SetStartingInventory()
    {
        // устанавливаем стартовый инвентарь
        // если в стартовом инвентаре были предметы - ставим их
        for (int i = 0; i < items.Count && i < slots.Length; i++)
        {
            Add(items[i]);
        }
        // если нет - пустой слот
        for (int i = 0; i < slots.Length; i++)
        {
            //slots[i].item = null;
        }
    }

    public void OnBeginDrag(InventorySlot slot)
    {
        if (slot.item != null)
        {        
            draggedSlot = slot;
            draggableItem.sprite = slot.item.icon;
            draggableItem.enabled = true;
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(InventorySlot slot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    public void Drag(InventorySlot slot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
            
    }

    public void Drop(InventorySlot dropInventorySlot)
    {
        if (draggedSlot != null)
        {
            if (dropInventorySlot.CanReceiveItem(draggedSlot.item) && draggedSlot.CanReceiveItem(dropInventorySlot.item))
            {
                Equipment dragItem = draggedSlot.item as Equipment;
                Equipment dropItem = dropInventorySlot.item as Equipment;
                int slotIndex;
                // если тащим в панель с эквипом
                if (dropInventorySlot is EquipSlot)
                {
                    // значение в массиве поулчает значение итема, который бросаем
                    slotIndex = (int)dragItem.equipSlot;
                    equipmentManager.currentEquipment[slotIndex] = dragItem;

                    if (equipmentManager.onEquipmentChanged != null)
                        equipmentManager.onEquipmentChanged.Invoke(dragItem, dropItem);
                }
                // если тащим из панели с эквипом
                if(draggedSlot is EquipSlot)
                {
                    // обнуляем его значение в массиве
                    slotIndex = (int)dragItem.equipSlot;
                    equipmentManager.currentEquipment[slotIndex] = null;
                    // обновляем статы
                    if (equipmentManager.onEquipmentChanged != null)
                        equipmentManager.onEquipmentChanged.Invoke(dropItem, dragItem);
                }

                Item draggedItem = draggedSlot.item;

                draggedSlot.item = dropInventorySlot.item;
                draggedSlot.icon.sprite = dropInventorySlot.icon.sprite;
                dropInventorySlot.icon.sprite = draggedItem.icon;
                dropInventorySlot.item = draggedItem;
            }
        }
    }
}
