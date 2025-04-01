using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private string videoPath = "Assets/Assets/Screenshots/output.mp4";

    void Start()
    {
        videoPlayer.url = "file://" + videoPath;
        videoPlayer.Play();
        Invoke("ChangeScene", 15f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
