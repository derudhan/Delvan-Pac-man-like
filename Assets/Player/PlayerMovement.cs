using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _powerUpDuration;

    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;

    public void PickPowerUp() {
        if (_powerUpCoroutine != null) {
            StopCoroutine(_powerUpCoroutine);
        }
        
        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp() {
        if (OnPowerUpStart != null) {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(_powerUpDuration);
        if (OnPowerUpStop != null) {
            OnPowerUpStop();
        }
    }

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        HideLockCursor();
    }

    private void HideLockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Horizontal = a/left (-) && d/right (+)
        float horizontal = Input.GetAxis("Horizontal");
        // Vertical = s/down (-) && w/up (+)
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        Vector3 verticalDirection = vertical * _camera.transform.forward;
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + verticalDirection;

        // Move
        _rigidbody.velocity = movementDirection * _speed * Time.fixedDeltaTime;

        // Debug.Log("Horizontal: " + horizontal);
        // Debug.Log("Vertical: " + vertical);
    }
}
