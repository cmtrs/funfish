using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// Manages the Monster's actions and states.
/// </summary>
public class MonsterController : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f; // Speed at which the Monster moves


    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // Move the Monster from right to left
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }


    /// <summary>
    /// Handles collision events.
    /// </summary>
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        if (collider2D.gameObject.CompareTag("Ball"))
        {
            // Destroy the Monster and Ball
            Destroy(collider2D.gameObject);
            Destroy(gameObject);

            // Play the hit sound
            MyGlobal.playHit();

            // Trigger the event when this monster dies
            EventManager.MonsterDestroyed();
        }
    }
}