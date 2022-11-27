using UnityEngine;

public class AutoDestroyBar : MonoBehaviour
{

    [Header("Snake Movement")]
    SnakeMovement SM;

    void Start()
    {

        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();

    }

    void Update()
    {
        if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10)
            Destroy(this.gameObject);
    }


}

