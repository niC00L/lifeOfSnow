using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject snowball;
    public GameObject latestSnowball;
    public float snowballSpawnSize = 0.0f;
    private Inventory inventory;
    
    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Start()
    {
        spawnSnowball();
    }

    public void trySpawnSnowball()
    {
        if (latestSnowball.GetComponent<SnowballController>().isConnected())
        {
            spawnSnowball();
        }
    }

    public void spawnSnowball()
    {

        latestSnowball = (GameObject)Instantiate(this.snowball);
        latestSnowball.transform.position = this.transform.position;

        SnowballController snowballController = latestSnowball.GetComponent<SnowballController>();
        if(snowballSpawnSize > 0) { 
            snowballController.startSize = snowballSpawnSize;
            snowballController.toSnowball();
        }

        foreach (ThirdPersonOrbitCamBasic followSnowball in GameObject.FindObjectsOfType<ThirdPersonOrbitCamBasic>())
        {
            followSnowball.player = latestSnowball.transform;
        }

        inventory.updateInventory(latestSnowball.GetComponent<SnowballInventory>());
    }
}
