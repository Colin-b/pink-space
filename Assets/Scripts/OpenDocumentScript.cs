using System;
using UnityEngine;
using WorkshopVR;

public class OpenDocumentScript : MonoBehaviour {
    
    private ViveInput viveInput;
    private Document currentDocument;
    private bool isNewRequest = true;

    void Start()
    {
        SteamVR_LaserPointer lazer = GetComponent<SteamVR_LaserPointer>();
        lazer.PointerIn += EnterObject;
        lazer.PointerOut += ExitObject;
        viveInput = GetComponent<ViveInput>();
    }

    private void EnterObject(object sender, PointerEventArgs e)
    {
        Document document = e.target.GetComponent<Document>();
        if (document != null)
            currentDocument = document;
    }

    private void ExitObject(object sender, PointerEventArgs e)
    {
        if (currentDocument == e.target.GetComponent<Document>())
            currentDocument = null;
    }

    void Update()
    {
        if (currentDocument != null && viveInput.IsGripPressed() && isNewRequest)
        {
            isNewRequest = false;
            currentDocument.Open();
        }
        if (!viveInput.IsGripPressed())
            isNewRequest = true;
    }
}
