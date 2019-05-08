using UnityEngine;

public class ItemPickup : Interactable
{
	public override void Interact()
	{
		base.Interact();
		PickUp();
	}
	
	
	void PickUp()
	{
		Debug.Log("picking up");
		//add item to inventory
		Destroy(gameObject);
	}
}
