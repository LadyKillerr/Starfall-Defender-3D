using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [SerializeField] InputAction movement;
    float horizontalThrow;
    float verticalThrow;

    [SerializeField] InputAction fire;


    [Header("Movement Tuning")]
    [Tooltip("How fast the player moving up and down")]
    [SerializeField] float controlSpeed = 10f;

    [Tooltip("How far player moving horizontally")] [SerializeField] float clampedXRange = 10f;
    [Tooltip("How far player moving vertically")] [SerializeField] float clampedYMin = 0f;
    [SerializeField] float clampedYMax = 15f;

    [Header("Position Based Rotation")]
    [Tooltip("Define player rotating speed when moving around\n Pitch is Y axis\n Yaw is X axis")]
    [SerializeField] float positionBasedPitch = -2f;
    [SerializeField] float positionBasedYaw = 2f;

    [Header("Control Based Rotation")]
    [SerializeField] float controlBasedPitch = -10f;
    [SerializeField] float controlBasedRoll = -15f;

    [Header("Weapon Firing")]
    [Tooltip("Put all player's laser particle here")]
    [SerializeField] GameObject[] lasers;


    void Awake()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
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

        float pitchDueToPosition = transform.localPosition.y * positionBasedPitch;
        float pitchDueToMovement = verticalThrow * controlBasedPitch;

        float yawDueToPosition = transform.localPosition.x * positionBasedYaw;
        //float yawDueToMovement = horizontalThrow * yawTurnSpeed;

        //float rollDueToPosition 
        float rollDueToMovement =  horizontalThrow * controlBasedRoll;

        float pitch = pitchDueToPosition + pitchDueToMovement;
        float yaw = yawDueToPosition;
        float roll = rollDueToMovement;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        #endregion

        #region ShipShooting
        if (fire.ReadValue<float>() > 0.5f)
        {
            ToggleLasers(true);
        }
        else
        {
            ToggleLasers(false);
        };
        
        #endregion

    }


    private void ToggleLasers(bool isActive)
    {
        foreach(GameObject laserParticle in lasers)
        {
            var emissionModules = laserParticle.GetComponent<ParticleSystem>().emission;
            emissionModules.enabled = isActive ;
        }
    }

}
