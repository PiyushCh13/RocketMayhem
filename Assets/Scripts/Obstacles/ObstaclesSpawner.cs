using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] List<GameObject> Obstacles = new List<GameObject>();
    float cameraRight;
    float cameraLeft;
    [SerializeField] float spacing;
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

        ObstaclesCurrentIndex = Random.Range(0, Obstacles.Count);
        ObstaclesPreviousIndex = ObstaclesCurrentIndex;

        currentObs = Obstacles[ObstaclesCurrentIndex].transform;
        prevObs = currentObs;

        cameraRight = HelperClass.GetCameraRight(Camera.main);
        cameraLeft = HelperClass.GetCameraLeft(Camera.main);

        nextSpawnPos = new Vector3(19f, 5f, 0f);

        SetPosition(currentObs.gameObject, nextSpawnPos);


    }

    // Update is called once per frame
    void Update()
    {
        InstantiateEasy();
    }

    public void InstantiateEasy() 
    {
        cameraRight = HelperClass.GetCameraRight(Camera.main);
        cameraLeft = HelperClass.GetCameraLeft(Camera.main);

        float maxExtentCurrent = currentObs.GetComponent<BoxCollider2D>().bounds.max.x;

        if ( maxExtentCurrent < cameraRight) 
        {
            ObstaclesPreviousIndex = ObstaclesCurrentIndex;

            prevObs = currentObs;

            ObstaclesCurrentIndex = Random.Range(0, Obstacles.Count);

            if(ObstaclesCurrentIndex == ObstaclesPreviousIndex) 
            {
                ObstaclesCurrentIndex = (ObstaclesPreviousIndex + 1) % Obstacles.Count;
            }

            currentObs = Obstacles[ObstaclesCurrentIndex].transform;
            nextSpawnPos = new Vector3(cameraRight + spacing, 5f, 0f);
            SetPosition(currentObs.gameObject, nextSpawnPos);
        }

        float prevMaxExtent = prevObs.GetComponent<BoxCollider2D>().bounds.max.x;

        if(prevMaxExtent < cameraLeft) 
        {
            prevObs.gameObject.SetActive(false);
        }
    }

    void SetPosition(GameObject obj, Vector3 position)
    {
        obj.SetActive(true);
        obj.transform.position = position;
    }
}
