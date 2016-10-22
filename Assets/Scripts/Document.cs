using System;
using UnityEngine;

public class Document : MonoBehaviour
{
    private Color initialColor;
    private Transform initialParent;

    public bool IsSelected { get; private set; }

    void Start()
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.drag = 1;
    }

    public void Selected()
    {
        IsSelected = true;
        Renderer render = transform.GetComponent<Renderer>();
        initialColor = render.material.color;
        render.material.color = Color.blue;
    }

    public void UnSelected()
    {
        IsSelected = false;
        transform.parent = initialParent;
        Renderer render = transform.GetComponent<Renderer>();
        render.material.color = initialColor;
    }

    public void ChangeParent(Transform anchor)
    {
        initialParent = transform.parent;
        transform.parent = anchor;
        //Vector3 fromDocToPointer = anchor.position - transform.position;
        //transform.Translate(fromDocToPointer);
    }
}
