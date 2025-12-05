using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("VFX Ayarları")] // Inspector'da düzenli görünmesi için başlık
    public GameObject DustEffect;
    public Transform feetPos;
    public float dustRate = 0.2f;
    private float nextDustTime;

    [Header("Hareket Ayarları")]
    [SerializeField] private float moveSpeed = 5f; // 1f çok yavaş olabilir, 5f yaptım

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
        HandleDustEffect();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection(); // <-- Burası değişti
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void HandleDustEffect()
    {
        // Karakter hareket ediyor mu?
        if (movement.sqrMagnitude > 0.01f)
        {
            // Zamanlayıcı kontrolü
            if (Time.time >= nextDustTime)
            {
                CreateDust();
                nextDustTime = Time.time + dustRate;
            }
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    // ##################################################################
    // ############ DEĞİŞTİRİLEN VE YENİLENEN FONKSİYON ############
    // ##################################################################
    private void AdjustPlayerFacingDirection()
    {
        // Yalnızca yatay (X ekseninde) hareket varsa karakterin yönünü değiştir.
        if (movement.x > 0.01f) // Sağa hareket (D tuşu)
        {
            // Karakter sağa baksın (normal)
            mySpriteRenderer.flipX = false;
        }
        else if (movement.x < -0.01f) // Sola hareket (A tuşu)
        {
            // Karakter sola baksın (sprite'ı yatayda çevir)
            mySpriteRenderer.flipX = true;
        }
        // Eğer hareket.x yaklaşık 0 ise (duruyorsa veya sadece yukarı/aşağı hareket ediyorsa), 
        // son baktığı yönü koruyacaktır.
    }
    // ##################################################################
    // ##################################################################

    void CreateDust()
    {
        // Güvenlik kontrolü
        if (DustEffect != null && feetPos != null)
        {
            Instantiate(DustEffect, feetPos.position, Quaternion.identity);
        }
    }
}