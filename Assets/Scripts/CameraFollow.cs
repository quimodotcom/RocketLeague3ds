using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private Transform playerTarget;

    private bool disableRotation = false;

    private void Update()
    {
        if(!disableRotation)
        {
            HandleTranslation();
            HandleRotation();

            transform.parent = null;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Ball").transform;
            transform.parent = playerTarget;
            transform.LookAt(target);
        }
        
        if (Input.GetKeyDown(KeyCode.V) || GamePad.GetButtonTrigger(N3dsButton.Y))
        {
            if(target != GameObject.FindGameObjectWithTag("Ball").transform)
            {
                if(playerTarget == null)
                {
                    playerTarget = target;
                }

                disableRotation = true;
            }
            else
            {
                target = playerTarget;

                disableRotation = false;
            }
        }
    }

    public void Awake()
    {
        LoadMatch.instance.LoadYourAsyncScene();
    }
   
    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
