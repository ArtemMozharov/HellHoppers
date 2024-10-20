using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
// Check if the user is scrolling the mouse wheel up.

//if (Input.mouseScrollDelta.y > 0)

public class PlayerBehaviourScript : MonoBehaviour
{
    public float fov = 60;
    public float movFov = 80;
    public Camera camera;
    //public signed vars
    [Range(50f, 1000f)]
    public float sensitivity = 500f;
    Rigidbody rb;
    [Range(50f, 400f)]
    public float jumpHeight = 150f;
    
    [Range(0f, 650f)]
    public float max_velocity_ground = 320f;

    [Range(0f, 100f)]
    public float max_velocity_air = 30f;

    float accelerate; 

    [Range(0f, 50f)]
    public float friction = 8f;

    public float reloadTime = 10;

    //unsigned vars
    float shotGunPower;
    float timeRemaining;
    Vector3 wishdir;

    //Conditions
    Boolean shotGunReady = true;
    Boolean inAir = false;
    

    public static void GetOutOfAir(PlayerBehaviourScript target)
    {
        target.inAir = false;
    }
    public static void GetInAir(PlayerBehaviourScript target)
    {
        target.inAir = true;
    }
    private void ReloadShotGun()
    {
        GameManager.GetInstance().ReloadingSoundPlay();
        
        shotGunReady = false;
        timeRemaining = reloadTime;
        Invoke("_tick", 1f);
    }
    private void _tick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("_tick", 1f);
        }
        else
        {
            shotGunReady = true;
        }
    }

    private Boolean Jump()
    {
        return (Input.GetKeyDown("space") ||
            Input.GetKey("space") || Input.GetMouseButton(3) || Input.GetMouseButton(4)) && !inAir;
    }
    private Boolean IsMoving()
    {
        return 0 != Vector3.Dot(rb.velocity, DefineWishDir());
    }

    void Start()
    {
        if (movFov <= fov + 10)
        {
            movFov = fov + 20;
        }
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = true;
        accelerate = max_velocity_ground * 10;
        shotGunPower = jumpHeight * 1.5f;
        timeRemaining = 0;
        inAir = true;
    }
    void Update()
    {
        if (IsMoving())
        {
            if (camera.fieldOfView <= movFov) camera.fieldOfView += 0.1f;
        }
        else
        {
            if (camera.fieldOfView >= fov) camera.fieldOfView -= 0.1f;
        }
        //Debug.Log(timeRemaining);
        //JUMP
        if (Jump())
        {
            rb.AddForce(transform.up * jumpHeight);
            inAir = true;
        }
        //SHOTGUN
        if (shotGunReady && Input.GetMouseButtonDown(0))
        {
            Quaternion rotation = transform.rotation;

            // Convert the rotation to a forward vector
            Vector3 forward = rotation * Vector3.forward;
            rb.AddForce(forward * -shotGunPower);
            
            if(true) { 
                rb.AddForce(transform.up * shotGunPower);
            }
            GameManager.GetInstance().ShootingSoundPlay();
            ReloadShotGun();
            GunPhysics.GetInstance().PlayShootingAnimation();
            //inAir = true;
        }
        //y rotation
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity);
    }
    void PlaySound()
    {
        if (inAir && IsMoving())
        {
            //Swinging sound
            GameManager.GetInstance().SwingingSoundPlay();
        }
        else if (IsMoving())
        {
            //walking sound
            GameManager.GetInstance().WalkingSoundPlay();

        }
    }
    void FixedUpdate()
    {
        PlaySound();
        wishdir = DefineWishDir();

        if (inAir)
        {
            rb.velocity = AccelInAir(wishdir);
        }
        else rb.velocity = AccelOnGround(wishdir);

    }
    
    private Vector3 DefineWishDir()
    {
        // Get the player's rotation
        Quaternion rotation = transform.rotation;

        // Convert the rotation to a forward vector
        Vector3 forward = rotation * Vector3.forward;
        Vector3 right = rotation * Vector3.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();


        wishdir = (Input.GetAxis("Horizontal") * right +
            Input.GetAxis("Vertical") * forward);
        wishdir.Normalize();
        return wishdir;
    }
    
    private Vector3 AccelOnGround(Vector3 wishdir)
    {
        Vector3 vel = rb.velocity;
        vel = ApplyFriction(vel);
        float currentSpeed = Vector3.Dot(vel, wishdir);
        //Debug.Log(currentSpeed);
        float addSpeed = Mathf.Clamp(max_velocity_ground - currentSpeed,
            0, accelerate * Time.deltaTime);
        return vel + wishdir * addSpeed;
    }
    private Vector3 AccelInAir(Vector3 wishdir)
    {
        Vector3 vel = rb.velocity;
        float currentSpeed = Vector3.Dot(vel, wishdir);
        //Debug.Log(currentSpeed);
        float addSpeed = Mathf.Clamp(max_velocity_air - currentSpeed,
            0, accelerate * Time.deltaTime /2 );
        return vel + wishdir * addSpeed;
    }
    private Vector3 ApplyFriction(Vector3 vel)
    {
        vel *= (1 - friction * Time.deltaTime);
        return vel;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Start")
        {
            GameManager.GetInstance().ChangeTimerState(GameManager.GameState.TIMERSTART);
            Debug.Log("Timer starts");
        }
        if (other.gameObject.tag == "Finish")
        {
            GameManager.GetInstance().ChangeTimerState(GameManager.GameState.TIMEREND);
            rb.detectCollisions = false;
            Debug.Log("Timer ends. Your time is: " + GameManager.GetInstance().GetTimerTime()) ;

        }
        if (other.gameObject.tag == "Void")
        {
            Debug.Log("Restart");
            GameManager.GetInstance().ChangeTimerState(GameManager.GameState.RESTART);
          
           
        }
    }







}
