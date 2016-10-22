using UnityEngine;
using System.Collections;

public class SphereCollision : MonoBehaviour {

    void Start()
    {
    }

    void OnCollisionEnter(Collision collision)
    {

        collision.rigidbody.velocity = Vector3.zero;
        collision.rigidbody.angularVelocity = Vector3.zero; ;

        Invoke("ResetKimematic", 2);

    }
    void ResetKimematic()
    {
        //collision.rigidbody.isKinematic = true;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
