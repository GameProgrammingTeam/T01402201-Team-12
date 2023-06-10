using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class Electricball : MonoBehaviour
{
    private Camera _camera;
    GameObject slimeobj;

    [SerializeField] public float damage;
    [SerializeField] private int count;
    [SerializeField] public float speed;
    [SerializeField] public float deg;
    [SerializeField] public float circleR;

    private void Start()
    {

        Transform electricball = this.transform;
        electricball.parent = transform;

        electricball.Translate(electricball.up * 3f, Space.World);

        slimeobj = GameObject.Find("Slime");
        
    }

    private void Update()
    {
        _camera = Camera.main;
        
        deg += Time.deltaTime * speed;
        if (deg < 360)
        {
            for (int i = 0; i < count; i++)
            {
                var rad = Mathf.Deg2Rad * (deg + (i * (360 / count)));
                var x = circleR * Mathf.Sin(rad);
                var y = circleR * Mathf.Cos(rad);
                this.transform.position = transform.position + new Vector3(x, y);
                this.transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / count))) * -1);
            }

        }
        else
        {
            deg = 0;
        }


        damage = slimeobj.GetComponent<Slime>().damage;
    }

    // Start is called before the first frame update
    public void  SetField(float damage, int count, float speed)
    {
        this.damage = damage;
        this.count = count;
        this.speed = speed;

    }
}
