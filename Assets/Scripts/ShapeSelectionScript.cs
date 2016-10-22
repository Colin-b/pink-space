using System;
using UnityEngine;
using WorkshopVR;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 1.1f;
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
        if (selectedDocument != null && !selectedDocument.IsSelected)
        {
            Debug.Log("Select document");
            selectedDocument.Selected();
            AllowToMove();
        }
    }

    private void AllowToMove()
    {
        Transform anchor = transform.FindChild("AnchorHolding");
        selectedDocument.ChangeParent(anchor);
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
