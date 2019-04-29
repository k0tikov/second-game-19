using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	Camera cam;
	PlayerMotor motor;
	public Interactable focus;
	
    void Start()
    {
        cam = Camera.main;	
		motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
		
		// If we pressed left mouse botton
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			//get point of click
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit, 100))
			{
				// Move to where we hit
				motor.MoveToPoint(hit.point);
				
				RemoveFocus();
			}
		}
		
		// If we pressed right mouse botton
		if(Input.GetMouseButtonDown(1)){
			RaycastHit hit;
			Debug.Log("we are here");
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit, 100))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				
				if(interactable != null)
				{
					SetFocus(interactable);
				}
				
			}
		}
		
		void SetFocus(Interactable newFocus)
		{
			focus = newFocus;
		}
        void RemoveFocus()
		{
			focus = null;
		}
    }
}
