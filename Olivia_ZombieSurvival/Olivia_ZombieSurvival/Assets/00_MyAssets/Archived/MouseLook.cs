using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //nice
    }

    // Update is called once per frame
    void Update()
    {                                                                                //Rotates camera not player
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //Left and right mouse movement.
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //Up and down mouse movement.

        xRotation -= mouseY; //minus so rotation is not flipped
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamps between -90 and 90 degrees, player dopesnt over rotate.

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //enables clamping a ceryaon degree range eg 180 degrees.
        playerBody.Rotate(Vector3.up * mouseX); //reference to what will rotate (player)
    }
}
