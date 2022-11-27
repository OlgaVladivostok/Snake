using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [Header("Snake Container")]
    public Transform SnakeContainer;

    Vector3 initialCameraPos;

    void Start()
    {

        initialCameraPos = transform.position;
    }

    void Update()
    {
        if (SnakeContainer.childCount > 0)
            transform.position = Vector3.Slerp(transform.position,
                (initialCameraPos + new Vector3(0, SnakeContainer.GetChild(0).position.y - Camera.main.orthographicSize / 2, 0)),
                0.1f);
    }
}
