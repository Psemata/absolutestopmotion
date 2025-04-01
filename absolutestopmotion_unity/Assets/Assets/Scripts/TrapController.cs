using System;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public GameController stopMotionController;
    public GameController.StopMotionState targetState;

    void Start()
    {
        stopMotionController.OnGameStateChanged += OnTrapMoving;

    }

    private void OnTrapMoving(GameController.StopMotionState state)
    {
        if (stopMotionController.CurrentState == targetState)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.25f);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.36f);
        }
    }
}
