using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
  private int score;
  public Text displayText;

	// Use this for initialization
	void Start () {
		score = 0;
		addScore(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void addScore(int n) {
      Debug.Log("addScore");
      score += n;
      displayText.text = "Score: " + score.ToString();
  }

}
