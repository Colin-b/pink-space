using UnityEngine;
using System.Collections;

public class SphereCollision : MonoBehaviour {

    void Start()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
        collision.rigidbody.velocity = Vector3.zero;
        collision.rigidbody.angularVelocity = Vector3.zero;
    }

}
