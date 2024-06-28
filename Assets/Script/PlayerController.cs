using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    
    [Header("Move Speed Tuning")]
    [SerializeField] float controlSpeed = 10f;

    //float horizontalThrow;
    //float verticalThrow;

    //float xOffset;
    //float yOffset;

    //float newXPos;
    //float newYPos;

    void Awake()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    void FixedUpdate()
    {
        Fly();

    }

    void Fly()
    {
        // Nhận Input từ bàn phím 
        float horizontalThrow = movement.ReadValue<Vector2>().x;
        float verticalThrow = movement.ReadValue<Vector2>().y;

        // Tạo biến offset để gán vào position mới của Ship
        float xOffset = horizontalThrow * controlSpeed * Time.deltaTime;
        float yOffset = verticalThrow * controlSpeed * Time.deltaTime;

        // thêm offset vào vị trí hiện tại (update từng frame)
        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        // gán vào vị trí mới 
        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
            

    }


}
