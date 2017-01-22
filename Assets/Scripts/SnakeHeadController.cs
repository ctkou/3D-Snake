using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour {
    // prefabs
    public GameObject snakeBodyPrefab;
	public GameObject snakeTailPrefab;
    public GameObject gameState;

    private GameStateController gameStateController;

    private int step = 0;
    private string lastPressedKey = "";
    private int updateFrequency = 5;
    private bool isGameOver = false;

    // Snake body parts 
    private List<GameObject> snakeBodySections = new List<GameObject>();

    void OnTriggerEnter(Collider other) {
		GameObject go = other.gameObject;
		Debug.Log ("Snake: " + go.name);
		if (!go.CompareTag ("Trigger")) {
			if (go.name.Equals ("Fruit")) {
				GameObject newBodySection = Instantiate (snakeBodyPrefab);
				AddBodyBeforeTail (newBodySection);
				Destroy (go);
				gameStateController.addScore (5);
			} else if (go.CompareTag ("Fire")) {
				// TODO maybe just chop off or burn the tail?
				// but for now...
				isGameOver = true;
				gameStateController.showGameOverMessage (true);
			} else if (!go.name.Equals("Body_Section_1")) {
                isGameOver = true;
                gameStateController.showGameOverMessage(true);
            }
        }
    }

    public void resetSnake() {
        isGameOver = false;
        gameStateController.showGameOverMessage(false);

        // destroy original body and tail
        if (snakeBodySections.Count > 1) {
            Destroy(snakeBodySections[1]);
            snakeBodySections.RemoveRange(1, snakeBodySections.Count - 1);
        }

        // create body part
        GameObject snakeBodySection = Instantiate(snakeBodyPrefab);
        AddPartToLastJoint(snakeBodySection);

        // create tail
        GameObject snakeTail = Instantiate(snakeTailPrefab);
        AddPartToLastJoint(snakeTail);

        // reset head position
        snakeBodySections[0].transform.position = new Vector3();
        snakeBodySections[0].transform.rotation = new Quaternion();
    }

    // Use this for initialization
    void Start() {
        gameStateController  = (GameStateController) gameState.GetComponent(typeof(GameStateController));

        // add this game object as the first body section
        snakeBodySections.Add(gameObject);

        resetSnake();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                lastPressedKey = "W";
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                lastPressedKey = "S";
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                lastPressedKey = "A";
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                lastPressedKey = "D";
            }

            if (step % updateFrequency == 0)
            {
                MoveSnakeForward();
                if (lastPressedKey.Equals("W"))
                {
                    RotateSnakeHeadToXPositive();
                    lastPressedKey = "";
                }
                else if (lastPressedKey.Equals("S"))
                {
                    RotateSnakeHeadToXNegative();
                    lastPressedKey = "";
                }
                else if (lastPressedKey.Equals("A"))
                {
                    RotateSnakeHeadToZNegative();
                    lastPressedKey = "";
                }
                else if (lastPressedKey.Equals("D"))
                {
                    RotateSnakeHeadToZPositive();
                    lastPressedKey = "";
                }
            }
            step++;
        }
    }

    private void MoveSnakeForward()
    {
        snakeBodySections[0].transform.Translate(new Vector3(0, 2, 0));
        UpdateRotation();
        snakeBodySections[0].transform.FindChild("Joint").transform.localRotation = new Quaternion();
    }

    private void RotateSnakeHeadToXNegative()
    {
        snakeBodySections[0].transform.Rotate(new Vector3(0, 0, 90));
        snakeBodySections[0].transform.FindChild("Joint").transform.Rotate(new Vector3(0, 0, -90));
    }

    private void RotateSnakeHeadToXPositive()
    {
        snakeBodySections[0].transform.Rotate(new Vector3(0, 0, -90));
        snakeBodySections[0].transform.FindChild("Joint").transform.Rotate(new Vector3(0, 0, 90));
    }

    private void RotateSnakeHeadToZNegative()
    {
        snakeBodySections[0].transform.Rotate(new Vector3(-90, 0, 0));
        snakeBodySections[0].transform.FindChild("Joint").transform.Rotate(new Vector3(90, 0, 0));
    }

    private void RotateSnakeHeadToZPositive()
    {
        snakeBodySections[0].transform.Rotate(new Vector3(90, 0, 0));
        snakeBodySections[0].transform.FindChild("Joint").transform.Rotate(new Vector3(-90, 0, 0));
    }

    private void UpdateRotation()
    {
        for (int i = snakeBodySections.Count - 2; i >= 1; i--)
        {
            snakeBodySections[i].transform.FindChild("Joint").transform.localRotation =
                new Quaternion(
                    snakeBodySections[i - 1].transform.FindChild("Joint").transform.localRotation.x,
                    snakeBodySections[i - 1].transform.FindChild("Joint").transform.localRotation.y,
                    snakeBodySections[i - 1].transform.FindChild("Joint").transform.localRotation.z,
                    snakeBodySections[i - 1].transform.FindChild("Joint").transform.localRotation.w
                );
        }
    }

    private void AddPartToLastJoint(GameObject gameObject)
    {
        Transform lastJointTransform = snakeBodySections[snakeBodySections.Count - 1].transform.FindChild("Joint");
        gameObject.transform.SetParent(lastJointTransform);
        gameObject.transform.localPosition = new Vector3();
        gameObject.transform.localRotation = new Quaternion();
        gameObject.name = "Body_Section_" + snakeBodySections.Count;
        snakeBodySections.Add(gameObject);
    }

    private void AddBodyBeforeTail(GameObject gameObject)
    {
        GameObject tail = snakeBodySections[snakeBodySections.Count - 1];
        snakeBodySections.RemoveAt(snakeBodySections.Count - 1);
        AddPartToLastJoint(gameObject);
        AddPartToLastJoint(tail);
    }
}
