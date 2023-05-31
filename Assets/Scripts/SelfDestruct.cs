using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 3f;

    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
