using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("VFX Ayarları")]
    public GameObject DustEffect;
    public Transform feetPos;
    public float dustRate = 0.2f;
    private float nextDustTime;

    [Header("Hareket Ayarları")]
    [SerializeField] private float moveSpeed = 5f; 

    [Header("İttirme Durumu")] 
    private bool isKnockedBack = false; 

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
    
    // Temiz kapatma için eklendi
    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Temiz kapatma için eklendi
    private void OnDestroy()
    {
        playerControls.Dispose();
    }

    private void Update()
    {
        // Sadece ittirilmiyorsa girdi al
        if (!isKnockedBack)
        {
            PlayerInput();
        }
        HandleDustEffect();
    }


    private void FixedUpdate()
    {
        // Sadece ittirilmiyorsa yüz yönünü ayarla ve hareket et
        if (!isKnockedBack)
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

    private void HandleDustEffect()
    {
        if (movement.sqrMagnitude > 0.01f)
        {
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

    private void AdjustPlayerFacingDirection()
    {
        // Yalnızca yatay (X ekseninde) hareket varsa karakterin yönünü değiştir.
        if (movement.x > 0.01f) // Sağa hareket (D tuşu)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (movement.x < -0.01f) // Sola hareket (A tuşu)
        {
            mySpriteRenderer.flipX = true;
        }
    }

    // İttirme (knockback) kuvvetini uygulayan dışarıdan çağrılan metot
    public void ApplyKnockback(Vector3 direction, float force, float duration)
    {
        StopAllCoroutines(); 
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    // İttirme sürecini ve yumuşak durmayı yöneten Coroutine
    private IEnumerator KnockbackRoutine(Vector3 direction, float force, float duration)
    {
        isKnockedBack = true; // Karakter kontrol dışı

        // 1. ANLIK VURUŞ: ForceMode2D.Impulse kullanarak anlık darbe uygula.
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        // 2. BEKLEME (İttirme süresi): Kuvvetin etkisini görmesi için bekle.
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 3. YUMUŞAK DURMA (Kayma/Sürtünme Efekti): 
        // Hızı yumuşak bir şekilde sıfıra indir.
        float smoothDuration = 0.15f; // Durma (kayma) süresi
        Vector2 startVelocity = rb.linearVelocity;

        elapsed = 0f;
        while (elapsed < smoothDuration)
        {
            float t = elapsed / smoothDuration; 
            
            // Hızı zamanla Vector2.zero'ya doğru çek.
            rb.linearVelocity = Vector2.Lerp(startVelocity, Vector2.zero, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // 4. BİTİŞ: Kontrolü geri ver
        isKnockedBack = false;
        rb.linearVelocity = Vector2.zero; // Tamamen durduğundan emin ol
    }

    void CreateDust()
    {
        if (DustEffect != null && feetPos != null)
        {
            Instantiate(DustEffect, feetPos.position, Quaternion.identity);
        }
    }
}