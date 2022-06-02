using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocker : MonoBehaviour
{
	[SerializeField]
	private List<Target> _targets = new List<Target>();
	[SerializeField]
	private CinemachineTargetGroup _targetGroup;
	private Camera _mainCamera;

	[field: SerializeField]
	public Target CurrentTarget { get; private set; }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<Target>(out Target target)) return;

		_targets.Add(target);
		target.OnDestroyed += RemoveTarget;
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.TryGetComponent<Target>(out Target target)) return;

		RemoveTarget(target);
	}

	public bool SelectTarget()
	{
		if (_targets.Count == 0) return false;
		Target closestTarget = null;
		float distanceToClosest = Mathf.Infinity;

		foreach(Target target in _targets)
        {
			Vector2 viewPos = _mainCamera.WorldToViewportPoint(target.transform.position);
			if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) continue;

			Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
			if (toCenter.sqrMagnitude < distanceToClosest)
            {
				closestTarget = target;
				distanceToClosest = toCenter.sqrMagnitude;
            }
        }

		if (closestTarget == null) return false;

		CurrentTarget = closestTarget;
		_targetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
		return true;
	}

	public void Cancel()
	{
		if (CurrentTarget == null) return;

		_targetGroup.RemoveMember(CurrentTarget.transform);
		CurrentTarget = null;
	}

	private void RemoveTarget(Target target)
    {
		if(CurrentTarget == target)
        {
            _targetGroup.RemoveMember(CurrentTarget.transform);
			CurrentTarget=null;
        }

		_targets.Remove(target);
		target.OnDestroyed -= RemoveTarget;
	}
}
