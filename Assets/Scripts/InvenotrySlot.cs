using UnityEngine;
using System.Collections;

public class InvenotrySlot : MonoBehaviour {

    private Inventory inventory;
    private PutOnMe putOnMe;
    public int slot;

	void Start () {
        inventory = GameObject.FindObjectOfType<Inventory>();
        putOnMe = GameObject.FindObjectOfType<PutOnMe>();
    }
	
	public void OnMouseDown()
    {
        GameObject item = inventory.getRealItem(slot);
        if (item != null) {
            inventory.close();
            putOnMe.putOnMe(item);
            inventory.removeItemSlot(slot);
        }
    }
}
