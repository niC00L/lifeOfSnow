using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;
    private PutOnMe putOnMe;
    public bool selected = false;

    
    void Awake()
    {
        putOnMe = GameObject.FindObjectOfType<PutOnMe>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon1;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    private void ToggleSelectItem()
    {
        if (!selected)
        {
            //not selected -> selected and put on
            spriteImage.sprite = item.icon2;
            //no pickin up now
            item.pickupDistance = float.MinValue;
            //this.item.instance.SetActive(true);
            this.selected = true;
            putOnMe.putOnMe(item.instance);
}
        else
        {
            this.selected = false;
            //deselect
            spriteImage.sprite = item.icon1;
            this.item.instance.SetActive(false);
        }
        /*if (spriteImage.color == Color.green)
        {
            spriteImage.color = Color.yellow;
            transform.parent.items
            //transform.parent.GetComponent<Image>().material.color = Color.yellow;
        }
        else
        {
            spriteImage.color = Color.green;
            transform.parent.GetComponent<Image>().material.color = Color.green;
        }*/
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
    }
}
