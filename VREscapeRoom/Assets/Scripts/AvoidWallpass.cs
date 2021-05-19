using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AvoidWallpass : MonoBehaviour
{

    private CharacterController character;
    private XRRig rig;

    public float additionalHeight = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
    }

    // make the capsule of the charactercontroller follow the camera
    private void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;

        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);

        character.center = new Vector3(capsuleCenter.x, character.height/2+character.skinWidth, capsuleCenter.z);
    }
}
