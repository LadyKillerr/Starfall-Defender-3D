using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float horizontalThrow;
    float verticalThrow;

    [SerializeField] InputAction movement;

    [Header("Movement Tuning")]
    [SerializeField] float controlSpeed = 10f;

    [SerializeField] float clampedXRange = 10f;
    [SerializeField] float clampedYMin = 0f;
    [SerializeField] float clampedYMax = 15f;

    [Header("Rotation Tuning")]
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float pitchTurnSpeed = -10f;

    [SerializeField] float YawFactor = 2f;
    //[SerializeField] float yawTurnSpeed = 15f;

    [SerializeField] float rollFactor = -15f;

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

    void Update()
    {
        Fly();

    }

    void Fly()
    {
        // Nhận Input từ bàn phím 
         horizontalThrow = movement.ReadValue<Vector2>().x;
         verticalThrow = movement.ReadValue<Vector2>().y;

        #region ShipMovement
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
        #endregion

        #region ShipRotation

        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToMovement = verticalThrow * pitchTurnSpeed;

        float yawDueToPosition = transform.localPosition.x * YawFactor;
        //float yawDueToMovement = horizontalThrow * yawTurnSpeed;

        //float rollDueToPosition 
        float rollDueToMovement = transform.localPosition.z * horizontalThrow * rollFactor;

        float pitch = pitchDueToPosition + pitchDueToMovement;
        float yaw = yawDueToPosition;
        float roll = rollDueToMovement;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        #endregion

    }


}
