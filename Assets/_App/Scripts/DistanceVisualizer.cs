using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceVisualizer : MonoBehaviour
{
    [SerializeField] 
    private TextMesh _textMesh;

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
        _textMesh.text = $"{transform.position.y} ps.";
    }
}
