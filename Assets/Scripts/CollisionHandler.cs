using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] ParticleSystem explosion;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        StopMovement();
        ExplodeShip();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ExplodeShip()
    {
        explosion.Play();
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        var boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    void StopMovement()
    {
        var playerControlsScript = GetComponent<PlayerControls>();
        playerControlsScript.enabled = false;
    }

    void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
