using System;
using UnityEngine;
using WorkshopVR;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 20;
    private Document selectedDocument;
    private ViveInput viveInput;

    public bool IsTriggerPressed;
    public bool IsTriggerDown;
    public bool IsTriggerUp;

    void Start()
    {
        SteamVR_LaserPointer lazer= GetComponent<SteamVR_LaserPointer>();
        lazer.PointerIn += HitObject;

        viveInput = GetComponent<ViveInput>();
    }

    private void HitObject(object sender, PointerEventArgs e)
    {
        Debug.Log("enter");
        if (viveInput.IsTriggerPressed())
            SelectShape(e.target.gameObject);
    }

    void Update () {
        IsTriggerPressed = viveInput.IsTriggerPressed();
        IsTriggerDown = viveInput.IsTriggerDown();
        if (IsTriggerDown) Debug.Log("Down");
        IsTriggerUp = viveInput.IsTriggerUp();
        if (IsTriggerUp) Debug.Log("Up");

        //if (TriggerUnselection())
        //    UnselectDocument();
        //else if (ShouldMove())
        //    Move();
    }

    private bool ShouldMove()
    {
        return selectedDocument != null && viveInput.IsTriggerDown();
    }

    private bool TriggerUnselection()
    {
        return selectedDocument != null && viveInput.IsTriggerUp();
    }

    private bool TriggerSelection()
    {
        return selectedDocument == null && viveInput.IsTriggerPressed();
    }

    private Vector3 GetPointer()
    {
        return transform.position;
    }

    private void SelectShape(GameObject shape)
    {
        //Debug.Log("Select " +shape);
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
