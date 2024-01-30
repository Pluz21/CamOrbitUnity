using UnityEngine;

public class CM_CameraOrbit : CM_CameraBehaviour
{
	#region f/p
	[SerializeField] float angle = 0;
	[SerializeField] float radius = 0;
	[SerializeField] float height = 0;

	public float Angle => angle;
	public float Radius => radius;
	public float Height => height;
	#endregion
	
	#region methods
	#endregion

	public override void CameraMoveTo()
	{
		if (!settings.IsValidCamera || !settings.CanMoveCamera) return;
		transform.position = GetPosition();
	}
	public override Vector3 GetPosition()
	{
		angle = SetAngle(angle, settings.CameraMoveSpeed) ;
		float _x = Mathf.Cos(angle* Mathf.Deg2Rad) * Radius;
		float _y = height;
		float _z = Mathf.Sin(angle * Mathf.Deg2Rad) * Radius;

		return new Vector3(_x, _y, _z) + settings.TargetPosition;
	}

	float SetAngle(float _angle, float _speed)
    {
		_angle += Time.deltaTime * _speed;
		_angle %= 360;
		return _angle;
    }

	public override void CameraRotateTo()
	{
		if (!settings.IsValidCamera || !settings.CanRotateCamera) return;
		transform.rotation = GetRotation();
	}


	public override Quaternion GetRotation()
	{
		Vector3 _lookAtDirection = settings.TargetPosition - settings.CurrentPosition;
		if (_lookAtDirection == Vector3.zero) return Quaternion.identity;
		Quaternion _lookAt = Quaternion.LookRotation(_lookAtDirection + settings.LookAtOffset.GetLookAtOffset(settings.Target));
		return Quaternion.RotateTowards(settings.CurrentRotation, _lookAt, Time.deltaTime * settings.CameraRotateSpeed);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		Gizmos.color = new Color(0, 1, 0, .5f);
		Vector3 _final = settings.Offset.GetOffset(settings.Target);
		Gizmos.DrawLine(settings.CurrentPosition, _final);
		Gizmos.DrawCube(_final, Vector3.one);
	}
}
