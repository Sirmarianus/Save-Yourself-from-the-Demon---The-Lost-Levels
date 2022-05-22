using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //VARIABLES
    [SerializeField] private float mouseSensitivity;

    //REFERENCES
    private Transform parent;
    public GameObject inventoryUI;
    public Transform player;
    private bool cursorLocked = true;

    private void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = PlayerManager.instance.player.transform;
        
    }

    private void Update()
    {
        if (!inventoryUI.activeSelf)
        {
            Debug.Log("inventory zamkniete");
            Rotate();
            player.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            Debug.Log("otwarte");
            player.GetComponent<PlayerMovement>().enabled = false;
        }
            

        if (Input.GetKeyDown(KeyCode.I) && cursorLocked)
        {
            Debug.Log("unlocking");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
            
        }
        else if(Input.GetKeyDown(KeyCode.I) && !cursorLocked)
        {
            Debug.Log("locking");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLocked = true;
        }
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        parent.Rotate(Vector3.up, mouseX);
    }
}