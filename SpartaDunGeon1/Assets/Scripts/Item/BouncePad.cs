using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float impulseStrength = 12f;
    public bool resetVerticalVelocity = true;
    public bool onlyAffectPlayer = true;

    [Header("Cooldown")]
    public float reuseDelay = 0.05f;
    private float lastBounceTime = -999f;

    private void OnCollisionEnter(Collision collision)
    {
        if (onlyAffectPlayer && !collision.gameObject.CompareTag("Player"))
            return;

        var rb = collision.rigidbody;
        if (rb == null) return;

        if (Time.time - lastBounceTime < reuseDelay) return;
        lastBounceTime = Time.time;

        if (resetVerticalVelocity)
        {
            var v = rb.velocity;
            Vector3 up = transform.up;
            float verticalSpeed = Vector3.Dot(v, up);
            v -= up * verticalSpeed;
            rb.velocity = v;
        }

        rb.AddForce(transform.up * impulseStrength, ForceMode.Impulse);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.up * 1.0f);
    }
}
