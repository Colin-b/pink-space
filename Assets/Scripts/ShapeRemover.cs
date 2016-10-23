using UnityEngine;
using WorkshopVR;

public class ShapeRemover : MonoBehaviour {

    private ViveInput2 viveInput;
    private Document selectedDocument;
    public AudioClip RemovalSound;

    void Start () {
        SteamVR_LaserPointer lazer = GetComponent<SteamVR_LaserPointer>();
        lazer.PointerIn += EnterObject;
        lazer.PointerOut += ExitObject;

        viveInput = GetComponent<ViveInput2>();
    }

    private void EnterObject(object sender, PointerEventArgs e)
    {
        if (selectedDocument == null)
            selectedDocument = e.target.gameObject.GetComponent<Document>();
    }

    private void ExitObject(object sender, PointerEventArgs e)
    {
        if (selectedDocument != null && selectedDocument == e.target.gameObject.GetComponent<Document>())
            selectedDocument = null;
    }

    void Update () {
        if (selectedDocument != null && !selectedDocument.IsSelected && viveInput.IsTriggerPressed())
            RemoveDocument();
    }

    private void RemoveDocument()
    {
        AudioSource.PlayClipAtPoint(RemovalSound, transform.position);
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
