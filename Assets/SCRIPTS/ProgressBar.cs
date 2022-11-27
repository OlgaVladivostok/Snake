using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public SnakeMovement BodyParts;
    public Transform FinishPlatform;
    public Slider Slider;
    public float AsseptablePlayerFinishDistance = 1f;
    private float _startY;
    private float _minimumReachedY;

    private void Start()
    {
        _startY = BodyParts.transform.position.y;
    }
    private void Update()
    {
        _minimumReachedY = Mathf.Min(_minimumReachedY, BodyParts.transform.position.y);

        float finishY = FinishPlatform.position.y;
        float t = Mathf.InverseLerp(_startY, finishY, _minimumReachedY + AsseptablePlayerFinishDistance);
        Slider.value = t;
    }
}
