using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy) {
        enemy.GetComponent<Renderer>().material = enemy.RetreatMaterial;
    }
    
    public void UpdateState(Enemy enemy) {
        if (enemy.player != null) {
            enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;
        }
    }
    
    public void ExitState(Enemy enemy) {
        enemy.GetComponent<Renderer>().material = enemy.BaseMaterial;
    }
}
