using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] List<GameObject> Obstacles = new List<GameObject>();
    [SerializeField] float spacing = 3f;

    float cameraRight;
    float cameraLeft;
    Vector3 nextSpawnPos;

    int ObstaclesCurrentIndex = 0;
    int ObstaclesPreviousIndex = 0;

    Transform currentObs;
    Transform prevObs;

    [Header("Manager")]
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        cameraRight = HelperClass.GetCameraRight(Camera.main);
        cameraLeft = HelperClass.GetCameraLeft(Camera.main);

        ObstaclesCurrentIndex = Random.Range(0, Obstacles.Count);
        ObstaclesPreviousIndex = ObstaclesCurrentIndex;

        currentObs = Obstacles[ObstaclesCurrentIndex].transform;
        prevObs = currentObs;

        nextSpawnPos = new Vector3(cameraRight + spacing, 5f, 0f);

        SetPosition(currentObs.gameObject, nextSpawnPos);

        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            cameraRight = HelperClass.GetCameraRight(Camera.main);
            cameraLeft = HelperClass.GetCameraLeft(Camera.main);

            float maxExtentCurrent = currentObs.GetComponent<BoxCollider2D>().bounds.max.x;

            if (maxExtentCurrent < cameraRight)
            {
                SpawnNewObstacle();
            }

            float prevMaxExtent = prevObs.GetComponent<BoxCollider2D>().bounds.max.x;
            if (prevMaxExtent < cameraLeft)
            {
                prevObs.gameObject.SetActive(false);
            }
        }
    }

    void SpawnNewObstacle()
    {
        prevObs = currentObs;
        ObstaclesPreviousIndex = ObstaclesCurrentIndex;

        do
        {
            ObstaclesCurrentIndex = Random.Range(0, Obstacles.Count);
        } while (ObstaclesCurrentIndex == ObstaclesPreviousIndex);

        currentObs = Obstacles[ObstaclesCurrentIndex].transform;

        float lastObstacleWidth = prevObs.GetComponent<BoxCollider2D>().bounds.size.x;
        nextSpawnPos = new Vector3(prevObs.position.x + lastObstacleWidth + spacing, 5f, 0f);

        SetPosition(currentObs.gameObject, nextSpawnPos);
    }

    void SetPosition(GameObject obj, Vector3 position)
    {
        obj.SetActive(true);
        obj.transform.position = position;
    }
}
