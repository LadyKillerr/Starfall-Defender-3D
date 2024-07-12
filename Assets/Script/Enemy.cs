using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyVFX;
    [SerializeField] Transform spawnAtRuntime;

    void OnParticleCollision(GameObject other)
    {

        ParticleSystem vfx = Instantiate(enemyVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = spawnAtRuntime;

        Destroy(gameObject);
    }
}
