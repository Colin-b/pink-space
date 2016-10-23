using System;
using UnityEngine;

public class Document : MonoBehaviour
{
    private Color initialColor;
    private Transform initialParent;
    private Document linkedDoc;
    
    private GameObject myLine;
    private LineRenderer lr;

    public bool IsSelected { get; private set; }

    void Start()
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.drag = 1;
    }

    void Update()
    {
        if(linkedDoc != null)
            UpdateLine(transform.position, linkedDoc.transform.position);
    }

    private void CreateLine(Vector3 start, Vector3 end)
    {
        myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Default"));
        lr.SetColors(Color.green, Color.green);
        lr.SetWidth(0.3f, 0.3f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public virtual void Open()
    {
        // No specific action by default
    }

    private void UpdateLine(Vector3 start, Vector3 end)
    {
        myLine.transform.position = start;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public void Selected()
    {
        IsSelected = true;
        Renderer render = transform.GetComponent<Renderer>();
        initialColor = render.material.color;
        render.material.color = Color.blue;
    }

    public void SelectedRemove()
    {
        IsSelected = true;
        Renderer render = transform.GetComponent<Renderer>();
        initialColor = render.material.color;
        render.material.color = Color.red;
    }

    public void UnSelected()
    {
        IsSelected = false;
        transform.parent = initialParent;
        Renderer render = transform.GetComponent<Renderer>();
        render.material.color = initialColor;
    }

    public void UnSelectedRemove()
    {
        IsSelected = false;
        Renderer render = transform.GetComponent<Renderer>();
        //render.material.color = initialColor;
    }

    public void ChangeParent(Transform anchor)
    {
        initialParent = transform.parent;
        transform.parent = anchor;
        //Vector3 fromDocToPointer = anchor.position - transform.position;
        //transform.Translate(fromDocToPointer);
    }

    public void IsLinkedTo(Document document)
    {
        CreateLine(transform.position, document.transform.position);
        linkedDoc = document;
    }
}
