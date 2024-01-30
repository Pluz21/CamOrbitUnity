using System;
using UnityEngine;

public abstract class CM_CameraBehaviour : MonoBehaviour//, IManagedItem<string>
{
    public event Action OnUpdateCamera = null;
    
    [SerializeField,Header("Cam ID")] string id = "Camera";
    [SerializeField, Header("Settings")] 
    protected CM_CameraSettings settings = new CM_CameraSettings();
    public string ID => id;
    public bool IsValidItem => !string.IsNullOrEmpty(id);

    void Start() => InitItem();
    void LateUpdate() => OnUpdateCamera?.Invoke();
    //void OnDestroy() => DestroyItem();
    //
    public abstract void CameraMoveTo();
    public abstract void CameraRotateTo();
    public abstract Vector3 GetPosition();
    public abstract Quaternion GetRotation();
    //
    public void Enable()
    {
        settings.SetCanMoveCamera(true);
        settings.SetCanRotateCamera(true);
        settings.SetCameraRenderer(true);
    }
    public void Disable()
    {
        settings.SetCanMoveCamera(false);
        settings.SetCanRotateCamera(false);
        settings.SetCameraRenderer(false);
    }
    public void InitItem()
    {
        //CM_CameraManager.Instance?.Add(this);
        settings.Init(transform);
        OnUpdateCamera += () =>
        {
            CameraMoveTo();
            CameraRotateTo();
        };
    }
    //public void DestroyItem()
    //{
    //    CM_CameraManager.Instance?.Remove(this);
    //    OnUpdateCamera = null;
    //}
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(settings.TargetPosition, settings.CurrentPosition);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(settings.TargetPosition, .5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(settings.CurrentPosition, .5f);
    }
}
