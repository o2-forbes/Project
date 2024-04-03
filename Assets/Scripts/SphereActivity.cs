using UnityEngine;

public class SphereActivity : MonoBehaviour
{
    private bool isActive = true;
    private float lastToggleTime;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial time
        lastToggleTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a second has passed since the last toggle
        if (Time.time - lastToggleTime >= 0.5f)
        {
            // Toggle isActive flag
            isActive = !isActive;

            // Toggle renderer visibility
            GetComponent<Renderer>().enabled = isActive;

            // Update last toggle time
            lastToggleTime = Time.time;
        }
    }
}
