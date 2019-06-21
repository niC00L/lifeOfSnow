using UnityEngine;
using System.Collections;

public class FollowSimple : MonoBehaviour {
	
	public Transform target;
	public float distance = 1.0f;
	public float height = 1.0f;

	void Start () {
		
		if (!target)
			return;

	}
	
	void Update () {
		
		if (!target)
			return;

		float wantedHeight = target.position.y + height;		
		
		Vector3 pos = target.position;
		pos -= transform.TransformDirection(Vector3.forward) * distance;
		pos.y = wantedHeight;
		
		transform.position = pos;
	}
}
