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
        if(document != null)
            currentDocument = document;
    }

    private void ExitObject(object sender, PointerEventArgs e)
    {
        if (currentDocument == e.target.GetComponent<Document>())
            currentDocument = null;
    }

    void Update()
    {
        IsTouchpadPressed = viveInput.IsTouchpadPressed();
        IsTouchpadUp = viveInput.IsTouchpadUp();
        IsTouchpadDown = viveInput.IsTouchpadDown();
        IsTouchpadTouchPressed = viveInput.IsTouchpadTouched();
        IsTouchpadTouchDown = viveInput.IsTouchpadTouchDown();
        IsTouchpadTouchUp = viveInput.IsTouchpadTouchUp();

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
        // Nothing to do for now
    }

    private void LineStart()
    {
        startDocument = currentDocument;
    }

    private void LineValid()
    {
        startDocument.IsLinkedTo(currentDocument);
    }

    private void LineInvalid()
    {
        viveInput.TriggerHapticPulse(2000);
    }

    private void AllowNewLine()
    {
        startDocument = null;
    }
}
