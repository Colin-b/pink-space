﻿using System;
using UnityEngine;
using WorkshopVR;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 20;
    private Document selectedDocument;
    private ViveInput viveInput;
    private bool isEntered = false;
    private GameObject enteredObject;
    public bool IsTriggerPressed;
    public bool IsTriggerDown;
    public bool IsTriggerUp;

    void Start()
    {
        SteamVR_LaserPointer lazer= GetComponent<SteamVR_LaserPointer>();
        lazer.PointerOut += OutObject;
        lazer.PointerIn += HitObject;

        viveInput = GetComponent<ViveInput>();
    }

    private void HitObject(object sender, PointerEventArgs e)
    {
        Debug.Log("enter : " + e.target.gameObject );
        isEntered = true;
        enteredObject = e.target.gameObject;
        if (viveInput.IsTriggerPressed())
            SelectShape(e.target.gameObject);
    }

    private void OutObject(object sender, PointerEventArgs e)
    {
        Debug.Log("leave :"+ e.target.gameObject);
        isEntered = false;
        enteredObject = e.target.gameObject;
        if (viveInput.IsTriggerPressed())
            SelectShape(e.target.gameObject);
    }
    void Update () {
        IsTriggerPressed = viveInput.IsTriggerPressed();
        IsTriggerDown = viveInput.IsTriggerDown();
        if (IsTriggerPressed && isEntered)
        {
            if (enteredObject)
                SelectShape(enteredObject);

        }
        IsTriggerUp = viveInput.IsTriggerUp();
        if (IsTriggerUp) Debug.Log("Up");

        //if (TriggerUnselection())
        //    UnselectDocument();
        //else if (ShouldMove())
        //    Move();
    }

    private bool ShouldMove()
    {
        return selectedDocument != null && viveInput.IsTriggerDown();
    }

    private bool TriggerUnselection()
    {
        return selectedDocument != null && viveInput.IsTriggerUp();
    }

    private bool TriggerSelection()
    {
        return selectedDocument == null && viveInput.IsTriggerPressed();
    }

    private Vector3 GetPointer()
    {
        return transform.position;
    }

    private void SelectShape(GameObject shape)
    {
        //Debug.Log("Select " +shape);
        selectedDocument = shape.GetComponent<Document>();
        if (selectedDocument != null)
        {
            selectedDocument.Selected();
            Move();
        }
    }

    private void UnselectDocument()
    {
        selectedDocument.UnSelected();
        selectedDocument = null;
    }

    private void Move()
    {
        Debug.Log("Enter on Move");
        selectedDocument.transform.parent = transform; 
        Vector3 fromDocToPointer = GetPointer() - selectedDocument.transform.position;
        fromDocToPointer.Normalize();
        selectedDocument.transform.Translate(fromDocToPointer);
    }

}
