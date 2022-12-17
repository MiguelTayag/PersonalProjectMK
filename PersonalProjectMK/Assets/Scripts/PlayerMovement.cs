using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerPos;

    public float movementSpeed = 10f;
    private float gravity = -20f;
    private Vector3 velocity; 
    private float jumpHeight = 3f;
    public float sensitivity = 60f;
    public float yRotation; 
    private CharacterController player;
    private Rigidbody rb;
    public Transform mainCamera;
    bool jumpFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main.GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked; // freeze cursor on screen centre
        Cursor.visible = false; // invisible cursor
    }

    void Update()
    {
        // camera rotation
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        transform.Rotate(Vector3.up * xmouse);
        yRotation -= ymouse;
        yRotation = Mathf.Clamp(yRotation, -85f, 60f);
        mainCamera.localRotation = Quaternion.Euler(yRotation, 0, 0);

        if (Input.GetButtonDown("Jump") && jumpFlag == true) rb.AddForce(transform.up * jumpHeight);
    }

    void FixedUpdate()
    {
        // body moving
        velocity = transform.forward * movementSpeed * Input.GetAxis("Vertical") +
                     transform.right * movementSpeed * Input.GetAxis("Horizontal") +
                     transform.up * rb.velocity.y;
        rb.velocity = velocity;

        
    }
    
    private void OnTriggerStay(Collider other)
    {
        jumpFlag = true; // hero can jump
    }

    private void OnTriggerExit(Collider other)
    {
        jumpFlag = false;
    }
}
