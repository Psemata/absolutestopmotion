using UnityEngine;

public class CharacterASMController : MonoBehaviour
{
    public GameController gameController;
    public GameObject[] characterModels;
    private GameObject currentModel;

    void Start()
    {
        if (characterModels.Length > 0)
        {
            currentModel = Instantiate(characterModels[(int)gameController.CurrentState], transform.position, characterModels[(int)gameController.CurrentState].transform.rotation);
            currentModel.transform.localPosition.Set(currentModel.transform.localPosition.x, currentModel.transform.localPosition.y - 0.5f, currentModel.transform.localPosition.z);
            currentModel.transform.SetParent(transform);
        }

        if (gameController != null)
        {
            gameController.OnGameStateChanged += SwitchModel;
        }
    }

    void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.OnGameStateChanged -= SwitchModel;
        }
    }

    public void SwitchModel(GameController.StopMotionState newState)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        currentModel = Instantiate(characterModels[(int)gameController.CurrentState], transform.position, characterModels[(int)gameController.CurrentState].transform.rotation);
        currentModel.transform.localPosition.Set(currentModel.transform.localPosition.x, currentModel.transform.localPosition.y - 0.5f, currentModel.transform.localPosition.z);
        currentModel.transform.SetParent(transform);
    }
}
