using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	
	public GameObject obj;
	public float rangeX = 0;
	public float rangeY = 0;
	public float rangeZ = 0;
	public int count = 1;
	public bool repeat = false;
	
	void Start () {
		spawn();
	}

	void FixedUpdate() {
		if (repeat) {
			spawn ();
		}
	}

	private void spawn() {
		for (int i=0; i<this.count; i++) {
			
			Vector3 pos = new Vector3(
				(Random.value * 2*this.rangeX) -this.rangeX,
				(Random.value * 2*this.rangeY) -this.rangeY,
				(Random.value * 2*this.rangeZ) -this.rangeZ
				);
			
			pos = this.transform.position + pos;
			
			GameObject spawned = (GameObject) Instantiate(obj);
			spawned.transform.parent = this.transform;
			spawned.transform.position = pos;
		}
	}
}
