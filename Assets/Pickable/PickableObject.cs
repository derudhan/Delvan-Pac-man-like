using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [SerializeField]
    public PickableType PickableType;

    public Action<PickableObject> OnPicked;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            OnPicked(this);
            Destroy(gameObject);
        }
    }
}
