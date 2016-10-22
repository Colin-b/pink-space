using UnityEngine;

public class Document : MonoBehaviour
{
    private Color initialColor;

    public void Selected()
    {
        Renderer render = transform.GetComponent<Renderer>();
        initialColor = render.material.color;
        render.material.color = Color.blue;
    }

    public void UnSelected()
    {
        Renderer render = transform.GetComponent<Renderer>();
        render.material.color = initialColor;
    }
}
