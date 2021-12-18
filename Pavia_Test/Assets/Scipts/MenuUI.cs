using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuUI : MonoBehaviour
{

    public GameObject Menu;
    public GameObject PlayerFollowCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Menu.SetActive(!Menu.activeSelf);
            PlayerFollowCamera.SetActive(!Menu.activeSelf);

            if (Menu.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.Confined;
            }

        }
            
        
    }
}
