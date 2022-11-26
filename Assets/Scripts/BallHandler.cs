using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D currentBallRigidbody;

    [SerializeField]
    private SpringJoint2D currentBallSprintJoint;

    [SerializeField]
    float delayDelay = .1f;

    private Camera mainCamera;

    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidbody == null) return;

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                LaunchBall();
            }

            isDragging = false;

            return;
        }

        isDragging = true;
        currentBallRigidbody.isKinematic = true;

        // get screen position
        Vector2 touchPosition =
            Touchscreen.current.primaryTouch.position.ReadValue();

        // convert to world position
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        Debug.Log (worldPosition);

        currentBallRigidbody.position = worldPosition;
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;

        // set null to reset position after launch by touch
        currentBallRigidbody = null;

        Invoke(nameof(DetachBall), delayDelay);
    }

    private void DetachBall()
    {
        currentBallSprintJoint.enabled = false;
        currentBallSprintJoint = null;
    }
}
