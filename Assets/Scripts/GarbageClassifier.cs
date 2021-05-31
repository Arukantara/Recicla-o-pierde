using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageClassifier : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cursorImage;
    [SerializeField] Vector3 targetPosition = new Vector3(2.405752f, 2.203f, -2.352f);
    [SerializeField] Vector3 targetAngle = new Vector3(30.359f, 0, 0);
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] Sprite openedHandSprite;
    [SerializeField] Sprite closedHandSprite;
    private Vector3 currentAngle;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = mainCamera.transform.localEulerAngles;
        currentPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateCamera();
        UpdateCursorPosition();
        GrabGarbage();
    }

    void AnimateCamera()
    {
        currentAngle = LerpVector(currentAngle, targetAngle, rotationSpeed);
        mainCamera.transform.localEulerAngles = currentAngle;
        currentPosition = LerpVector(currentPosition, targetPosition, movementSpeed);
        mainCamera.transform.position = currentPosition;
    }

    Vector3 LerpVector(Vector3 originalVector, Vector3 targetVector, float speed)
    {
        originalVector = new Vector3(
            Mathf.LerpAngle(originalVector.x, targetVector.x, Time.deltaTime * speed),
            Mathf.LerpAngle(originalVector.y, targetVector.y, Time.deltaTime * speed),
            Mathf.LerpAngle(originalVector.z, targetVector.z, Time.deltaTime * speed)
        );

        return originalVector;
    }

    void UpdateCursorPosition()
    {
        cursorImage.transform.position = Input.mousePosition;
        if (Input.GetMouseButton(0)) {
            cursorImage.GetComponent<Image>().sprite = closedHandSprite;
        } else {
            cursorImage.GetComponent<Image>().sprite = openedHandSprite;
        }
    }

    void GrabGarbage()
    {
        
    }
}
