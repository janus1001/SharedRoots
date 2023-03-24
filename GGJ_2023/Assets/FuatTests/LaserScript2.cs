using UnityEngine;

public class LaserScript2 : MonoBehaviour {
    [SerializeField] private LayerMask laserLayer;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private bool bAnimateLerp = false;
    [SerializeField] private float lerpProgress = 0;
    [SerializeField] private float desiredDuration = 0.25f;
    [SerializeField] private float timeElapsed;

    private Transform targetObject;

    public CastDirection castDirection = CastDirection.Forward;
    public bool alwaysOn;
    public float timeSinceLastPower;
    [SerializeField] Light focusLight;

    private void Update() {
        if (timeSinceLastPower < 0.1f || alwaysOn) {
            lineRenderer.enabled = true;

            // Sphere cast to find the target object
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + DirToVec() * 3f, 2f, DirToVec(), maxDistance, laserLayer);
            if (hits.Length > 0) {
                foreach (var hit in hits) {
                    targetObject = hit.collider.transform;

                    LazerScript lazerScript = targetObject.GetComponent<LazerScript>();
                    if (lazerScript) {
                        lazerScript.timeSinceLastPower = 0;
                    }

                    // Enable the line renderer
                    lineRenderer.enabled = true;

                    //set flag for line-renderer animation
                    if (!bAnimateLerp) {
                        bAnimateLerp = true;
                        timeElapsed = 0;
                    }
                    else {
                        NormalLaser(transform.position, targetObject.position);
                    }
                }
            }
            else {
                targetObject = null;

                NormalLaser(transform.position, transform.position + transform.forward * 3);
                //lineRenderer.SetPosition(0, transform.position);
                //lineRenderer.SetPosition(1, transform.position + transform.forward * 3);
            }
        }
        else {
            lineRenderer.enabled = false;
            bAnimateLerp = false;

            return;
        }

        if (bAnimateLerp) {
            // if there is no target object, then do normal projection and return
            if (targetObject == null) {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * 3);
                bAnimateLerp = false;
                Debug.Log("----------- target == NULL -------------");
            }
            else {
                AnimateLaser(transform.position, targetObject.position);

                //if lerp has finished
                if (lerpProgress >= 1.0f) {

                }
            }
        }
    }
    public void LateUpdate() {
        timeSinceLastPower += Time.deltaTime;
    }

    private void NormalLaser(Vector3 vecFrom, Vector3 vecTo) {
        //Set the positions of the line renderer
        lineRenderer.SetPosition(0, vecFrom);
        lineRenderer.SetPosition(1, vecTo);
    }

    private void AnimateLaser(Vector3 vecFrom, Vector3 vecTo) {
        //keep track of time elapsed to feed into lerp-function
        timeElapsed += Time.deltaTime;

        //calculate next lerp step
        lerpProgress = timeElapsed / desiredDuration;

        //Set the positions of the line renderer
        lineRenderer.SetPosition(0, vecFrom);//this is to always project it from source even if source is moving
        lineRenderer.SetPosition(1, Vector3.Lerp(vecFrom, vecTo, lerpProgress));
    }

    Vector3 DirToVec() {
        Vector3 castDirectionVector = Vector3.zero;
        switch (castDirection) {
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

    public enum CastDirection {
        Forward,
        Right,
        Back,
        Left
    }
}