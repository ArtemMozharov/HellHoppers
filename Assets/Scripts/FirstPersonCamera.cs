using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform target;
    
    public float sensitivity = 150f;
    
    float rotationX = 1f;
    

    void Start()
    {
        
        Cursor.visible = false;
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -89f, 89f);
        transform.position = target.position;
        transform.LookAt(transform.position + target.forward, Vector3.up);
        transform.Rotate(Vector3.left, rotationX);
    }

}
