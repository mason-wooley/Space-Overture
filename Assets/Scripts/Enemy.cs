using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utility;
using UnityEditor;

public class Enemy : MonoBehaviour {
    public float SpeedMultiplier;

    // Used for field of view
    public float ViewRadius;

    [Range(0, 360)]
    public float ViewAngle;
    public GameObject TargetObject;
    public LayerMask TargetMask;
    public LayerMask ObstructionMask;

    private CharacterController characterController;
    private NavMeshAgent agent;
    private Vector3 destination;
    private MoveState moveState;
    public bool canSeeTarget = false;

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();

        destination = transform.position;
        moveState = MoveState.STOPPED;

        StartCoroutine(FOVRoutine());
    }

    void MoveToPoint(Vector3 destination) {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update() {
        if (canSeeTarget) {
            MoveToPoint(TargetObject.transform.position);
            moveState = MoveState.WALKING;
            agent.isStopped = false;
        }
        switch (moveState) {
            case MoveState.STOPPED:
                Vector3 oppositeFacingDirection = transform.forward * -1;
                Vector3 patrolDestination = transform.position + (oppositeFacingDirection * 25);
                MoveToPoint(patrolDestination);
                moveState = MoveState.WALKING;
                agent.isStopped = false;
                break;
            case MoveState.RUNNING:
                SpeedMultiplier = 2;
                break;
            case MoveState.WALKING:
                Vector3 toDestination = agent.destination - transform.position;
                // Ignore the y component
                toDestination.y = 0;
                if (toDestination.magnitude < 0.1) {
                    agent.isStopped = true;
                    moveState = MoveState.STOPPED;
                }
                break;
            default:
                throw new Exception(String.Format("MoveState {0} not detected.", moveState));
        }
    }

    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, ViewRadius, TargetMask);

        if (rangeChecks.Length != 0) {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < ViewAngle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstructionMask)) {
                    canSeeTarget = true;
                } else {
                    canSeeTarget = false;
                }
            } else {
                canSeeTarget = false;
            }
        } else if (canSeeTarget) {
            canSeeTarget = false;
        }
    }
}
