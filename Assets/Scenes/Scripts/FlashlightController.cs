using UnityEngine;
using Tobii.Gaming;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    public float rotationSpeed = 5f;

    [Header("Flicker Settings")]
    public float flickerIntensity = 0.5f;
    public float flickerSpeed = 0.1f;

    private float baseIntensity;
    private float flickerTimer;

    void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("Flashlight not assigned!");
            enabled = false;
            return;
        }

        baseIntensity = flashlight.intensity;
        flashlight.color = Color.white; // ensure default color
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        if (!flashlight.enabled) return;

        HandleFlicker();
        RotateToGaze();
    }

    void HandleFlicker()
    {
        flickerTimer += Time.deltaTime;
        if (flickerTimer >= flickerSpeed)
        {
            float randomIntensity = Random.Range(baseIntensity - flickerIntensity, baseIntensity + flickerIntensity);
            flashlight.intensity = randomIntensity;
            flickerTimer = 0f;
        }
    }

    void RotateToGaze()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        if (!gazePoint.IsValid || !gazePoint.IsRecent()) return;

        if (Camera.main == null) return;

        Ray gazeRay = Camera.main.ScreenPointToRay(gazePoint.Screen);
        Vector3 direction = gazeRay.direction;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        flashlight.transform.rotation = Quaternion.Slerp(flashlight.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
