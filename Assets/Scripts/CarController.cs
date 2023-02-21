using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.N3DS;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    public Transform groundedPos;

    [SerializeField] private AudioSource engineMixer;

    public float timePressed;

    public int jumpPower;

    public bool isGrounded = false;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        CheckForGround();

        if(Input.GetKey(KeyCode.E) || GamePad.GetButtonRelease(N3dsButton.Y))
        {
            FlipCar();
        }

        if (Input.GetKey(KeyCode.Space) || GamePad.GetButtonRelease(N3dsButton.B) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Time.deltaTime * jumpPower);
        }

        if (GamePad.GetButtonHold(N3dsButton.A) || Input.GetAxisRaw("Vertical") > 0)
        {
            timePressed = Mathf.Clamp01(timePressed + Time.deltaTime);

            verticalInput = Mathf.SmoothStep(0, 1, timePressed);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            timePressed = Mathf.Clamp01(timePressed +- Time.deltaTime);

            verticalInput = Mathf.SmoothStep(0, -1, timePressed);
        }
        else
        {
            verticalInput = 0;

            timePressed = 0;
        }

        engineMixer.pitch = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude/3.4f, 2, 4);
    }

    private void CheckForGround()
    {
        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        float radius = 2;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = groundedPos.position + Vector3.down * (radius * 1f);
        //returns true if the sphere touches something on that layer
        LayerMask layer = ~(1 << LayerMask.NameToLayer("Ground"));
        isGrounded = Physics.CheckSphere(pos, radius, layer);

        UnityEngine.Debug.DrawRay(pos, Vector3.up * radius, Color.red);
    }

    private void GetInput()
    {
        horizontalInput = Mathf.Clamp(Input.GetAxis(HORIZONTAL)+GamePad.CirclePad.x, -1, 1);
        isBreaking = Input.GetKey(KeyCode.F) || GamePad.GetButtonHold(N3dsButton.L);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void FlipCar()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
