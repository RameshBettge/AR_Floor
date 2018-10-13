using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum is out of class so an instance can be displayed in editor
public  enum PendulumState { To, AtEnd, From, AtStart, Delayed}
enum Dimension { X, Y, Z }


public class PendulumMove : MonoBehaviour
{
    [SerializeField]
    Dimension dimension;

    [SerializeField]
    [Tooltip("When the script should be active after Game Start")]
    float Delay;
    [SerializeField]
    float StartValue;
    [SerializeField]
    float EndValue;
    [SerializeField]
    float ToDuration;
    [SerializeField]
    float FromDuration;
    [SerializeField]
    float StartWait;
    [SerializeField]
    float EndWait;
    [SerializeField]
    AnimationCurve smoothCurve;
    [SerializeField]
    bool disableAtEnd;





    delegate float LerpDel(float timer);


    PendulumState currentState;
    float timer;
    float targetTime;

    bool isReturing;

    void Start()
    {
        currentState = PendulumState.Delayed;
        targetTime = Delay;
    }

    void Update()
    {
        float percentage = timer / targetTime;

        if(currentState == PendulumState.To || currentState == PendulumState.From)
        {
            float adjustedPercentage = smoothCurve.Evaluate(percentage);
            if(currentState == PendulumState.From) { adjustedPercentage = 1 - adjustedPercentage; }
            float pos = Mathf.Lerp(StartValue, EndValue, adjustedPercentage);
            ChangePosition(dimension, pos);

        }
        

        if(timer >= targetTime)
        {
            switch (currentState)
            {
                case PendulumState.To:
                    //Arrived at end.
                    if (disableAtEnd) { ToggleActive(false); }
                    currentState = PendulumState.AtEnd;
                    targetTime = EndWait;
                    break;
                case PendulumState.AtEnd:
                    currentState = PendulumState.From;
                    targetTime = FromDuration;

                    break;
                case PendulumState.From:
                    currentState = PendulumState.AtStart;
                    targetTime = StartWait;

                    break;
                case PendulumState.AtStart:
                    //Starting Again
                    if (disableAtEnd) { ToggleActive(true); }
                    currentState = PendulumState.To;
                    targetTime = ToDuration;
                    break;
                case PendulumState.Delayed:
                    currentState = PendulumState.To;
                    targetTime = ToDuration;
                    break;
                default:
                    break;
            }
            timer = 0.0f;
        }
        timer += Time.deltaTime;


    }

    void ChangePosition(Dimension dim, float pos)
    {
        Vector3 newPos = transform.position;

        if(float.IsNaN(pos)) { return;}
        switch (dim)
        {
            case Dimension.X:
                newPos.x = pos;
                break;
            case Dimension.Y:
                newPos.y = pos;
                break;
            case Dimension.Z:
                newPos.z = pos;
                break;
            default:
                break;
        }

        transform.position = newPos;
    }

    void ToggleActive(bool b)
    {
        GetComponent<Collider>().enabled = b;
        GetComponent<Renderer>().enabled = b;
    }
}
