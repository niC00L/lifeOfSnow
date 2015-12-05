using UnityEngine;
using System.Collections;

public class InvenotrySlot : MonoBehaviour {

    private Inventory inventory;
    private PutOnMe putOnMe;

	void Start () {
        inventory = GameObject.FindObjectOfType<Inventory>();
        putOnMe = GameObject.FindObjectOfType<PutOnMe>();
    }
	
	public void OnMouseDown()
    {
        Debug.Log("CLICK SLOT");
        Transform item = transform.GetChild(0);
        if (item != null) { 
            inventory.close();
            putOnMe.putOnMe(item);
            item.gameObject.layer = 0;
        }
    }
}
