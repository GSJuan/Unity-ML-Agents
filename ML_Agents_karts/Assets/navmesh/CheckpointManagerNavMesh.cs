using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagerNavMesh : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Checkpoint nextCheckPointToReach;
    public bool continuous = false;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;

    public event Action<Checkpoint> reachedCheckpoint; 

    void Start()
    {
        Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        ResetCheckpoints();
        agent.SetDestination(nextCheckPointToReach.transform.position);
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;        
        SetNextCheckpoint();
    }

    private void Update()
    {
        //
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            //ended track
            if (continuous)
            {
                Debug.Log("Finished current Lap, starting next one");
                ResetCheckpoints();
            }
            else
            {
                Debug.Log("Finished");
                return;
            }
        }
        else
        {
            SetNextCheckpoint();
            
        }
        
        agent.SetDestination(nextCheckPointToReach.transform.position);
    }

    private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
        }
    }
}
