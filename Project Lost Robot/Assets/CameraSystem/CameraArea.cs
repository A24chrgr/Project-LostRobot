using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
    public event Action<CameraArea> OnPlayerEntered;
    
    [SerializeField] private CinemachineCamera _camera;
    
    public CinemachineCamera Camera
    {
        get { return _camera; }
    }

    public void Activate()
    {
        _camera.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _camera.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayersMidPoint"))
        {
            OnPlayerEntered?.Invoke(this);
            Debug.Log("Player Entered");
        }
    }
}