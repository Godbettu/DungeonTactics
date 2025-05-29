using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    public Button changeSceneToFarmCardButton;
    public Button changeSceneToMapButton;
    public Button changeSceneToShopButton;
    public Button changeSceneToInventoryButton;
    public Button changeSceneHTPButton;
    public Button changeSceneHTFarmButton;
    public Button changeSceneHome;

    //map
    public Button changeSceneMapLevelD;
    public Button changeSceneMapLevelC;
    public Button changeSceneMapLevelB;
    public Button changeSceneMapLevelA;
    public Button changeSceneMapLevelS;
    public Button changeSceneMapLevelU;

    void Start()
    {
        changeSceneHome.onClick.AddListener(() => ChangeToHome(0));
        changeSceneToFarmCardButton.onClick.AddListener(() => ChangeToFarmScene(1));
        changeSceneToMapButton.onClick.AddListener(() => ChangeToMapScene(2));
        changeSceneToInventoryButton.onClick.AddListener(() => ChangeToInventoryScene(3));
        changeSceneToShopButton.onClick.AddListener(() => ChangeToShopScene(4));
        changeSceneHTPButton.onClick.AddListener(() => ChangeToHTPScene(5));
        changeSceneHTFarmButton.onClick.AddListener(() => ChangeToHTFarmScene(6));

        //map
        changeSceneMapLevelD.onClick.AddListener(() => ChangeToLevelD(7));
        changeSceneMapLevelC.onClick.AddListener(() => ChangeToLevelC(8));
        changeSceneMapLevelB.onClick.AddListener(() => ChangeToLevelB(9));
        changeSceneMapLevelA.onClick.AddListener(() => ChangeToLevelA(10));
        changeSceneMapLevelS.onClick.AddListener(() => ChangeToLevelS(11));
        changeSceneMapLevelU.onClick.AddListener(() => ChangeToLevelU(12));

    }
    public void ChangeToHome(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeToFarmScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToMapScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToInventoryScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToShopScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToHTPScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToHTFarmScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadInventory()
    {
        SceneTracker.SetLastScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Inventory");
    }

    //map
    public void ChangeToLevelD(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToLevelC(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToLevelB(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToLevelA(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToLevelS(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeToLevelU(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
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
