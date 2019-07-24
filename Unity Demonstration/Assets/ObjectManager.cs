using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    #region Singleton
    private static ObjectManager instance;
    public static ObjectManager Instance
    {
        get
        {
            return instance ?? FindObjectOfType<ObjectManager>();
        }
    }
    #endregion

    private void Awake()
    {
        instance = instance ?? this;
    }

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
    internal float maxScale = 5f;
    [SerializeField]
    internal float rotationTime = 30f;
    [SerializeField]
    internal float sineRange = 90f;
    [SerializeField]
    internal bool useSinWave = true;
    [SerializeField]
    internal bool rotateAroundX = false;
    [SerializeField]
    internal bool rotateAroundY = true;
    [SerializeField]
    internal bool rotateAroundZ = false;
    [SerializeField]
    internal bool rotate = true;

    private Quaternion originalRotation;
    void Update()
    {
        if (rotate)
        {
            if (rotationTime <= Mathf.Epsilon) rotationTime = 30f;

            float x = originalRotation.eulerAngles.x, 
                  y = originalRotation.eulerAngles.y, 
                  z = originalRotation.eulerAngles.z;

            if (rotateAroundX && useSinWave) x = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundX) x = (Time.timeSinceLevelLoad * 1000) / (360 / rotationTime);

            if (rotateAroundY && useSinWave) y = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundY) y = (Time.timeSinceLevelLoad * 1000) / (360 / rotationTime);

            if (rotateAroundZ && useSinWave) z = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundZ) z = (Time.timeSinceLevelLoad * 1000) / (360 / rotationTime);

            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(x, y, z), Time.deltaTime);
        }
    }

    private Vector3 normal;
    private bool lockedScale = false;

    public void UpdateScaling()
    {
        var scale = new Vector3(xAxis.value * maxScale + 1, yAxis.value * maxScale + 1, zAxis.value * maxScale + 1);

        if (lockToggle.isOn && !lockedScale)
        {
            lockedScale = true;
            normal = scale.normalized;
        }
        else if (!lockToggle.isOn) lockedScale = false;

        if (lockToggle.isOn)
        {
            scale = normal * scale.magnitude;
        }

        gameObject.transform.localScale = scale;
    }
}
