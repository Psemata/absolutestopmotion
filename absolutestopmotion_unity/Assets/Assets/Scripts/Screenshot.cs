using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.IO;

public class ScreenshotTaker : MonoBehaviour
{
    // Chemin vers le dossier Screenshots
    private string screenshotsFolderPath = "Assets/Assets/Screenshots"; // Remplace par le chemin correct
    private float lastCaptureTime = 0f;
    public float captureCooldown = 0.5f; // Temps minimum entre deux captures


    void Start()
    {
        ClearScreenshots();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time - lastCaptureTime > captureCooldown)
        {
            lastCaptureTime = Time.time;
            string filename = screenshotsFolderPath + "/Screenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            ScreenCapture.CaptureScreenshot(filename);
            Debug.Log("Screenshot saved as " + filename);
        }
    }

    void ClearScreenshots()
    {
        if (Directory.Exists(screenshotsFolderPath))
        {
            // Récupérer tous les fichiers dans le dossier Screenshots
            string[] files = Directory.GetFiles(screenshotsFolderPath);

            // Supprimer chaque fichier
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                    Debug.Log("Fichier supprimé : " + file);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Erreur lors de la suppression du fichier : " + e.Message);
                }
            }
        }
        else
        {
            Debug.LogWarning("Le dossier Screenshots n'existe pas à l'emplacement spécifié.");
        }
    }
}
