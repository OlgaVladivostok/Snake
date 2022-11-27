using UnityEngine;
using UnityEngine.UI;

public class FoodBehavior : MonoBehaviour
{


    [Header("Snake Manager")]
    SnakeMovement SM;

    [Header("Food Amount")]
    public int foodAmount;

    void Start()
    {
        var text = GetComponentInChildren<UnityEngine.UI.Text>();
        Debug.Log("test for 0"+ text);



        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();

        foodAmount = Random.Range(1, 10);

        transform.GetComponentInChildren< UnityEngine.UI.Text > ().text = "" + foodAmount;
        

    }

    void Update()
    {
        if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(this.gameObject);
    }
}
