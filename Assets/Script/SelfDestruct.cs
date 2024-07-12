using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float destroyDelay = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DestroyVFX();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyVFX()
    {
        Destroy(gameObject, destroyDelay);
        
    }

}
