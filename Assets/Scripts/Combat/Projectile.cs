using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public string targetTag = "Enemy";
    public float lifetime = 5f;

    Transform shooter;
    Vector3 launchDir;

    public void Launch(Vector3 dir, float speed, float range, Transform shooterTransform)
    {
        launchDir = dir;
        shooter = shooterTransform;
        lifetime = range / speed; 
        GetComponent<Rigidbody>().linearVelocity = dir * speed; 
    }

    void Start() => Destroy(gameObject, lifetime);

    void Update()
    {
        if (shooter && Vector3.Dot(transform.position - shooter.position, launchDir) < -0.1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Projectile>() != null) return; 
        Health health = other.GetComponentInParent<Health>();
        if (health != null && health.CompareTag(targetTag))
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger && health == null)
        {
            Destroy(gameObject);
        }
    }
}