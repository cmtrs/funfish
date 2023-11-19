using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartbeatEffect : MonoBehaviour
{
    public Button yourButton; // Assign your UI Button in the inspector

    // These variables control the heartbeat effect's speed and size
    public float pulseSpeed = 1f; // Speed of the heartbeat effect
    public float pulseSize = 1.1f; // How much the button increases in size at its largest

    private Vector3 originalScale;

    void Start()
    {
        if (yourButton == null)
        {
            Debug.LogError("No button assigned. Please assign a button in the inspector.");
            return;
        }

        originalScale = yourButton.transform.localScale; // Remember the original scale

        // Optional: If you want the heartbeat effect to start immediately, uncomment the line below
        StartCoroutine(Pulse());
    }

    public void TriggerHeartbeatEffect()
    {
        // Call this method to start the heartbeat effect
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        while (true) // This makes the loop infinite
        {
            // Scale the button up over time
            float time = 0f;
            while (time < pulseSpeed)
            {
                time += Time.deltaTime;
                yourButton.transform.localScale = Vector3.Lerp(originalScale, originalScale * pulseSize, time / pulseSpeed);
                yield return null; // Wait one frame and continue
            }

            // Scale the button back down over time
            time = 0f;
            while (time < pulseSpeed)
            {
                time += Time.deltaTime;
                yourButton.transform.localScale = Vector3.Lerp(originalScale * pulseSize, originalScale, time / pulseSpeed);
                yield return null; // Wait one frame and continue
            }
        }
    }
}
