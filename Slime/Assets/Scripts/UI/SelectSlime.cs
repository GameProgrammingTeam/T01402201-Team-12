using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class SelectSlime : MonoBehaviour
{
    public SlimeManager slimeManager;
    [SerializeField] public SlimeSet slime;
      

    
    // Start is called before the first frame update
    void Start()
    {
        
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
                break;
            case SlimeSet.Fire:
                slimeManager.PlayerID = 18;
                break;
            case SlimeSet.Sand:
                slimeManager.PlayerID = 38;
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
