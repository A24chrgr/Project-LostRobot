using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private List<CameraArea> cameraAreas;
    private CameraArea currentCameraArea;

    private void Awake()
    {
        cameraAreas = FindObjectsByType<CameraArea>(FindObjectsSortMode.None).ToList();

        foreach (CameraArea area in cameraAreas)
        {
            area.OnPlayerEntered += OnCameraAreaEntered;
            area.Deactivate();
        }
    }

    private void OnCameraAreaEntered(CameraArea area)
    {
        currentCameraArea?.Deactivate();
        currentCameraArea = area;
        currentCameraArea.Activate();
    }
}
