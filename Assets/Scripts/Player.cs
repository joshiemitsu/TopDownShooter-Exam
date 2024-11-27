using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput m_playerInput;

    [SerializeField] private float m_movementSpeed = 0;
    [SerializeField] private float m_rotationSpeed = 0;

    // Minimum movement to consider a joystick value
    private const float AIM_MIN_MOVEMENT = 0.01f;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector2 movementInput = m_playerInput.actions["Movement"].ReadValue<Vector2>();
        Vector2 aimInput = m_playerInput.actions["Aim"].ReadValue<Vector2>();

        this.transform.position += m_movementSpeed * Time.deltaTime * new Vector3(movementInput.x, 0, movementInput.y);

        // Check joystick value for change before setting the aim rotation
        if(aimInput.sqrMagnitude > AIM_MIN_MOVEMENT)
        {
            float angle = Mathf.Atan2(aimInput.x, aimInput.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);
        }
    }
}
