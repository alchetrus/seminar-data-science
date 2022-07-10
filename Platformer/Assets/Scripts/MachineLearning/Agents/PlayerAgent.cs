using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.Burst;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor.Search;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class PlayerAgent : Agent
{
    [SerializeField] private End treasure;
    [SerializeField] private GameObject[] gems;
    [SerializeField] private GameObject[] spikes;
    [SerializeField] private GameObject[] saws;
    [SerializeField] private GameObject[] fireTraps;
    [SerializeField] private GameObject[] enemyPatrols;
    [SerializeField] private GameObject[] arrowTraps;

    CharacterState _characterState;

    public override void Initialize()
    {
        _characterState = GetComponent<CharacterState>();
        MaxStep = 300;
    }

    public override void OnEpisodeBegin()
    {
        float[] possiblePositionX = { -3.5f, 3.5f, 2f, 1f, 6f, 5f };
        float[] possiblePositionY = { -2.5f, -1.5f, -1f, 0f, 1f, 1.5f, 2.5f };
        int randomIndex = Random.Range(0, possiblePositionX.Length);
        int randomIndexY = Random.Range(0, possiblePositionY.Length);
        float positionX = possiblePositionX[randomIndex];
        float positionY = possiblePositionY[randomIndexY];

        /*foreach (var spike in spikes)
        {
            spike.transform.localPosition = new Vector3(positionX, -2.5f, 0);
            randomIndex = Random.Range(0, possiblePositionX.Length);
            positionX = possiblePositionX[randomIndex];
        }


        foreach (var gem in gems)
        {
            // gem.SetActive(true);
            gem.transform.localPosition = new Vector3(positionX, positionY, 0);
            randomIndexY = Random.Range(0, possiblePositionY.Length);
            positionY = possiblePositionY[randomIndexY];
        }*/
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 localPosition = transform.localPosition;

        Vector3 dirToTreasure = (treasure.transform.localPosition - localPosition).normalized;

        List<Vector3> dirToGems = new List<Vector3>();
        foreach (var gem in gems)
        {
            dirToGems.Add((gem.transform.localPosition - localPosition).normalized);
        }

        foreach (var dirToGem in dirToGems)
        {
            sensor.AddObservation(dirToGem.x);
            sensor.AddObservation(dirToGem.y);
        }

        List<Vector3> dirToTraps = new List<Vector3>();
        foreach (var spike in spikes)
        {
            dirToTraps.Add((spike.transform.localPosition - localPosition).normalized);
        }

        foreach (var dirToTrap in dirToTraps)
        {
            sensor.AddObservation(dirToTrap.x);
            sensor.AddObservation(dirToTrap.y);
        }

        sensor.AddObservation(dirToTreasure.x);
        sensor.AddObservation(dirToTreasure.y);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        _characterState.horizontal = actions.DiscreteActions[0] == 2 ? -1 : actions.DiscreteActions[0];
        _characterState.vertical = actions.DiscreteActions[3];
        if (_characterState.horizontal == 1)
        {
            AddReward(.05f);
        }
        if (MaxStep > 0) AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1:
                discreteActions[0] = 2;
                break;
            case 0:
                discreteActions[0] = 0;
                break;
            case +1:
                discreteActions[0] = 1;
                break;
        }

        discreteActions[1] = Input.GetButton("Pull") ? 1 : 0;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.TryGetComponent<End>(out End end))
        {
            Debug.Log("END REACHED");
            AddReward(30f);
            EndEpisode();
        }
        switch (col.gameObject.name)
        {
            case "Emerald":
                Debug.Log("Emerald Collected");
                AddReward(1f);
                break;
            case "Spikes":
                Debug.Log("SPIKES FOUND");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "Firetrap":
                Debug.Log("Firetrap");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "Saw":
                Debug.Log("Saw");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "End":
                Debug.Log("End");
                AddReward(20f);
                EndEpisode();
                break;
            case "EnemyPatrol":
                Debug.Log("Patrol");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "ArrowTrap":
                Debug.Log("ArrowTrap");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "FireTrap":
                Debug.Log("FireTrap");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "WallLeft":
                Debug.Log("Wall");
                SetReward(-10f);
                EndEpisode();
                break;
            case "WallRight":
                Debug.Log("Wall");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<End>(out End end))
        {
            Debug.Log("END REACHED");
            AddReward(30f);
            EndEpisode();
        }

        switch (col.gameObject.name)
        {
            case "Emerald":
                Debug.Log("Emerald Collected");
                AddReward(1f);
                break;
            case "Spikes":
                Debug.Log("SPIKES FOUND");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "Firetrap":
                Debug.Log("Firetrap");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "Saw":
                Debug.Log("Saw");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "End":
                Debug.Log("End");
                AddReward(10f);
                EndEpisode();
                break;
            case "EnemyPatrol":
                Debug.Log("Patrol");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "ArrowTrap":
                Debug.Log("ArrowTrap");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "FireTrap":
                Debug.Log("FireTrap");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
            case "WallLeft":
                Debug.Log("Wall");
                SetReward(-10f);
                EndEpisode();
                break;
            case "WallRight":
                Debug.Log("Wall");
                //SetReward(-1f);
                AddReward(-10f);
                EndEpisode();
                break;
        }
    }
}