using UnityEngine;

public class LazerScript : MonoBehaviour
{
    [SerializeField] private LayerMask laserLayer;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private LineRenderer lineRenderer;

    private Transform targetObject;

    public CastDirection castDirection = CastDirection.Forward;
    public bool alwaysOn;
    public float timeSinceLastPower;

    private void Update()
    {
        if (timeSinceLastPower < 0.1f || alwaysOn)
        {
            lineRenderer.enabled = true;

            // Sphere cast to find the target object
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + DirToVec() * 3f, 2f, DirToVec(), maxDistance, laserLayer);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    targetObject = hit.collider.transform;

                    LazerScript lazerScript = targetObject.GetComponent<LazerScript>();
                    if (lazerScript)
                    {
                        lazerScript.timeSinceLastPower = 0;
                    }

                    // Enable the line renderer
                    lineRenderer.enabled = true;

                    // Set the positions of the line renderer
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, targetObject.position);
                }
            }
            else
            {
                targetObject = null;

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * 3);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            return;
        }
    }
    public void LateUpdate()
    {
        timeSinceLastPower += Time.deltaTime;
    }

    Vector3 DirToVec()
    {
        Vector3 castDirectionVector = Vector3.zero;
        switch (castDirection)
        {
            case CastDirection.Forward:
                castDirectionVector = transform.forward;
                break;
            case CastDirection.Right:
                castDirectionVector = transform.right;
                break;
            case CastDirection.Back:
                castDirectionVector = -transform.forward;
                break;
            case CastDirection.Left:
                castDirectionVector = -transform.right;
                break;
        }
        return castDirectionVector;
    }

    public enum CastDirection
    {
        Forward,
        Right,
        Back,
        Left
    }
}