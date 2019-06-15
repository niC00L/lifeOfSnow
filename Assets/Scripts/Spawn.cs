using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject snowball;
    public GameObject latestSnowball;
    public float snowballSpawnSize = 0.0f;
    
    void Awake()
    {
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

        foreach (FollowSnowball followSnowball in GameObject.FindObjectsOfType<FollowSnowball>())
        {
            followSnowball.target = latestSnowball.transform;
        }

    }
}
