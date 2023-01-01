using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Vehicles.Car;

public class gettingInandOutCar : MonoBehaviour
{
    [SerializeField] private Rigidbody carBody;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject car = null;
    //[SerializeField] private CarUserControl carController = null;
    [SerializeField]private CinemachineVirtualCamera Playercamera;
    [SerializeField]private CinemachineVirtualCamera Carcamera;
    //[SerializeField] private CarController carInput = null;
    public bool inCar = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        car = this.gameObject;
        Playercamera = GameObject.FindWithTag("playerCam").GetComponent<CinemachineVirtualCamera>();
        Carcamera = GameObject.FindWithTag("carCam").GetComponent<CinemachineVirtualCamera>();
        carBody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        inCar = car.activeSelf;
        inCar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inCar)
            {
                GetOutOfCar();
            }
            else if(Vector3.Distance(car.transform.position,player.transform.position)<10f)
            {
                getInToCar();
            }
        }
    }

    void GetOutOfCar()
    {
        Carcamera.enabled = false;
        Playercamera.enabled = true;
        inCar = false;
        carBody.isKinematic = true;
        
        var gridPos = snapToGrid.Gridposision3D(car.transform.position, 1.0f);
        car.transform.rotation = new Quaternion(0, 0, 0, 0);
        car.transform.position = gridPos;
        car.transform.position += new Vector3(0, -0.175f, 0);
        
        //carController.enabled = false;
        //carInput.Move(0,0,1,1);
        player.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);
        player.SetActive(true);
        
    }

    void getInToCar()
    {   
        Carcamera.Follow = car.transform;
        inCar = true;
        player.SetActive(false);
        //carController.enabled = true;
        Playercamera.enabled = false;
        Carcamera.enabled = true;
        carBody.isKinematic = false;
    }
}
