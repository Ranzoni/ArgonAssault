using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int objectPerHit = 1;
    [SerializeField] int hitsPoint = 3;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHitPoint();
    }

    void ProcessHitPoint()
    {
        ProcessHit();

        if (hitsPoint <= 0)
            KillEnemy();
    }

    void KillEnemy()
    {
        var vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        scoreBoard.IncreaseScore(objectPerHit);
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        var vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        --hitsPoint;
    }
}
