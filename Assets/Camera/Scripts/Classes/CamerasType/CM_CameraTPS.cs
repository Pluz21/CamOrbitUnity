using UnityEngine;

public class CM_CameraTPS : CM_CameraBehaviour
{
	
	public override void CameraMoveTo()
	{
		if (!settings.IsValidCamera || !settings.CanMoveCamera) return;
		transform.position = GetPosition();

	}
	public override void CameraRotateTo()
	{
		if (!settings.IsValidCamera || !settings.CanRotateCamera) return;
		transform.rotation = GetRotation();
	}

	public override Vector3 GetPosition()
	{
		return Vector3.MoveTowards(settings.CurrentPosition,settings.Offset.GetOffset(settings.Target) , Time.deltaTime * settings.CameraMoveSpeed);
	}
	public override Quaternion GetRotation()
	{
		Vector3 _lookAtDirection = settings.TargetPosition - settings.CurrentPosition;
		if (_lookAtDirection == Vector3.zero) return Quaternion.identity;
		Quaternion _lookAt = Quaternion.LookRotation(_lookAtDirection + settings.LookAtOffset.GetLookAtOffset(settings.Target));
		return Quaternion.RotateTowards(settings.CurrentRotation, _lookAt  , Time.deltaTime * settings.CameraRotateSpeed);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		Gizmos.color = new Color(0, 1, 0, .5f);
		Vector3 _final = settings.Offset.GetOffset(settings.Target);
		Gizmos.DrawLine(settings.CurrentPosition, _final);
		Gizmos.DrawCube(_final, Vector3.one );
	}
}
