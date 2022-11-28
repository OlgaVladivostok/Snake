using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision Food");
        if (collision.transform.CompareTag("Food"))
        {
            Destroy(collision.transform.gameObject);
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.CompareTag("Food"))
        {
            Destroy(collision.transform.gameObject);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Food"))
        {
            Destroy(collision.transform.gameObject);
        }
    }
}

        