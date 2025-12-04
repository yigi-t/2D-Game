using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("VFX Ayarlarý")] // Inspector'da düzenli görünmesi için baþlýk
    public GameObject DustEffect;
    public Transform feetPos;
    public float dustRate = 0.2f;
    private float nextDustTime;

    [Header("Hareket Ayarlarý")]
    [SerializeField] private float moveSpeed = 5f; // 1f çok yavaþ olabilir, 5f yaptým

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

    private void Update()
    {
        PlayerInput();
        HandleDustEffect(); // <-- YENÝ: Toz efektini burada kontrol ediyoruz
    }


    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    // --- YENÝ EKLENEN FONKSÝYON ---
    private void HandleDustEffect()
    {
        // Karakter hareket ediyor mu? 
        // movement.sqrMagnitude > 0 demek, vektörün boyu 0'dan büyük yani hareket var demektir.
        if (movement.sqrMagnitude > 0.01f)
        {
            // Zamanlayýcý kontrolü
            if (Time.time >= nextDustTime)
            {
                CreateDust();
                nextDustTime = Time.time + dustRate;
            }
        }
    }
    // -----------------------------

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }

    void CreateDust()
    {
        // Güvenlik kontrolü: Eðer inspector'dan atamayý unuttuysan oyun çökmesin
        if (DustEffect != null && feetPos != null)
        {
            Instantiate(DustEffect, feetPos.position, Quaternion.identity);
        }
    }
}