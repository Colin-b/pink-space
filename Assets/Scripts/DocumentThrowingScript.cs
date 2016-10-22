using System;
using UnityEngine;
using WorkshopVR;

public class DocumentThrowingScript : MonoBehaviour {
    
    private ViveInput viveInput;
    private Transform throwingDestination;

    public int InputFactor = 3;

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
        Rigidbody body = doc.GetComponent<Rigidbody>();
        body.AddForce(viveInput.GetVelocity() * InputFactor, ForceMode.Impulse);
    }
}
