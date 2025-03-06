using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{

    public Transform PlayerModel;
    public CharacterController Controller;
    public Animator HandsAnimator;
    
    public void HandleInputs()
    {
        //basic movement - left, right, forward, backward
        float yAxis = Input.GetAxis("Horizontal");
        float xAxis = Input.GetAxis("Vertical");
        Vector3 dir = ((xAxis * transform.forward) + (yAxis * transform.right));
        float combinedAxis = Mathf.Min(dir.magnitude, 1);
        dir = dir.normalized;

        Vector3 motion = Shortcuts.MOVEMENT_SPEED * combinedAxis * Time.deltaTime * dir;
        if (motion.magnitude > 0)
        {
            HandsAnimator.SetBool("Moving", true);
        }
        else
        {
            HandsAnimator.SetBool("Moving", false);
        }
        Controller.Move(motion);

        //horizontal rotation
        float extraRotation = Input.GetKey(KeyCode.E) ? 50 : 0;
        extraRotation -= Input.GetKey(KeyCode.Q) ? 50 : 0;
        extraRotation *= Time.deltaTime;
        float horizontalRotation = Shortcuts.ROTATION_SPEED * (Input.GetAxis("Mouse X") + extraRotation);
        transform.Rotate(axis: transform.up, angle: horizontalRotation);

        //vertical rotation
        float verticalRotation = Shortcuts.ROTATION_SPEED * Input.GetAxis("Mouse Y");
        Quaternion rotationCache = Camera.main.transform.localRotation;
        Camera.main.transform.Rotate(axis: Vector3.right, angle: -verticalRotation);

        //constraint vertical rotation
        float currentVerticalAngle = Camera.main.transform.localRotation.eulerAngles.x;
        if (currentVerticalAngle > Shortcuts.MAX_VERTICAL_ROTATION && currentVerticalAngle < 360 - Shortcuts.MAX_VERTICAL_ROTATION)
        {
            Camera.main.transform.localRotation = rotationCache;
        }

        //Actions
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            HandsAnimator.SetTrigger("Thrust");
        }
    }
}
