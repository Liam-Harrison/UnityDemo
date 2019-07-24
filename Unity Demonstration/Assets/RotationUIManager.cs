using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationUIManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [Header("UI Assignments")]
    [SerializeField]
    private GameObject toggle;
    [SerializeField]
    private GameObject dropdown;
    [SerializeField]
    private GameObject sinUI;
    [SerializeField]
    private GameObject sinSlider;
    [SerializeField]
    private GameObject endlessUI;
    [SerializeField]
    private GameObject endlessSlider;
    [SerializeField]
    private GameObject xAxis;
    [SerializeField]
    private GameObject yAxis;
    [SerializeField]
    private GameObject zAxis;

    public void EnabledToggleChanged()
    {
        if (!toggle.GetComponent<Toggle>().isOn)
        {
            dropdown.SetActive(false);
            sinUI.SetActive(false);
            endlessUI.SetActive(false);
            xAxis.SetActive(false);
            yAxis.SetActive(false);
            zAxis.SetActive(false);
            ObjectManager.Instance.rotate = false;
        }
        else
        {
            dropdown.SetActive(true);
            if (dropdown.GetComponent<Dropdown>().value == 0) SetSinMode();
            else SetEndlessMode();
            xAxis.SetActive(true);
            yAxis.SetActive(true);
            zAxis.SetActive(true);
            ObjectManager.Instance.rotate = true;
        }
    }

    public void AxisToggleChanged()
    {
        ObjectManager.Instance.rotateAroundX = xAxis.GetComponent<Toggle>().isOn;
        ObjectManager.Instance.rotateAroundY = yAxis.GetComponent<Toggle>().isOn;
        ObjectManager.Instance.rotateAroundZ = zAxis.GetComponent<Toggle>().isOn;
    }

    private void SetSinMode()
    {
        sinUI.SetActive(true);
        endlessUI.SetActive(false);
        ObjectManager.Instance.useSinWave = true;
    }

    public void SinSliderChanged()
    {
        ObjectManager.Instance.sineRange = sinSlider.GetComponent<Slider>().value;
    }

    private void SetEndlessMode()
    {
        sinUI.SetActive(false);
        endlessUI.SetActive(true);
        ObjectManager.Instance.useSinWave = false;
    }

    public void EndlessSliderChanged()
    {
        ObjectManager.Instance.rotationTime = endlessSlider.GetComponent<Slider>().value;
    }

    public void SwitchMode()
    {
        if (dropdown.GetComponent<Dropdown>().value == 0) SetSinMode();
        else SetEndlessMode();
    }
}
