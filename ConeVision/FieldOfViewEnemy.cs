using System;
using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(IFOVReceiver))]
public class FieldOfViewEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask, obstructionMask;
    [SerializeField] private IFOVReceiver fovReceiver;

    public float radius;
    public float angle;
    [HideInInspector] public bool canSeePlayer;
    [HideInInspector] public Transform target;

    private void Start()
    {
        fovReceiver = GetComponent<IFOVReceiver>();
    }


    private void Update()
    {
        FieldOfViewCheck();
    }

    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform _target = rangeChecks[0].transform;
            target = _target;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            CheckAngle(transform.forward, directionToTarget);
        }
        else
        {
            if (canSeePlayer)
                fovReceiver.TargetExitFromArea(target);
            canSeePlayer = false;
        }
    }

    void CheckAngle(Vector3 from, Vector3 to)
    {
        if (Vector3.Angle(from, to) < angle / 2f)
        {
            if (Physics.Raycast(transform.position, to, Mathf.Infinity, obstructionMask))
            {
                if (canSeePlayer)
                    fovReceiver.TargetExitFromArea(target);
                canSeePlayer = false;
            }
            else
            {
                if (!canSeePlayer)
                    fovReceiver.TargetEnterInArea(target);
                canSeePlayer = true;
                fovReceiver.TargetInArea(target);
            }
        }
        else
        {
            if (canSeePlayer)
            {
                fovReceiver.TargetExitFromArea(target);
                Debug.Log("Player is not in the vision");
            }

            canSeePlayer = false;
        }
    }
}