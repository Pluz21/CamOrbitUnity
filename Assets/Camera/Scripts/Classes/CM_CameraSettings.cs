using System;
using UnityEngine;

[Serializable]
public struct OffsetCamera
{
    [SerializeField, Header("X offset"), Range(-180, 180)]
    float xOffset;
    [SerializeField, Header("Y offset"), Range(-180, 180)]
    float yOffset;
    [SerializeField, Header("Z offset"), Range(-180, 180)]
    float zOffset;
    [SerializeField] 
    bool isLocalOffset;

    public Vector3 GetOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        return isLocalOffset ? GetLocalOffset(_target) : GetWorldOffset(_target);
    }
    public Vector3 GetLookAtOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        return isLocalOffset ? GetLocalLookAtOffset(_target) : GetWorldLookAtOffset(_target);
    }
    public bool IsLocalOffset => isLocalOffset;

    Vector3 GetLocalOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        Vector3 _x = _target.right * xOffset;
        Vector3 _y = _target.up * yOffset;
        Vector3 _z = _target.forward * zOffset;
        return _target.position + _x + _y + _z;
    }
    Vector3 GetLocalLookAtOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        Vector3 _x = _target.right * xOffset;
        Vector3 _y = _target.up * yOffset;
        Vector3 _z = _target.forward * zOffset;
        return  _x + _y + _z;
    }
    Vector3 GetWorldLookAtOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        Vector3 _worldOffset = new Vector3(xOffset, yOffset, zOffset);
        return _worldOffset;
    }
    Vector3 GetWorldOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        Vector3 _worldOffset = _target.position + new Vector3(xOffset, yOffset, zOffset);
        return _worldOffset;
    }
}
[Serializable]
public class CM_CameraSettings
{
    [SerializeField] Camera cameraRender = null;
    [SerializeField, Header("Offset camera")] 
    OffsetCamera offsetCamera = new OffsetCamera(), 
                 lookAtOffset = new OffsetCamera();
    [SerializeField] bool canMoveCamera = true, canRotateCamera = true;
    [SerializeField,Header("Camera speed"), Range(-1000, 1000)] float cameraMoveSpeed = 2, cameraRotateSpeed = 50;
    [SerializeField] Transform target = null;

    public Transform Target => target;
    public OffsetCamera Offset => offsetCamera;
    public OffsetCamera LookAtOffset => lookAtOffset;
    public Vector3 CurrentPosition
    {
        get
        {
            return IsValidCamera ? cameraRender.transform.position : Vector3.zero;
        }
    }
    public Vector3 TargetPosition
    {
        get
        {
            return IsValidCamera ? target.position : Vector3.zero;
        }
    }
    public Quaternion CurrentRotation
    {
        get
        {
            return IsValidCamera ? cameraRender.transform.rotation : Quaternion.identity;
        }
    }
    public bool IsValidCamera => cameraRender && target;
    public bool CanMoveCamera => canMoveCamera;
    public bool CanRotateCamera => canRotateCamera;
    public float CameraMoveSpeed => cameraMoveSpeed;
    public float CameraRotateSpeed => cameraRotateSpeed;
    public void SetCanMoveCamera(bool _status) => canMoveCamera = _status;
    public void SetCanRotateCamera(bool _status) => canRotateCamera = _status;
    public void SetCameraRenderer(bool _status)
    {
        if (!IsValidCamera) return;
        cameraRender.enabled = _status;
    }
    public void Init(Transform _origin)
    {
        if (!_origin) return;
        cameraRender = _origin.GetComponent<Camera>();
    }
}
