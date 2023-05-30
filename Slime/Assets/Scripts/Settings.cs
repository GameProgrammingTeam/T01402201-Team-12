using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    GameObject PanelSettings;
    Image image;


    void Start()
    {
        PanelSettings = GameObject.Find("PanelSettings"); // ������
        image = PanelSettings.GetComponent<Image>();
        this.gameObject.SetActive(false);
        Color color = image.color;
        color.a = 0.75f;
        image.color = color;
    }

    private void Update()
    {
        /*if (PanelSettings.activeSelf)
        {
            Color color = image.color;
            color.a = 200;
            image.color = color;
        }*/
    }


    public void ClickSettings()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0; // ����â ������ ���ӽð� ����

    }

    public void ClickBack()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

   
}
