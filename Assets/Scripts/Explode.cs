using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Exploder : MonoBehaviour
{
    public void ExplodeFrom(Vector3 origin, float force, float radius)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        rb.AddExplosionForce(force, origin, radius);
    }
}
