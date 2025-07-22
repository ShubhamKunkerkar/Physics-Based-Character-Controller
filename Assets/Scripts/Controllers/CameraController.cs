using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public bool UsingOrbitalCamera { get; private set; } = false;

    [SerializeField] HumanoidLandInput _input;

    [SerializeField] float _cameraZoomModifier = 32.0f;

    float _minCameraZoomDistance = 0.0f;
    float _minOrbitCameraZoomDistance = 1.0f;
    float _maxCameraZoomDistance = 12.0f;
    float _maxOrbitCameraZoomDistance = 36.0f;

    CinemachineCamera _activeCamera;
    int _activeCameraPriorityModifer = 31337;

    public Camera MainCamera;
    public CinemachineCamera cinemachine1stPerson;
    public CinemachineCamera cinemachine3rdPerson;
    CinemachinePositionComposer _CinemachinePositionComposer3rdPerson;
    public CinemachineCamera cinemachineOrbit;
    CinemachinePositionComposer _CinemachinePositionComposerOrbit;

    private void Awake()
    {
        _CinemachinePositionComposer3rdPerson = cinemachine3rdPerson.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachinePositionComposer;
        _CinemachinePositionComposerOrbit = cinemachineOrbit.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachinePositionComposer;
    }

    private void Start()
    {
        ChangeCamera(); // First time through, lets set the default camera.
    }

    private void Update()
    {
        if (!(_input.ZoomCameraInput == 0.0f)) { ZoomCamera();  }
        if (_input.ChangeCameraWasPressedThisFrame) { ChangeCamera(); }
    }

    private void ZoomCamera()
    { 
        if (_activeCamera == cinemachine3rdPerson)
        {
            _CinemachinePositionComposer3rdPerson.CameraDistance = Mathf.Clamp(_CinemachinePositionComposer3rdPerson.CameraDistance +
                                (_input.InvertScroll ? _input.ZoomCameraInput : -_input.ZoomCameraInput) / _cameraZoomModifier,
                                _minCameraZoomDistance,
                                _maxCameraZoomDistance);
        }
        else if (_activeCamera == cinemachineOrbit)
        {
            _CinemachinePositionComposerOrbit.CameraDistance = Mathf.Clamp(_CinemachinePositionComposerOrbit.CameraDistance +
                                (_input.InvertScroll ? _input.ZoomCameraInput : -_input.ZoomCameraInput) / _cameraZoomModifier,
                                _minOrbitCameraZoomDistance,
                                _maxOrbitCameraZoomDistance);
        }
    }

    private void ChangeCamera()
    {
        if (cinemachine3rdPerson == _activeCamera)
        {
            SetCameraPriorities(cinemachine3rdPerson, cinemachine1stPerson);
            UsingOrbitalCamera = false;
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Player (Self)"));
        }
        else if (cinemachine1stPerson == _activeCamera)
        {
            SetCameraPriorities(cinemachine1stPerson, cinemachineOrbit);
            UsingOrbitalCamera = true;
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Player (Self)");
        }
        else if (cinemachineOrbit == _activeCamera)
        {
            SetCameraPriorities(cinemachineOrbit, cinemachine3rdPerson);
            _activeCamera = cinemachine3rdPerson;
            UsingOrbitalCamera = false;
        }
        else // for first time through or if there's an error
        {
            cinemachine3rdPerson.Priority += _activeCameraPriorityModifer;
            _activeCamera = cinemachine3rdPerson;
        }
    }

    private void SetCameraPriorities(CinemachineCamera CurrentCameraMode, CinemachineCamera NewCameraMode)
    {
        CurrentCameraMode.Priority -= _activeCameraPriorityModifer;
        NewCameraMode.Priority += _activeCameraPriorityModifer;
        _activeCamera = NewCameraMode;
    }
}
