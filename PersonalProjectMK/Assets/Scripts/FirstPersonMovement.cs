using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public Animator animator;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        Debug.Log("y: " + targetVelocity.y);
        Debug.Log("x: " + targetVelocity.x);
        if(targetVelocity.x != 0 || targetVelocity.y != 0)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);

        }
        if (targetVelocity.y > 0 && targetVelocity.x == 0)
        {
            animator.SetBool("movingFront", true);
        }
        else
        {
            animator.SetBool("movingFront", false);

        }
        /*if (targetVelocity.y < 0 && targetVelocity.x == 0)
        {
            animator.SetBool("movingBack", true);
            Debug.Log("true");
        }
        else
        {
            animator.SetBool("movingFront", false);
        }*/
        if (targetVelocity.x < 0 && targetVelocity.y == 0)
        {
            animator.SetBool("movingLeft", true);
        }
        else
        {
            animator.SetBool("movingLeft", false);

        }
        /*if (targetVelocity.y < 0 && targetVelocity.x == 0)
        {
            animator.SetBool("movingBack", true);
            Debug.Log("true");
        }
        else
        {
            animator.SetBool("movingFront", false);
        }*/

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}