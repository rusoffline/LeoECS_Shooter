using Cinemachine;
using UnityEngine;

public struct VirtualCameraComponent
{
    public Camera mainCamera;
    public Transform cameraTransform => mainCamera.transform;
    public CinemachineVirtualCamera virtualCamera;
    public Transform virtualCameraTransform => virtualCamera.transform;
    public Transform target;
    public Vector3 rotation;
    public Vector3 targetPosition;
}
