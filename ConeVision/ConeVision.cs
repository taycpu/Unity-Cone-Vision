using System;
using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(IConeVisionReceiver))]
public class ConeVision : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask, obstructionMask;
    [SerializeField] private IConeVisionReceiver coneVisionReceiver;

    public float radius;
    public float angle;
    [HideInInspector] public bool canSeePlayer;
    [HideInInspector] public GameObject target;

    private void Start()
    {
        coneVisionReceiver = GetComponent<IConeVisionReceiver>();
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
            target = _target.gameObject;
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            CheckAngle(transform.forward, directionToTarget);
        }
        else
        {
            if (canSeePlayer)
                coneVisionReceiver.TargetExitFromArea(target);
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
                    coneVisionReceiver.TargetExitFromArea(target);
                canSeePlayer = false;
            }
            else
            {
                if (!canSeePlayer)
                    coneVisionReceiver.TargetEnterInArea(target);
                canSeePlayer = true;
                coneVisionReceiver.TargetInArea(target);
            }
        }
        else
        {
            if (canSeePlayer)
            {
                coneVisionReceiver.TargetExitFromArea(target);
            }

            canSeePlayer = false;
        }
    }
}