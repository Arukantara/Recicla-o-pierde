using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    [SerializeField] Vector3 targetAngle = new Vector3(-90f, 0f, 0f);
    [SerializeField] float rotationSpeed = 0.5f;
    private Vector3 currentAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAngle = transform.Find("TrashbinLid").localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime * rotationSpeed),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime * rotationSpeed)
        );
 
         transform.Find("TrashbinLid").localEulerAngles = currentAngle;
    }
}
