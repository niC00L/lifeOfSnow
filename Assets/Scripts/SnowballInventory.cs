using UnityEngine;
using System.Collections;

public class SnowballInventory : MonoBehaviour {

    public int invSize = 9;
    public int[] items;

    private Inventory inventory;

    void Start () {
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

                inventory.updateInventory();

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

                inventory.updateInventory();

                return true;
            }
        }

        return false;
    }

    public void removeItemSlot(int slot)
    {
        items[slot] = 0;
        inventory.updateInventory();
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
}
