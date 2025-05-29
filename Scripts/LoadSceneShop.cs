using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneShop : MonoBehaviour
{
    public void OpenShop()
    {
        SceneTracker.SetLastScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Shop");
    }
    public void BackToLastScene()
    {
        int lastSceneIndex = SceneTracker.GetLastScene();

        if (lastSceneIndex != -1)
        {
            SceneManager.LoadScene(lastSceneIndex);
        }
        else
        {
            Debug.LogWarning("No previous scene index stored!");
        }
    }
}
