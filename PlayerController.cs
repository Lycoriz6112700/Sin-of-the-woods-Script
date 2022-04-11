using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : Photon.MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] private GameObject PausedUI;
    [SerializeField] private GameObject Flashlight;
    public LayerMask interactableLayermask = 9;
    public Camera PlayerCamera;
    // public Interactable interactable;
    string onInteract;
    float verticalLookRotation;
    float horizonLookRotation;
    bool grounded;
    bool PauseMenu;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;    
    PhotonView PV;
    bool Paused = false;
    bool canMove = true;
    bool canRotate = true;
    public Animator anim;
    bool toggle = false;

    int[] score = {1};
    // float distance = 5;
    


    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start() 
    {
        Cursor.visible = false;
        if(!PV.isMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    void Update() 
    {
        if(!PV.isMine)
        {
            return;
        }
        Look();
        Move();
        Jump(); 
        Menu();
        ToggleLight();
        Interaction();
        
    }

    void ToggleLight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            toggle = !toggle;
            
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            Flashlight.SetActive(!toggle);
        }
    }

    void Look()
    {
        if(canRotate)
        {
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

            horizonLookRotation += Input.GetAxisRaw("Mouse X") * mouseSensitivity;

            verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

            cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
            
        }
        
    }

    void Move() 
    {
        if(canMove)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("IsRun",true);
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("IsRun",false);
            }
            anim.SetFloat("vertical",Input.GetAxis("Vertical"));
            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
            if(Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("IsBackward",true);
            }
            else if(Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("IsBackward",false);
            }
        }
        
    }

    void Jump() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded && canMove)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void Menu()
    {
        
        if(Input.GetButtonDown("Cancel"))
        {
            Paused = !Paused;

            if(Paused)
            {   
                if(Cursor.lockState != CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                Cursor.visible = true;
                moveAmount = Vector3.zero;
                canMove = false;
                canRotate = false;      
                PausedUI.SetActive(true);
            }
            else 
            {
                if(Cursor.lockState != CursorLockMode.Locked)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                canMove = true;
                canRotate = true;   
                PausedUI.SetActive(false);
            }
            
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    void FixedUpdate() 
    {
        if(!PV.isMine)
        {
            return;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void Resume()
    {
        PausedUI.SetActive(false);
        canMove = true;
        canRotate = true;
        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Exit() 
    {
        Debug.Log("PlayerDisconnected");
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Menu");
    }

    void Interaction()
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position,PlayerCamera.transform.forward, out hit, 2, interactableLayermask))
        {
            onInteract = hit.collider.gameObject.name;
            if(Input.GetKeyDown(KeyCode.E))
            {
                PV.RPC("RPC_PlayerEvents", PhotonTargets.AllBuffered, int.Parse(onInteract));
                    
            }
        }
    }

    [PunRPC]
    void RPC_PlayerEvents(int viewID)
    {
        PhotonView.Find(viewID).gameObject.SetActive(false);
        GameManager.Score--;
        Debug.Log(GameManager.Score);
    }

}
