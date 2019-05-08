using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flirt : MonoBehaviour
{
    [SerializeField] Vector3 escapespeed;

    Vector3 initpost;
    
   [SerializeField]  float initmagnitude;
   [SerializeField]  float inittimedelay;

   [SerializeField] float magnitude;
   [SerializeField] float timedelay;

    [SerializeField] float shaketolerance = 0.1f;
    [SerializeField] GameObject sad,love;
    [SerializeField] float step;
    [SerializeField] GameObject boomsad,sadchmoora;
    public GameObject serduchoprefab;
    [SerializeField] GameObject kafelkiobj;
    public bool isserducho =false;


     void OnEnable() {
     PaluchBehaviour.lovelove += Spawnlove;   
     PaluchBehaviour.sadsad += Spawnsad; 
     magnitude = initmagnitude;
     timedelay = inittimedelay;

    }

    void OnDisable() {
     PaluchBehaviour.lovelove -= Spawnlove;   
     PaluchBehaviour.sadsad -= Spawnsad; 
    }


    void Awake()
    {
        initpost = transform.position;
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

    void Update() {
        if (timedelay<0)
        {
            magnitude = 1;
             timedelay = 1;
            Nextscene("lose");
            
        }    
        if(magnitude<0)
        {
            magnitude =1;
            isserducho = true;
            Nextscene("plotka");
        }
    }
 
    void Spawnsad()
    {
        GameObject t = Instantiate(sad,transform.position,Quaternion.identity);
        magnitude += step;
        timedelay -= step/10;
        StartCoroutine(Move(t));
    }

    void Spawnlove()
    {
        GameObject t = Instantiate(love,transform.position,Quaternion.identity);
         magnitude -= step;
         timedelay += step/10;
        StartCoroutine(Move(t));

    }

    IEnumerator Move(GameObject tak)
    {
        tak.GetComponent<Rigidbody2D>().velocity= escapespeed;
        Destroy(tak,1);

        yield return null;
    }

    void Nextscene(string state)
    {
        switch(state)
        {
            case "lose":
            StartCoroutine(Sceneloader());
            break;

            case "plotka":
            StartCoroutine(plotkaloader());
            break;


            

        }
        //if(SceneManager.sce)
    }

    IEnumerator Sceneloader()
    {
        Instantiate(boomsad,gameObject.transform.position,Quaternion.identity);
         gameObject.GetComponent<SpriteRenderer>().enabled = false;
         Instantiate(sadchmoora,gameObject.transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1);

        Instantiate(boomsad,kafelkiobj.transform.position,Quaternion.identity);
        Destroy(kafelkiobj);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Single);

        Destroy(gameObject);

        yield return null;
    }
     IEnumerator plotkaloader()
    {

        Destroy(kafelkiobj);
        yield return new WaitForSeconds(1);

        Instantiate(boomsad,new Vector3(0,0,0),Quaternion.identity);
    
        
        yield return new WaitForSeconds(1);

        Instantiate(serduchoprefab,new Vector3(0,0,0),Quaternion.identity);

        

        yield return null;
    }
}
