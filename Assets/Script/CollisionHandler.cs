using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        string objectTriggering = this.name;
        string otherObject = other.gameObject.name;

        LoadCrashSequence();

        

        // another way of saying debug.log
        //Debug.Log($"{this.name} Triggered by {otherObject}");
    }

    void LoadCrashSequence()
    {
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
}
