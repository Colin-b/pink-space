using UnityEngine;
using WorkshopVR;

public class LinkRemoverScript : MonoBehaviour {

    private ViveInput2 viveInput;
    private Link selectedLink;
    public AudioClip RemovalSound;

    void Start () {
        SteamVR_LaserPointer lazer = GetComponent<SteamVR_LaserPointer>();
        lazer.PointerIn += EnterObject;
        lazer.PointerOut += ExitObject;

        viveInput = GetComponent<ViveInput2>();
    }

    private void EnterObject(object sender, PointerEventArgs e)
    {
        if (selectedLink == null)
            selectedLink = e.target.gameObject.GetComponent<Link>();
    }

    private void ExitObject(object sender, PointerEventArgs e)
    {
        if (selectedLink != null && selectedLink == e.target.gameObject.GetComponent<Link>())
            selectedLink = null;
    }

    void Update () {
        if (selectedLink != null && viveInput.IsTriggerPressed())
            RemoveLink();
    }

    private void RemoveLink()
    {
        AudioSource.PlayClipAtPoint(RemovalSound, transform.position);
        selectedLink.Delete();
        selectedLink = null;
    }
}
