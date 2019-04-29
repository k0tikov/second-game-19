using UnityEngine;

public class Interactable : MonoBehaviour
{

	public float radius = 3f;
	
	
	void OnDrawGizmosSelected(){
		//if (interactionTransform == null){
		//	interactionTransform = transform;
		//}
		Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
	}

}
