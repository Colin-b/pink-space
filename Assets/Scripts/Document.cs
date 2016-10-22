using System;
using UnityEngine;

public class Document : MonoBehaviour
{
    private Color initialColor;
    private Transform initialParent;

    public bool IsSelected { get; private set; }

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
    }
}
