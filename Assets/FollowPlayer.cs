using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public float distanceFromPlayer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 lookAt = player.position - transform.position;
        //transform.forward = lookAt.normalized;

        Vector3 newPosition = player.position - (player.position - transform.position).normalized * distanceFromPlayer;
        newPosition.y = player.position.y + distanceFromPlayer/ 2;

        transform.position = newPosition;
    }
}
