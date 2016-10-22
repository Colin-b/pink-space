using UnityEngine;

public class Document : MonoBehaviour
{
    public void Selected()
    {
        Renderer render = transform.GetComponent<Renderer>();
        render.material.color = Color.blue;
    }

    public void UnSelected()
    {
        Renderer render = transform.GetComponent<Renderer>();
        render.material.color = Color.green;
    }
}
