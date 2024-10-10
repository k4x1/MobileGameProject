using UnityEngine;

public class AntiPhasingThroughTheFloor : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private float offsetFromFloor = 0.01f;

    private Rigidbody rb;
    private float ballRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject!");
            return;
        }

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            ballRadius = sphereCollider.radius * transform.lossyScale.x; // scale
        }
        else
        {
            
            ballRadius = Mathf.Min(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z) / 2f;
     
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, floorLayer))
        {
            float distanceToFloor = hit.distance;
            if (distanceToFloor < ballRadius + offsetFromFloor)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = hit.point.y + ballRadius + offsetFromFloor;
                rb.MovePosition(newPosition);
            }
        }
    }
}
