using System;
using UnityEngine;
using WorkshopVR;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 1.1f;
    private Document selectedDocument;
    private ViveInput viveInput;

    void Start()
    {
        SteamVR_LaserPointer lazer= GetComponent<SteamVR_LaserPointer>();
        lazer.PointerOut += LeaveObject;
        lazer.PointerIn += EnterObject;

        viveInput = GetComponent<ViveInput>();
    }

    private void EnterObject(object sender, PointerEventArgs e)
    {
        if(selectedDocument == null)
            selectedDocument = e.target.gameObject.GetComponent<Document>();
    }

    private void LeaveObject(object sender, PointerEventArgs e)
    {
        if(selectedDocument != null)
        {
            Document unselectedDocument = e.target.gameObject.GetComponent<Document>();
            if (unselectedDocument == selectedDocument && !selectedDocument.IsSelected)
                selectedDocument = null;
        }
    }

    void Update () {
        if (selectedDocument != null && !selectedDocument.IsSelected && viveInput.IsTriggerPressed())
            SelectDocument();
    }

    private void SelectDocument()
    {
        viveInput.TriggerHapticPulse(1200);
        Rigidbody body = selectedDocument.GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        selectedDocument.Selected();
        AllowToMove();
    }

    private void AllowToMove()
    {
        Transform anchor = transform.FindChild("AnchorHolding");
        selectedDocument.ChangeParent(anchor);
    }

    public Document GetSelectedDocument()
    {
        if (selectedDocument == null || !selectedDocument.IsSelected)
            return null;
        return selectedDocument;
    }

    public void UnSelectDocument()
    {
        if(selectedDocument != null)
        {
            selectedDocument.UnSelected();
            selectedDocument = null;
        }
    }

}
