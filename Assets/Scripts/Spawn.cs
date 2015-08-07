using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject snowball;

	public GameObject latestSnowball;

	void Start () {
		spawnSnowball();
	}

	public void trySpawnSnowball () {
		if(latestSnowball.GetComponent<SnowballController>().isConnected()) {
			spawnSnowball();
		}
	}

	public void spawnSnowball() {

		latestSnowball = (GameObject) Instantiate(this.snowball);
		latestSnowball.transform.position = this.transform.position;

		foreach(FollowSnowball followSnowball in GameObject.FindObjectsOfType<FollowSnowball>() ) {
			followSnowball.target = latestSnowball.transform;
		}
	}
}
