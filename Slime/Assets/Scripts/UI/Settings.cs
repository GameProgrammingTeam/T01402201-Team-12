using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    GameObject PanelSettings;
    Image image;
    


    void Start()
    {
        PanelSettings = GameObject.Find("PanelSettings"); // 반투명
        image = PanelSettings.GetComponent<Image>();
        this.gameObject.SetActive(false);
        Color color = image.color;
        color.a = 0.75f;
        image.color = color;
    }

    private void Update()
    {
        
    }


    public void ClickPause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0; // 설정창 열람시 게임시간 멈춤

    }

    public void ClickBack()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ClickQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainTitleScene");
    }

   
}
