using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class PlayerAgent : Agent
{
    [SerializeField] private End treasure;
    [SerializeField] private Transform gems;
    [SerializeField] private Transform spikes;
    CharacterState _characterState;

    public override void Initialize()
    {
        _characterState = GetComponent<CharacterState>();
        MaxStep = 1000;
    }

    public override void OnEpisodeBegin()
    {
        float[] possiblePositionX = { -3.5f, 3.5f, 2f, 1f, 6f, 5f };
        int randomIndex = Random.Range(0, possiblePositionX.Length);
        float positionX = possiblePositionX[randomIndex];

        spikes.transform.localPosition = new Vector3(positionX, -2.5f, 0);
        
        randomIndex = Random.Range(0, possiblePositionX.Length);
        positionX = possiblePositionX[randomIndex];
        gems.transform.localPosition = new Vector3(positionX, 0, 0);
        
        // transform.localPosition = new Vector3(2, 0, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 localPosition = transform.localPosition;

        Vector3 dirToTreasure = (treasure.transform.localPosition - localPosition).normalized;
        Vector3 dirToGem = (gems.transform.localPosition - localPosition).normalized;

        Debug.Log(dirToGem.x);

        sensor.AddObservation(dirToTreasure.x);
        sensor.AddObservation(dirToTreasure.y);

        sensor.AddObservation(dirToGem.x);
        sensor.AddObservation(dirToGem.y);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        _characterState.horizontal = actions.DiscreteActions[0] == 2 ? -1 : actions.DiscreteActions[0];
        _characterState.vertical = actions.DiscreteActions[3];
        if (MaxStep > 0) AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        // continuousActions[0] = Input.GetAxisRaw("Horizontal");
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     continuousActions[0] = 1;
        // }
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
        switch (col.gameObject.name)
        {
            case "Emerald":
                Debug.Log("Emerald Collected");
                AddReward(.5f);
                break;
            case "Spikes":
                Debug.Log("SPIKES FOUND");
                SetReward(-1f);
                EndEpisode();
                break;
            case "Firetrap":
                Debug.Log("Firetrap");
                SetReward(-1f);
                EndEpisode();
                break;
            case "Saw":
                Debug.Log("Saw");
                SetReward(-1f);
                EndEpisode();
                break;
            case "End":
                Debug.Log("End");
                AddReward(10f);
                EndEpisode();
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<End>(out End end))
        {
            Debug.Log("END REACHED");
            AddReward(1f);
            EndEpisode();
        }

        switch (col.gameObject.name)
        {
            case "Emerald":
                Debug.Log("Emerald Collected");
                AddReward(.5f);
                break;
            case "Spikes":
                Debug.Log("SPIKES FOUND");
                SetReward(-1f);
                EndEpisode();
                break;
            case "Firetrap":
                Debug.Log("Firetrap");
                SetReward(-1f);
                EndEpisode();
                break;
            case "Saw":
                Debug.Log("Saw");
                SetReward(-1f);
                EndEpisode();
                break;
            case "End":
                Debug.Log("End");
                AddReward(10f);
                EndEpisode();
                break;
        }
    }
}