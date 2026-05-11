using UnityEngine;

public class EnemyAI_Optimized : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform currentTarget; 

    [Header("Referanslar")]
    [SerializeField] private Transform backupPlayer; 

    [Header("Hareket Ayarları")]
    public float moveSpeed = 3f;
    public float sightRange = 15f;
    private float sightRangeSq;

    [Header("Ateş Etme Ayarları")]
    public Transform muzzleTransform; 
    public GameObject bulletPrefab;   
    public float fireRate = 1.2f;     
    private float nextFireTime = 0f;  

    [Header("Hedefleme Ayarları")]
    public string[] targetTags = { "Cat", "Enemy" }; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sightRangeSq = sightRange * sightRange;
    }

    void Update()
    {
        currentTarget = FindClosestTarget();

        if (currentTarget == null) currentTarget = backupPlayer;
        if (currentTarget == null) return;

        FlipCharacter(currentTarget.position);

        float distanceSq = (currentTarget.position - transform.position).sqrMagnitude;
        if (distanceSq < sightRangeSq && Time.time >= nextFireTime)
        {
            Shoot(currentTarget);
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        if (currentTarget == null) return;

        float distanceSq = (currentTarget.position - transform.position).sqrMagnitude;
        if (distanceSq < sightRangeSq)
            rb.linearVelocity = (currentTarget.position - transform.position).normalized * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    private Transform FindClosestTarget()
    {
        float closestDistanceSq = sightRangeSq;
        Transform bestTarget = null;

        foreach (string tag in targetTags)
        {
            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in potentialTargets)
            {
                if (obj == gameObject) continue;

                // --- KRİTİK: Sadece canı olan (Health scriptli) objeleri vur ---
                if (obj.GetComponent<Health>() == null) continue;

                float distSq = (obj.transform.position - transform.position).sqrMagnitude;
                if (distSq < closestDistanceSq)
                {
                    closestDistanceSq = distSq;
                    bestTarget = obj.transform;
                }
            }
        }
        return bestTarget;
    }

    private void FlipCharacter(Vector3 target)
    {
        float dirX = target.x - transform.position.x;
        Vector3 s = transform.localScale;
        if (dirX > 0 && s.x < 0) s.x *= -1;
        else if (dirX < 0 && s.x > 0) s.x *= -1;
        transform.localScale = s;
    }

    private void Shoot(Transform target)
    {
        if (muzzleTransform == null || bulletPrefab == null) return;

        Vector2 dir = (target.position - muzzleTransform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject b = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.Euler(0, 0, angle));
        Bullet bs = b.GetComponent<Bullet>();
        if (bs != null) bs.shooter = gameObject;
    }
}