using UnityEngine;

public class BoulderController : MonoBehaviour
{

    public GameController gameController;

    public float rotationSpeed = 100f;

    public Vector3[] positions;

    private Vector3 targetPosition;

    void Start()
    {
        UpdateTargetPosition();

        if (gameController != null)
        {
            gameController.OnGameStateChanged += Move;
        }
    }

    void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.OnGameStateChanged -= Move;
        }
    }

    public void Move(GameController.StopMotionState newState)
    {
        targetPosition = positions[(int)newState];

        transform.localPosition = targetPosition;
    }

    void UpdateTargetPosition()
    {
        targetPosition = positions[(int)gameController.CurrentState];
    }
}
