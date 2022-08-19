using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CharacterAiming : MonoBehaviour
{
    public float normalSensitivity = 300;
    public float aimSensitivity = 200;
    public float firstSensitivity = 400;

    public float sensitivity;
    public float turnSpeed = 15;

    public float aimDurationg = 0.3f;
    public Transform cameraLookAt;
    public Transform camModeRoot;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    public bool isAiming;


    Camera mainCamera;
    Animator animator;
    ActiveWeapon activeWeapon;
    int isAimingParam = Animator.StringToHash("isAiming");

    [SerializeField] private Canvas thirdPersonCanvas;
    [SerializeField] private Canvas firstPersonCanvas;
    [SerializeField] private Canvas aimCanvas;

    [SerializeField] private CinemachineVirtualCamera firstPersonCamera;

    void Start()
    {
        sensitivity = normalSensitivity;
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        activeWeapon= GetComponent<ActiveWeapon>();

        aimCanvas.enabled = false;
    }


    private void Update()
    {
        HandleAiming();
        HandleCamMode();
        var weapon = activeWeapon.GetActiveWeapon();
        if(weapon)
        {
            weapon.recoil.recoilModifier = isAiming ? 0.3f : 1.0f;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime*sensitivity);

        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        camModeRoot.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }


    void HandleAiming()
    {
        isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam, isAiming);

        if (isAiming)
        {
            SetSensitivity(aimSensitivity);
            aimCanvas.enabled = true;
            thirdPersonCanvas.enabled = false;
        }
        else
        {
            SetSensitivity(normalSensitivity);
            aimCanvas.enabled = false;
            thirdPersonCanvas.enabled = true;
        }
    }


    bool isCamModing;
    [SerializeField] private LayerMask camColliderLayerMask = new LayerMask();
    public Image camCrosshair;


    void HandleCamMode()
    {
        bool tryCamMode = Input.GetKeyDown(KeyCode.V);
        if (tryCamMode && !isCamModing)
        {
            isCamModing = true;

        }

        else if (isCamModing)
        {
            SetSensitivity(firstSensitivity);
            aimCanvas.enabled = false;
            thirdPersonCanvas.enabled = false;
            firstPersonCamera.gameObject.SetActive(true);


            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, camColliderLayerMask))
            {

                
                if (raycastHit.transform.gameObject.CompareTag("KeyItemDetectArea"))
                {
                    camCrosshair.color = new Color(1f, 0.8f, 0f, 1f);
                }
                else if (raycastHit.transform.gameObject.CompareTag("KeyItem"))
                {


                    camCrosshair.color = new Color(1f, 0f, 0f, 1f);

                    if(Input.GetKeyDown(KeyCode.Z))
                    {
                        raycastHit.transform.gameObject.SetActive(false);

                    }



                }
                else if(raycastHit.transform.gameObject.CompareTag("DoorLock"))
                {
                    camCrosshair.color = new Color(1f, 0f, 0f, 1f);
                    var Door = raycastHit.collider.GetComponentInParent<JH_DoorActive>();
                    if (Input.GetKeyDown(KeyCode.Z)&&Door)
                    {   
                        Door.isOpen = true;

                    }

                }

                else camCrosshair.color = new Color(1f, 1f, 1f, 1f);

            }
            else camCrosshair.color = new Color(1f, 1f, 1f, 1f);


   

            if (tryCamMode)
            {
                SetSensitivity(normalSensitivity);
                thirdPersonCanvas.enabled = true;
                firstPersonCamera.gameObject.SetActive(false);
                isCamModing = false;
            }


        }

    }



    public void SetSensitivity(float newSensitivity)
    {
        xAxis.m_MaxSpeed = newSensitivity;
        yAxis.m_MaxSpeed = newSensitivity;
    }

}