using UnityEngine;

public class ShapeSelectionScript : MonoBehaviour {

    public float DocumentDistanceToUserWhenDragged = 20;

    private Document selectedDocument;

    public bool triggerUp = false;
    public bool triggerDown = false;
    public bool triggerPressed = false;

    private SteamVR_Controller.Device controller;
    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update () {
        controller = SteamVR_Controller.Input((int)trackedObj.index);
        if (TriggerSelection())
            SelectPointedAtShape();
        else if (TriggerUnselection())
            UnselectDocument();
        else if (ShouldMove())
            Move();

        //if (controller != null)
        //{
        //    if (triggerDown)
        //    {

        //    }
        //    else if (triggerPressed)
        //    {

        //    }
        //    else if (triggerUp)
        //    {

        //    }
        //}
    }

    private bool ShouldMove()
    {
        return selectedDocument != null && controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
    }

    private bool TriggerUnselection()
    {
        return selectedDocument != null && controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
    }

    private bool TriggerSelection()
    {
        return selectedDocument == null && controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
    }

    private Vector3 GetPointer()
    {
        return Input.mousePosition;
    }

    private void SelectPointedAtShape()
    {
        Debug.Log("selected");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(GetPointer());
        if (Physics.Raycast(ray, out hit))
            if (hit.collider != null)
                SelectShape(hit.collider.gameObject);
    }

    private void SelectShape(GameObject shape)
    {
        selectedDocument = shape.GetComponent<Document>();
        if (selectedDocument != null)
            selectedDocument.Selected();
    }

    private void UnselectDocument()
    {
        selectedDocument.UnSelected();
        selectedDocument = null;
    }

    private void Move()
    {
        Debug.Log("Enter on Move");
        Vector3 fromDocToPointer = GetPointer() - selectedDocument.transform.position;
        fromDocToPointer.Normalize();
        selectedDocument.transform.Translate(fromDocToPointer);
    }

}
