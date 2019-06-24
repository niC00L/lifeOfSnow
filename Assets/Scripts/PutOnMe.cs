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
            if (puttingObject.name == "Carrot")
            {
                //puttingObject.transform.position = snowballPos + new Vector3(1, 0, 0) * puttingOn.size / 2;
            }
            else
            {
                //puttingObject.transform.position = snowballPos + new Vector3(0, 1, 0) * puttingOn.size / 2;
                //puttingObject.transform.localPosition = new Vector3(0, 1, 0) * puttingOn.size / 2;
            }

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
        puttingOn = spawn.latestSnowball.GetComponent<SnowballController>();

        if (item.name == "Pot")
        {
            item.transform.localPosition = new Vector3(0.05f, 0.0f, 0.28f);
            /*if (puttingOn.size <= 1.85)
            {
                item.transform.localPosition = new Vector3(0, 1, 0) * puttingOn.size / 1.5f;
            }
            else
            {*/
                item.transform.localPosition = new Vector3(0, 1, 0) * puttingOn.size / 2;
                    
            //}

            //0.05, 0, 0.28
            //item.transform.localPosition = new Vector3(0.1f, 0.1f, 0.2f);
            puttingObject = item.transform;

        }
         //hotfix teleproting of the pot - carrot goes to the UItem with the same PutOnMe object(first slot) and it uses puttingObject of pot, so I set it to null
        if (item.name == "Carrot")
        {
           /*if (puttingOn.size <= 1.15)
            {
                item.transform.localPosition = new Vector3(1, 0, 0) * puttingOn.size / 100.4f;
            }
            else
            {*/
                item.transform.localPosition = new Vector3(1, 0, 0) * puttingOn.size / 2.4f;

            //}
            item.transform.eulerAngles = new Vector3(
                0, 90, 0 
            );
            Vector3 snowballPos = puttingOn.transform.position;

            puttingObject = item.transform;
        }

        item.SetActive(true);
    }

    public void stopPutOnMe()
    {
        puttingObject = null;
    }
}
