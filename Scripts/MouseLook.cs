using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    private float mouseSensitivity = 250f;

    public Transform playerBody;

    float xRotation = 0f;
    float mouseX;
    float mouseY = 0;
    public bool lockY = true;

    private bool isInPauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if(isInPauseMenu)
        {
            mouseX = 0;
            mouseY = 0;
            Cursor.lockState = CursorLockMode.None;
		}
		else
		{
            Cursor.lockState = CursorLockMode.Locked;
        }

        playerBody.Rotate(Vector3.up * mouseX);
    }
    public void UpdateMouse(float[] actions)
    {
         mouseX = actions[2] * mouseSensitivity * Time.deltaTime;
        if (!lockY)
        {
                mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
        
    }

    public void pauseMenu(bool isPausing)
	{
        isInPauseMenu = isPausing;
	}

	public void OnEnable()
	{
        PauseMenu.OnPause += pauseMenu;
	}

	public void OnDisable()
	{
        PauseMenu.OnPause -= pauseMenu;
    }
}
