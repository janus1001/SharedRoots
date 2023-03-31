using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBlendTest : MonoBehaviour
{
    public Material material;
    public Color c;
    public float timeElapsed;
    public bool bDoAnim;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        c = material.color;
        c.a = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            c.a = 0.0f;
            timeElapsed = 0;
            StartCoroutine("doAnimate");
        }
    }

    IEnumerator doAnimate() {
        while (c.a < 1.0f) {
            timeElapsed += Time.deltaTime;
            //c.a = Mathf.Lerp(0, 1.0f, timeElapsed / 5);
            c.a += 0.01f;
            material.color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
