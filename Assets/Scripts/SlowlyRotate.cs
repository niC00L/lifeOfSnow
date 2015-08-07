using UnityEngine;
using System.Collections;

public class SlowlyRotate : MonoBehaviour {

	public float speedX = 0;
	public float speedY = 0;
	public float speedZ = 0;

	void FixedUpdate () {

		transform.Rotate (speedX, speedY, speedZ);

	}
}
