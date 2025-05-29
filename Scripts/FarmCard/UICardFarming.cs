using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICardFarming : MonoBehaviour
{
    public GameObject PanelMenu;


    private void Start()
    {
        
    }

    public void OnClickMenu(bool value)
    {
        PanelMenu.SetActive(value);
    }

    public void OnClickCloseMenu(bool value)
    {
        PanelMenu?.SetActive(false);
    }

}
