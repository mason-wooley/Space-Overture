using System;
using UnityEngine;
using UnityEngine.AI;
using Utility;

public class Player : MonoBehaviour
{
    public float SpeedMultiplier;
    public MeshCollider ClickCollider;
    public Camera Camera;
    public Boolean EnableMouseMovement;

    private CharacterController characterController;
    private NavMeshAgent agent;
    private Vector3 destination;
    private Ray ray;
    private LineDrawer line;

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();

        destination = new Vector3(0, 0, 0);
    }

    void MoveToPoint(Vector3 destination) {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update() {
        if (EnableMouseMovement) {
            MouseMove();
        } else {
            KeyboardMove();
        }


        line.DrawLineInGameView(transform.position, agent.destination, Color.red);
        // agent.Move(agent.desiredVelocity);
    }

    void MouseMove() {
        if (Input.GetMouseButton(0)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (ClickCollider.Raycast(ray, out hit, 1000)) {
                MoveToPoint(hit.point);
            }
        }

        Vector3 towardDestination = (destination - transform.position) * SpeedMultiplier;
        towardDestination.y = 0;
    }

    void KeyboardMove() {
        Vector3 desiredDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float movementStep = agent.speed * Time.deltaTime;
        agent.SetDestination(agent.destination + desiredDirection * movementStep);
    }
}
