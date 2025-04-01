using System;
using UnityEngine;

public class SetController : MonoBehaviour
{
    public GameController stopMotionController;
    public GameController.StopMotionState targetState;

    void Start()
    {
        stopMotionController.OnGameStateChanged += OnSetAppearing;
    }

    private void OnSetAppearing(GameController.StopMotionState state)
    {
        if (stopMotionController.CurrentState == targetState)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
