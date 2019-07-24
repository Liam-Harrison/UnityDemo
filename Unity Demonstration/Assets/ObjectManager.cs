using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    void Start()
    {
        originalRotation = gameObject.transform.rotation;
    }

    [Header("Object Assignment")]
    [SerializeField]
    private GameObject gameObject;

    [Header("UI Assignments")]
    [SerializeField]
    private Slider xAxis;
    [SerializeField]
    private Slider yAxis;
    [SerializeField]
    private Slider zAxis;
    [SerializeField]
    private Toggle lockToggle;

    [Header("Display Settings")]
    [SerializeField]
    private float maxScale = 5f;
    [SerializeField]
    private float rotationTime = 30f;
    [SerializeField]
    private float sineRange = 90f;
    [SerializeField]
    private bool useSinWave = true;
    [SerializeField]
    private bool rotateAroundX = false;
    [SerializeField]
    private bool rotateAroundY = true;
    [SerializeField]
    private bool rotateAroundZ = false;

    private Quaternion originalRotation;
    void Update()
    {
        if (rotationTime > float.Epsilon)
        {
            float x = originalRotation.eulerAngles.x, 
                  y = originalRotation.eulerAngles.y, 
                  z = originalRotation.eulerAngles.z;

            if (rotateAroundX && useSinWave) x = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundX) x = (Time.timeSinceLevelLoad * 1000) / rotationTime;

            if (rotateAroundY && useSinWave) y = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundY) y = (Time.timeSinceLevelLoad * 1000) / rotationTime;

            if (rotateAroundZ && useSinWave) z = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundZ) z = (Time.timeSinceLevelLoad * 1000) / rotationTime;

            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(x, y, z), Time.deltaTime);
        }
    }

    private Vector3 originalScale = Vector3.zero;
    private Vector3 lastScale = Vector3.zero;
    private Vector3 normal;
    public void UpdateScaling()
    {
        var scale = new Vector3(xAxis.value * maxScale + 1, yAxis.value * maxScale + 1, zAxis.value * maxScale + 1);
        if (scale == lastScale) return;

        if (lockToggle.isOn && originalScale == Vector3.zero)
        {
            originalScale = scale;
            normal = scale.normalized;
        }
        else if (!lockToggle.isOn) originalScale = Vector3.zero;

        if (lockToggle.isOn)
        {
            var newScale = normal * scale.magnitude;
            scale = new Vector3(newScale.x, newScale.y, newScale.z);
        }

        gameObject.transform.localScale = scale;
        lastScale = scale;
    }
}
