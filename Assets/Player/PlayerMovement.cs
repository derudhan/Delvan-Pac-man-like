using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private int _health;
    [SerializeField]
    private TMP_Text _Hp;
    [SerializeField]
    private Transform _respawnPoint;

    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive = false;

    public void Hit() {
        _health -= 1;
        if (_health > 0) {
            transform.position = _respawnPoint.position;
        } else {
            _health = 0;
            Debug.Log("You Lose!");
        }
        UpdateUI();
    }

    public void PickPowerUp() {
        if (_powerUpCoroutine != null) {
            StopCoroutine(_powerUpCoroutine);
        }
        
        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp() {
        _isPowerUpActive = true;
        if (OnPowerUpStart != null) {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(_powerUpDuration);
        _isPowerUpActive = false;
        if (OnPowerUpStop != null) {
            OnPowerUpStop();
        }
    }

    private void Awake() {
        UpdateUI();
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
    }

    private void OnCollisionEnter(Collision other) {
        if (_isPowerUpActive) {
            if (other.gameObject.CompareTag("Enemy")) {
                other.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI() {
        _Hp.text = "HP : " + _health;
    }
}
