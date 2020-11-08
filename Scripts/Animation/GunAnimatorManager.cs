using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using Unity.Mathematics;

using UnityEngine;

public class GunAnimatorManager : MonoBehaviour
{
    Animator anim;
    public GameObject rotationHandler;

    public float rotationDamper;
    public float rotationLimit;

    bool pausing = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pausing)
            return;

        float x = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");

        if (x == 0 && y == 0)
        {
            anim.SetBool("walking", false);
        }
        else
        {
            anim.SetBool("walking", true);
        }

        float locX = Mathf.Lerp(x / 10, 0, .1f);
        float locZ = Mathf.Lerp(y / 10, 0, .1f);

        rotationHandler.transform.localPosition = new Vector3(-locZ, 0, -locX);


        float rotX = Input.GetAxis("Mouse Y") * 5;
        float rotY = Input.GetAxis("Mouse X") * 5;

        float finalX = Mathf.Clamp(rotX / rotationDamper, -rotationLimit, rotationLimit);
        float finalY = Mathf.Clamp(rotY / rotationDamper, -rotationLimit, rotationLimit);

        rotationHandler.transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(finalX, finalY, 0), .1f);

    }

    public void fireWeapon()
    {
        anim.SetTrigger("Fire");
        Debug.Log("Firing");
    }

    public void playerInPauseMenu(bool pause)
	{
        pausing = pause;
	}

	public void OnEnable()
	{
        PauseMenu.OnPause += playerInPauseMenu;
	}

	public void OnDisable()
	{
        PauseMenu.OnPause -= playerInPauseMenu;
    }
}
