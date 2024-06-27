using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Vector2 moveInput;

    [SerializeField] float moveSpeed = 10f;

    [SerializeField] InputAction playerMovement;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //float horizontalThrow = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalThrow);

        //float verticalThrow = Input.GetAxis("Vertical");
        //Debug.Log(verticalThrow);


    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Fly()
    {
        
    }
}
