using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    //public event Action<Item> OnRightClickEvent;

    public delegate void OnInventorySlotChanged(Item item);
    public OnInventorySlotChanged onInventorySlotChangedCallback;

    public Image icon;
	
	public Item item;
	
	public void AddItem(Item newItem)
	{
		item = newItem;
		
		icon.sprite = item.icon;
		icon.enabled = true;
	}
	
	public void ClearSlot()
	{
		item = null;
		
		icon.sprite = null;
		icon.enabled = false;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            if (item != null && onInventorySlotChangedCallback != null)
                onInventorySlotChangedCallback.Invoke(item);
                Debug.Log("EVENT IS WORKING");
    }
}
