using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aaaa : MonoBehaviour
{

 

   public Vector3 initpost;
    
    [SerializeField] float magnitude = 1;
    [SerializeField] float timedelay;

    [SerializeField] float shaketolerance = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        initpost = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
          
         
    }

    void Start()
{
    StartCoroutine(shake());
}

IEnumerator shake()
{
    for (;;)
    {
         transform.position = Vector2.Lerp(transform.position, initpost + Random.insideUnitSphere * magnitude, shaketolerance);
        //transform.position = initpost + Random.insideUnitSphere * magnitude;  
        yield return new WaitForSecondsRealtime(timedelay);
    }
}



}
