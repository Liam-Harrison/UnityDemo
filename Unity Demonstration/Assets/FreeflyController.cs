using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeflyController : MonoBehaviour
{
    void Start()
    {
        targetDistance = distance;
    }

    [Header("Camera Settings")]
    [SerializeField]
    private Vector3 lookCentre;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float distanceMinimum;
    [SerializeField]
    private float distanceMaximum;
    [SerializeField]
    private float smoothTime = 0.5f;

    private Vector3 rotationEuler = new Vector3();
    private float distanceVelcoity;
    private float targetDistance;
    void Update()
    {

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        var scroll = -Input.GetAxis("Mouse ScrollWheel") * 2;

        if (Input.GetMouseButton(2))
        {
            horizontal = -mouseX * 80 * Time.deltaTime;
            vertical = mouseY * 80 * Time.deltaTime;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        targetDistance = Mathf.Clamp(targetDistance + scroll, distanceMinimum, distanceMaximum);
        distance = Mathf.SmoothDamp(distance, targetDistance, ref distanceVelcoity, smoothTime);

        rotationEuler.x = Mathf.Clamp(rotationEuler.x + vertical, -40, 60);
        rotationEuler.y += horizontal;

        var rotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, 0);
        transform.rotation = rotation;
        transform.position = rotation  * new Vector3(0, 0, -distance) + lookCentre;
    }
}
