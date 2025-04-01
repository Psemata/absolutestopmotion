using System;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public GameController stopMotionController;
    public GameController.StopMotionState targetState;

    void Start()
    {
        UpdateColliderState();
    }

    void Update()
    {
        UpdateColliderState();
    }

    private void UpdateColliderState()
    {
        if (stopMotionController.CurrentState == targetState)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stopMotionController.ChangeState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stopMotionController.StopDelayedScreenshot();
        }
    }
}
