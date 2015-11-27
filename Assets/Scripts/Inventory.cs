using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public GameObject[] itemsIds;

    private Camera inventoryCamera;
    private Transform inventoryContent;
    private Spawn spawn;
    private SnowballInventory activeInventory;

    void Start ()
    {
        inventoryCamera = GameObject.Find("InventoryCamera").GetComponent<Camera>();
        spawn = FindObjectOfType<Spawn>();
        inventoryContent = GameObject.Find("InventoryContent").transform;
    }
    
	void Update ()
    {

		if(Input.GetKeyDown(KeyCode.I)) {
			if(inventoryCamera.enabled) {
                inventoryCamera.enabled = false;
			} else {
                inventoryCamera.enabled = true;
			}
		}

	}

    public void updateInventory()
    {
        //clear
        var children = new List<GameObject>();
        foreach (Transform child in inventoryContent) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        //put new items
        if (spawn.latestSnowball) {
            activeInventory = spawn.latestSnowball.GetComponent<SnowballInventory>();
            for (int i = 0; i < activeInventory.getInventorySize(); i++)
            {
                updateItem(i, activeInventory.getItem(i));
            }
        }
    }

    public void updateItem(int slot, int itemId)
    {
        if(itemId > 0) {
            Vector3 position = getPositionForSlot(slot);
            GameObject item = Instantiate(itemsIds[itemId]);
            item.transform.parent = inventoryContent;
            item.transform.position = position;
            item.layer = 8;
        }
    }

    public Vector3 getPositionForSlot(int slot)
    {
        return new Vector3(slot * 0.5f, 0, 5);
    }
}
