using UnityEngine;
using System.Collections;

public class SnowballMover : MonoBehaviour
{

    private Spawn spawn;
    private Inventory inventory;

    private Camera mainCamera;
    private Vector3 screenCenter;
    
    public float moveSpeed = 2.0f;
    public float moveSpeedAir = 1.0f;
    public float jumpSpeed = 2.0f;
    public float jumpRepeatTime = 10000.4f;
    public float cameraMoveSpeed = 200.0f;

    private float lastJumpTime = -1.0f;
    
    private bool manualInputJump;
    private bool manualInputConnect;
    private bool manualInputRespawn;
    private bool manualInputInvenotry;

    private Joystick controllerMove;
    private TouchPad controllerCamera;
    private Vector2 lastCameraControllerInput;

    void Start()
    {
        spawn = this.GetComponent<Spawn>();
        inventory = FindObjectOfType<Inventory>();

        controllerMove = FindObjectOfType<Joystick>();
        controllerCamera = FindObjectOfType<TouchPad>();
        mainCamera = Camera.main;
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, mainCamera.nearClipPlane);
    }

    void Update()
    {
        
        Vector3 forward;

        SnowballController snowball = getSnowball();

        if (snowball.size > 0)
            forward = mainCamera.transform.TransformDirection(Vector3.forward); //snowball
        else
            forward = mainCamera.transform.TransformDirection(Vector3.up); //snowflake

        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v = getVerticalInput();
        float h = getHorizontalInput();

        Vector3 targetDirection = h * right + v * forward;


        bool grounded = IsGrounded();

        if (grounded)
        {
            targetDirection = targetDirection * moveSpeed;
        }
        else
        {
            targetDirection = targetDirection * moveSpeedAir;
        }

        targetDirection *= Time.deltaTime;

        snowball.GetComponent<Rigidbody>().AddForce(targetDirection);

        if (grounded)
        {
            if (manualInputJump || Input.GetButton("Jump"))
            {
                if (lastJumpTime + jumpRepeatTime < Time.time)
                {
                    lastJumpTime = Time.time;
                    snowball.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpSpeed, 0));
                }
            }
        }

        //rotateCamera();

        if (Input.GetKey(KeyCode.C) || manualInputConnect)
        {
            snowball.requestConnectChange();
        }

        if (Input.GetKey(KeyCode.R) || manualInputRespawn)
        {
            //            GameObject cube = GameObject.FindWithTag("InventoryPanel");
            inventory.inventoryUI.RemoveSelectedItems();
            //var x = GameObject.Find("InventoryPanel");
              // x.GetComponent<UIInventory>().RemoveSelectedItems();
            spawn.trySpawnSnowball();
        }
    }

    private float getVerticalInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            return Input.GetAxisRaw("Vertical");
        }
        return controllerMove.JoystickInput.y;
    }

    private float getHorizontalInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            return Input.GetAxisRaw("Horizontal");
        }
        return controllerMove.JoystickInput.x;
    }

    private bool IsGrounded()
    {
        SnowballController snowball = getSnowball();
        return Physics.Raycast(snowball.transform.position, new Vector3(0, -1, 0), snowball.jumpAllowDistance + snowball.size);
    }

    private SnowballController getSnowball()
    {
        return spawn.latestSnowball.GetComponent<SnowballController>();
    }

    private void rotateCamera()
    {
        if(controllerCamera.TouchPadInput.magnitude > 0) {
            
            float dx = lastCameraControllerInput.x - controllerCamera.TouchPadInput.x;
            float dy = lastCameraControllerInput.y - controllerCamera.TouchPadInput.y;
            
            dx *= 100; dy *= 100;

            Vector3 centerPoint = mainCamera.ScreenToWorldPoint(screenCenter);
            Vector3 movePoint = mainCamera.ScreenToWorldPoint(screenCenter + new Vector3(dx, dy, 0));
            Vector3 moveVector = movePoint - centerPoint;

            mainCamera.transform.position += moveVector * cameraMoveSpeed;
        }

        lastCameraControllerInput = controllerCamera.TouchPadInput;
    }

    public void _increaseBallSize()
    {
        SnowballController snowball = getSnowball();
        snowball.size *= 2;
        snowball.updateSize();
    }

    public void _decreaseBallSize()
    {
        SnowballController snowball = getSnowball();
        snowball.size /= 2;
        snowball.updateSize();
    }

    public void _setManualInputJump(bool input)
    {
        this.manualInputJump = input;
    }

    public void _setManualInputConnect(bool input)
    {
        this.manualInputConnect = input;
    }

    public void _setManualInputRespawn(bool input)
    {
        this.manualInputRespawn = input;
    }

    public void _setManualInputInvenotry(bool input)
    {
        this.manualInputInvenotry = input;
    }
}
