using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
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
            if (hand)
            {
                // disable continuous movement and climb
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
        //make sure we climb only with direct interactor, not ray
        if(args.interactor is XRDirectInteractor)
        {
            // add hand/controller to the list
            ActionBasedController hand = args.interactor.gameObject.GetComponent<ActionBasedController>();

            //Debug.Log("Grabbed with hand : " + hand.name);
            
            climbingHands.Add(hand);

            //add previous position
            previousPositions.Add(hand, hand.positionAction.action.ReadValue<Vector3>());
        }
    }

    public void SelectExited(SelectExitEventArgs args)
    {
        // remove the hand that was let go
        if (args.interactor is XRDirectInteractor)
        {
            var hand = climbingHands.Find(x => x.name == args.interactor.name);

            //Debug.Log("Let go with hand : " + hand.name);

            if (hand)
            {
                climbingHands.Remove(hand);
                previousPositions.Remove(hand);
            }
        }
    }

    private void Climb(ActionBasedController hand)
    {
        if(previousPositions.TryGetValue(hand, out Vector3 preciousPos))
        {
            // get the current velocity based on the hand's previous position and the current position
            currentVelocity = (hand.positionAction.action.ReadValue<Vector3>() - preciousPos) / Time.fixedDeltaTime;

            // move the chacacter controller in the opposite direction
            characterController.Move(transform.rotation * -currentVelocity / Time.fixedDeltaTime);

            // update the precious position
            previousPositions[hand] = hand.positionAction.action.ReadValue<Vector3>();
        }
    }
}
