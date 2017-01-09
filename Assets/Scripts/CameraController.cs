using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject snakeHead;
    public float speed;

    private float mouseXMovement;
    private float mouseYMovement;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        mouseXMovement = Input.GetAxis("Mouse X");
        mouseYMovement = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseXMovement * speed, 0);
        transform.Rotate(-1 * mouseYMovement * speed, 0, 0);
        transform.position = snakeHead.transform.position - transform.forward * 15;
    }
}
