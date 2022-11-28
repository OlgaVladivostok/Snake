using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoDestroy : MonoBehaviour
{

    [Header("Snake Manager")]
    SnakeMovement SM;

    [Header("Hits to be destroyed")]
    public int life;
    public float lifeForColor;
    Text thisText;

    GameObject[] ToDestroy;
    GameObject[] ToUnparent;

    [Header("Color Management")]
    readonly int maxLifeForRed = 50;

    Vector3 initialPos;
    public bool dontMove;

    void SetBoxSize()
    {
        float x;
        float y;

        //Debug.Log(Screen.height + "\n" + Screen.width + "\n" + Screen.height / Screen.width);

        transform.localScale *= ((float)Screen.width / (float)Screen.height) / (9f / 16f);
    }

    void Start()
    {
         var text1 = GetComponentInChildren<UnityEngine.UI.Text>();
        Debug.Log("test for 0"+ text1);



        SetBoxSize();

        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();

        life = Random.Range(1, GameController.SCORE / 2 + 5);

        if (transform.CompareTag("SimpleBox"))
        {
            life = Random.Range(5, 50);
        }

        lifeForColor = life;

        thisText = GetComponentInChildren<UnityEngine.UI.Text>();
        thisText.text = "" + life;

        ToDestroy = new GameObject[transform.childCount];
        ToUnparent = new GameObject[transform.childCount];

        StartCoroutine("EnableSomeBars");

        //SetBoxColor();

        initialPos = transform.position;
        dontMove = false;
    }

    void Update()
    {

        if (dontMove)
            transform.position = initialPos;

        if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10)
            Destroy(this.gameObject);


        lifeForColor = life;

        if (life <= 0)
        {
            
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;

            transform.GetComponent<BoxCollider>().enabled = false;



            if (transform.GetComponentInChildren<ParticleSystem>().isStopped) 

                transform.GetComponentInChildren<ParticleSystem>().Play();

            Destroy(this.gameObject);
        }

    }

    public void UpdateText()
    {
        thisText.text = "" + life;
    }

    IEnumerator EnableSomeBars()
    {
        int i = 0;

        while (i < transform.childCount)
        {
            if (transform.GetChild(i).CompareTag("Bar"))
            {
                int r = Random.Range(0, 6);

                if (r == 1)
                {
                    ToUnparent[i] = transform.GetChild(i).gameObject;
                }
                else
                    ToDestroy[i] = transform.GetChild(i).gameObject;

                i++;
                yield return new WaitForSeconds(0.01f);
            }
            else
                i++;
        }


        for (int k = 0; k < ToUnparent.Length; k++)
        {
            if (ToUnparent[k] != null)
                ToUnparent[k].transform.parent = null;

            if (ToDestroy[k] != null)
                Destroy(ToDestroy[k]);
        }

        yield return null;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision Box");

        if (collision.transform.CompareTag("SimpleBox") && transform.CompareTag("Box"))
        {
            Destroy(collision.transform.gameObject);
        }
        else if (transform.CompareTag("SimpleBox") && collision.transform.CompareTag("SimpleBox"))
        {

            Destroy(collision.transform.gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.CompareTag("SimpleBox") && transform.CompareTag("Box"))
        {
            Destroy(collision.transform.gameObject);
        }
        else if (transform.CompareTag("SimpleBox") && collision.transform.CompareTag("SimpleBox"))
        {
            Destroy(collision.transform.gameObject);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("SimpleBox") && transform.CompareTag("Box"))
        {
            Destroy(collision.transform.gameObject);
        }

        else if (collision.transform.CompareTag("SimpleBox") && transform.CompareTag("SimpleBox"))
        {

            Destroy(collision.transform.gameObject);
            //    Debug.Log("Overlapping");
        }

    }

    //public void SetBoxColor()
    //{
    //    Color32 thisImageColor = GetComponent<Material>().color;

      //  if (lifeForColor > maxLifeForRed)
         //   thisImageColor = new Color32(255, 0, 0, 255);

        //else if (lifeForColor >= maxLifeForRed / 2 && lifeForColor <= maxLifeForRed)
         //   thisImageColor = new Color32(255, (byte)(510 * (1 - (lifeForColor / maxLifeForRed))), 0, 255);

     //   else if (lifeForColor > 0 && lifeForColor < maxLifeForRed)
      //      thisImageColor = new Color32((byte)(510 * lifeForColor / maxLifeForRed), 255, 0, 255);

       // GetComponent<Material>().color = thisImageColor;

    }


