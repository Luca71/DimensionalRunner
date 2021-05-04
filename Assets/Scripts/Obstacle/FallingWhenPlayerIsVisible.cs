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

    public Vector3 ConeStartingPos;
    public float ConeAngle = 0f;
    public float ConeHeight = 0f;
    private bool visible = false;
    private float speed;
    private int index = 1;
    Vector3 startingPos;
    Transform parent;
    Vector3 offset;
    Transform player;
    [HideInInspector]
    public float edgesDistance = 0f;
    [HideInInspector]
    public float actualAngle = 0f;

    AudioSource audio;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
        speed = FallingSpeed;
        offset = Vector3.zero;
        parent = transform.parent;
        if (parent != null)
            startingPos = parent.position;
        else
            startingPos = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
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
            if (!visible)
            {
                visible =   Vector3.Angle(ConeStartingPos - player.position, transform.up) <= (actualAngle * 0.5f) && 
                            Vector3.Distance(player.position, ConeStartingPos) <= edgesDistance &&
                            speed == FallingSpeed;

            }
            if (visible || speed == GoUpSpeed)
            {
                transform.position = Vector3.MoveTowards(transform.position, Edges[index], Time.deltaTime * speed);
            }

            if(distance < 0.01f)
            {
                
                if (speed == FallingSpeed)
                {
                    audio.PlayOneShot(audio.clip, 1.8f);
                    speed = GoUpSpeed;
                }
                else
                {
                    speed = FallingSpeed;
                }
                index = (index + 1) % Edges.Count;
                visible = false;
            }
        }
    }

    private void PlaySound()
    {

    }
}


[CustomEditor(typeof(FallingWhenPlayerIsVisible)), CanEditMultipleObjects]
public class FreeMoveHandleFallingWhenPlayerIsVisible : Editor
{
    List<Vector3> Edges;
    FallingWhenPlayerIsVisible instance;
    Vector3 coneStartingPos;
    float width = 0f;
    float height = 0f;
    float scaleFactor = 1f;

    private void OnEnable()
    {
        instance = (FallingWhenPlayerIsVisible)target;
        Edges = instance.Edges;
        //scaleFactor = 1f;
        //width = instance.GetComponent<SpriteRenderer>().size.x * scaleFactor* instance.transform.lossyScale.x;
        width = instance.GetComponent<SpriteRenderer>().sprite.bounds.size.x * instance.transform.lossyScale.x;
        height = instance.GetComponent<SpriteRenderer>().sprite.bounds.size.y * instance.transform.lossyScale.y;
        coneStartingPos = instance.transform.position;
    }

    protected virtual void OnSceneGUI()
    {
        if (Edges.Count == 0) return;
        if (Edges.Count > 2) SetEdges();
        if (Vector3.Distance(instance.ConeStartingPos, coneStartingPos) > 0.001f) coneStartingPos = instance.ConeStartingPos;
        float size = HandleUtility.GetHandleSize(Edges[0]) * 0.2f;
        Vector3 snap = Vector3.one * 0.5f;

        EditorGUI.BeginChangeCheck();
        width = instance.GetComponent<SpriteRenderer>().sprite.bounds.size.x *  instance.transform.lossyScale.x;
        height = instance.GetComponent<SpriteRenderer>().sprite.bounds.size.y * instance.transform.lossyScale.y;
        for (int i = 0; i < Edges.Count; i++)
        {

            Vector3 newTargetPosition = Handles.FreeMoveHandle(Edges[i], Quaternion.identity, size, snap, Handles.SphereHandleCap);
            Vector3 newConePos = Handles.FreeMoveHandle(coneStartingPos, Quaternion.identity, size, snap, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                Edges[i] = newTargetPosition;
                coneStartingPos = newConePos;
                instance.ConeStartingPos = coneStartingPos;
            }
        }
        if (Edges.Count > 1)
        {
            Handles.DrawLine(Edges[0], Edges[Edges.Count - 1], 2);
            Handles.DrawSolidRectangleWithOutline(new Rect(Edges[Edges.Count - 1].x - (width * 0.5f), Edges[Edges.Count - 1].y - (height * 0.5f), width, height), new Color(1f, 1f, 1f, 0.3f), Color.black);
        }

        DrawCone(coneStartingPos, instance.ConeHeight, instance.ConeAngle);
    }

    private void DrawCone(Vector3 center, float coneHeight, float coneAngle)
    {

        Vector3 firstHalf = center; //RightOne
        Vector3 secondHalf = center; //LeftOne

        firstHalf.y -= coneHeight /** Mathf.Cos(Mathf.Deg2Rad * coneAngle)*/;
        secondHalf.y -= coneHeight /** Mathf.Cos(Mathf.Deg2Rad * coneAngle)*/;

        firstHalf.x -= coneHeight * Mathf.Sin(Mathf.Deg2Rad * coneAngle);
        secondHalf.x += coneHeight * Mathf.Sin(Mathf.Deg2Rad * coneAngle);

        instance.edgesDistance = (center - firstHalf).magnitude;
        instance.actualAngle = Vector3.Angle(center - firstHalf, center - secondHalf);

        //Draw Cone
        Handles.color = Color.blue;
        Handles.DrawLine(center, firstHalf, 5);
        Handles.DrawLine(center, secondHalf, 5);
        Handles.color = new Color(0f, 1f, 1f, 0.2f);
        Handles.DrawAAConvexPolygon(new Vector3[] { center, firstHalf, secondHalf });
        Handles.color = Color.white;
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
