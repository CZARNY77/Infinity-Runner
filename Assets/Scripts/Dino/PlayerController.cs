using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce;
    float gravity = 9.81f;
    CharacterController characterController;
    Vector3 direction;
    Animator animator;
    Vector3 startPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        direction = Vector3.zero;
        animator = GetComponent<Animator>();
        switchPlayer(false);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.startGame && !GameManager.instance.death)
        {
            transform.position = startPosition;
            switchPlayer(true);
        }
        if (!GameManager.instance.startGame && Input.GetButtonDown("Jump"))
        {
            switchPlayer(true);
            GameManager.instance.startGame = true;
            GameManager.instance.speedWorld = 5f;
        }
        Jump();
    }

    void Jump()
    {
        if (characterController.enabled == false) return;
        direction += Vector3.down * gravity * Time.deltaTime;
        if (characterController.isGrounded)
        {
            direction = Vector3.down;
            if (Input.GetButtonDown("Jump"))
            {
                direction = Vector3.up * jumpForce;
                GameManager.instance.PlaySound(GameManager.instance.jump);
            }
        }
        characterController.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            switchPlayer(false);
            GameManager.instance.GameOver();
        }
    }

    void switchPlayer(bool turn)
    {
        characterController.enabled = turn;
        animator.enabled = turn;
    }
}
