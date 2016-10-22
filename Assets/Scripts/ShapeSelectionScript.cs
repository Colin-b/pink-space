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
        Transform anchor = transform.FindChild("AnchorHolding");
        selectedDocument.transform.parent = anchor;

       // selectedDocument.transform.localPosition = transform.position ;
        //Vector3 fromDocToPointer = transform.position - selectedDocument.transform.position;
        //float percentageDifferenceAllowed = 0.01f; // is 1%

        //if (!Approximately(transform.position - selectedDocument.transform.position, fromDocToPointer*DocumentDistanceToUserWhenDragged, percentageDifferenceAllowed))
        //{
        //    fromDocToPointer.Normalize();
        //    selectedDocument.transform.Translate(fromDocToPointer);
        //}

    }
    public bool Approximately(Vector3 me, Vector3 other, float allowedDifference)
    {
        var dx = me.x - other.x;
        if (Mathf.Abs(dx) > allowedDifference)
            return false;

        var dy = me.y - other.y;
        if (Mathf.Abs(dy) > allowedDifference)
            return false;

        var dz = me.z - other.z;

        return Mathf.Abs(dz) >= allowedDifference;
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
