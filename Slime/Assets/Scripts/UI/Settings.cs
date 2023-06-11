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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click3);

    }

    public void ClickBack()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click1);
    }

    public void ClickRestart()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Retry);
        Invoke("Loadscene", 1f);

    }


    public void ClickQuit()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Quite);
        SceneManager.LoadScene("MainTitleScene");
    }

    public void Loadscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
