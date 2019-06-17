using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Item: MonoBehaviour {
    public int id;
    public string title;
    public string description;
    public Sprite icon1;
    public Sprite icon2;
    public GameObject instance;

    public float pickupDistance = 50.0f;
    private Spawn spawn;


    public Item(int id, string title, string description)//, GameObject instance)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon1 = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.icon2 = Resources.Load<Sprite>("Sprites/SelectedItems/" + title);
        //this.instance = GameObject.Find(title);
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon1 = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.icon2 = Resources.Load<Sprite>("Sprites/SelectedItems/" + title);
        //this.instance = GameObject.Find(title);
    }


    void Start()
    {
        spawn = GameObject.Find("Spawn").GetComponent<Spawn>();
        instance = GameObject.Find(title);

    }
    void Update()
    {
        Vector3 snowballPosition = spawn.latestSnowball.transform.position;
        
        if ((snowballPosition - instance.transform.position).magnitude < pickupDistance)
        {
            Inventory inv = GameObject.Find("Player").GetComponent<Inventory>();

            inv.GiveItem(this);
            instance.SetActive(false);
            //Destroy(v);
        }
    }
}
