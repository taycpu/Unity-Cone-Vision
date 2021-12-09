using UnityEngine;

public interface IFOVReceiver
{
    public void TargetEnterInArea(Transform target);
    public void TargetInArea(Transform target);
    public void TargetExitFromArea(Transform target);
}