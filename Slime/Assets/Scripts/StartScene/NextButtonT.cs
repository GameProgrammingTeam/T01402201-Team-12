using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonT : MonoBehaviour // 튜토리얼 화살표 버튼 
{
    public static int j;
    public Button NextButton;
    public GameObject TutorialBoard1;
    public GameObject TutorialBoard2;
    public GameObject TutorialBoard3;
    public GameObject TutorialBoard4;
    public GameObject Canvas;
    public GameObject CanvasTutorial;


    public void Awake()
    {
        NextButton.onClick.AddListener(() => OnClick(1));
    }

    public void OnClick(int i)
    {
        j += i;
        switch (j)
        {
            case 1:
                TutorialBoard1.gameObject.SetActive(false);
                TutorialBoard2.gameObject.SetActive(true);
                
                break;
            case 2:
                TutorialBoard2.gameObject.SetActive(false);
                TutorialBoard3.gameObject.SetActive(true);
                
                break;
            case 3:
                TutorialBoard3.gameObject.SetActive(false);
                TutorialBoard4.gameObject.SetActive(true);
                
                break;
            case 4:
                CanvasTutorial.gameObject.SetActive(false);
                TutorialBoard4.gameObject.SetActive(false);
                TutorialBoard1.gameObject.SetActive(true);
                Canvas.gameObject.SetActive(true);

                /*if (TutorialBoard1.gameObject.activeSelf == false)
                {
                    TutorialBoard1.gameObject.SetActive(true);
                }*/
                j = 0;
                break;
        }
       
        
        
    }
}
