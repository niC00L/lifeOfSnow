using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public int invSize = 9;

	public GameObject[] itemsIds;
	public int[] inventory;

	private bool showInv;
	private Rect guiRect;
	private GUIContent invGuiContent;
	private int invGuiWidth = 200;
	private int invGuiHeight = 200;

	void Start() {
		inventory = new int[invSize];

		float cx = Camera.main.pixelWidth / 2;
		float cy = Camera.main.pixelHeight / 2;

		guiRect = new Rect(Mathf.Round(cx - invGuiWidth / 2), Mathf.Round(cy - invGuiHeight / 2), invGuiWidth, invGuiHeight);
	
	
		invGuiContent = new GUIContent ("Inventory");
	}

	public bool canAddItem(int itemID) {

		for(int i=0; i<invSize; i++) {
			if(inventory[i] == 0) {
				return true;
			}
		}

		return false;
	}

	public bool addItem(int itemID) {

		for(int i=0; i<invSize; i++) {
			if(inventory[i] == 0) {
				inventory[i] = itemID;
				return true;
			}
		}

		return false;
	}

	public bool hasItem(int itemID) {
		
		for(int i=0; i<invSize; i++) {
			if(inventory[i] == itemID) {
				return true;
			}
		}

		return false;
	}

	public bool removeItemType(int itemID) {
		
		for(int i=0; i<invSize; i++) {
			if(inventory[i] == itemID) {
				inventory[i] = 0;
				return true;
			}
		}
		
		return false;
	}

	public void removeItemSlot(int slot) {
		
		inventory[slot] = 0;
	}

	//GUI

	void Update () {

		if(Input.GetKeyDown(KeyCode.I)) {
			if(showInv) {
				showInv = false;
			} else {
				showInv = true;
			}
		}

	}

	void OnGUI () {

		if(showInv) {
			
			GUI.Window(1, guiRect, drawInvGUI, invGuiContent);

		}

	}

	protected void drawInvGUI(int id) {

		for(int i=0; i<invSize; i++) {
			if(GUI.Button(getGuiSlotLocation(i), "" + inventory[i])) {

				int dropid = inventory[i];

				removeItemSlot(i);
				
				Debug.Log("Drop "+inventory[i]);

				if(itemsIds[dropid] != null) {
					Instantiate(itemsIds[dropid], transform.position, transform.rotation);
				}
			}
		}
	}

	private Rect getGuiSlotLocation(int slot) {

		int row = slot / 3;
		int col = slot - (row * 3);

		return new Rect (10 + col * 60, 10 + row * 60, 60, 60);
	}
}
