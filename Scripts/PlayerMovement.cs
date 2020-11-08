using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class deals with the natural course of physics and level interaction
public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float[] inputVariables;

    Vector3 velocity;
    bool isGrounded;

    [SerializeField] private Vector3 playerRotation;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
            velocity.y = -2f;
            
        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        playerRotation = transform.rotation.eulerAngles;
    }
    //Used to communicate with Combatant Agent
   public void GetActionMove(float[] actions)
    {
        float x = actions[0];
        float z = actions[1];
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        /*if (actions[2]!=0 && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);*/

    }
}

