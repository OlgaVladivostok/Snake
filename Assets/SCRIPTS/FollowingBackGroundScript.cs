using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBackGroundScript : MonoBehaviour
{
   

    [Header("Snake Container")]
    public Transform SnakeContainer;

    Vector3 initialBackgroundPos;

    void Start()
    {

        initialBackgroundPos = transform.position;
    }

    void Update()
    {
        if (SnakeContainer.childCount > 0)
            transform.position = Vector3.Slerp(transform.position,
                (initialBackgroundPos + new Vector3(0, SnakeContainer.GetChild(0).position.y - Camera.main.orthographicSize / 2, 0)),
                0.1f);
    }
}
