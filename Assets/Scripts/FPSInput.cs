using UnityEngine;

public class FPSInput : MonoBehaviour
{
    [SerializeField] float playerMovementSpeed = 10f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Get horizontal input
        float inputX = Input.GetAxis("Horizontal");

        //Get vertical input
        float inputY = Input.GetAxis("Vertical");

        //Get the movement direction
        Vector3 move = Vector3.right * inputX + Vector3.forward * inputY;

        //Transform movement direction to local coordinates
        move = transform.TransformDirection(move);

        //Move player
        controller.Move(move * playerMovementSpeed * Time.deltaTime);
    }
}