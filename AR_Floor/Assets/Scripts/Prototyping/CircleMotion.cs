using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMotion : MonoBehaviour
{
    [SerializeField]
    Dimension dimension;
    [SerializeField]
    float duration;
    [SerializeField]
    int dir = 1;
    [SerializeField]
    AnimationCurve curve;

    float timer;

    Vector3 startEuler;

    public float percentage;

    private void Start()
    {
        startEuler = transform.eulerAngles;
    }
    void Update()
    {
        percentage = timer/duration;

        if(percentage >= 1f) { percentage -= 1f; timer -= duration; }
   
        float adjustedPercentage = curve.Evaluate(percentage);

        SetEulerAngles(adjustedPercentage * 360 * dir);

        timer += Time.deltaTime;
    }


    void SetEulerAngles(float angle)
    {
        Vector3 newEuler = startEuler;

        switch (dimension)
        {
            case Dimension.X:
                newEuler.x = angle;
                break;
            case Dimension.Y:
                newEuler.y = angle;
                break;
            case Dimension.Z:
                newEuler.z = angle;
                break;
            default:
                break;
        }

        transform.eulerAngles = newEuler;
    }
}
