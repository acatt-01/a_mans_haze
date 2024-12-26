using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";

    [SerializeField] private float movementSpeed = 2f, runningSpeed = 3f, crouchSpeed, normalHeight, crouchHeight;
    public Vector3 offset;
    public Transform player;
    bool crouching;
    private CharacterController charController;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
        Crouching();
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;     //CharacterController.SimpleMove() applies deltaTime
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        //simple move applies delta time automatically
        charController.SimpleMove(forwardMovement + rightMovement);
    }

    private void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            crouching = !crouching;
        }

        if (crouching)
        {
            charController.height = charController.height - crouchSpeed * Time.deltaTime;

            if (charController.height <= crouchHeight)
            {
                charController.height = crouchHeight;
            }
        }
        else if(!crouching)
        {
            charController.height = charController.height + crouchSpeed * Time.deltaTime;

            if (charController.height < normalHeight)
            {
                player.position = player.position + offset * Time.deltaTime;
            }
            if (charController.height >= normalHeight)
            {
                charController.height = normalHeight;
            }
        }
    }
}
