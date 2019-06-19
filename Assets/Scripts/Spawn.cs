using System.Collections;
using UnityEngine;
using Wacki.IndentSurface;

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
        latestSnowball.transform.position = new Vector3(0, 50, 0);

        SnowballController snowballController = latestSnowball.GetComponent<SnowballController>();
        if(snowballSpawnSize > 0) { 
            snowballController.startSize = snowballSpawnSize;
            snowballController.toSnowball();
        }

        foreach (SnowCamera followSnowball in GameObject.FindObjectsOfType<SnowCamera>())
        {
            followSnowball.player = latestSnowball.transform;
        }

        foreach (DrawWithMouse snowSurface in GameObject.FindObjectsOfType<DrawWithMouse>())
        {
            snowSurface._snowball = latestSnowball.transform;
        }

        inventory.updateInventory(latestSnowball.GetComponent<SnowballInventory>());
    }
}
