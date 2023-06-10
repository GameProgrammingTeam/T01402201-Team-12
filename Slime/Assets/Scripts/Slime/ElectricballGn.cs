using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricballGn : MonoBehaviour
{
    [SerializeField] public Electricball ElectricballPrefab;
    [SerializeField] private float damage;
    [SerializeField] private int count;
    [SerializeField] public float speed;



    private Slime _slime;
    

    private float _currentTime;

    void Start()
    {

      
    }

    void Update()
    {
        
        transform.Rotate(Vector3.back * Time.deltaTime * speed * 10f);




        SetValues();
    }
    void CreateElectricballGn()
    {

        Electricball electricball = Instantiate(ElectricballPrefab, _slime.transform.position, Quaternion.identity);
        electricball.SetField(damage, count, speed);
        electricball.transform.parent = this.transform;
        
    }


    // slime의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(_slime, null))
        {
            ElectricballPrefab = _slime.ElectricballPrefab;
            damage = _slime.damage;
            speed = _slime.speed;
            if(count != _slime.count)
            {
                CreateElectricballGn();
            }
            count = _slime.count;
        }
    }

    // slime 연결
    public void SetSlime(GameObject slimeObject)
    {
        _slime = slimeObject.GetComponent<Slime>();
        transform.SetParent(slimeObject.transform, false);

    }
}
