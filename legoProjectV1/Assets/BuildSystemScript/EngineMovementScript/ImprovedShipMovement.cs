using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedShipMovement : MonoBehaviour
{
    //speeds
    private float currentForward;
    private float currentHorizontal;
    private float currentVertical;

    public float forward = 200f;
    public float vertical = 50f;
    public float horizontal = 100f;
    public float rotateSpeed = 500f;

    //input
    private float forwardVelocity;
    private float verticalVelocity;
    private float horizantalVelocity;

    // how fast get to top speed
    //private HashIDs hash;
    private Animator anim;

    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;

    private Rigidbody ourBody;

    // Start is called before the first frame update
    private void Start()
    {
        ourBody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
      
    }
    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // gets input
        forwardVelocity = Mathf.Lerp(currentForward, forward, 1f) * Input.GetAxisRaw("Forward");
        horizantalVelocity = Mathf.Lerp(currentHorizontal, horizontal, 1f) * Input.GetAxis("Strafe");
        verticalVelocity = Mathf.Lerp(currentVertical, vertical, 1f) * Input.GetAxis("Hover");

        Vector3 forwardInput = transform.forward * forwardVelocity * Time.fixedDeltaTime;
        Vector3 horizontalInput = transform.right * horizantalVelocity * Time.deltaTime;
        Vector3 verticalInput = transform.up * verticalVelocity * Time.deltaTime;

        //applys input
        MovementManager(v, h);

        float turn = Input.GetAxis("shipTurn");
        Rotating(turn);
        ourBody.MovePosition(ourBody.position + forwardInput + horizontalInput + verticalInput);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void Rotating(float Input_turn)
    {

        if (Input_turn != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, Input_turn * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }

    }
    void MovementManager(float vertical, float horizontal)
    {
        if (vertical > 0)
        {
            //anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
           //anim.SetBool("Backwards", false);
        }
        if (vertical < 0)
        {
            //anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            //anim.SetBool("Backwards", true);
        }
        if (vertical == 0)
        {
            //anim.SetFloat(hash.speedFloat, 0.0f);
            //anim.SetBool(hash.backwardsBool, false);
        }

        // controlls horizontal
        if (horizontal > 0)
        {
            //anim.SetBool(hash.strafeRightBool, true);

        }

        if (horizontal < 0)
        {
            //anim.SetBool(hash.strafeLeftBool, true);
        }

        if (horizontal == 0)
        {
            //anim.SetBool(hash.strafeRightBool, false);
            //anim.SetBool(hash.strafeLeftBool, false);
        }
    }
    void AudioManagement()
    {

    }
}