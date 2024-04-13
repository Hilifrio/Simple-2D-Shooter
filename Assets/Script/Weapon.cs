using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    [Header("Weapon Stats")]
    public float fireRate = 0;
    public int damage = 10;
    public int bulletSpeed;
    public int magasinSize;
    public float reloadTime = .3f;

    public LayerMask whatToHit;

    public Transform BulletTrailPrefab;
    public Transform HitPrefab;
    public Transform MuzzleFlashPrefab;

    public float camShakeAmt = 0.1f;
    public float camShakeLength = 0.1f;

    CameraShake camShake;

    public string weaponShootSound = "DefaultShot";

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    float timeToFire = 0;
    Transform firePoint;

    //Caching
    AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No firepoint?");
        }
    }

    void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if(camShake == null)
        {
            Debug.LogError("No camera shake script found on GM object");
        }
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager referenced in Weapon");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        
        //Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)*100, Color.cyan);

        if(hit.collider != null)
        {
            //Debug.Log("We hit " + hit.collider.name + " and did " + damage);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.DamageEnemy(damage);
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if(lr != null)
        {
            //SET POSITIONS
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.08f);

        if(hitNormal != new Vector3(9999, 9999, 9999)) 
        {
            Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitParticle.gameObject, 0.5f);
        }

        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 1f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.05f);

        //shake the camera
        camShake.Shake(camShakeAmt,camShakeLength);

        //Play sound 
        audioManager.PlaySound(weaponShootSound);
    }
}
