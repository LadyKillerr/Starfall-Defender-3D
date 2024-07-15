using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Score")]
    ScoreBoard scoreBoard;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int scorePerKill = 100;

    [Header("Health")]
    [SerializeField] int enemyHealth = 100;
    [SerializeField] int subtractedPerHit = 10;

    [SerializeField] ParticleSystem shootedVFX;
    [SerializeField] ParticleSystem explodedVFX;
    [SerializeField] Transform spawnAtRuntime;

    private void Awake()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {


        ProcessHit();
    }

    void ProcessHit()
    {
        if (enemyHealth > 1)
        {
            enemyHealth -= subtractedPerHit;

            ParticleSystem hitVFX = Instantiate(shootedVFX, transform.position, Quaternion.identity);
            hitVFX.transform.parent = spawnAtRuntime;

            scoreBoard.IncreaseScore(scorePerHit);

        }
        else if (enemyHealth <= 1)
        {
            ParticleSystem explodeVFX = Instantiate(explodedVFX, transform.position, Quaternion.identity);
            explodeVFX.transform.parent = spawnAtRuntime;

            scoreBoard.IncreaseScore(scorePerKill);


            Destroy(gameObject);

        }
    }

    void Update()
    {

    }


}
