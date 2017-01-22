using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
    
    public Text displayText, stateText;

	private int score;
	public int hitCounts;


	// Use this for initialization
	void Start () {
		hitCounts = 0;
		score = 0;
		addScore(0);
		updateText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addScore(int n) {
      	score += n;
      	displayText.text = "Score: " + score.ToString();
  	}

	public void onSnakeEnter(TriggerController tc) {
		hitCounts++;
		updateText();
	}

	public void onSnakeExit(TriggerController tc) {
		hitCounts--;
		updateText();
	}

	private int remainingCount() {
		return 2 - hitCounts;
	}

	private void updateText() {
		stateText.text = "Remaining: " + remainingCount().ToString();
	}
}