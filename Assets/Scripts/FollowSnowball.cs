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

	private SnowballController snowballController;

	void Start () {

		if (!target)
			return;

		snowballController = target.GetComponent<SnowballController>();
	}

	void LateUpdate () {
	
		if (!target)
			return;

		if(!firstPerson) {
		
			/**
			 * 3rd person camera
			 */

			float snowballDistance = 1.0f;
			if(snowballController)
				snowballDistance = minSnowballDistance + snowballController.size / 2;
			
			float wantedHeight = 0;
			if (!snowballController || snowballController.size > 0)
				wantedHeight = target.position.y + height * snowballDistance; //snowball
			else
				wantedHeight = target.position.y + snowflakeHeight; //snowflake

			float currentHeight = transform.position.y;
			
			Vector3 pos = target.position;
			if (!snowballController || snowballController.size > 0) {
				pos -= transform.TransformDirection(Vector3.forward) * distance * snowballDistance; //snowball
				
				pos.x = Mathf.Lerp (transform.position.x, pos.x, positionDamping * Time.deltaTime);
				pos.y = Mathf.Lerp (currentHeight, wantedHeight, positionDamping * Time.deltaTime);
				pos.z = Mathf.Lerp (transform.position.z, pos.z, positionDamping * Time.deltaTime);
			} else {
				//pos.x = Mathf.Lerp (transform.position.x, pos.x, snowflakePositionDamping * Time.deltaTime);
				pos.y = wantedHeight; //Mathf.Lerp (currentHeight, wantedHeight, snowflakePositionDamping * Time.deltaTime);
				//pos.z = Mathf.Lerp (transform.position.z, pos.z, snowflakePositionDamping * Time.deltaTime);
			}
			


			//bumper - prevent seeing trought textures;
			RaycastHit hit;
			Vector3 hitVector = transform.position - target.position;

			if (Physics.Raycast(target.position, hitVector, out hit, hitVector.magnitude)
			    && hit.transform != target) {
					pos = hit.point;
			}

			//move camera
			transform.position = pos;
			
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

    public void setFirstPerson(bool firstPerson)
    {
        this.firstPerson = firstPerson;
    }
}
