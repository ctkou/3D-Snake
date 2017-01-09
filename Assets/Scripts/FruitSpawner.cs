using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

    public GameObject fruitPrefab;
    public GameObject snakeHead;

    private int roomLeft = -22;
    private int roomRight = 22;
    private int roomBottom = -22;
    private int roomTop = 22;
    private int roomFar = -22;
    private int roomNear = 22;
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.transform.FindChild("Fruit") == null)
        {
            GameObject fruit = Instantiate(fruitPrefab);
            fruit.name = "Fruit";
            fruit.transform.SetParent(gameObject.transform);
            fruit.transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomSpawnPosition = GetRandomPosition();
        while (!CollideWithSnakeHead(randomSpawnPosition))
        {
            randomSpawnPosition = GetRandomPosition();
        }
        return randomSpawnPosition;
    }

    private bool CollideWithSnakeHead(Vector3 spawnPositionCandidate)
    {
        float snakeXCoordinate = snakeHead.transform.position.x;
        float snakeYCoordinate = snakeHead.transform.position.y;
        float snakeZCoordinate = snakeHead.transform.position.z;
        if ((spawnPositionCandidate.x < snakeXCoordinate + 5 && spawnPositionCandidate.x > snakeXCoordinate - 5) &&
            (spawnPositionCandidate.y < snakeYCoordinate + 5 && spawnPositionCandidate.y > snakeYCoordinate - 5) &&
            (spawnPositionCandidate.z < snakeZCoordinate + 5 && spawnPositionCandidate.z > snakeZCoordinate - 5))
        {
            return false;
        }
        return true;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            RandomXCoordinate(),
            RandomYCoordinate(),
            RandomZCoordinate()
        );
    }

    private int RandomXCoordinate()
    {
        return (int) Random.Range(roomLeft, roomRight);
    }

    private int RandomYCoordinate()
    {
        return (int) Random.Range(roomBottom, roomTop);
    }

    private int RandomZCoordinate()
    {
        return (int) Random.Range(roomFar, roomNear);
    }
}
