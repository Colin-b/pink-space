﻿using UnityEngine;
using System.Collections;
using WorkshopVR;

public class ShapeRemover : MonoBehaviour {
    private ViveInput2 viveInput;
    private Document selectedDocument;
    // Use this for initialization
    void Start () {
        SteamVR_LaserPointer lazer = GetComponent<SteamVR_LaserPointer>();
        //lazer.PointerOut += LeaveObject;
        lazer.PointerIn += EnterObject;

        viveInput = GetComponent<ViveInput2>();
    }
    private void EnterObject(object sender, PointerEventArgs e)
    {
        if (selectedDocument == null)
            selectedDocument = e.target.gameObject.GetComponent<Document>();
    }
    // Update is called once per frame
    void Update () {
        if (selectedDocument != null && !selectedDocument.IsSelected && viveInput.IsTriggerPressed())
            RemoveDocument();
    }
    private void RemoveDocument()
    {
        Rigidbody body = selectedDocument.GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        selectedDocument.SelectedRemove();
        ThrowToGarbageZone();
    }
    private void ThrowToGarbageZone()
    {
        Rigidbody body = selectedDocument.GetComponent<Rigidbody>();
        body.AddForce(new Vector3(0,0,-70), ForceMode.Impulse);
        selectedDocument.UnSelectedRemove();
        selectedDocument = null;

    }
}
