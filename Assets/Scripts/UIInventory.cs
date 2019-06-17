using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;

    void Awake()
    {
        for(int i = 0; i < 24; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uiItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i=> i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i=> i.item == item), null);
    }

    public void RemoveSelectedItems()
    {
        //var selecteditems = uiItems.Where(item => item.item != null && item.spriteImage == item.item.icon2).ToList();
        var selecteditems = uiItems.Where(item => item.selected).ToList();

        selecteditems.ForEach(item =>
        {
            this.RemoveItem(item.item);
        });
    }


    /*
    private void ToggleSelectItem()
    {
        if (slotPrefab.GetComponent<MeshRenderer>().material.color == Color.green)
        {
            slotPrefab.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            slotPrefab.GetComponent<MeshRenderer>().material.color = Color.green;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ToggleSelectItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            tooltip.GenerateTooltip(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }*/
}
