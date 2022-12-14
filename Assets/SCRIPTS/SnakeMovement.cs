using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    [Header("Managers")]
    public GameController GC;

    [Header("Some Snake Variables & Storing")]
    public List<Transform> BodyParts = new List<Transform>();
    public float minDistance = 0.25f;
    public int initialAmount;
    public float speed = 1;
    public float rotationSpeed = 50;
    public float LerpTimeX;
    public float LerpTimeY;

    [Header("Snake Head Prefab")]
    public GameObject BodyPrefab;

    [Header("Parts Text Amount Management")]
    public TextMesh PartsAmountTextMesh;

    [Header("Private Fields")]
    private float distance;
    private Vector3 refVelocity;

    private Transform curBodyPart;
    private Transform prevBodyPart;

    private bool firstPart;

    [Header("Mouse Control Variables")]
    Vector2 mousePreviousPos;
    Vector2 mouseCurrentPos;

    [Header("Particle System Management")]
    public ParticleSystem SnakeParticle;

       void Start()
    {


        
        firstPart = true;

        
        for (int i = 0; i < initialAmount; i++)
        {
            
            Invoke("AddBodyPart", 0.1f);
        }

    }

    public void SpawnBodyParts()
    {
        firstPart = true;

        
        for (int i = 0; i < initialAmount; i++)
        {
            
            Invoke("AddBodyPart", 0.1f);
        }
    }

  
    void Update()
    {

    
        if (GameController.gameState == GameController.GameState.GAME)
        {
            Move();

         
            if (BodyParts.Count == 0)
                GC.SetGameOver();
        }

     
        if (PartsAmountTextMesh != null)
            PartsAmountTextMesh.text = transform.childCount + "";


    }

    public void Move()
    {
        float curSpeed = speed;

     
        if (BodyParts.Count > 0)
            BodyParts[0].Translate(Vector2.up * curSpeed * Time.smoothDeltaTime);


        float maxX = Camera.main.orthographicSize * Screen.width / Screen.height;

        if (BodyParts.Count > 0)
        {
            if (BodyParts[0].position.x > maxX) //Right pos
            {
                BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
            else if (BodyParts[0].position.x < -maxX) //Left pos
            {
                BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
        }

        //Move the snake on the Horizontal Axis with mouse control-????????, ?????????, ???????? ?????????, Speculative Collision ? ?????? ?????? ??? ????
        if (Input.GetMouseButtonDown(0))
        {
            mousePreviousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
         

            if (BodyParts.Count > 0 && Mathf.Abs(BodyParts[0].position.x) < maxX)
            {
                mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float deltaMousePos = Mathf.Abs(mousePreviousPos.x - mouseCurrentPos.x);
                float sign = Mathf.Sign(mousePreviousPos.x - mouseCurrentPos.x);


                
                BodyParts[0].GetComponent<Rigidbody>().AddForce
                    (Vector2.right * rotationSpeed * deltaMousePos * -sign);

                mousePreviousPos = mouseCurrentPos;
            }
            else if (BodyParts.Count > 0 && BodyParts[0].position.x > maxX) //Right pos
            {
                BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
            else if (BodyParts.Count > 0 && BodyParts[0].position.x < maxX) //Left pos
            {
                BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
        }


       
        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.z = BodyParts[0].position.z;

           
            Vector3 pos = curBodyPart.position;

            pos.x = Mathf.Lerp(pos.x, newPos.x, LerpTimeX);
            pos.y = Mathf.Lerp(pos.y, newPos.y, LerpTimeY);

            curBodyPart.position = pos;


        }
    }

    public void AddBodyPart()
    {
        Transform newPart;

        if (firstPart)
        {
            newPart = (Instantiate(BodyPrefab, new Vector3(0, 0, 0),
                       Quaternion.identity) as GameObject).transform;

            //Disable the collision with snake

            // Set this part as the parent of the Text Mesh
            PartsAmountTextMesh.transform.parent = newPart;

            //Place it correctly
            PartsAmountTextMesh.transform.position = newPart.position +
                new Vector3(0, 0.5f, 0);

            firstPart = false;
        }
        else
            newPart = (Instantiate(BodyPrefab, BodyParts[BodyParts.Count - 1].position,
           BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;


        newPart.SetParent(transform);

        BodyParts.Add(newPart);

    }
}
