using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocker : MonoBehaviour
{
	[SerializeField]
	private List<Target> targets = new List<Target>();
	[SerializeField]
	private CinemachineTargetGroup targetGroup;

	[field: SerializeField]
	public Target CurrentTarget { get; private set; }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Target>(out Target target)) 
			targets.Add(target);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<Target>(out Target target)) 
			targets.Remove(target);
	}

	public bool SelectTarget()
	{
		if (targets.Count == 0) return false;
		CurrentTarget = targets[0];
		targetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
		return true;
	}

	public void Cancel()
	{
		if (CurrentTarget == null) return;

		targetGroup.RemoveMember(CurrentTarget.transform);
		CurrentTarget = null;
	}
}
