using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDown : MonoBehaviour
{
    public float descentSpeed = 2f;
    public Transform endPosition;
    public float playerMovementDistance = 5f;
    public float delayBeforePlayerMove = 2f;

    private bool isFlagLowered = false;
    private bool playerControlsDisabled = false;
    private Rigidbody playerRigidbody;
    private MarioMovement playerMovementScript;
    private Vector3 initialPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFlagLowered && !playerControlsDisabled)
        {
            LowerFlag();
            DisablePlayerControls(other.gameObject);
            Invoke("MovePlayerAfterDelay", delayBeforePlayerMove);
        }
    }

    private void DisablePlayerControls(GameObject player)
    {
        playerMovementScript = player.GetComponent<MarioMovement>();
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
            playerControlsDisabled = true;
            initialPosition = player.GetComponent<Rigidbody>().position;
        }
    }

    private void MovePlayerAfterDelay()
    {
        if (playerControlsDisabled)
        {
            playerRigidbody = playerMovementScript.GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        if (playerControlsDisabled)
        {
            if (playerRigidbody != null)
            {
                float moveSpeed = playerMovementDistance / delayBeforePlayerMove;
                float distanceCovered = moveSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(playerRigidbody.position + new Vector3(distanceCovered, 0f, 0f));
                playerMovementDistance -= distanceCovered;

                if (playerMovementDistance <= 0.001f)
                {
                    playerControlsDisabled = false;
                }
            }
        }

        if (isFlagLowered)
        {
            LowerFlagAnimation();
        }
    }

    private void LowerFlag()
    {
        isFlagLowered = true;
    }

    private void LowerFlagAnimation()
    {
        if (transform.position.y > endPosition.position.y)
        {
            float step = descentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);
        }
    }
}