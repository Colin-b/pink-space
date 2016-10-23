using System;
using UnityEngine;

public class Link : MonoBehaviour
{
    public Document First { get; internal set; }
    public Document Last { get; internal set; }

    void Start()
    {
    }

    void Update()
    {
        UpdateLine(First.transform.position, Last.transform.position);
    }

    private void UpdateLine(Vector3 start, Vector3 end)
    {
        transform.gameObject.transform.position = start;
        LineRenderer render = GetComponent<LineRenderer>();
        render.SetPosition(0, start);
        render.SetPosition(1, end);
    }

    public void Delete()
    {
        Destroy(transform.gameObject);
    }
}
