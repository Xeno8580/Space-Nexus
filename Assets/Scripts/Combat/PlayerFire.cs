using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootSpeed = 25f;
    [SerializeField] private float range = 12f;
    [SerializeField] private float spacing = 2.5f; // a fixed gap between bullets

    private bool hasFired;
    private Vector3 lastSpawnPos, lastDir;
    private float lastSpawnTime;

    void Update()
    {
        if (!Input.GetMouseButton(0)) { hasFired = false; return; }

        Vector3 newestBullet = lastSpawnPos + lastDir * shootSpeed * (Time.time - lastSpawnTime);

        if (hasFired && Vector3.Distance(newestBullet, firePoint.position) < spacing) return;

        Vector3 dir = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(dir));
        bullet.GetComponent<Projectile>().Launch(dir, shootSpeed, range, transform);

        hasFired = true;
        lastSpawnPos = firePoint.position;
        lastDir = dir;
        lastSpawnTime = Time.time;
    }
}