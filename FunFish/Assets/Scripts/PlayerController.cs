using Unity.VisualScripting;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Vector3 startingPosition;
    private float nextThrowTime = 0.0f; // The time when the player can throw the next ball
    private int playerHealth;
    private int damagedAmount;



    [SerializeField] private GameObject ballPrefab; // Reference to the Ball prefab
    [SerializeField] private float throwCooldown = 1.0f; // Cooldown time in seconds between each throw
    [SerializeField] private float throwSpeed = 5.0f; // Speed at which the ball will be thrown
    [SerializeField] private float moveDistance = 0.1f; // Distance the player will move to the right after throwing



    private void Awake()
    {
        startingPosition = new Vector3(-10, -3, 0);
        playerHealth = 100;
        damagedAmount = 100;
    }


    // Update is called once per frame
    private void Update()
    {
        // Detect touch input to throw the ball
        //if (Input.touchCount > 0 && Time.time >= nextThrowTime) // Check if cooldown has elapsed
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
        //        ThrowBall(touchPosition);

        //        MovePlayerForward();

        //        nextThrowTime = Time.time + throwCooldown; // Set the next time the player can throw
        //    }
        //}
    }


    ///// <summary>
    ///// Instantiates and throws the ball towards the touch position in an arc.
    ///// </summary>
    ///// <param name="target">The position where the ball should land.</param>
    //private void ThrowBall(Vector3 target)
    //{
    //    GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    //    Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

    //    Vector2 direction = (target - transform.position).normalized;

    //    // Calculate angle to throw the ball in an arc
    //    float angle = 45f; // 45 degrees for a balanced arc
    //    float radianAngle = angle * Mathf.Deg2Rad;

    //    // Calculate the velocity vector components
    //    float xComponent = Mathf.Cos(radianAngle) * throwSpeed;
    //    float yComponent = Mathf.Sin(radianAngle) * throwSpeed;

    //    // Apply the calculated components to the direction
    //    Vector2 velocity = new Vector2(direction.x * xComponent, direction.y * yComponent);

    //    // Apply the velocity to the Rigidbody2D
    //    rb.velocity = velocity;
    //}


    /// <summary>
    /// Instantiates and throws the ball towards the touch position.
    /// </summary>
    /// <param name="target">The position where the ball should land.</param>
    void ThrowBall(Vector3 target)
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        Vector2 direction = (target - transform.position).normalized;
        rb.velocity = direction * throwSpeed;
    }

    public void throwBubble()
    {
        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        Renderer renderer = GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        var target = new Vector3(bounds.max.x, bounds.center.y, bounds.center.z);

        Vector2 direction = (target - transform.position).normalized;
        rb.velocity = direction * throwSpeed;
    }

    /// <summary>
    /// Moves the player a bit to the right.
    /// </summary>
    private void MovePlayerForward()
    {
        transform.position += new Vector3(moveDistance, 0, 0);
    }

    /// <summary>
    /// Moves the player a bit to the left.
    /// </summary>
    private void MovePlayerBackwards()
    {
        transform.localPosition -= new Vector3(moveDistance, 0, 0);

        if (transform.localPosition.x < -10)
        {
            transform.localPosition = startingPosition;
        }
    }

    private void TakeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Current Health: " + playerHealth);  // Debug log for current health

        if (playerHealth <= 0)
        {
            playerHealth = 0;

            Destroy(this.gameObject);

            EventManager.GameOver();
        }

        // Update health UI
    }


    /// <summary>
    /// Handles collision events.
    /// </summary>
    void OnTriggerEnter2D(Collider2D collider2D)
    {

        Debug.Log("Collided with Monster! " + collider2D.gameObject.name);

        if (collider2D.gameObject.name.StartsWith("Monster"))
        {
            // Debug.Log("Collided with Monster! " + collider2D.gameObject.name);
            // Trigger the event when this monster dies
            EventManager.MonsterDestroyed();

            // Trigger game over
            EventManager.GameOver();



            //  MovePlayerBackwards();

            // Destroy the Monster and do some damage to the Player
            // Destroy(collider2D.gameObject);

            // Player damaged logic
            // TakeDamage(damagedAmount);

        }
    }

}