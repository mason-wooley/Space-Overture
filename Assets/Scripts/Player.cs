using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float SpeedMultiplier;
    public MeshCollider ClickCollider;
    public Camera Camera;

    private CharacterController characterController;
    private NavMeshAgent agent;
    private Vector3 destination;
    private Ray ray;
    private LineDrawer line;

    // Set this to false to enable WASD controls
    static private Boolean EnableMouseMovement = true;

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

public struct LineDrawer {
    private LineRenderer lineRenderer;
    private float lineSize;

    public LineDrawer(float lineSize = 0.2f) {
        GameObject lineObj = new GameObject("LineObj");
        lineRenderer = lineObj.AddComponent<LineRenderer>();
        //Particles/Additive
        lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));

        this.lineSize = lineSize;
    }

    private void init(float lineSize = 0.2f) {
        if (lineRenderer == null) {
            GameObject lineObj = new GameObject("LineObj");
            lineRenderer = lineObj.AddComponent<LineRenderer>();
            //Particles/Additive
            lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));

            this.lineSize = lineSize;
        }
    }

    //Draws lines through the provided vertices
    public void DrawLineInGameView(Vector3 start, Vector3 end, Color color) {
        if (lineRenderer == null) {
            init(0.2f);
        }

        //Set color
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        //Set width
        lineRenderer.startWidth = lineSize;
        lineRenderer.endWidth = lineSize;

        //Set line count which is 2
        lineRenderer.positionCount = 2;

        //Set the postion of both two lines
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void Destroy() {
        if (lineRenderer != null) {
            UnityEngine.Object.Destroy(lineRenderer.gameObject);
        }
    }
}
