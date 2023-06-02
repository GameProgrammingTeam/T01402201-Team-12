using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Score, Time, Health, None}
    public InfoType type;
    GameObject obj;

    TextMeshProUGUI myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>(); // textmeshpro 사용시 초기화...
        mySlider = GetComponent<Slider>();
        obj = GameObject.Find("GameManager");
    }

    private void Update()
    {
        
    }

    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float CurExp = obj.GetComponent<SlimeManager>().exp;
                float MaxExp = obj.GetComponent<SlimeManager>().nextExp[obj.GetComponent<SlimeManager>().level];
                mySlider.value = CurExp / MaxExp;
                break;
            case InfoType.Level:
                
                myText.text = string.Format("Lv.{0:F0}", obj.GetComponent<SlimeManager>().level);
                break;
            case InfoType.Score:
                myText.text = string.Format("{0:F0}", obj.GetComponent<SlimeManager>().score);
                break;
            case InfoType.Time:
                /*
                float remainTime = obj.GetComponent<GameManager>().maxGameTime - obj.GetComponent<GameManager>().gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                */
                float curTime = obj.GetComponent<GameManager>().gameTime;
                int min = Mathf.FloorToInt(curTime / 60);
                int sec = Mathf.FloorToInt(curTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float CurHp = obj.GetComponent<SlimeManager>().health;
                float MaxHp = obj.GetComponent<SlimeManager>().maxHealth;
                mySlider.value = CurHp / MaxHp;
                break;
        }   
    }
}
