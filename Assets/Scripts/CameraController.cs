using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour {
	
    public float speed;
	public Transform playerTransform;

    private float mouseXMovement;
    private float mouseYMovement;

	void Start () {
		if (GetComponent<NetworkView>().isMine) {
			GetComponent<Camera>().enabled = true;
		} else {
			GetComponent<Camera>().enabled = false;
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		if (playerTransform == null) {
			return;
		}
        mouseXMovement = Input.GetAxis("Mouse X");
        mouseYMovement = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseXMovement * speed, 0);
        transform.Rotate(-1 * mouseYMovement * speed, 0, 0);
        transform.position = playerTransform.position - transform.forward * 15;
    }
		
	public void setTarget(Transform target) {
		speed = 2;
		playerTransform = target;
	}
}