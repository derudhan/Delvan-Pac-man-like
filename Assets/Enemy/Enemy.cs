using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<Transform> wayPoints = new List<Transform>();
    [SerializeField]
    public float ChaseDistance;
    [SerializeField]
    public PlayerMovement player;

    private BaseState _currentState;

    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public RetreatState retreatState = new RetreatState();

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;

    [SerializeField]
    public Material BaseMaterial;
    [SerializeField]
    public Material RetreatMaterial;

    public void SwitchState(BaseState currentState) {
        _currentState.ExitState(this);
        _currentState = currentState;
        _currentState.EnterState(this);
    }

    public void Dead() {
        Destroy(gameObject);
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        _currentState = patrolState;
        _currentState.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        if (player != null) {
            player.OnPowerUpStart += StartRetreat;
            player.OnPowerUpStop += StopRetreat;
        }
    }

    private void Update() {
        if (_currentState != null) {
            _currentState.UpdateState(this);
        }
    }

    private void StartRetreat() {
        SwitchState(retreatState);
    }

    private void StopRetreat() {
        SwitchState(patrolState);
    }

    private void OnCollisionEnter(Collision other) {
        if (_currentState != retreatState) {
            if (other.gameObject.CompareTag("Player")) {
                other.gameObject.GetComponent<PlayerMovement>().Hit();
            }
        }
    }
}
