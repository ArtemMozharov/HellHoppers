using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    private PlayerBehaviourScript player;

    //PlayerBehaviourScript player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerBehaviourScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("Player collided with the floor.");
            PlayerBehaviourScript.GetOutOfAir(player);
            GameManager.GetInstance().JumpingSoundPlay();
        }

        /*
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerBehaviour.enabled = false;
            GameManager.GetInstance().state = GameState.LOST;
        }
        */
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            
            PlayerBehaviourScript.GetInAir(player);
        }
    }
}
