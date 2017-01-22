using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {

	public GameObject gameState;

	public bool entered;
	private GameStateController gameStateController;

	Color colorNormal = new Color(0.328f, 1f, 0.169f, 0.422f);
	Color colorEntered = new Color(1f, 0.1f, 0.08f, 0.75f);

	// Use this for initialization
	void Start () {
		entered = false;
		gameStateController = (GameStateController) gameState.GetComponent(typeof(GameStateController));
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Snake")) {
			Debug.Log ("+");
			transform.GetComponent<MeshRenderer> ().material.color = colorEntered;
			entered = true;
			gameStateController.onSnakeEnter (this);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Snake")) {
			Debug.Log ("-");
			transform.GetComponent<MeshRenderer> ().material.color = colorNormal;
			entered = false;
			gameStateController.onSnakeExit (this);
		}
	}

	public bool isSnakeOn() {
		return entered;
	}
}