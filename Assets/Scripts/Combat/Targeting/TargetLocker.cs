using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocker : MonoBehaviour
{
    private List<Target> targets = new List<Target>();

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
}
