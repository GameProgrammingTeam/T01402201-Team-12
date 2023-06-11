using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SFXButton : MonoBehaviour
{
    public Sprite OnIcon;
    public Sprite OffIcon;

    Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();

    }
    
    public void ToggleSound()
    {
        if (AudioListener.volume == 1)
        {
            buttonImage.sprite = OffIcon;
            AudioListener.volume = 0;
        }
        else
        {
            buttonImage.sprite = OnIcon;
            AudioListener.volume = 1;
        }
    }
}
