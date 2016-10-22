using System;
using UnityEngine;
using WorkshopVR;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 20;
    private Document selectedDocument;
    private ViveInput viveInput;
    private bool isEntered = false;
    private GameObject enteredObject;

    void Start()
    {
        SteamVR_LaserPointer lazer= GetComponent<SteamVR_LaserPointer>();
        lazer.PointerOut += LeaveObject;
        lazer.PointerIn += EnterObject;

        viveInput = GetComponent<ViveInput>();
    }

    private void EnterObject(object sender, PointerEventArgs e)
    {
        isEntered = true;
        enteredObject = e.target.gameObject;
        if (viveInput.IsTriggerPressed())
            SelectShape();
    }

    private void LeaveObject(object sender, PointerEventArgs e)
    {
        isEntered = false;
    }

    void Update () {
        if (isEntered && viveInput.IsTriggerPressed())
            SelectShape();
    }

    private void SelectShape()
    {
        selectedDocument = enteredObject.GetComponent<Document>();
        if (selectedDocument != null)
        {
            selectedDocument.Selected();
            AllowToMove();
        }
    }

    private void AllowToMove()
    {
        selectedDocument.transform.parent = transform; 
        Vector3 fromDocToPointer = transform.position - selectedDocument.transform.position;
        fromDocToPointer.Normalize();
        selectedDocument.transform.Translate(fromDocToPointer);
    }

    public Document GetSelectedDocument()
    {
        return selectedDocument;
    }

    public void UnSelectDocument()
    {
        selectedDocument.UnSelected();
        selectedDocument = null;
    }

}
