using System;
using UnityEngine;
using WorkshopVR;

public class DocumentThrowingScript : MonoBehaviour {
    
    private ViveInput viveInput;
    private Transform throwingDestination;

    void Start()
    {
        SteamVR_LaserPointer lazer = GetComponent<SteamVR_LaserPointer>();
        lazer.PointerIn += EnterObject;
        viveInput = GetComponent<ViveInput>();
    }

    private void EnterObject(object sender, PointerEventArgs e)
    {
        throwingDestination = e.target;
    }

    void Update()
    {
        ShapeSelectionScript selectScript = GetComponent<ShapeSelectionScript>();
        Document doc = selectScript.GetSelectedDocument();
        if (ShouldThrow(doc))
        {
            Throw(doc);
            selectScript.UnSelectDocument();
        }
    }

    private bool ShouldThrow(Document doc)
    {
        return doc != null && !viveInput.IsTriggerPressed();
    }

    private void Throw(Document doc)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Debug.Log("Hit at " + hit.distance + " meters");
            Rigidbody body = doc.GetComponent<Rigidbody>();
            body.AddForce(hit.point);
        }
        else
            Debug.Log("Do not hit");
    }
}
