using PathCreation;
using System.Collections.Generic;
using UnityEngine;

// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class CameraPathFollower : MonoBehaviour {

    enum CamState {
        FreeFlow = 0,
        Focusing,
        SlowMotion,
        Exit
    };

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;

    public GameObject[] woodenSigns;
    public GameObject[] slowMotionPoints;
    public GameObject[] unFocusPoints;
    public GameObject[] preFocusPoints;
    public GameObject finalFocus;
    public GameObject ceiliingSphere;

    public Renderer finalFocusRen;
    public Material finalFocusMat;

    int state = 0;
    public float speed = 3;
    public float slowMotionSpeed = 0.75f;
    public float normalSpeed = 10.0f;
    public int signToLookAt = 0;
    public bool bLookingAtSign = false;
    public float signDistance = 0;
    public float focusPointDistance = 0;
    public float unFocusPointDistance = 0;
    public float PreFocusDistance = 0;
    public float slowDownCameraDelta = 0.01f;
    public float desiredDuration = 2.0f;
    public float timeElapsed;

    public float distanceThreshold = 0.75f;

    public bool bPreFocusSet = false;
    public bool bSlowMotion = false;
    public bool bFinalFocus = false;

    public bool bSetAlpha = false;
    public float alphaAnimTime = 0;

    [SerializeField] CamState cameraState;

    public GameObject signLookAtCenter;

    void Start() {
        if (pathCreator != null) {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            //signLookAtCenter = woodenSigns[0].getChi
        }

        finalFocusRen = GameObject.Find("inner-ring").GetComponent<Renderer>();
        List<Material> materials = new List<Material>();
        finalFocusRen.GetMaterials(materials);
        finalFocusMat = materials[0];
        cameraState = CamState.FreeFlow;

        Debug.Log("sdfkjlasdgslfjghslekjghsk;egf");
    }

    void Update() {
        if (pathCreator == null)
            return;

        if (woodenSigns == null || slowMotionPoints == null || unFocusPoints == null || preFocusPoints == null) {
            Debug.Log("Error: CameraPathFollower: one of the arrays is NULL");
            return;
        }

        distanceTravelled += Time.deltaTime * speed;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

        timeElapsed += Time.deltaTime;

        if (signToLookAt < woodenSigns.Length) {
            focusPointDistance = Vector3.Distance(transform.position, slowMotionPoints[signToLookAt].transform.position);
            unFocusPointDistance = Vector3.Distance(transform.position, unFocusPoints[signToLookAt].transform.position);
            PreFocusDistance = Vector3.Distance(transform.position, preFocusPoints[signToLookAt].transform.position);

            if(PreFocusDistance <= distanceThreshold && !bPreFocusSet) {
                bPreFocusSet = true;
                cameraState = CamState.Focusing;
            }

            else if(focusPointDistance <= distanceThreshold && !bSlowMotion) {
                bSlowMotion = true;
                cameraState = CamState.SlowMotion;
                speed = slowMotionSpeed;
            }

            if(unFocusPointDistance <= distanceThreshold && !bFinalFocus){
                bPreFocusSet = false;
                bSlowMotion = false;
                cameraState = CamState.FreeFlow;
                speed = normalSpeed;

                signToLookAt++;
                if(signToLookAt >= woodenSigns.Length) {
                    signToLookAt = woodenSigns.Length - 1;//not needed, to prevent out-of-bounds incase accessed
                    bFinalFocus = true;
                }
            }

            
            if (bPreFocusSet) {
                lookAtSign();
            }
            else if (bFinalFocus) {
                lookAtFinalPoint();
            }
            else {
                lookAhead();
            }

            if (bSetAlpha) {
                doAlphaAnimation();
            }
        }


        //SetAlpha(Mathf.Lerp(0, 1, timeElapsed / desiredDuration));
        //finalFocusRen.material.color = new Color(255, 255, 255, (int)Mathf.Lerp(0, 255, Time.deltaTime) * 255);
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
    distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    void lookAtSign() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(woodenSigns[signToLookAt].transform.position - transform.position), Time.deltaTime / desiredDuration);
    }
    
    void lookAhead() {
        transform.rotation = Quaternion.Slerp(transform.rotation, pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction), Time.deltaTime / desiredDuration);
    }

    void lookAtFinalPoint() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalFocus.transform.position - transform.position), Time.deltaTime / desiredDuration * 5);
        if (Vector2.Distance(transform.position, ceiliingSphere.transform.position) <= 1.0f) {
            Invoke("showLogo", 3);
        }
    }

    void showLogo() {
        SetAlpha(1.0f);
      //  bSetAlpha = true;
       // alphaAnimTime = 0;
    }

    void doAlphaAnimation() {
        alphaAnimTime += Time.deltaTime;
        Color color = finalFocusMat.color;
        color.a = Mathf.Clamp(Mathf.Lerp(0, 1, alphaAnimTime), 0, 1);
        finalFocusMat.color = color;
        
    }

    void SetAlpha(float alpha) {
        // Here you assign a color to the referenced material,
        // changing the color of your renderer
        Color color = finalFocusMat.color;
        color.a = Mathf.Clamp(alpha, 0, 1);
        finalFocusMat.color = color;
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("------------ OnCollisionEnter ----------------------");
        if (collision.gameObject.tag.Equals("CeilingSphere")) {
            Debug.Log("------------- collided  -----------------------");
            SetAlpha(1);
        }
    }
}
