using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOUSE : MonoBehaviour
{
    [SerializeField] float intensity;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var p = transform.position;
            p.x += Input.GetAxis("Mouse X") * intensity;
            transform.position = p;
        }
    }
}
