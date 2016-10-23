using UnityEngine;

public class Document : MonoBehaviour
{
    private Color initialColor;
    private Transform initialParent;
    private Document linkedDoc;

    public bool IsSelected { get; private set; }
    private bool IsRemoved;

    void Start()
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.drag = 1;
    }

    public virtual void Open()
    {
        // No specific action by default
    }

    public void Selected()
    {
        IsSelected = true;
        Renderer render = transform.GetComponent<Renderer>();
        if(!IsRemoved)
            initialColor = render.material.color;
        IsRemoved = false;
        render.material.color = Color.blue;
    }

    public void SelectedRemove()
    {
        IsSelected = true;
        Renderer render = transform.GetComponent<Renderer>();
        if (!IsRemoved)
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
        IsRemoved = true;
        Renderer render = transform.GetComponent<Renderer>();
    }

    public void ChangeParent(Transform anchor)
    {
        initialParent = transform.parent;
        transform.parent = anchor;
    }

    public void IsLinkedTo(Document document)
    {
        CreateLineTo(document);
        linkedDoc = document;
    }

    private void CreateLineTo(Document document)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = transform.position;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Standard"));
        lr.SetColors(Color.green, Color.green);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, document.transform.position);
        myLine.AddComponent<BoxCollider>();
        myLine.AddComponent<Link>();
        Link link = myLine.GetComponent<Link>();
        link.First = this;
        link.Last = document;
    }
}
