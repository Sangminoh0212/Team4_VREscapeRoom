using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class M_Climber : MonoBehaviour
{
    private CharacterController characterController;
    private ActionBasedContinuousMoveProvider continuousMovement;
    private List<ActionBasedController> climbingHands = new List<ActionBasedController>();
    private Dictionary<ActionBasedController, Vector3> previousPositions = new Dictionary<ActionBasedController, Vector3>();
    private Vector3 currentVelocity; 

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ActionBasedContinuousMoveProvider>();
    }

    private void FixedUpdate()
    {
        foreach(ActionBasedController hand in climbingHands)
        {
            if(hand)
            {
                continuousMovement.enabled = false;

                Climb(hand);
            }
        }

        if(climbingHands.Count == 0)
        {
            continuousMovement.enabled = true;
        }
    }

    public void SelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactor is XRDirectInteractor)
        {
            ActionBasedController hand = args.interactor.gameObject.GetComponent<ActionBasedController>();
            
            climbingHands.Add(hand);

            previousPositions.Add(hand, hand.positionAction.action.ReadValue<Vector3>());
        }
    }
    public void SelectExited(SelectExitEventArgs args)
    {
        if (args.interactor is XRDirectInteractor)
        {
            var hand = climbingHands.Find(x => x.name == args.interactor.name);

            if (hand)
            {
                climbingHands.Remove(hand);
                previousPositions.Remove(hand);
            }
        }
    }

    private void Climb(ActionBasedController hand)
    {
        if(previousPositions.TryGetValue(hand, out Vector3 previousPos))
        {
            currentVelocity = (hand.positionAction.action.ReadValue<Vector3>() 
                - previousPos) / Time.fixedDeltaTime;

            characterController.Move(transform.rotation * -currentVelocity * Time.fixedDeltaTime);

            previousPositions[hand] = hand.positionAction.action.ReadValue<Vector3>();
        }
    }
}
