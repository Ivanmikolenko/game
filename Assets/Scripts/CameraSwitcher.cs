using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject varManager;
    public Camera MenuCamera;
    public CinemachineCamera MainCamera;
    public Camera StoreCamera;

    public Canvas MenuCanvas;
    public Canvas StoreCanvas;

    public UnityEvent<Camera> OnCameraSwitched;

    private VarManager varManagerScript;
    private Camera currentActiveCamera;

    private void Start()
    {
        varManagerScript = varManager.GetComponent<VarManager>();
        SetActiveCamera(MenuCamera);
    }

    public void SwitchToMenuCamera() => SetActiveCamera(MenuCamera);
    public void SwitchToMainCamera() => SetActiveCamera(MainCamera.GetComponent<Camera>());
    public void SwitchToStoreCamera() => SetActiveCamera(StoreCamera);

    private void SetActiveCamera(Camera newActiveCamera)
    {
        SetCameraState(MenuCamera, false);
        SetCameraState(MainCamera.GetComponent<Camera>(), false);
        SetCameraState(StoreCamera, false);

        SetCanvasState(MenuCanvas, false);
        SetCanvasState(StoreCanvas, false);

        SetCameraState(newActiveCamera, true);

        if (newActiveCamera == MenuCamera)
        {
            SetCanvasState(MenuCanvas, true);
            varManagerScript.UpdateCoinsTotal();
        }
        else if (newActiveCamera == StoreCamera)
        {
            SetCanvasState(StoreCanvas, true);
            varManagerScript.UpdateCoinsTotalText();
        }
        else if (newActiveCamera == MainCamera) 
        {
            varManagerScript.UpdateCoinsCurText();
        }

            currentActiveCamera = newActiveCamera;
        OnCameraSwitched.Invoke(currentActiveCamera);
    }

    private void SetCameraState(Camera camera, bool state)
    {
        if (camera == null) return;

        camera.enabled = state;
        var audioListener = camera.GetComponent<AudioListener>();
        if (audioListener != null)
        {
            audioListener.enabled = state;
        }

        if (camera == MainCamera.GetComponent<Camera>())
        {
            MainCamera.enabled = state;
        }
    }

    private void SetCanvasState(Canvas canvas, bool state)
    {
        if (canvas != null)
        {
            canvas.enabled = state;
        }
    }
}