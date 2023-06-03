using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    private static DamageTextController _instance = null;
    private float destroyRange = 20.0f;

    public static DamageTextController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DamageTextController>();
            }
            return _instance;
        }

    }

    public Canvas MainCanvas;
    public GameObject dmgText;

    public void CreateDamageText(Vector3 hitPoint, float hitDamage)
    {
        GameObject damageText = Instantiate(dmgText, hitPoint, Quaternion.identity, MainCanvas.transform);
        damageText.GetComponent<TextMeshProUGUI>().text = hitDamage.ToString();
        DamageText damageTextComponent = damageText.GetComponent<DamageText>();
        damageTextComponent.SetRange(destroyRange);
    }
}
   
