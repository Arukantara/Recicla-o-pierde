using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageGrabber : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    bool grabbed = false;
    Rigidbody garbageRigidBody;
    Transform garbageTransform;
    void Start()
    {
    }

    void Update()
    {
        GrabGarbage();
        MoveGarbage();
    }

    void GrabGarbage()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Garbage") {
                grabbed = true;
                garbageRigidBody = hit.rigidbody;
                garbageTransform = hit.transform;
            }
        }

        if (Input.GetMouseButtonUp(0) && garbageRigidBody) {
            grabbed = false;
            garbageRigidBody.useGravity = true;
            garbageRigidBody = null;
            garbageTransform = null;
        }
    }

    void MoveGarbage()
    {
        if (grabbed) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Bin") {
                Transform objectHit = hit.transform;
                
                Vector3 newPosition = new Vector3(objectHit.position.x, 1.329f, objectHit.position.z);
                garbageTransform.position = Vector2.Lerp(transform.position, newPosition, 1f);
            }
        }
    }
}
