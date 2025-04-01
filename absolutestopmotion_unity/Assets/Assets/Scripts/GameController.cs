using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // The 10 states corresponding to the available frames for the small stop motion animation
    public enum StopMotionState
    {
        Frame0,
        Frame1,
        Frame2,
        Frame3,
        Frame4,
        Frame5,
        Frame6,
        Frame7,
        Frame8,
        Frame9
    }
    private StopMotionState currentState;

    // State getter
    public StopMotionState CurrentState
    {
        get { return currentState; }
    }

    public event Action<StopMotionState> OnGameStateChanged;
    public event Action<StopMotionState> OnSFXPlay;

    public ColliderController[] colliderController;

    private Coroutine delayedScreenshotCoroutine;
    private Coroutine idleSoundCoroutine;
    private Coroutine wrongSoundCoroutine;

    public AudioSource audioSource;
    public AudioClip introSound;
    public AudioClip wrongRedo;
    public AudioClip[] stateChangeSounds;
    public AudioClip[] wrongSounds;

    private float idleStartTime;

    private string screenshotsFolderPath = "Assets/Assets/Screenshots";

    public Canvas canvas;
    public RawImage photoWhiteScreen;
    public TextMeshProUGUI countdownText;

    void Start()
    {
        // Clear the screenshot folder at the start of the game
        ClearScreenshots();

        // Define the state as the 0 frame
        currentState = StopMotionState.Frame0;

        // Start the coroutine to play the intro and the first frame sound
        StartCoroutine(PlayIntroThenFrame0());
        // Start the coroutine to play the idle sounds (in case the user do not act for a while)
        idleSoundCoroutine = StartCoroutine(PlayIdleSounds());
        StartCoroutine(LateStart(0.5f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        OnGameStateChanged.Invoke(currentState);
    }

    public void ChangeState()
    {
        if ((int)currentState < System.Enum.GetValues(typeof(StopMotionState)).Length)
        {
            // Start the Coroutine to take a photo when the state change is called
            delayedScreenshotCoroutine = StartCoroutine(DelayedScreenshot());
        }
    }

    public void StopDelayedScreenshot()
    {
        // Stop the coroutine when the player goes out of the collider
        if (delayedScreenshotCoroutine != null)
        {
            countdownText.gameObject.SetActive(false);
            StopCoroutine(delayedScreenshotCoroutine);
            delayedScreenshotCoroutine = null;
            UnityEngine.Debug.Log("Delayed Screenshot interrompu.");
            idleSoundCoroutine = StartCoroutine(PlayIdleSounds());
        }
    }

    IEnumerator DelayedScreenshot()
    {
        // Stop the idle sound coroutine
        if (idleSoundCoroutine != null)
        {
            StopCoroutine(idleSoundCoroutine);
            idleSoundCoroutine = null;
        }
        if (wrongSoundCoroutine != null)
        {
            StopCoroutine(wrongSoundCoroutine);
            wrongSoundCoroutine = null;
        }

        countdownText.gameObject.SetActive(true);
        for (int countdown = 5; countdown > 0; countdown--)
        {
            countdownText.text = countdown.ToString();
            UnityEngine.Debug.Log($"Capture d'écran dans : {countdown} secondes...");
            yield return new WaitForSeconds(1f);
        }
        countdownText.gameObject.SetActive(false);

        // Photo whitescreen effect
        photoWhiteScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        photoWhiteScreen.gameObject.SetActive(false);

        if (canvas != null)
        {
            canvas.enabled = false;
        }

        string filename = screenshotsFolderPath + "/Frame_" + (int)CurrentState + ".png";
        ScreenCapture.CaptureScreenshot(filename);

        // Wait for a bit to assure that the screen was correctly taken
        yield return new WaitForSeconds(0.2f);

        if (canvas != null)
        {
            canvas.enabled = true;
        }

        if (currentState == StopMotionState.Frame9)
        {
            UnityEngine.Debug.Log("Dernier état atteint, création du GIF...");
            CreateMP4();
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            currentState = (StopMotionState)((int)currentState + 1);

            // Invoke the event to notify the subscribers of the state change
            OnGameStateChanged.Invoke(currentState);

            // Play the sound corresponding to the new state
            StartCoroutine(PlayStateChangeSound());
        }
    }

    IEnumerator PlayIntroThenFrame0()
    {
        // Play the intro sound
        if (audioSource != null && introSound != null)
        {
            audioSource.PlayOneShot(introSound);
            yield return new WaitForSeconds(introSound.length);
        }

        // Play the sound corresponding to the first frame
        StartCoroutine(PlayStateChangeSound());
    }

    IEnumerator PlayStateChangeSound()
    {
        if (idleSoundCoroutine != null)
        {
            StopCoroutine(idleSoundCoroutine);
            idleSoundCoroutine = null;
        }

        int stateIndex = (int)currentState;
        if (audioSource != null && stateChangeSounds != null && stateIndex < stateChangeSounds.Length)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(stateChangeSounds[stateIndex]);
            if (OnSFXPlay != null)
            {
                OnSFXPlay.Invoke(currentState);
            }

            yield return new WaitForSeconds(stateChangeSounds[stateIndex].length);

            idleSoundCoroutine = StartCoroutine(PlayIdleSounds());
        }
        else
        {
            UnityEngine.Debug.LogWarning("Pas de son assigné pour l’état : " + currentState);
        }
    }

    IEnumerator PlayIdleSounds()
    {
        idleStartTime = Time.time;
        while (true)
        {
            float randomDelay = UnityEngine.Random.Range(15f, 35f);
            yield return new WaitForSeconds(randomDelay);

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (Time.time - idleStartTime >= 60f)
            {
                if (wrongSoundCoroutine == null)
                {
                    wrongSoundCoroutine = StartCoroutine(PlayWrongRedoSequence());
                    break;
                }
            }
            else if (audioSource != null && wrongSounds.Length > 0)
            {
                AudioClip randomSound = wrongSounds[UnityEngine.Random.Range(0, wrongSounds.Length)];
                audioSource.PlayOneShot(randomSound);
            }
        }
    }

    IEnumerator PlayWrongRedoSequence()
    {
        if (audioSource != null && wrongRedo != null)
        {
            audioSource.PlayOneShot(wrongRedo);
            yield return new WaitForSeconds(wrongRedo.length);
        }

        StartCoroutine(PlayStateChangeSound());
    }

    void ClearScreenshots()
    {
        if (Directory.Exists(screenshotsFolderPath))
        {
            string[] files = Directory.GetFiles(screenshotsFolderPath);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                    UnityEngine.Debug.Log("Fichier supprimé : " + file);
                }
                catch (System.Exception e)
                {
                    UnityEngine.Debug.LogError("Erreur lors de la suppression du fichier : " + e.Message);
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Le dossier Screenshots n'existe pas à l'emplacement spécifié.");
        }
    }

    void CreateMP4()
    {
        string screenshotsFolder = screenshotsFolderPath;
        string outputPath = Path.Combine(screenshotsFolder, "output.mp4");

        // FFmpeg commande pour assembler les images en vidéo
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "/opt/homebrew/bin/ffmpeg",
            Arguments = $"-framerate 3 -i \"{screenshotsFolder}/Frame_%d.png\" -c:v libx264 -crf 18 -b:v 5000k -pix_fmt yuv420p \"{outputPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = psi };
        process.Start();
        process.WaitForExit();

        string errorOutput = process.StandardError.ReadToEnd();
        UnityEngine.Debug.LogError("FFmpeg Error: " + errorOutput);

        if (process.ExitCode == 0)
        {
            UnityEngine.Debug.Log("MP4 created at: " + outputPath);
        }
        else
        {
            UnityEngine.Debug.LogError("FFmpeg MP4 creation failed.");
        }
    }
}
