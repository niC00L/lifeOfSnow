using UnityEngine;
using System.Collections;

public class PutOnMe : MonoBehaviour {

    private Transform mainCamera;
    private Spawn spawn;
    
    private SnowballController puttingOn;
    private Transform puttingObject;

	void Awake() {
        mainCamera = GameObject.Find("Main Camera").transform;
        spawn = FindObjectOfType<Spawn>();
    }
	
	void Update () {
	    if(puttingObject != null)
        {
            Vector3 snowballPos = puttingOn.transform.position;
            Vector3 toCamera = (mainCamera.position - snowballPos).normalized;
            puttingObject.transform.position = snowballPos + toCamera * puttingOn.size/2;
            puttingObject.transform.LookAt(snowballPos);

            if(Input.GetKeyDown(KeyCode.Space))
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
        item.transform.localPosition = Vector3.zero;
        puttingOn = spawn.latestSnowball.GetComponent<SnowballController>();
        if (item.name == "Pot")
        {
            item.transform.localPosition = new Vector3(0.05f, 0.0f, 0.28f);
            //0.05, 0, 0.28
            //item.transform.localPosition = new Vector3(0.1f, 0.1f, 0.2f);
            puttingObject = item.transform;

        }

        item.SetActive(true);
    }

    public void stopPutOnMe()
    {
        puttingObject = null;
    }
}
