using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class SelectSlime : MonoBehaviour
{
    public SlimeManager slimeManager;
    GameObject Slime;
    [SerializeField] public SlimeSet slime;
      

    
    // Start is called before the first frame update
    void Start()
    {
        Slime = GameObject.Find("Slime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAspect()
    {
        switch (slime)
        {
            case SlimeSet.Blue:
                slimeManager.PlayerID = 0;
                AudioManager.instance.PlayBgm(true);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Click2);
                Slime.GetComponent<Slime>().CreateAttacker();
                break;
            case SlimeSet.Fire:
                slimeManager.PlayerID = 18;
                AudioManager.instance.PlayBgm(true);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Click2);
                Slime.GetComponent<Slime>().CreateFireballGn();
                break;
            case SlimeSet.Lightning:
                slimeManager.PlayerID = 28;
                AudioManager.instance.PlayBgm(true);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Click2);
                Slime.GetComponent<Slime>().CreateElectricballGn();

                break;
            case SlimeSet.Vine:
                slimeManager.PlayerID = 46;
                break;

        }
        


    }
    /*
    public void SelectFire()
    {
        slime = slimeManager.gameObject.GetComponent<SlimeManager>().slime.Fire;
        
    }

    public void SelectSand()
    {
        slimeManager.gameObject.GetComponent<SlimeManager>().slime = (int).38;
    }

    public void SelectVine()
    {
        slimeManager.gameObject.GetComponent<SlimeManager>().slime = (int).46;
    }
    */
}
