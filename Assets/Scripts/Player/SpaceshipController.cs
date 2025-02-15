using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), (typeof(SpriteRenderer)))]
public class SpaceshipController : MonoBehaviour
{

#if UNITY_ANDROID
    Touch touch;
#endif

    [Header("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    [Header("Player Movement")]

    float xSpeed = 50.0f;
    float ySpeed;
    float MAX_YSPEED = 70.0f;
    float MIN_YSPEED = -70.0f;
    float MAX_XSPEED = 60.0f;
    float MULTIPLIER = 0.05f;

    [Header("Player Raycast")]
    float raycastDistance = 10.0f;
    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] GameObject rayCastObject;


    [Header("Player Rotation")]
    float rotSpeed = 2.5f;
    float SPEED_OFFSET = 10.0f;


    [Header("Borders")]
    float cameraUp;
    float cameraDown;
    float distancefromCamUp;
    float distancefromCamDown;

    [Header("Player Sound")]
    float pitch;
    const float MIN_PITCH = 1.5f;
    const float MAX_PITCH = 3.0f;

    [Header("Managers")]
    GameManager gameManager;
    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        HelperClass.TopWindow = GameObject.Find("TopWindow").GetComponent<RectTransform>();
        HelperClass.BottomWindow = GameObject.Find("BottomWindow").GetComponent<RectTransform>();

        cameraUp = HelperClass.GetCamerUp(Camera.main);
        cameraDown = HelperClass.GetCameraDown(Camera.main);

        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;

        uiManager.StartCountdownCoroutine();
        gameManager.ResetScoreandTime();

        pitch = audioSource.pitch;


    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentGameStates == GameStates.isPlaying)
        {
            SpaceshipMovement();
            ClampPlayerPosition();

            pitch = Mathf.Clamp(pitch, MIN_PITCH, MAX_PITCH);
            audioSource.pitch = pitch;

            xSpeed += Time.deltaTime * MULTIPLIER;
            xSpeed = Mathf.Clamp(xSpeed, xSpeed, MAX_XSPEED);

            ySpeed = Mathf.Clamp(ySpeed, MIN_YSPEED, MAX_YSPEED);

            distancefromCamUp = transform.position.y - cameraUp;
            distancefromCamDown = transform.position.y - cameraDown;
            if (distancefromCamDown <= 0f || distancefromCamUp >= 0f) UpdateRotationAtBorders();
            RaycastBasedCollision();
        }
    }

    void SpaceshipMovement()
    {

#if UNITY_ANDROID

        bool isTouch = Input.touchCount > 0 && touch.phase == TouchPhase.Began ? true : false;

        if (isTouch)
        {
            pitch = Mathf.Lerp(pitch, MAX_PITCH, 0.01f);
            ySpeed += rotSpeed;
        }
        else
        {
            pitch = Mathf.Lerp(pitch, MIN_PITCH, 0.01f);
            ySpeed -= rotSpeed;
        }
#endif

#if UNITY_STANDALONE || UNITY_EDITOR_64
        bool isButtonPressed = Input.GetMouseButton(0);

        if (isButtonPressed)
        {
            pitch = Mathf.Lerp(pitch, MAX_PITCH, 0.01f);
            ySpeed += rotSpeed;
        }
        else
        {
            pitch = Mathf.Lerp(pitch, MIN_PITCH, 0.01f);
            ySpeed -= rotSpeed;
        }
#endif
        Vector2 moveVector = new Vector2(xSpeed, ySpeed);
        rb.velocity = moveVector * (SPEED_OFFSET * Time.deltaTime);
        transform.right = rb.velocity;
    }

    void ClampPlayerPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(transform.position.y, cameraDown, cameraUp);
        transform.position = clampedPosition;
    }

    void UpdateRotationAtBorders()
    {
        ySpeed = Mathf.Lerp(ySpeed, 0.0f, 1f);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 1f);

        if (Mathf.Abs(transform.rotation.z) <= 10f)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void RaycastBasedCollision()
    {
        RaycastHit2D upRay = Physics2D.Raycast(rayCastObject.transform.position, transform.up, raycastDistance, raycastLayerMask);
        RaycastHit2D downRay = Physics2D.Raycast(rayCastObject.transform.position, -transform.up, raycastDistance, raycastLayerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(rayCastObject.transform.position, transform.right, raycastDistance, raycastLayerMask);

        float upDistance = Vector2.Distance(transform.position, upRay.point);
        float downDistance = Vector2.Distance(transform.position, downRay.point);
        float rightDistance = Vector2.Distance(rayCastObject.transform.position, rightRay.point);


        if (upRay.collider != null && upRay.collider.CompareTag("Box"))
        {
            if (upDistance < 0.75f)
            {
                UpdateRotationAtBorders();
            }
        }

        if (downRay.collider != null && downRay.collider.CompareTag("Box"))
        {
            if (downDistance < 0.75f)
            {
                UpdateRotationAtBorders();
            }
        }

        if (rightRay.collider != null && rightRay.collider.CompareTag("Box"))
        {
            if (rightDistance < 0.1f)
            {
                gameManager.OnGameOver();
                Destroy(this.gameObject);
            }
        }
    }
}
