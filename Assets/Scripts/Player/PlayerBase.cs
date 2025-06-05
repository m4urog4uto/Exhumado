using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    CharacterController _controller;
    PlayerRespawn playerRespawn;

    // public Animator animator;

    // private bool isArmed = false;

    float health = 100f;
    public float Health => health;
    float protection = 0f;
    public float Protection => protection;

    float _posV;
    float _posH;
    float _gravity = -9.8f;

    Vector3 _playerVelocity;
    Vector3 _spherePhysics;

    [SerializeField] float _speed = 0;
    void Awake()
    {
        playerRespawn = GetComponent<PlayerRespawn>();
        _controller = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (IsGrounded() && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0;
        }

        _posV = Input.GetAxis("Vertical");
        _posH = Input.GetAxis("Horizontal");

        _playerVelocity.x = _posH * _speed;
        _playerVelocity.z = _posV * _speed;
        _playerVelocity.y += _gravity;

        _controller.Move(_playerVelocity * Time.deltaTime);

        // animator.SetFloat("VelY", _posH);
        // animator.SetFloat("VelX", _posV);

        // if (isArmed)
        // {
        //     animator.SetBool("isArmed", true);
        // }
    }

    bool IsGrounded()
    {
        _spherePhysics = new Vector3(transform.position.x, transform.position.y - 0.71f, transform.position.z);
        return Physics.CheckSphere(_spherePhysics, 0.3f);
    }

    // public void RemoveLive()
    // {
    //     if (GameManager.Instance.Lives != 0)
    //     {
    //         playerRespawn.Respawn();
    //         health = 100f;
    //         GameManager.Instance.RemoveLives();
    //     }
    //     else
    //     {
    //         GameManager.Instance.ResetData();
    //         Destroy(gameObject);
    //         SceneManager.LoadScene("Lose");
    //     }
    // }

    public void TakeDamage()
    {
        playerRespawn.Respawn();
    }

    public float GetProtection()
    {
        if (protection <= 0)
        {
            return protection = 0;
        }
        else
        {
            return protection;
        }
        ;
    }

    public void RestoreHealth()
    {
        health = 100f;
    }

    public void AddProtection()
    {
        protection = 100f;
    }

    public void AddLive()
    {
        // GameManager.Instance.AddLives();
    }

    // public void SetAnimationIsArmed(bool value)
    // {
    //     animator.SetBool("isArmed", value);
    // }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);       
    }
}
