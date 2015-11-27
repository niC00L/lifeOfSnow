using UnityEngine;

public class Item : MonoBehaviour
{

    public int itemID = 0;
    public float pickupDistance = 5.0f;

    private Spawn spawn;

    void Start()
    {
        spawn = GameObject.Find("Spawn").GetComponent<Spawn>();
    }

    void OnMouseDown()
    {

        if (itemID <= 0)
        {
            return;
        }

        Debug.Log("Pick item");

        Vector3 snowballPosition = spawn.latestSnowball.transform.position;

        if ((snowballPosition - transform.position).magnitude < pickupDistance)
        {

            SnowballInventory inv = spawn.latestSnowball.GetComponent<SnowballInventory>();

            if (inv.canAddItem(itemID))
            {

                inv.addItem(itemID);

                Destroy(this.gameObject);

            }
            else
            {
                //TODO
            }
        }
        else
        {
            //TODO
        }
    }
}
