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
        latestSnowball.transform.position = new Vector3(0, 60, 0);

        SnowballController snowballController = latestSnowball.GetComponent<SnowballController>();
        if(snowballSpawnSize > 0) { 
            snowballController.startSize = snowballSpawnSize;
            snowballController.toSnowball();
        }

        foreach (SnowCamera followSnowball in GameObject.FindObjectsOfType<SnowCamera>())
        {
            followSnowball.player = latestSnowball.transform;
        }

        foreach (DrawTracks snowSurface in GameObject.FindObjectsOfType<DrawTracks>())
        {
            snowSurface._snowball = latestSnowball.transform;
        }

    }
}
