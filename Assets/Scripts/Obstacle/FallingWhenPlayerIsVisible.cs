using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FallingWhenPlayerIsVisible : MonoBehaviour
{
    public float FallingSpeed = 5f;
    public float GoUpSpeed = 1f;
    public List<Vector3> Edges = new List<Vector3>();
    private float speed;
    private int index = 1;
    Vector3 startingPos;
    Transform parent;
    Vector3 offset;

    private void Start()
    {
        speed = FallingSpeed;
        offset = Vector3.zero;
        parent = transform.parent;
        if (parent != null)
            startingPos = parent.position;
        else
            startingPos = Vector3.zero;
    }
    void Update()
    {
        if (Edges.Count > 1)
        {
            if (parent != null && Vector3.Distance(startingPos, parent.position) > 0.0001f)
            {
                offset = parent.position - startingPos;
                for (int i = 0; i < Edges.Count; i++)
                {
                    Edges[i] += offset;
                }
                startingPos = parent.position;
            }
            float distance = Vector3.Distance(transform.position, Edges[index]);
            if (distance > 0.01f)
                transform.position = Vector3.MoveTowards(transform.position, Edges[index], Time.deltaTime * speed);
            else
            {
                if (speed == FallingSpeed)
                    speed = GoUpSpeed;
                else
                    speed = FallingSpeed;
                index = (index + 1) % Edges.Count;
            }
        }
    }
}
[CustomEditor(typeof(FallingWhenPlayerIsVisible)), CanEditMultipleObjects]
public class FreeMoveHandleFallingWhenPlayerIsVisible : Editor
{
    List<Vector3> Edges;
    FallingWhenPlayerIsVisible instance;
    float width = 0f;
    float height = 0f;
    float scaleFactor = 1f;

    private void OnEnable()
    {
        instance = (FallingWhenPlayerIsVisible)target;
        Edges = instance.Edges;
        scaleFactor = 1.28f;
        width = instance.GetComponent<SpriteRenderer>().size.x * scaleFactor* instance.transform.lossyScale.x;
        height = instance.GetComponent<SpriteRenderer>().size.y * scaleFactor * instance.transform.lossyScale.y;
    }

    protected virtual void OnSceneGUI()
    {
        if (Edges.Count == 0) return;
        if (Edges.Count > 2) SetEdges();
        float size = HandleUtility.GetHandleSize(Edges[0]) * 0.2f;
        Vector3 snap = Vector3.one * 0.5f;

        EditorGUI.BeginChangeCheck();
        width = instance.GetComponent<SpriteRenderer>().size.x * scaleFactor * instance.transform.lossyScale.x;
        height = instance.GetComponent<SpriteRenderer>().size.y * scaleFactor * instance.transform.lossyScale.y;
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
            Handles.DrawLine(Edges[0], Edges[Edges.Count - 1], 2);
            Handles.DrawSolidRectangleWithOutline(new Rect(Edges[Edges.Count - 1].x - (width * 0.5f), Edges[Edges.Count - 1].y - (height * 0.5f), width, height), new Color(1f, 1f, 1f, 0.3f), Color.black);
        }
    }

    private void OnValidate()
    {
        SetEdges();
        width = instance.GetComponent<SpriteRenderer>().size.x * scaleFactor * instance.transform.lossyScale.x;
        height = instance.GetComponent<SpriteRenderer>().size.y * scaleFactor * instance.transform.lossyScale.y;
    }


    private void SetEdges()
    {
        if (instance.Edges.Count > 2)
        {
            instance.Edges = instance.Edges.Take(2).ToList();
            Edges = instance.Edges;
        }
        else
            Edges = instance.Edges;
    }
}
