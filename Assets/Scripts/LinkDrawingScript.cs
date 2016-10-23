using System;
using UnityEngine;
using WorkshopVR;

public class LinkDrawingScript : MonoBehaviour {
    
    private ViveInput viveInput;
    private Document startDocument;
    private Document currentDocument;

    public bool IsTouchpadPressed;
    public bool IsTouchpadUp;
    public bool IsTouchpadDown;
    public bool IsTouchpadTouchPressed;
    public bool IsTouchpadTouchDown;
    public bool IsTouchpadTouchUp;

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
        {
            Debug.Log("Hover a document");
            currentDocument = document;
        }
    }

    private void ExitObject(object sender, PointerEventArgs e)
    {
        if (currentDocument == e.target.GetComponent<Document>())
        {
            Debug.Log("Exit previous document");
            currentDocument = null;
        }
    }

    void Update()
    {
        if (viveInput.IsTouchpadPressed())
        {
            if(startDocument == null && currentDocument != null)
                LineStart();
            else if(startDocument != null)
                LineContinue();
        }
        else if(startDocument != null)
        {
            if (currentDocument != null)
            {
                if(currentDocument == startDocument)
                    LineInvalid();
                else
                    LineValid();
            }
            else
            {
                LineInvalid();
            }
            AllowNewLine();
        }
    }

    private void LineContinue()
    {
        Debug.Log("Continue drawing");
    }

    private void LineStart()
    {
        Debug.Log("Start drawing");
        startDocument = currentDocument;
    }

    private void LineValid()
    {
        Debug.Log("End drawing");
        startDocument.IsLinkedTo(currentDocument);
    }

    private void LineInvalid()
    {
        Debug.Log("Invalid drawing");
        viveInput.TriggerHapticPulse(2000);
    }

    private void AllowNewLine()
    {
        Debug.Log("Reset for a new line");
        startDocument = null;
    }
}
