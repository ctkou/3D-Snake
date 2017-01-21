using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
    private int score;
    public Text displayText;
    public Text gameOverMessage;


	// Use this for initialization
	void Start () {
		score = 0;
		addScore(0);
	}

    public void addScore(int n) {
        Debug.Log("addScore");
        score += n;
        displayText.text = "Score: " + score.ToString();
    }

    public void showGameOverMessage(bool isShown) {
        gameOverMessage.gameObject.SetActive(isShown);
    }
}
