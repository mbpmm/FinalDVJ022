using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    public bool moving;
    public Vector3 desiredPos;
    
    public float clickRayDistance;
    public float normalRayDistance;
    public float distanceBetweenPositions;
    public float speed;
    public float rotationSpeed;
    public float stopDistance;
    public Vector3 carVel;
    public float brakeForce;

    public bool stop;

    public WheelCollider[] wheels;

    private Rigidbody rb;
    public static Action<Car> carStarted;
    public delegate void OnCarAction();
    public static OnCarAction pickUpCollected;
    public bool gameStarted;
    void Start()
    {
        gameStarted = true;
        rb = GetComponent<Rigidbody>();
        carStarted.Invoke(this);
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            stop = false;
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, clickRayDistance) && hit.transform.gameObject.tag == "Terrain")
            {
                desiredPos = hit.point;
                moving = true;
            }
        }

        distanceBetweenPositions = Vector3.Distance(transform.position, desiredPos);
    }

    void FixedUpdate()
    {
        if (moving)
        {
            RaycastHit hitOnPlane;

            carVel = transform.forward * speed * Time.deltaTime;

            if (Physics.Raycast(transform.position, Vector3.down, out hitOnPlane, normalRayDistance))
            {
                Vector3 goToPos = new Vector3(desiredPos.x, transform.position.y, desiredPos.z);
                carVel = Vector3.ProjectOnPlane(carVel, hitOnPlane.normal);
                Quaternion desiredRot = Quaternion.identity;
                desiredRot.SetLookRotation(goToPos - transform.position, hitOnPlane.normal);
                transform.rotation = Quaternion.Lerp(transform.localRotation, desiredRot, rotationSpeed * Time.deltaTime);
            }
            rb.velocity = carVel;
            Debug.DrawRay(transform.position, Vector3.down * normalRayDistance, Color.blue, 1f);
        }

        if (distanceBetweenPositions < stopDistance)
        {
            if (!stop)
            {
                moving = false;
                rb.velocity = Vector3.zero;
                stop = true;

                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = brakeForce;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            if (pickUpCollected!=null)
                pickUpCollected();
            Destroy(collision.gameObject);
        }
    }
}