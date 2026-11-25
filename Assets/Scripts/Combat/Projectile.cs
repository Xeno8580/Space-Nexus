using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage = 10;
    public string targetTag = "Enemy";
    public float lifetime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => Destroy(gameObject, lifetime);

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit: {other.name} | tag: {other.tag} | layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        
        if (other.CompareTag(targetTag) && other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger && !other.CompareTag("Player") && !other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    
}
