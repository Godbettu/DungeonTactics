using UnityEngine;

public static class SceneTracker
{
    public static int lastSceneIndex = -1;

    public static void SetLastScene(int index)
    {
        lastSceneIndex = index;
    }

    public static int GetLastScene()
    {
        return lastSceneIndex;
    }
}
