using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public GameObject snowball;
    public GameObject latestSnowball;
    public float snowballSpawnSize = 0.0f;
    private static Spawn instance;
    public Text youWinText;


    public static Spawn Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Spawn();
            }

            return instance;
        }
    }

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

    }

    public void Win()
    {
        Time.timeScale = 0.25f;

        youWinText = GameObject.Find("You Win Text").GetComponent<Text>();
        youWinText.text = "You have succesfully built a snowman!  Congratulations!";
    }

}
