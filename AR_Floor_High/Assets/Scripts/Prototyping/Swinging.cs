using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{

    [SerializeField]
    Dimension dimension;
    [SerializeField]
    float halfRotation;
    [SerializeField]
    float halfRotTime;
    [SerializeField]
    AnimationCurve curve;

    int dir = 1;
    bool back;
    Vector3 startEuler;

    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    private void Start()
    {
        startEuler = transform.eulerAngles;
        StartCoroutine(Swing(1));
    }


    //void Update()
    //{
    //    float percentage = timer / halfRotTime;
    //    float currentAngle = halfRotation * Mathf.Min(1f, percentage);
    //    SetEulerAngles(currentAngle);

    //    if (percentage >= 1f)
    //    {

    //    }

    //}

    IEnumerator Swing(int dir)
    {
        float percentage = 0f;
        float timer = 0f;
        //Swing to the right
        while (percentage <= 1.0f)
        {
            percentage = timer / halfRotTime;

            float adjustedPercentage = curve.Evaluate(percentage);
            float currentAngle = halfRotation*  adjustedPercentage * dir;
            //if(currentAngle < 0f) { currentAngle += 360; }

            SetEulerAngles(currentAngle);

            timer += Time.deltaTime;
            yield return wait;
        }
        timer -= halfRotTime;
        percentage = timer / halfRotTime;

        // back to middle
        while (percentage <= 1.0f)
        {
            percentage = timer / halfRotTime;
            float reversedP = 1 - percentage;
            reversedP = curve.Evaluate(reversedP);

            float currentAngle = halfRotation * reversedP * dir;
            //if (currentAngle < 0f) { currentAngle += 360; }

            SetEulerAngles(currentAngle);

            timer += Time.deltaTime;
            yield return wait;
        }

        StartCoroutine(Swing(-dir));
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
