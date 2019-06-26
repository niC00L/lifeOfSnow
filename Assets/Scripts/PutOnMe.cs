using UnityEngine;
using System.Collections;

public class PutOnMe : MonoBehaviour
{

    private Transform mainCamera;
    private Spawn spawn;

    private SnowballController puttingOn;
    private Transform puttingObject;

    void Start()
    {
       
            mainCamera = GameObject.Find("Main Camera").transform;
            spawn = FindObjectOfType<Spawn>();
        
    }
    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
        spawn = FindObjectOfType<Spawn>();
    }

    void Update()
    {
        if (puttingObject != null)
        {
            Vector3 snowballPos = puttingOn.transform.position;
            Vector3 toCamera = (mainCamera.position - snowballPos).normalized;


            puttingObject.transform.LookAt(snowballPos);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                stopPutOnMe();
            }
        }
    }

    /*public void putOnMe(Transform item)
    {
        puttingOn = spawn.latestSnowball.GetComponent<SnowballController>();
        puttingObject = item;
    }*/

    public void putOnMe(GameObject item)
    {
        item.layer = 0;
        item.transform.parent = spawn.latestSnowball.transform;
        puttingOn = spawn.latestSnowball.GetComponent<SnowballController>();

        if (item.name == "Pot" || item.name == "Bottom button" || item.name == "Middle button")
        {
            //item.transform.localPosition = new Vector3(0.05f, 0.0f, 0.28f);

            item.transform.localPosition = new Vector3(0, 1, 0.2f) * puttingOn.size / 2;
            puttingObject = item.transform;

        }



        //eyes 08,05,02
        /*else if (item.name == "button.000 (1)")
        {
            //item.transform.localPosition = new Vector3(0.05f, 0.0f, 0.28f);

            item.transform.localPosition = new Vector3(0.8f, 0.5f, 0.02f) * puttingOn.size / 2;
            puttingObject = item.transform;

        }
        else if (item.name == "button.000 (4)")
        {
            item.transform.localPosition = new Vector3(0.8f, 0.5f, -0.3f) * puttingOn.size / 2;
            puttingObject = item.transform;
        }*/

        else if (item.name == "Hat")
        {
            item.transform.localPosition = new Vector3(0.05f, 0.0f, 0.28f);

            item.transform.localPosition = new Vector3(0, 1, 0) * puttingOn.size / 2;
            item.transform.localEulerAngles = new Vector3(
                0, 0, 0
            );
            puttingObject = item.transform;

        }

        else if (item.name == "Carrot")
        {
            item.transform.localPosition = new Vector3(1, 0, 0) * puttingOn.size / 2.4f;
            item.transform.eulerAngles = new Vector3(
                -20, 0, 0
            );
            Vector3 snowballPos = puttingOn.transform.position;

            puttingObject = item.transform;
        }

        /*else if (item.name == "pipe")
        {
            
            item.transform.localPosition = Vector3.zero;
            //item.transform.localPosition = new Vector3(0.1f, -0.2f, 0) * puttingOn.size*5;
            Vector3 snowballPos = puttingOn.transform.position;
            
            item.transform.localRotation = Quaternion.Euler(new Vector3(10, 0, 0));
            
            puttingObject = item.transform;
        }*/


        item.SetActive(true);
    }

    public void stopPutOnMe()
    {
        puttingObject = null;
    }
}
