using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float cameraRight;
    float cameraLeft;
    float spriteRight;
    float spriteRightPrev = 100f;

    /// <summary>
    /// Array of Random Colors to give more randomness to game
    /// </summary>
    [SerializeField]
    Color[] randomSpriteColors;

    SpriteRenderer spriteRenderer;
    SpriteRenderer currentSp;
    SpriteRenderer nextSp;
    SpriteRenderer prevSp;


    bool isSpawned = false;

    void Start()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        currentSp = spriteRenderer;
        prevSp = currentSp;
    }


    void Update()
    {
        InfiniteScroll();
    }

    private void InfiniteScroll()
    {
        if (!isSpawned)
        {
            cameraRight = HelperClass.GetCameraRight(Camera.main);
            cameraLeft = HelperClass.GetCameraLeft(Camera.main);
            spriteRight = SpritePoint(currentSp);

            if (cameraRight >= spriteRight)
            {
                DestroyObjects();

                float spawnPosX = currentSp.transform.position.x + currentSp.bounds.size.x;
                Vector3 spawnPos = new Vector3(spawnPosX, currentSp.transform.position.y, currentSp.transform.position.z);

                nextSp = Instantiate(currentSp, spawnPos, Quaternion.identity, transform);
                ApplyRandomColorToNextSprite();
                nextSp.name = "BG";
                nextSp.tag = "Background";

                prevSp = currentSp;
                spriteRightPrev = SpritePoint(prevSp);

                currentSp = nextSp.transform.GetComponent<SpriteRenderer>();
            }
        }
    }

    /// <summary>
    /// Function to change color while spawning new sprites
    /// </summary>
    private void ApplyRandomColorToNextSprite()
    {
        int randColorIndex = UnityEngine.Random.Range(0, randomSpriteColors.Length);

        var sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in sprites)
        {
            sprite.color = randomSpriteColors[randColorIndex];
        }
    }

    private float SpritePoint(SpriteRenderer sp)
    {
        return sp.transform.position.x + (sp.bounds.size.x / 2);
    }

    private void DestroyObjects()
    {
        if (cameraLeft > spriteRightPrev)
        {
            if (prevSp.gameObject != null)
            {
                Destroy(prevSp.gameObject);
                prevSp = null;
            }
        }
    }

}