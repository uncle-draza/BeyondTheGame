using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineAssigner : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;

    void Start()
    {
        line.SetUpLine(points);
    }
}
