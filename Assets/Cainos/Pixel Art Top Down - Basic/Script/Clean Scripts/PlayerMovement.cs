using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("VFX Ayarları")]
    public GameObject DustEffect;
    public Transform feetPos;
    public float dustRate = 0.2f;
    private float nextDustTime;

    [Header("Hareket Ayarları")]
    [SerializeField] private float moveSpeed = 5f; 

    [HideInInspector] public bool CanMove = true; // KnockbackReceiver tarafından kontrol edilir

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    
    // Temiz kapatma
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnDestroy()
    {
        playerControls.Dispose();
    }

    private void Update()
    {
        if (CanMove)
        {
            PlayerInput();
        }
        HandleDustEffect();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            AdjustPlayerFacingDirection(); 
            Move();
        }
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x > 0.01f) // Sağa hareket
        {
            mySpriteRenderer.flipX = false;
        }
        else if (movement.x < -0.01f) // Sola hareket
        {
            mySpriteRenderer.flipX = true;
        }
    }

    private void HandleDustEffect()
    {
        if (movement.sqrMagnitude > 0.01f && CanMove)
        {
            if (Time.time >= nextDustTime)
            {
                CreateDust();
                nextDustTime = Time.time + dustRate;
            }
        }
    }

    void CreateDust()
    {
        if (DustEffect != null && feetPos != null)
        {
            Instantiate(DustEffect, feetPos.position, Quaternion.identity);
        }
    }
}