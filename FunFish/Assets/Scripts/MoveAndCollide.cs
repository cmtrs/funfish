using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndCollide : MonoBehaviour
{
    public GameObject movingObject; // The object that moves (image from the right)
    public float moveSpeed = 1f; // Speed at which the object moves

    private Rigidbody2D rb; // Rigidbody component of the moving object

    void Start()
    {
        // Get the Rigidbody component
        rb = movingObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the object to the left
        Vector3 movement = Vector3.left * moveSpeed * Time.deltaTime;
        rb.MovePosition(movingObject.transform.position + movement);
    }

    // This function is called when the moving object collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it has collided with the specific object
        if (other.gameObject == movingObject)
        {
            // Call the function you wish to execute on collision
            OnCollisionWithObject();
        }
    }

    // Function to define what happens when collision occurs
    void OnCollisionWithObject()
    {
        // For example, print a message to the console
        Debug.Log("Objects have collided!");

        // Here, you can add any action you want to happen upon collision
    }
}
