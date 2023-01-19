using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelAddForce : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> wheels; 
    private float speed = 200000;
    private bool wheelsFound = false;
    public gettingInandOutCar GettingInandOutCar;
    
    Vector3 m_EulerAngleVelocityL;
    Vector3 m_EulerAngleVelocityR;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        GettingInandOutCar = GetComponent<gettingInandOutCar>();
        wheels = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        
        m_EulerAngleVelocityR = new Vector3(0, 350 * Time.deltaTime, 0);
        m_EulerAngleVelocityL = new Vector3(0, -350 * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (var wheel in wheels)
        {
            if (wheel == null)
            {
                wheels.Remove(wheel);
            }
        }
        
        CheckingWheel();
        if (Input.GetKey(KeyCode.W))
        {
            foreach (var wheel in wheels)
            {
                rb.AddForceAtPosition(transform.forward * (speed * Time.deltaTime), wheel.transform.position);
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {
            foreach (var wheel in wheels)
            {
                rb.AddForceAtPosition(transform.forward * (-speed * Time.deltaTime), wheel.transform.position);
            }
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            foreach (var wheel in wheels)
            {
                if (GettingInandOutCar.inCar)
                {
                    Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocityR * Time.fixedDeltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
                
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            foreach (var wheel in wheels)
            {
                if (GettingInandOutCar.inCar)
                {
                    Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocityL * Time.fixedDeltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
            }
        }
    }

    public void CheckingWheel()
    {

        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Wheel" && child.gameObject)
            {
                wheels.Add(child.gameObject);
                child.gameObject.name = "RoundThings";
            }
        }
    }
}
