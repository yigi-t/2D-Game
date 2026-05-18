using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ateş Etme Ayarları")]
    public GameObject bulletPrefab;   // Kullanılacak mermi prefab'ı
    public Transform muzzleTransform; // Oyuncunun mermi çıkış noktası (Silah ucu)
    public float fireRate = 0.3f;     // Ateş etme sıklığı (Brawl Stars gibi hızlı olması için düşük tuttuk)
    private float nextFireTime = 0f;

    private Camera mainCamera;

    void Start()
    {
        // Fare konumunu dünya koordinatlarına çevirmek için ana kamerayı alıyoruz
        mainCamera = Camera.main;

        if (muzzleTransform == null)
        {
            Debug.LogError(gameObject.name + ": Lütfen oyuncunun içine bir Muzzle (boş obje) koyun ve buraya atayın!");
        }
    }

    void Update()
    {
        // Fare konumuna doğru karakteri döndür (Görsel yön ayarı)
        LookAtMouse();

        // Sol tık basılı tutulduğunda veya basıldığında ve süre dolduğunda ateş et
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || muzzleTransform == null) return;

        // 1. Farenin ekrandaki pozisyonunu oyun dünyasındaki pozisyona çevir
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // 2D oyunda olduğumuz için Z eksenini sıfırlıyoruz

        // 2. Namlu ucundan fareye giden yön vektörünü hesapla
        Vector2 shootDirection = (mouseWorldPosition - muzzleTransform.position).normalized;

        // 3. Bu yöne uygun açıyı (derece cinsinden) hesapla
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        // 4. Mermiyi namlu ucunda ve o açıda oluştur
        GameObject newBullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.Euler(0, 0, angle));

        // 5. Mermiye "Bunu oyuncu attı, oyuncuya çarpma" talimatını ver
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shooter = gameObject; 
        }
    }

    // Karakterin fare neredeyse o yöne bakmasını sağlayan estetik fonksiyon (Flip)
    void LookAtMouse()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float directionX = mouseWorldPosition.x - transform.position.x;

        Vector3 newScale = transform.localScale;
        if (directionX > 0 && newScale.x < 0)
        {
            newScale.x *= -1; // Sağa bak
        }
        else if (directionX < 0 && newScale.x > 0)
        {
            newScale.x *= -1; // Sola bak
        }
        transform.localScale = newScale;
    }
}