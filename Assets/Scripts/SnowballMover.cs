using UnityEngine;
using System.Collections;

public class SnowballMover : MonoBehaviour {

	private Spawn spawn;

	public float moveSpeed = 2.0f;
	public float moveSpeedAir = 1.0f;
	public float jumpSpeed = 2.0f;
	public float jumpRepeatTime = 0.4f;

	private float lastJumpTime = -1.0f;
	
	private bool manualInputTop;
	private bool manualInputRight;
	private bool manualInputBottom;
	private bool manualInputLeft;
	private bool manualInputJump;
	private bool manualInputConnect;
	private bool manualInputRespawn;

    private Joystick joystick;

    void Start () {
		spawn = this.GetComponent<Spawn>();
        joystick = FindObjectOfType<Joystick>();
    }

	void Update () {
		
		Transform cameraTransform = Camera.main.transform;
		
		Vector3 forward;

		SnowballController snowball = getSnowball ();
		
		if(snowball.size > 0)
			forward = cameraTransform.TransformDirection(Vector3.forward); //snowball
		else
			forward = cameraTransform.TransformDirection(Vector3.up); //snowflake
		
		forward.y = 0;
		forward = forward.normalized;
		
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v = getVerticalInput();
		float h = getHorizontalInput();
		
		Vector3 targetDirection = h * right + v * forward;
		
		
		bool grounded = IsGrounded();
		
		if(grounded) {
			targetDirection = targetDirection * moveSpeed;
		} else {
			targetDirection = targetDirection * moveSpeedAir;
		}
		
		targetDirection *= Time.deltaTime;
		
		snowball.GetComponent<Rigidbody>().AddForce(targetDirection);
		
		if(grounded) {
			if(manualInputJump || Input.GetButton("Jump")) {
				if (lastJumpTime + jumpRepeatTime < Time.time) {
					lastJumpTime = Time.time;
					snowball.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpSpeed, 0));
				}
			}
		}


		if(Input.GetKey(KeyCode.C) || manualInputConnect) {
			snowball.requestConnectChange();
		}

		if(Input.GetKey(KeyCode.R) || manualInputRespawn) {
			spawn.trySpawnSnowball();
		}
	}

	private float getVerticalInput() {
        if (manualInputTop)
            return 1;
		if(manualInputBottom)
            return -1;
		
        if(Input.GetAxisRaw("Vertical") != 0) {
            return Input.GetAxisRaw("Vertical");
        }
		return joystick.JoystickInput.y;
	}
	
	private float getHorizontalInput() {
        if (manualInputRight)
			return 1;
		if(manualInputLeft)
			return -1;

        if (Input.GetAxisRaw("Horizontal") != 0) {
            return Input.GetAxisRaw("Horizontal");
        }
        return joystick.JoystickInput.x;
	}

	private bool IsGrounded () {
		SnowballController snowball = getSnowball();
		return Physics.Raycast(snowball.transform.position, new Vector3(0, -1, 0), snowball.jumpAllowDistance + snowball.size);
	}

	private SnowballController getSnowball() {
		return spawn.latestSnowball.GetComponent<SnowballController>();
	}

	public void _increaseBallSize() {
		SnowballController snowball = getSnowball ();
		snowball.size *= 2;
		snowball.updateSize();
	}
	
	public void _decreaseBallSize() {
        SnowballController snowball = getSnowball ();
		snowball.size /= 2;
		snowball.updateSize();
	}
	
	public void _setManualInputTop(bool input) {
		this.manualInputTop = input;
	}
	
	public void _setManualInputRight(bool input) {
		this.manualInputRight = input;
	}
	
	public void _setManualInputBottom(bool input) {
		this.manualInputBottom = input;
	}
	
	public void _setManualInputLeft(bool input) {
		this.manualInputLeft = input;
	}
	
	public void _setManualInputJump(bool input) {
		this.manualInputJump = input;
	}

	public void _setManualInputConnect(bool input) {
		this.manualInputConnect = input;
	}

	public void _setManualInputRespawn(bool input) {
		this.manualInputRespawn = input;
	}
}
