using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;
    private const float RadiusDetection = 0.25f;

    public bool IsGround()
    {
        bool collision = Physics.OverlapSphere(transform.position, RadiusDetection, _collisionMask).Length > 0;
        return collision;
    }
}
