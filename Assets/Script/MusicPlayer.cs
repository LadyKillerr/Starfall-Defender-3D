using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int musicPlayerNum = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.InstanceID).Length ;

        if (musicPlayerNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
