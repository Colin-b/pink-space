﻿using UnityEngine;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 20;

    private Document selectedDocument;

    void Update () {
        Debug.Log(Input.GetMouseButtonDown(0));
        if (TriggerSelection())
            SelectPointedAtShape();
        else if (TriggerUnselection())
            UnselectDocument();
        else if (ShouldMove())
            Move();
	}

    private bool ShouldMove()
    {
        return selectedDocument != null && Input.GetMouseButtonDown(0);
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
        Debug.Log("selected");
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

    private void Move()
    {
        Debug.Log("Enter on Move");
        Vector3 fromDocToPointer = GetPointer() - selectedDocument.transform.position;
        fromDocToPointer.Normalize();
        selectedDocument.transform.Translate(fromDocToPointer);
    }
}
