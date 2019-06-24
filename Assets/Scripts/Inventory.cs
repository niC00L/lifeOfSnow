using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> characterItems = new List<Item>();
    public UIInventory inventoryUI = new UIInventory();

    void Start()
    {
    }
    void Update()
    {
        var spawn = FindObjectOfType<Spawn>();
        var snowball = spawn.latestSnowball.GetComponent<SnowballController>();
        //var insideSnowBallForScale = snowball.transform.GetChild(1);
        //items on snowball
        for (int i = 2; i < snowball.transform.childCount; i++)
        {
            var child = snowball.transform.GetChild(i);
            if (child.name == "Pot")
            {
                child.transform.localScale = new Vector3(snowball.size/2.5f, snowball.size/2.5f, snowball.size/2.5f);
                //child.transform.localScale = snowball.transform.localScale;

                /*if (snowball.size <= 1.85)
                {
                    child.transform.localPosition = new Vector3(0, 1, 0) * snowball.size / 2.4f;
                }
                else
                {*/
                    child.transform.localPosition = new Vector3(0, 1, 0) * snowball.size / 2.8f;

                //}
                

            }

            if (child.name == "Carrot")
            {
                //child.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                //child.transform.localScale = snowball.transform.localScale;
                child.transform.localScale = new Vector3(0.0846585f*snowball.size / 2.0f, 0.111269f * snowball.size / 2.0f, 0.687029f * snowball.size / 2.0f);
                //cim vacsa gula, tym dalej je mrkva, preto 2.4
                //if (snowball.size > 1.15)
                //{
                    child.transform.localPosition = new Vector3(1, 0, 0) * snowball.size / 2.4f;

                //}
            }
        }
        /*
        if (this.selected)
        {
            var spawn = FindObjectOfType<Spawn>();
            var snowball = spawn.latestSnowball.GetComponent<SnowballController>();
            snowball.transform.chi
            if (item.name == "Pot")
            {
                item.transform.localPosition = new Vector3(0, 1, 0) * snowball.size / 2;


            }
            if (item.name == "Carrot")
            {
                item.transform.localPosition = new Vector3(1, 0, 0) * snowball.size / 3;
            }
        }*/
    }
    public void GiveItem(Item item)
    {
        characterItems.Add(item);
        inventoryUI.AddNewItem(item);
        Debug.Log("Added item: " + item.title);
    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Removed item: " + itemToRemove.title);
        }
    }
}
