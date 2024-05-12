using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private ScoreManager _scoreManager;

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
        _scoreManager.SetMaxScore(_pickablesList.Count);

    }

    private void OnPickablePicked(PickableObject pickable) {
        _pickablesList.Remove(pickable);

        if (_scoreManager != null) {
            _scoreManager.AddScore(1);
        }

        if (pickable.PickableType == PickableType.PowerUp) {
            player.PickPowerUp();
        }

        if (_pickablesList.Count <= 0) {
            Debug.Log("Win");
        }
    }
}
