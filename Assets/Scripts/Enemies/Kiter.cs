using UnityEngine;

public class Kiter : MonoBehaviour
{
    public float moveSpeed = 3f, preferredDistance = 7f, distanceTolerance = 1.5f, strafeSpeed = 2f;
    public GameObject projectilePrefab;
    public Transform firePoint; // optional empty child at the front
    public float projectileSpeed = 10f, fireInterval = 1.5f, shootRange = 12f, aimInaccuracy = 5f;

    Rigidbody rb;
    Transform player;
    float nextFireTime, nextStrafeFlip, strafeDir = 1f;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        nextFireTime = Time.time + Random.Range(0.5f, fireInterval); // stagger enemies
    }

    void FixedUpdate()
    {
        if (!player) { rb.linearVelocity = Vector3.zero; return; }

        Vector3 to = Vector3.ProjectOnPlane(player.position - transform.position, Vector3.up);
        float dist = to.magnitude;
        Vector3 dir = to.normalized;

        if (Time.time >= nextStrafeFlip)
        {
            strafeDir = Random.value < 0.5f ? -1f : 1f;
            nextStrafeFlip = Time.time + Random.Range(1f, 3f);
        }

        float approach = dist > preferredDistance + distanceTolerance ? 1f : dist < preferredDistance - distanceTolerance ? -1f : 0f;
        rb.linearVelocity = dir * approach * moveSpeed + Vector3.Cross(Vector3.up, dir) * strafeDir * strafeSpeed;
        if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(dir);

        if (projectilePrefab && dist <= shootRange && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireInterval;
            Vector3 shot = Quaternion.Euler(0f, Random.Range(-aimInaccuracy, aimInaccuracy), 0f) * dir;
            GameObject p = Instantiate(projectilePrefab, firePoint ? firePoint.position : transform.position + dir, Quaternion.LookRotation(shot));
            p.GetComponent<Rigidbody>().linearVelocity = shot * projectileSpeed;
        }
    }
}