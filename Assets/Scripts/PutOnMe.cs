using UnityEngine;
using System.Collections;

public class PutOnMe : MonoBehaviour {

    private Transform mainCamera;
    private Spawn spawn;
    
    private SnowballController puttingOn;
    private Transform puttingObject;

	void Start () {
        mainCamera = GameObject.Find("Main Camera").transform;
        spawn = FindObjectOfType<Spawn>();
    }
	
	void Update () {
	    if(puttingObject != null)
        {
            Vector3 snowballPos = puttingOn.transform.position;
            Vector3 toCamera = (mainCamera.position - snowballPos).normalized;
            puttingObject.transform.position = snowballPos + toCamera * puttingOn.size;
            puttingObject.transform.LookAt(snowballPos);
        }
	}

    public void putOnMe(Transform item)
    {
        puttingOn = spawn.latestSnowball.GetComponent<SnowballController>();
        puttingObject = item;
    }
}
