using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static string targetSceneName;

    public static void Load(string targetSceneName)
    {
        Loader.targetSceneName = targetSceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
