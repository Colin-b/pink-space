using System;
using UnityEngine;
using WorkshopVR;

public class DocumentThrowingScript : MonoBehaviour {
    
    private ViveInput viveInput;

    void Start()
    {
        viveInput = GetComponent<ViveInput>();
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
        Vector3 throwingDirection = transform.position;
        body.AddForce(throwingDirection);
    }
}
