using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarEnterExitSystem : MonoBehaviour
{
    public MonoBehaviour carcontroller;
    public MonoBehaviour attackFlame;

    public Transform Player;
    public Transform Car;
    public GameObject detact;

    [SerializeField] private CinemachineFreeLook carCam;


    //public GameObject driveUI;

    bool candrive;
    bool driving;

    
    void Start()
    {
        detact.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        carcontroller.enabled = false;
        carCam.enabled = false;
    }

    
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E)&& candrive&& !driving)
        {
            carCam.enabled = true;
            carcontroller.enabled = true;

            driving = true;
            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);

            detact.SetActive(true);
        }

        else if(Input.GetKeyDown (KeyCode.E) && driving)
        {   
            candrive = false;
            carCam.enabled = false;
            carcontroller.enabled = false;

            driving =false;
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);
            
            detact.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            candrive = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            candrive = false;
        }
    }





}
