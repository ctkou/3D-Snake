using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFireController : MonoBehaviour {

	public GameObject gameState;
	private GameStateController gameStateController;
	private ParticleSystem ps;
	private float horizontal;
	private float center;

	float amplitudeX = 20.0f;
	float amplitudeY = 7.0f;
	float omegaX = 0.5f;
	float omegaY = 2.0f;
	float omegaZ = 0.8f;
	float index;


	// Use this for initialization
	void Start () {
		gameStateController = (GameStateController) gameState.GetComponent(typeof(GameStateController));
		ps = GetComponent<ParticleSystem>();
		startLater();
	}

	// Update is called once per frame
	void Update () {
		if (!isActive()) {
			return;
		}

		index += Time.deltaTime;
		float x = amplitudeX * Mathf.Cos(omegaX * index);
		float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
		float z = Mathf.Sin(omegaZ * index);
		if (horizontal == 0) {
			transform.localPosition = new Vector3(x,y,z + center);
		} else if (horizontal == 1) {
			transform.localPosition = new Vector3(z + center, x, y);
		} else {
			transform.localPosition = new Vector3(y,z + center, x);
		}


		if (index > ps.main.duration) {
			startLater();
		}
	}
	public void startLater() {
		StartCoroutine(startEmission());
	}
		
	IEnumerator startEmission() {
		ps.EnableEmission(false);
		ps.Stop();
		float edge = Mathf.Floor (Random.Range (0, 3));
		float dim1 = Random.Range (-15.0f, 15.0f);
		float dim2 = Random.Range (-15.0f, 15.0f);

		amplitudeY = Mathf.Min (amplitudeX + 3.0f, 25.0f);

		center = dim1;
		horizontal = edge;

		if (edge == 0) {
			transform.position = new Vector3 (0, dim1, dim2);
		} else if (edge == 1) {
			transform.position = new Vector3 (dim2, 0, dim1);
		} else {
			transform.position = new Vector3 (dim1, dim2, 0);
		}

		this.horizontal = edge;
		yield return new WaitForSeconds(1.5f);

		index = 0;
		ps.EnableEmission(true);
		ps.Play();
	}

	public bool isActive() {
		return ps.emission.enabled;
	}
}