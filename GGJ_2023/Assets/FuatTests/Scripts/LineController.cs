using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _boxTransforms;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _boxTransforms.Length;
        for (int i = 0; i < _lineRenderer.positionCount; i++) {
            _lineRenderer.SetPosition(i, _boxTransforms[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _lineRenderer.positionCount; i++) {
            _lineRenderer.SetPosition(i, _boxTransforms[i].position);
        }
    }
}
