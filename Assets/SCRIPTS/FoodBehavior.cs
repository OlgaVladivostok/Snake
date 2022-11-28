using UnityEngine;
using UnityEngine.UI;

public class FoodBehavior : MonoBehaviour
{


    [Header("Snake Manager")]
    SnakeMovement SM;

    [Header("Food Amount")]
   readonly public int foodAmount=1;

    void Start()
    {
        var text = GetComponentInChildren<UnityEngine.UI.Text>();
       Debug.Log("test for 0" + text);



        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();

       // foodAmount = Random.Range(1, 10);

        transform.GetComponentInChildren<UnityEngine.UI.Text>().text="1";


    }

    void Update()
    {
        if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10)
            Destroy(gameObject);
    }


 //   void OnTriggerEnter(Collider col)
   // {
       // Debug.Log("collision");
       // Destroy(col.gameObject);
    //}




     private void OnTriggerEnter(Collider collision)
     {



     Debug.Log("collision");
     Destroy(gameObject, 0.7f);
     }
}
