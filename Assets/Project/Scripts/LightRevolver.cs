using UnityEngine;

public class LightRevolver : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private TargetEnemy targetEnemy;
    private bool canShoot = false;
    
    void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void EnableShooting()
    {
        canShoot = true;
    }

    public void DisableShooting()
    {
        canShoot = false;
    }

    private void Shoot()
    {
            
            Vector3 aimPoint = targetEnemy.GetAimPoint();
            Debug.Log("Zielpunkt getroffen: " + aimPoint);
            Vector3 shootDirection = (aimPoint - muzzlePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, muzzlePoint.position,muzzlePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.linearVelocity = shootDirection * 20f;
            
    }
}
