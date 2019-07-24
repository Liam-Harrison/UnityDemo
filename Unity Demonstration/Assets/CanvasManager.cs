using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasManager : MonoBehaviour
{

    #region Singleton
    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get
        {
            return instance ?? FindObjectOfType<CanvasManager>();
        }
    }
    #endregion

    private void Awake()
    {
        instance = instance ?? this;
    }

    private Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        
    }

    public static RenderMode ScreenMode
    {
        get
        {
            return Instance.canvas.renderMode;
        }
        set
        {
            Instance.canvas.renderMode = value;
        }
    }
}
