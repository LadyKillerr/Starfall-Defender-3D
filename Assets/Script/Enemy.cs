using UnityEngine;

public class Enemy : MonoBehaviour
{


    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Im Hit by:  {other.gameObject.name }" );
        //Destroy(gameObject);
    }
}
