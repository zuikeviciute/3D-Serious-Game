using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float xRotation = 0f;
    public bool typing = false;

    private void OnEnable()
    {
        Actions.OnCodeOpen += LockCamera;
        Actions.OnCodeClose += UnlockCamera;
    }

    private void OnDisable()
    {
        Actions.OnCodeOpen -= LockCamera;
        Actions.OnCodeClose -= UnlockCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!typing) MoveCamera();
    }

    public void MoveCamera()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        target.Rotate(Vector3.up * inputX);
    }

    public void LockCamera()
    {
        typing = true;
    }

    public void UnlockCamera()
    {
        typing = false;
    }
}
