using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * This should control the logic for controlling any sort of "patrol zone." The
 * idea is for zones to be some abstract-ish game area that restricts movement 
 * in the game world. It can then be attached to anything that needs this code
 * without having to worry so much about implementation.
 * 
 * For now, it just draws stuff related the patroller.
 */
public class Patroller : MonoBehaviour {
    public float DetectionRadius;
    public LayerMask GroundMask;
    public Color ViewRadiusColor;

    [Range (0.01f, 0.5f)]
    public float DistanceOffGround;

    private NavMeshAgent navAgent;
    private GameObject quad;
    private Material quadMaterial;
    private Renderer quadRenderer;
    private MaterialPropertyBlock props;
    private GameObject parent;

    void Start() {
        parent = gameObject.transform.parent.gameObject;

        // Get the NavMeshAgent component
        bool navMeshAgentExists = parent.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent);
        Debug.Assert(navMeshAgentExists);
        navAgent = agent;

        // Create a quad underneath the patroller object to render to
        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
 
        // Get the Renderer of the quad
        quadRenderer = quad.GetComponent<Renderer>();

        quad.transform.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        quad.transform.parent = gameObject.transform;
        quad.transform.localScale = new Vector3(DetectionRadius, DetectionRadius);
        quad.name = "view_radius";

        quadMaterial = (Material)Resources.Load("Materials/Patroller_ViewRadius");

        // Now just attach the material to the quad
        quadRenderer.material = quadMaterial;

        props = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update() {
        // Set it to follow along the ground
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity, GroundMask)) {
            Vector3 groundPoint = hit.point;
            groundPoint.y += DistanceOffGround;
            quad.transform.position = groundPoint;
        }

        quadRenderer = quad.GetComponent<Renderer>();
        quadMaterial = quadRenderer.material;
        // quadMaterial.SetFloat("_Radius", DetectionRadius);
        props.SetColor("_Color", ViewRadiusColor);
        props.SetVector("_Position", navAgent.transform.position);
        quadRenderer.SetPropertyBlock(props);

        string[] names = quadMaterial.GetTexturePropertyNames();

        foreach (string name in names) {
            //Debug.Log(name);
            //Debug.Log(quadMaterial.shader);
        }
    }
}
