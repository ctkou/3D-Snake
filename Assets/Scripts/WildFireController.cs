using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFireController : MonoBehaviour {

	public GameObject gameState;

	private GameStateController gameStateController;
	private ParticleSystem ps;

	float amplitudeX = 25.0f;
	float amplitudeY = 10.0f;
	float omegaX = 0.5f;
	float omegaY = 2.0f;
	float index;


	// Use this for initialization
	void Start () {
		gameStateController = (GameStateController) gameState.GetComponent(typeof(GameStateController));
		ps = GetComponent<ParticleSystem>();
		ps.EnableEmission(false);
		transform.localPosition = new Vector3(25, 0, 0);
		startLater();
	}

	// Update is called once per frame
	void Update () {
		if (!ps.emission.enabled) {
			return;
		}

		index += Time.deltaTime;
		float x = amplitudeX * Mathf.Cos(omegaX * index);
		float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
		transform.localPosition = new Vector3(x,y,0);

		if (index > ps.main.duration) {
			ps.EnableEmission(false);
			startLater();
		}
	}
	public void startLater() {
		StartCoroutine(startEmission());
	}
		
	IEnumerator startEmission() {
		yield return new WaitForSeconds(1.5f);

		index = 0;
		ps.EnableEmission(true);
	}
}