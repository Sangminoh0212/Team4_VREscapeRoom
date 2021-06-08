using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovement : MonoBehaviour
{
    public float speed = 2;
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    private InputDevice device;
    private XRRig rig;
    public float gravity = -9.81f;
    private float fallingSpeed;
    public float additionalHeight = 0.2f;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);

        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        Debug.Log(inputAxis);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        bool isGrounded = character.isGrounded;
        if(isGrounded && fallingSpeed < 0)
        {
            fallingSpeed = 0f;
        }

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y , 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        if (!isGrounded)
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
    }

    private void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;

        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);

        character.center = new Vector3(capsuleCenter.x, character.height/2+character.skinWidth, capsuleCenter.z);
    }
}
