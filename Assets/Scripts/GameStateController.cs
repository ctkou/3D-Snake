using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
    private int score;
    public Text gameOverMessage, gameWinMessage;
    
    public Text displayText, stateText;

	public int hitCounts;

	// Use this for initialization
	void Start () {
		hitCounts = 0;
		score = 0;
		addScore(0);
		updateText();
		showGameOverMessage(false);
		showGameWinMessage(false);
	}

    public void showGameOverMessage(bool isShown) {
        gameOverMessage.gameObject.SetActive(isShown);
    }

	public void showGameWinMessage(bool isShown) {
		gameWinMessage.gameObject.SetActive(isShown);
	}

    public void addScore(int n) {
      	score += n;
      	displayText.text = "Score: " + score.ToString();
  	}

	public void onSnakeEnter(TriggerController tc) {
		hitCounts++;
		update();
	}

	public void onSnakeExit(TriggerController tc) {
		hitCounts--;
		update();
	}
	private void update() {
		if (remainingCount() == 0) {
			showGameWinMessage(true);
		} else {
			updateText();
		}
	}

	private int remainingCount() {
		return 2 - hitCounts;
	}

	private void updateText() {
		stateText.text = "Remaining: " + remainingCount().ToString();
	}
}
