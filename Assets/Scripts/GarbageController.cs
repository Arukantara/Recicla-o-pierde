using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    GarbageClassifier classifier;
    [SerializeField] string validBin1;
    [SerializeField] string validBin2;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        GameObject classifierObject = GameObject.Find("GarbageClassifier");
        if(classifierObject) {
        classifier = GameObject.Find("GarbageClassifier").GetComponent<GarbageClassifier>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!classifier) {
            return;
        }
        
        if (collision.gameObject.name == validBin1) {
            classifier.CountSuccess(collision.gameObject);
        } else if (collision.gameObject.name == validBin2) {
            classifier.CountPartialSuccess(collision.gameObject);
        } else {
            classifier.CountFail(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
