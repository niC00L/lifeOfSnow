using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public GameObject[] itemsIds;
    public GameObject slotObject;

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
                close();
			} else {
                open();
			}
		}

	}

    public void close()
    {
        inventoryCamera.enabled = false;
    }

    public void open()
    {
        inventoryCamera.enabled = true;
    }

    public void updateInventory()
    {
        //clear
        var children = new List<GameObject>();
        foreach (Transform child in inventoryContent) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        
        if (spawn.latestSnowball)
        {
            activeInventory = spawn.latestSnowball.GetComponent<SnowballInventory>();
            //add slots
            for (int i = 0; i < activeInventory.getInventorySize(); i++)
            {
                updateSlot(i);
            }
            
            //put new items
            for (int i = 0; i < activeInventory.getInventorySize(); i++)
            {
                updateItem(i, activeInventory.getItem(i));
            }
        }
    }

    private void updateItem(int slot, int itemId)
    {
        if(itemId > 0) {
            GameObject slotObject = GameObject.Find("InventorySlot" + slot);
            GameObject item = Instantiate(itemsIds[itemId]);
            item.transform.parent = slotObject.transform;
            item.transform.position = slotObject.transform.position + new Vector3(0, 0, -4);
            item.layer = 8;
        }
    }

    private void updateSlot(int slot)
    {
        Vector3 pos = getPositionForSlot(slot);
        GameObject newSlot = Instantiate(slotObject);
        newSlot.transform.parent = transform;
        newSlot.transform.position = pos;
        newSlot.name = "InventorySlot" + slot;
    }

    protected Vector3 getPositionForSlot(int slot)
    {
        int row = slot / 3;
        int col = slot % 3;
        return new Vector3(0.505f + col * 1.1f, -0.505f + row * -1.1f, 8);
    }
}
