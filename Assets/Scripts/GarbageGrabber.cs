using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageGrabber : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    bool grabbed = false;
    Rigidbody rigidBody;
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
                Transform objectHit = hit.transform;
                rigidBody = hit.rigidbody;
                
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, objectHit.position.z);
                objectHit.position = Vector2.Lerp(objectHit.position, newPosition, 1f);
                Debug.Log(objectHit.position);
            }
        }

        if (Input.GetMouseButtonUp(0) && rigidBody) {
            grabbed = false;
            rigidBody.useGravity = true;
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
                transform.position = Vector2.Lerp(transform.position, newPosition, 1f);
            }
        }
    }
}
