using UnityEngine;
using System.Collections.Generic;

public class SnowballController : MonoBehaviour {
	
	public float sizeIncreaseSpeed = 1.0f;

	public float size = 0;
	public float startSize = 0.04f;
	public float snowflakeFallSpeed = 0.1f;
    public float moveSpeed = 1.0f;
	public float jumpAllowDistance = 0.4f;

    public float connectRepeatTime = 1.0f;
    public bool isOnGround = false;

	private bool landed;

	private bool connected;
	private float connectTime = -1.0f;
	private bool wantConnectChange;

	void Start () {
		updateSize();

		GetComponent<Rigidbody>().useGravity = false; //because snowflake have own gravity skills, she doesnt need this
        //isOnGround = false;
    }

	void Update () {
		//disconnect
		if(connected) {
			if(wantConnectChange) {
				wantConnectChange = false;
				if (canConnect()) {

					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

					connected = false;
				}
			}
		}

		if(landed) {
            isOnGround = true;
            Vector3 vel = GetComponent<Rigidbody>().velocity;
			vel.y = 0;

			if(vel.magnitude > 0.1) {
				landed = false;
				toSnowball();
			}
		}

		if(!GetComponent<Rigidbody>().useGravity) {
			if(GetComponent<Rigidbody>().velocity.y == 0)
				GetComponent<Rigidbody>().AddForce(new Vector3(0, -snowflakeFallSpeed, 0));
		}
	}

	void FixedUpdate() {
	
	}

	void OnCollisionEnter(Collision collision) {

		checkFallOnGround(collision);

	}

	void OnCollisionStay(Collision collision) {

		checkFallOnGround(collision);

		if(!connected) {
			if(wantConnectChange) {
				wantConnectChange = false;
				if(canConnect()) {
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

					connected = true;
				}
			}
		}
	}

	private bool canConnect() {
		bool can = connectTime + connectRepeatTime < Time.time;

		if(can) {
			connectTime = Time.time;
		}

		return can;
	}

	public void requestConnectChange() {
		wantConnectChange = true;
	}

	private void checkFallOnGround(Collision collision) {
		if(size == 0) {

			//if(collision.gameObject.name == "SnowTerrainLayer") {
				
				landed = true;
				
			//} else {
			//	Application.Quit();
			//}
		}
	}
	
	public bool isConnected() {
		return connected;
	}

	public void toSnowball() {
        size = startSize;
		
		GetComponent<Rigidbody>().useGravity = true; //put gravity back for snowball

		transform.GetChild(0).gameObject.SetActive(false);

		transform.position = transform.position + new Vector3(0, size*2, 0);

		updateSize();
	}

	private bool IsGrounded () {
		return Physics.Raycast(transform.position, new Vector3(0, -1, 0), jumpAllowDistance+size);
	}

	public void updateSize() {
        this.transform.GetChild(1).localScale = new Vector3(size, size, size);

		float colSize = size / 2;

		if(colSize == 0) {
			colSize = 0.4f;
		}

		this.GetComponent<SphereCollider> ().radius = colSize;
	}
}
