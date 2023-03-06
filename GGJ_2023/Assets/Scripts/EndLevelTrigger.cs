using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Dialogue aaa;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            DialogueSystem.CreateDialogue(aaa);
        }
    }
}
