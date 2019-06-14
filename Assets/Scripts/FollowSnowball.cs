using UnityEngine;
using System.Collections;

public class FollowSnowball : MonoBehaviour {

	public Transform target;
	public float distance = 3.0f;
	public float height = 1.0f;
	public float positionDamping = 2.0f;
	public float minSnowballDistance = 0.1f;
	public float snowflakeHeight = 0.1f;
	public float snowflakePositionDamping = 1.1f;
	public bool firstPerson = false;
    private float angleH = 0;
    private float angleV = 0;

    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);       // Offset to repoint the camera.
    public Vector3 camOffset = new Vector3(0.4f, 0.5f, -2.0f);

    public float horizontalAimingSpeed = 6f;                           // Horizontal turn speed.
    public float verticalAimingSpeed = 6f;                             // Vertical turn speed.
    public float maxVerticalAngle = 30f;                               // Camera max clamp angle. 
    public float minVerticalAngle = -60f;
    private float targetMaxVerticalAngle;                              // Custom camera max vertical clamp angle.

    // Current camera distance to the player.
    private Vector3 smoothPivotOffset;                                 // Camera current pivot offset on interpolation.
    private Vector3 smoothCamOffset;                                   // Camera current offset on interpolation.
    private Vector3 targetPivotOffset;                                 // Camera pivot offset target to iterpolate.
    private Vector3 targetCamOffset;
    public float smooth = 10f;                                         // Speed of camera responsiveness.
    private Vector3 relCameraPos;                                      // Current camera position relative to the player.
    private float relCameraPosMag;


    private SnowballController snowballController;

	void Start () {

		if (!target)
			return;

	}

    private void Awake()
    {
        snowballController = target.GetComponent<SnowballController>();

        // Set camera default position.
        transform.position = target.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        transform.rotation = Quaternion.identity;

        // Get camera position relative to the player, used for collision test.
        relCameraPos = transform.position - target.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        // Set up references and default values.
        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        angleH = target.eulerAngles.y;

    }

    void LateUpdate () {
	
		if (!target)
			return;

		if(!firstPerson) {

            /**
			 * 3rd person camera
			 */
            // Get mouse movement to orbit the camera.
            // Mouse:
            angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * horizontalAimingSpeed;
            angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * verticalAimingSpeed;

            //// Set camera orientation.
            Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
            Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
            transform.rotation = aimRotation;


            float snowballDistance = 1.0f;
			if(snowballController)
				snowballDistance = minSnowballDistance + snowballController.size / 2;
			
			float wantedHeight = 0;
			if (!snowballController || snowballController.size > 0)
				wantedHeight = target.position.y + height * snowballDistance; //snowball
			else
				wantedHeight = target.position.y + snowflakeHeight; //snowflake

			float currentHeight = transform.position.y;


            // Test for collision with the environment based on current camera position.
            Vector3 baseTempPosition = target.position + camYRotation * targetPivotOffset;
            Vector3 noCollisionOffset = targetCamOffset;
            for (float zOffset = targetCamOffset.z; zOffset <= 0; zOffset += 0.5f)
            {
                noCollisionOffset.z = zOffset;
                if (DoubleViewingPosCheck(baseTempPosition + aimRotation * noCollisionOffset, Mathf.Abs(zOffset)) || zOffset == 0)
                {
                    break;
                }
            }

            smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
            smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);
            //var newPos = target.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset + transform.TransformDirection(Vector3.forward) * distance * snowballDistance;
            //bumper - prevent seeing trought textures;
            //RaycastHit hit;
            //Vector3 hitVector = transform.position - target.position;

            //if (Physics.Raycast(target.position, hitVector, out hit, hitVector.magnitude)
            //    && hit.transform != target)
            //{
            //    newPos = hit.point;
            //}

            //transform.position = newPos;
            transform.position = target.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;

            //transform.position = target.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset + transform.TransformDirection(Vector3.forward) * distance * snowballDistance;
            //transform.position = target.position - transform.TransformDirection(Vector3.forward) * distance * snowballDistance;


            //Vector3 pos = target.position;
            //if (!snowballController || snowballController.size > 0) {
            //             pos -= transform.TransformDirection(Vector3.forward) * distance * snowballDistance; //snowball
            //             smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
            //             smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);

            //pos = target.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;
            //pos.x = Mathf.Lerp(transform.position.x, pos.x, positionDamping * Time.deltaTime);
            //pos.y = Mathf.Lerp(currentHeight, wantedHeight, positionDamping * Time.deltaTime);
            //pos.z = Mathf.Lerp(transform.position.z, pos.z, positionDamping * Time.deltaTime);
            //}
            //else {
            //pos.x = Mathf.Lerp (transform.position.x, pos.x, snowflakePositionDamping * Time.deltaTime);
            //pos.y = wantedHeight; //Mathf.Lerp (currentHeight, wantedHeight, snowflakePositionDamping * Time.deltaTime);
            //transform.LookAt(target);
            //pos.z = Mathf.Lerp (transform.position.z, pos.z, snowflakePositionDamping * Time.deltaTime);
            //}





            //move camera

            // Always look at the target
            if (!snowballController || snowballController.size > 0) {
				transform.LookAt (target);
			}

		} else {

			/**
			 * first person camera
			 */

			transform.position = target.position;
			transform.rotation = target.rotation;

		}
	}
    bool DoubleViewingPosCheck(Vector3 checkPos, float offset)
    {
        float playerFocusHeight = target.GetComponent<SphereCollider>().radius * 2;
        return ViewingPosCheck(checkPos, playerFocusHeight) && ReverseViewingPosCheck(checkPos, playerFocusHeight, offset);
    }

    bool ViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight)
    {
        // Cast target.
        Vector3 t = target.position + (Vector3.up * deltaPlayerHeight);
        // If a raycast from the check position to the player hits something...
        if (Physics.SphereCast(checkPos, 0.2f, t - checkPos, out RaycastHit hit, relCameraPosMag))
        {
            // ... if it is not the player...
            if (hit.transform != target && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                // This position isn't appropriate.
                return false;
            }
        }
        // If we haven't hit anything or we've hit the player, this is an appropriate position.
        return true;
    }

    // Check for collision from player to camera.
    bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight, float maxDistance)
    {
        // Cast origin.
        Vector3 origin = target.position + (Vector3.up * deltaPlayerHeight);
        if (Physics.SphereCast(origin, 0.2f, checkPos - origin, out RaycastHit hit, maxDistance))
        {
            if (hit.transform != target && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }
    public void setFirstPerson(bool firstPerson)
    {
        this.firstPerson = firstPerson;
    }
}
