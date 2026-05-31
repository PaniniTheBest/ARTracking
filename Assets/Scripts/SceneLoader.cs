using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class SceneLoader : MonoBehaviour
{
    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    System.Collections.IEnumerator GoToMenu()
    {
        ARSession session = FindFirstObjectByType<ARSession>();

        if (session != null)
        {
            session.Reset();
            session.enabled = false;
        }

        yield return null;

        SceneManager.LoadScene("MainMenu");
    }

    public void ImageTrackButton()
    {
        SceneManager.LoadScene("AR_TEST.MENUDO");
    }

    public void PlaneTrackButton()
    {
        SceneManager.LoadScene("TEST_ARScene_Nyx");
    }
}