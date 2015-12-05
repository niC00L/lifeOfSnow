using UnityEngine;
using System.Collections;

public class SnowballInventory : MonoBehaviour {

    public int invSize = 9;
    public int[] items;

    private Inventory inventory;

    void Awake() {
        items = new int[invSize];

        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    public bool canAddItem(int itemID)
    {

        for (int i = 0; i < invSize; i++)
        {
            if (items[i] == 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool addItem(int itemID)
    {

        for (int i = 0; i < invSize; i++)
        {
            if (items[i] == 0)
            {
                items[i] = itemID;

                updateInv();

                return true;
            }
        }

        return false;
    }

    public bool hasItem(int itemID)
    {

        for (int i = 0; i < invSize; i++)
        {
            if (items[i] == itemID)
            {
                return true;
            }
        }

        return false;
    }

    public bool removeItemType(int itemID)
    {

        for (int i = 0; i < invSize; i++)
        {
            if (items[i] == itemID)
            {
                items[i] = 0;

                updateInv();

                return true;
            }
        }

        return false;
    }

    public void removeItemSlot(int slot)
    {
        items[slot] = 0;
        updateInv();
    }

    public int getInventorySize()
    {
        return invSize;
    }

    public int getItem(int slotId)
    {
        if (slotId >= 0 && slotId < invSize) {
            return items[slotId];
        }
        return 0;
    }

    private void updateInv()
    {
        if (inventory.getActiveInventory() == this)
        {
            inventory.updateInventory(this);
        }
    }
}
