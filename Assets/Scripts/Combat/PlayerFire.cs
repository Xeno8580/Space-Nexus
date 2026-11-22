using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // Update is called once per frame
    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);


        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            
            rb.linearVelocity = firePoint.forward * shootSpeed;
        }

        Destroy(bullet, 10f);
    }
}
