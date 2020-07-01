using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    public GameObject thePanel;
    public GameObject closeBtn;
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void InfoGamePanelChange()
    {
        SceneManager.LoadScene("Info Scene");
    }
    public void GoBack()
    {
        thePanel.SetActive(!thePanel.activeInHierarchy);
        closeBtn.SetActive(!closeBtn.activeInHierarchy);
        
    }
}
