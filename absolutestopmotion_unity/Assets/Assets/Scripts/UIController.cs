using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class UIController : MonoBehaviour
{
    public RawImage[] filmReelProgressionImages;
    private string screenshotsFolder = "Assets/Assets/Screenshots";
    private string[] frames;

    void Start()
    {
        frames = new string[0];

        // Vérifie si filmReelProgressionImages est bien assigné
        if (filmReelProgressionImages == null || filmReelProgressionImages.Length == 0)
        {
            Debug.LogError("FilmReelProgressionImages is not assigned or is empty.");
            return;
        }

        // Démarrer la Coroutine pour vérifier les images régulièrement
        StartCoroutine(LoadFramesFromDirectory());
    }

    IEnumerator LoadFramesFromDirectory()
    {
        if (!Directory.Exists(screenshotsFolder))
        {
            Debug.LogError("Screenshots folder does not exist: " + screenshotsFolder);
            yield break;
        }

        while (true)
        {
            string[] newFrames = Directory.GetFiles(screenshotsFolder, "Frame_*.png");

            if (newFrames.Length != frames.Length || frames.Length == 0)
            {
                frames = newFrames;
                Debug.Log("Frames updated: " + frames.Length);

                int count = Mathf.Min(frames.Length, filmReelProgressionImages.Length);
                for (int i = 0; i < count; i++)
                {
                    if (filmReelProgressionImages[i] != null)
                    {
                        string filePath = frames[i];
                        if (File.Exists(filePath))
                        {
                            byte[] imageBytes = File.ReadAllBytes(filePath);
                            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                            texture.LoadImage(imageBytes);
                            texture.Apply();

                            // Appliquer la texture à RawImage
                            filmReelProgressionImages[i].texture = texture;

                            // Ajuster le ratio 16:9 avec AspectRatioFitter
                            AspectRatioFitter fitter = filmReelProgressionImages[i].GetComponent<AspectRatioFitter>();
                            if (fitter != null)
                            {
                                fitter.aspectRatio = (float)texture.width / texture.height;
                            }
                        }
                        else
                        {
                            Debug.LogError("File does not exist: " + filePath);
                        }
                    }
                    else
                    {
                        Debug.LogError("RawImage at index " + i + " is not assigned.");
                    }
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
