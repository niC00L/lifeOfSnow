using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public int itemID = 0;
	
	private Spawn spawn;

	void Start () {

		spawn = GameObject.Find ("Spawn").GetComponent<Spawn> ();

	}
	
	void OnMouseDown() {

		if(itemID <= 0) {
			return;
		}

		Debug.Log ("Pick item");

		Inventory inv = spawn.latestSnowball.GetComponent<Inventory> ();

		if(inv.canAddItem(itemID)) {

			inv.addItem(itemID);

			Destroy(this.gameObject);

		} else {

		}
	}
}
