using UnityEngine;

public class Document : MonoBehaviour
{
    private Color initialColor;

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
        Renderer render = transform.GetComponent<Renderer>();
        render.material.color = initialColor;
    }
}
