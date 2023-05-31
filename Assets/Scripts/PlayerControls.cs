using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float moveSpeed = 10f;
    [Tooltip("Maximum and minimum value on the X axis")]
    [SerializeField] float xRange = 10f;
    [Tooltip("Maximum and minimum value on the Y axis")]
    [SerializeField] float yRange = 7f;
    [Tooltip("Lasers objects to controls")]
    [SerializeField] GameObject[] lasers;

    [Header("Pitch Settings")]
    [Tooltip("Factor value to manipulate pitch based on position")]
    [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("Factor value to manipulate pitch based on control pressed")]
    [SerializeField] float controlPitchFactor = -10f;

    [Header("Yaw Settings")]
    [Tooltip("Factor value to manipulate yaw based on control pressed")]
    [SerializeField] float positionYawFactor = 2f;

    [Header("Roll Settings")]
    [Tooltip("Factor value to manipulate roll based on control pressed")]
    [SerializeField] float controlRollFactor = -20f;

    float yThrow, xThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
            SetLasersActive(true);
        else
            SetLasersActive(false);
    }

    void SetLasersActive(bool isActive)
    {
        foreach (var laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;
        }
    }

    void ProcessRotation()
    {
        var pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        var pitchDueToControlThrow = yThrow * controlPitchFactor;
        var pitch = pitchDueToPosition + pitchDueToControlThrow;

        var yaw = transform.localPosition.x * positionYawFactor;

        var roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        var xOffset = xThrow * moveSpeed * Time.deltaTime;
        var rawXPos = transform.localPosition.x + xOffset;
        var clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = Input.GetAxis("Vertical");
        var yOffset = yThrow * moveSpeed * Time.deltaTime;
        var rawYPos = transform.localPosition.y + yOffset;
        var clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
