using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaluchBehaviour : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction sadsad;
    public static event ClickAction lovelove;


    [SerializeField] Vector3 starthero;
    [SerializeField] Vector3 startgirl;
    [SerializeField] Vector3 chatstep;

    [SerializeField] GameObject[] responselist;
    [SerializeField] GameObject dym;

    [SerializeField]   Collider2D[] collist;
    [SerializeField]   List<Collider2D> kafcollist;
    [SerializeField]   List<GameObject> chathero;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public GameObject prefab;
    [SerializeField] float destdelay= 1;
    [SerializeField] float fadestep= 0.1f;
    public LayerMask m_LayerMask;
    bool now;

    [SerializeField] GameObject prefabserca;
 
    // Use this for initialization
    void Start () {
   
    }
   
    // Update is called once per frame
    void Update () {

            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
            

        if (Input.GetMouseButtonDown(0))
        {
            mycollider();
            
            for (int i = 0; i < collist.Length; i++)
            {
                if ((collist[i].tag == "love")|| (collist[i].tag == "sad"))
                {
                    kafcollist.Add(collist[i]);
                } 
            }

            int randpos = Random.Range(0,collist.Length);
            Debug.Log(randpos);
            if (collist[randpos].tag == "love" )
            {
                Debug.Log(collist[randpos].name);
                Debug.Log(collist[randpos].tag);
                //lovelove();
                 chatupdate(collist[randpos].gameObject);
                 StartCoroutine(response("love"));
            }
            else if (collist[randpos].tag == "sad" )
            {
                Debug.Log(collist[randpos].name);
                Debug.Log(collist[randpos].tag);
               // sadsad();
                chatupdate(collist[randpos].gameObject);
                StartCoroutine(response("sad"));
            }
             else if (collist[randpos].tag == "serducho" )
            {
                Debug.Log(collist[randpos].name);
                Debug.Log(collist[randpos].tag);
               // sadsad();
                chatupdate(collist[randpos].gameObject);
                StartCoroutine(response("serducho"));
            }

            kafcollist.Clear();

            GameObject tak = Instantiate(prefab,transform.position,Quaternion.identity);
            StartCoroutine(Scaleandfade(tak));

            
        }}

IEnumerator Scaleandfade(GameObject tak) 
{
    for (float f = 1f; f >= 0; f -= fadestep) 
    {
        Material m = tak.GetComponent<SpriteRenderer>().material;
        m.color =  new Color(m.color.r,m.color.g,m.color.b,m.color.a - fadestep);
        yield return null;
    }
    Destroy(tak);
}
 
void mycollider()
{   Vector2 v2 = new Vector2(gameObject.transform.position.x +gameObject.GetComponent<CircleCollider2D>().offset.x- transform.parent.position.x,gameObject.transform.position.y + gameObject.GetComponent<CircleCollider2D>().offset.y - transform.parent.position.y);
    collist = Physics2D.OverlapCircleAll(v2, gameObject.GetComponent<CircleCollider2D>().radius, m_LayerMask);
}

void chatupdate(GameObject tak)
{
    now = true;
    GameObject nie;
    GameObject wee;

    if ((tak.tag == "love")||(tak.tag == "sad")||(tak.tag == "serducho"))
    {
    nie = Instantiate(tak,starthero,Quaternion.identity);    
    wee = Instantiate(dym,starthero,Quaternion.identity);
    wee.transform.parent = nie.transform;
    //wee.transform.position = new Vector3(0,0,0);    
    }
    else
    {
       if (tak.tag =="serduchogirl")
       {
        nie = Instantiate(prefabserca,startgirl,Quaternion.identity);
        wee = Instantiate(dym,startgirl,Quaternion.identity);
        wee.transform.parent = nie.transform;
       }else
       {
        nie = Instantiate(tak,startgirl,Quaternion.identity);
        wee = Instantiate(dym,startgirl,Quaternion.identity);
        wee.transform.parent = nie.transform;
        }
      //  wee.transform.position = new Vector3(0,0,0);  
        dym.transform.localScale = new Vector3( dym.transform.localScale.x, -dym.transform.localScale.y, dym.transform.localScale.z);
         
        Debug.Log(tak.tag);
    
        if (tak.tag == "lovegirl")
        {
        lovelove();
        }
         if (tak.tag == "sadgirl")
        {
        sadsad();
        }
    }
    for (int i=0;i < chathero.ToArray().Length;i++)
    {
       GameObject item = chathero.ToArray()[i];
        item.GetComponent<Aaaa>().initpost += chatstep;
        if (item.transform.position.y>8)
        {
            chathero.Remove(item);
        }
    }


 
        chathero.Add(nie);

    now = false;

    }

IEnumerator response(string s)
{
    GameObject bop;

    bop = responselist[Random.Range(0,responselist.Length-1)];
    

    
    GameObject pom = bop;
    if (s == "love")
    {
        pom.tag = "lovegirl";

    }
    else if (s == "sad")
    {
        pom.tag = "sadgirl";
    }
    else if (s == "serducho")
    {
         pom = prefabserca;
        pom.tag ="serduchogirl";
       
    }

    float waitTime = Random.Range(0.01f,2);
    yield return new WaitForSecondsRealtime(waitTime);

    chatupdate(pom);

yield return null;
 }
 
 
 
}

