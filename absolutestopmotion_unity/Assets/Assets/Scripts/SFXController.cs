using System.Collections;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public GameController gameController;

    public AudioSource sfxAudioSource;

    public AudioClip boulderAppear;
    public AudioClip boulderFalling;
    public AudioClip boulderRolling;

    public AudioClip snakes;

    public AudioClip spiders;
    public AudioClip spiderWeb;

    public AudioClip trap;

    void Start()
    {
        if (gameController != null)
        {
            gameController.OnSFXPlay += PlaySFX;
            Debug.Log("SFXController: abonné à OnGameStateChanged");
        }
    }

    void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.OnSFXPlay -= PlaySFX;
        }
    }

    public void PlaySFX(GameController.StopMotionState newState)
    {
        switch (newState)
        {
            case GameController.StopMotionState.Frame0:
                Invoke("PlayBoulderAppear", 10f);
                Invoke("PlayTrap", 15f);
                break;
            case GameController.StopMotionState.Frame1:
                Invoke("PlayBoulderRolling", 6f);
                break;
            case GameController.StopMotionState.Frame2:
                Invoke("PlaySpiderWeb", 6f);
                break;
            case GameController.StopMotionState.Frame3:
                break;
            case GameController.StopMotionState.Frame4:
                Invoke("PlaySnakes", 6f);
                break;
            case GameController.StopMotionState.Frame5:
                Invoke("PlaySpiders", 6f);
                break;
            case GameController.StopMotionState.Frame6:
                Invoke("PlayTrap", 6f);
                break;
            case GameController.StopMotionState.Frame7:
                Invoke("PlayBoulderRolling", 6f);
                break;
            case GameController.StopMotionState.Frame8:
                break;
            case GameController.StopMotionState.Frame9:
                Invoke("PlayBoulderFalling", 6f);
                break;
            default:
                break;
        }
    }

    private void PlayBoulderAppear()
    {
        Debug.Log("Play boulder appear");
        sfxAudioSource.PlayOneShot(boulderAppear);
    }

    private void PlayBoulderFalling()
    {
        sfxAudioSource.PlayOneShot(boulderFalling);
    }

    private void PlayBoulderRolling()
    {
        sfxAudioSource.PlayOneShot(boulderRolling);
    }

    private void PlaySnakes()
    {
        sfxAudioSource.PlayOneShot(snakes);
    }

    private void PlaySpiders()
    {
        sfxAudioSource.PlayOneShot(spiders);
    }

    private void PlaySpiderWeb()
    {
        sfxAudioSource.PlayOneShot(spiderWeb);
    }

    private void PlayTrap()
    {
        sfxAudioSource.PlayOneShot(trap);
    }
}
