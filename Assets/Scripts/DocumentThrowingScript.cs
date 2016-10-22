using System;
using UnityEngine;
using WorkshopVR;

public class DocumentThrowingScript : MonoBehaviour {
    
    private ViveInput viveInput;

    void Start()
    {
        viveInput = GetComponent<ViveInput>();
    }

    //void Update () {
    //    Document doc = GetComponent<ShapeSelectionScript>().GetSelectedDocument();
    //    if (ShouldThrow(doc))
    //        Throw(doc);
    //}

    private bool ShouldThrow(Document doc)
    {
        return doc != null && !viveInput.IsTriggerPressed();
    }

    private void Throw(Document doc)
    {
        doc.UnSelected();
    }
}
