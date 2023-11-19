using UnityEngine;



public class BallController : MonoBehaviour
{

    private Camera mainCamera; // Reference to the main camera



    private void Start()
    {
        mainCamera = Camera.main; // Initialize the main camera
    }


    private void Update()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the ball is outside the viewport
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject); // Destroy the ball
        }
    }


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Monster"))
    //    {
    //        // You can add an explosion effect or sound here

    //        // Destroy the ball object
    //        //Destroy(gameObject);
    //    }
    //}
}
