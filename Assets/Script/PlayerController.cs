using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    
    [Header("Move Speed Tuning")]
    [SerializeField] float controlSpeed = 10f;

    [SerializeField] float clampedXRange = 10f;
    [SerializeField] float clampedYMin = 0f;
    [SerializeField] float clampedYMax = 10f;


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
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        // Clamped giá trị di chuyển 
        float clampedXpos = Mathf.Clamp(rawXPos, -clampedXRange, clampedXRange);
        float clampedYPos = Mathf.Clamp(rawYPos, clampedYMin, clampedYMax);

        // gán vào vị trí mới 
        transform.localPosition = new Vector3(clampedXpos, clampedYPos, transform.localPosition.z);
            

    }


}
