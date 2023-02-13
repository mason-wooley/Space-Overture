using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FogOfWar : MonoBehaviour
{

    public GameObject FogOfWarPlane;
    public Transform Player; //todo: get the actual player here
    public LayerMask FogLayer;
    public float Radius = 5f;
    private float radiusSqr { get { return Radius * Radius; } }

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;
    private int maxDistance = 1000;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(transform.position, Player.position - transform.position);
        RaycastHit hit;
        if(Physics.Raycast(r, out hit, maxDistance, FogLayer)) {
            for (int i=0; i < vertices.Length; i++) {
                Vector3 v = FogOfWarPlane.transform.TransformPoint(vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < radiusSqr) {
                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqr);
                    colors[i] = Color.white;
                    colors[i].a = 0;
                }
            }
            UpdateColor();
        }
    }

    void Initialize() {
        mesh = FogOfWarPlane.GetComponent<MeshFilter>().sharedMesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        for (int i=0; i < colors.Length; i++) {
            colors[i] = Color.black;
        }
        UpdateColor();
        print("UwU");
    }

    void UpdateColor() {
        mesh.colors = colors;
    }
}
