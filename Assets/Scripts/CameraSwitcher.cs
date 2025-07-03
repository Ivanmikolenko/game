using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;

public class Menu : MonoBehaviour
{
    public Camera MenuCamera;
    public CinemachineCamera MainCamera;

    public UnityEvent<Camera> OnCameraSwitched;

    private bool isMenuCameraActive;

    private void Start()
    {
        SwitchCamera();
    }

    public void SwitchCamera()
    {
        isMenuCameraActive = !isMenuCameraActive;

        MenuCamera.enabled = isMenuCameraActive;
        MainCamera.enabled = !isMenuCameraActive;

        MenuCamera.GetComponent<AudioListener>().enabled = isMenuCameraActive;
        MainCamera.GetComponent<AudioListener>().enabled = !isMenuCameraActive;

        OnCameraSwitched.Invoke(isMenuCameraActive ? MenuCamera : MainCamera.GetComponent<Camera>());
    }
}