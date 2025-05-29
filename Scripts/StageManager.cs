using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StageData
{
    public Button stageButton;
    public GameObject lockIcon;
    public int stageIndex;
}

public class StageManager : MonoBehaviour
{
    public List<StageData> stages;
    private int highestClearedStage;

    void Start()
    {
        LoadStageProgress();
        UpdateStageUI();
    }

    void LoadStageProgress()
    {
        highestClearedStage = PlayerPrefs.GetInt("HighestStage", 0);
    }

    void UpdateStageUI()
    {
        for (int i = 0; i < stages.Count; i++)
        {
            int actualStageIndex = stages[i].stageIndex;
            bool unlocked = actualStageIndex <= highestClearedStage;

            // à»Ô´ËÃ×Í»Ô´»ØèÁ
            stages[i].stageButton.interactable = unlocked;
            stages[i].lockIcon.SetActive(!unlocked);

            // âËÅ´´èÒ¹àÁ×èÍ¤ÅÔ¡
            int stageToLoad = actualStageIndex;
            stages[i].stageButton.onClick.RemoveAllListeners();
            stages[i].stageButton.onClick.AddListener(() =>
            {
                LoadStageScene(stageToLoad);
            });
        }
    }

    void LoadStageScene(int stageIndex)
    {
        string sceneName = "Scene_Stage" + (stageIndex + 1);
        SceneManager.LoadScene(sceneName);
    }
}
