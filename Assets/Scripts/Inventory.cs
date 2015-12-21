using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public GameObject[] itemsIds;
    public GameObject slotObject;

    private Camera inventoryCamera;
    private Transform inventoryContent;

    private SnowballInventory activeInventory;
    private int inventoryCols;
    private int inventoryRows;

    void Awake()
    {
        inventoryCamera = GameObject.Find("InventoryCamera").GetComponent<Camera>();
        inventoryContent = GameObject.Find("InventoryContent").transform;
    }
    
    /* Already in snowball controller
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
    */

    public void close()
    {
        inventoryCamera.enabled = false;
    }

    public void open()
    {
        inventoryCamera.enabled = true;
    }

    public void toggle()
    {
        if (inventoryCamera.enabled)
        {
            close();
        } else
        {
            open();
        }
    }

    public void updateInventory(SnowballInventory snowballInventory)
    {
        //clear
        activeInventory = snowballInventory;
        inventoryRows = 0;
        inventoryCols = 0;
        for (int i = inventoryContent.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(inventoryContent.GetChild(i).gameObject);
        }
        
        if (activeInventory)
        {
            calculateInvenotryRowsCols();

            //put new items
            for (int i = 0; i < activeInventory.getInventorySize(); i++)
            {
                updateSlot(i);
                updateItem(i, activeInventory.getItem(i));
            }

            //move invenotry camera
            inventoryCamera.transform.position = getPositionForCamera();
        }
    }

    private void updateItem(int slot, int itemId)
    {
        if(itemId > 0) {
            //item preview
            Vector3 pos = getPositionForSlot(slot);
            GameObject previewItem = Instantiate(itemsIds[itemId]);
            previewItem.transform.parent = inventoryContent;
            previewItem.transform.position = pos + new Vector3(0, 0, -4);
            previewItem.layer = 8;
        }
    }

    private void updateSlot(int slot)
    {
        Vector3 pos = getPositionForSlot(slot);
        GameObject newSlot = Instantiate(slotObject);
        newSlot.transform.parent = inventoryContent;
        newSlot.transform.position = pos;
        newSlot.name = "InventorySlot" + slot;
        newSlot.GetComponent<InvenotrySlot>().slot = slot;
    }

    protected Vector3 getPositionForSlot(int slot)
    {
        int row = slot / inventoryCols;
        int col = slot % inventoryCols;
        return new Vector3(0.55f + col * 1.1f, -0.55f + row * -1.1f, 8);
    }

    protected Vector3 getPositionForCamera()
    {
        return new Vector3(inventoryCols * 0.55f, inventoryRows * -0.55f, 0);
    }

    protected void calculateInvenotryRowsCols()
    {
        int s = activeInventory.getInventorySize();

        int ms = Mathf.FloorToInt(Mathf.Sqrt(s));
        
        this.inventoryRows = s / ms;
        this.inventoryCols = ms;
    }

    public SnowballInventory getActiveInventory()
    {
        return activeInventory;
    }

    public int getItem(int slot)
    {
        return activeInventory.getItem(slot);
    }

    public GameObject getRealItem(int slot)
    {
        return itemsIds[activeInventory.getItem(slot)];
    }

    public void removeItemSlot(int slot)
    {
        activeInventory.removeItemSlot(slot);
    }
}
