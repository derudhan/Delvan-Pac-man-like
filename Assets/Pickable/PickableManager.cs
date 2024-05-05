using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;

    private List<PickableObject> _pickablesList = new List<PickableObject>();

    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList() {
        PickableObject[] pickableObjects = GameObject.FindObjectsOfType<PickableObject>();

        for (int i = 0; i < pickableObjects.Length; i++) {
            _pickablesList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }

        // Debug.Log("Jumlah Pickable: " + _pickablesList.Count);
    }

    private void OnPickablePicked(PickableObject pickable) {
        _pickablesList.Remove(pickable);
        // Debug.Log("Sisa " + _pickablesList.Count);

        if (pickable.PickableType == PickableType.PowerUp) {
            player.PickPowerUp();
        }

        if (_pickablesList.Count <= 0) {
            Debug.Log("Win");
        }
    }
}
