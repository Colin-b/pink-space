using UnityEngine;

public class ShapeSelectionScript : MonoBehaviour {

    private Document selectedDocument;

    void Update () {
        if (TriggerSelection())
            SelectPointedAtShape();
        else if (TriggerUnselection())
            UnselectDocument();
	}

    private bool TriggerUnselection()
    {
        return selectedDocument != null && Input.GetMouseButtonUp(0);
    }

    private bool TriggerSelection()
    {
        return selectedDocument == null && Input.GetMouseButtonDown(0);
    }

    private Vector3 GetPointer()
    {
        return Input.mousePosition;
    }

    private void SelectPointedAtShape()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(GetPointer());
        if (Physics.Raycast(ray, out hit))
            if (hit.collider != null)
                SelectShape(hit.collider.gameObject);
    }

    private void SelectShape(GameObject shape)
    {
        selectedDocument = shape.GetComponent<Document>();
        if (selectedDocument != null)
            selectedDocument.Selected();
    }

    private void UnselectDocument()
    {
        selectedDocument.UnSelected();
        selectedDocument = null;
    }
}
