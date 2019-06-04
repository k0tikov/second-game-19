﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public int space = 20;
	
	public List<Item> items = new List<Item>();
	
    //add item to list
	public bool Add (Item item)
	{	
		if (!item.isDefaultItem)
		{
            //check if there are any slots in the inventory
			if (items.Count >= space)
			{
				Debug.Log("Not enough room");
				return false;
			}
			items.Add(item);
			
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
		return true;
	}
	
	public void Remove(Item item)
	{
		items.Remove(item);
					
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
            Debug.Log("ITS WOOOOOOOOOOORKIONGGGGG");
	}
}
