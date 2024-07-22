using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashParticles;

    [Header("Player Health")]
    [SerializeField] int playerHealth = 100;
    [SerializeField] int damageOnHit = 25;
    [SerializeField] int deductAmount = 5;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] ParticleSystem onHitEffects;
    [SerializeField] Transform runtimeBin;

    int targetHealth;

    [Header("Player Voice")]
    AudioSource shipAudioSource;
    [SerializeField] float sfxVolume = .75f;
    [SerializeField] AudioClip gotHitSFX;
    [SerializeField] AudioClip onDeadSFX;

    private void Awake()
    {
        shipAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        targetHealth = playerHealth;
    }

    void Update()
    {
        DeductPlayerHealth();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (playerHealth > damageOnHit)
        {
            targetHealth = playerHealth - damageOnHit;

            if (shipAudioSource.isPlaying)
            {
                shipAudioSource.Stop();
                PlayOnHitSFX();
            }
            else
            {
                PlayOnHitSFX();
            }

            ParticleSystem vfx = Instantiate(onHitEffects, transform.position, Quaternion.identity);
            vfx.transform.parent = runtimeBin.transform;
        }
        else
        {
            targetHealth = 0;
            LoadCrashSequence();
        }

    }

    void DeductPlayerHealth()
    {
        if (playerHealth > targetHealth)
        {
            playerHealth -= deductAmount;
        }

        healthText.text = "Shield: " + playerHealth.ToString();
    }

    void LoadCrashSequence()
    {
        if (shipAudioSource.isPlaying)
        {
            shipAudioSource.Stop();
            PlayOnDeadSFX();
        }
        else
        {
            PlayOnDeadSFX();
        }

        // khoá player movement
        GetComponent<PlayerController>().enabled = false;

        // bật VFX 
        crashParticles.Play();

        // tắt hình tàu
        GetComponent<MeshRenderer>().enabled = false;

        // tắt box collider để ko va chạm được với 2 vật liền nhau 
        GetComponent<BoxCollider>().enabled = false;

        StartCoroutine(ResetScene(loadDelay));
    }

    IEnumerator ResetScene(float delay)
    {
        yield return new WaitForSeconds(delay);


        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);



    }

    void PlayAudioClip(AudioClip SFXToPlay, float volume)
    {
        if (shipAudioSource != null)
        {
            shipAudioSource.PlayOneShot(SFXToPlay, volume);
        }
    }

    void PlayOnHitSFX()
    {
        PlayAudioClip(gotHitSFX, sfxVolume);
    }

    void PlayOnDeadSFX()
    {
        PlayAudioClip(onDeadSFX, sfxVolume);

    }
}
