using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//public class MyList<T> : List<T>
//{
//    public Action OnAdd;

//    public new void Add(T item) // "new" to avoid compiler-warnings, because we're hiding a method from base-class
//    {
//        if (null != OnAdd)
//        {
//            OnAdd.Invoke();
//        }
//        base.Add(item);
//    }
//}
public class MoveAroundPerimeter : MonoBehaviour
{
    public float Speed = 1;
    public bool ClockWise = true;
    public List<Vector3> Edges = new List<Vector3>();
    private int index = 0;

    private void Start()
    {
        if (!ClockWise)
        {
            Edges.Reverse();
        }
    }
    void Update()
    {
        if (Edges.Count > 1)
        {
            float distance = Vector3.Distance(transform.position, Edges[index]);
            if (distance > 0.01f)
                transform.position = Vector3.MoveTowards(transform.position, Edges[index], Time.deltaTime * Speed);
            else
                index = (index + 1) % Edges.Count;
        }
    }
}
[CustomEditor(typeof(MoveAroundPerimeter)), CanEditMultipleObjects]
public class FreeMoveHandleMoveAroundPerimeter : Editor
{
    List<Vector3> Edges;
    MoveAroundPerimeter instance;

    private void OnEnable()
    {
        instance = (MoveAroundPerimeter)target;
        Edges = instance.Edges;
    }
    protected virtual void OnSceneGUI()
    {
        if (Edges.Count == 0) return;
        float size = HandleUtility.GetHandleSize(Edges[0]) * 0.2f;
        Vector3 snap = Vector3.one * 0.5f;

        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < Edges.Count; i++)
        {

            Vector3 newTargetPosition = Handles.FreeMoveHandle(Edges[i], Quaternion.identity, size, snap, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                Edges[i] = newTargetPosition;
            }
        }
        if (Edges.Count > 1)
        {
            Handles.DrawAAPolyLine(5, Edges.ToArray());
            Handles.DrawLine(Edges[0], Edges[Edges.Count - 1], 2);
        }
    }

    private void OnValidate()
    {
        Edges = instance.Edges;
    }
}
