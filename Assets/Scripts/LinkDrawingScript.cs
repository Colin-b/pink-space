using System;
using UnityEngine;
using WorkshopVR;

public class LinkDrawingScript : MonoBehaviour {
    
    private ViveInput viveInput;
    private Document startDocument;
    private Document currentDocument;

    public AudioSource StartLinkingSound;
    public AudioSource ValidLinkSound;
    public AudioSource InvalidLinkSound;

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
    }

    private void LineStart()
    {
        StartLinkingSound.Play();
        startDocument = currentDocument;
    }

    private void LineValid()
    {
        ValidLinkSound.Play();
        startDocument.IsLinkedTo(currentDocument);
    }

    private void LineInvalid()
    {
        InvalidLinkSound.Play();
        viveInput.TriggerHapticPulse(2000);
    }

    private void AllowNewLine()
    {
        startDocument = null;
    }
}
