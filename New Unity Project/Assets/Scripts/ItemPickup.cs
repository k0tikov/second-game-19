using UnityEngine;

public class ItemPickup : Interactable
{
	public Item item;
	
	
	public override void Interact()
	{
		base.Interact();
		PickUp();
	}
	
	void PickUp()
	{
		Debug.Log("picking up" + item.name);
		// Add item to inventory
		bool wasPickedUp = Inventory.instance.Add(item);
		// Destroy item if its picked up
		if (wasPickedUp)
			Destroy(gameObject);
	}
}
