using UnityEngine;

public interface IConeVisionReceiver
{
    public void TargetEnterInArea(GameObject target);
    public void TargetInArea(GameObject target);
    public void TargetExitFromArea(GameObject target);
}