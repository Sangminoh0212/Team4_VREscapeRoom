using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverLIstener : MonoBehaviour
{
    public float angleThreshold = 10.0f;
    public HingeJointState hingeJointState = HingeJointState.None;
    public UnityEvent OnMinLimitReached;
    public UnityEvent OnMaxLimitReached;
    public UnityEvent OnNoneState;
    public enum HingeJointState { Min, Max, None }
    private HingeJoint hinge;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()

    {
        float distanceToMin = Mathf.Abs(hinge.angle - hinge.limits.min);
        float distanceToMax = Mathf.Abs(hinge.angle - hinge.limits.max);

        //Reached Min
        if (distanceToMin < angleThreshold)
        {
            if (hingeJointState != HingeJointState.Min)
                OnMinLimitReached.Invoke();

            hingeJointState = HingeJointState.Min;
        }

        //Reached Max
        else if (distanceToMax < angleThreshold)
        {
            if (hingeJointState != HingeJointState.Max)
                OnMaxLimitReached.Invoke();

            hingeJointState = HingeJointState.Max;
        }

        //No Limit reached
        else
        {
            if (hingeJointState != HingeJointState.None)
                OnNoneState.Invoke();
            hingeJointState = HingeJointState.None;
        }
    }
}
