using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class CameraMoveToObject : MonoBehaviour
{
    public GameObject A;
    public bool StartCamera = false;
    Transform AT;
    void Start()
    {
        AT = A.transform;
        


    }
    void Update()
    {
        
            transform.position = Vector3.Lerp(transform.position, AT.position, 0.8f * Time.deltaTime);
            transform.Translate(0, 0, 0);
        
    }
}