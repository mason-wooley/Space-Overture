using UnityEngine;

[ExecuteAlways]
public class ToggleFoggle : MonoBehaviour {
    public bool OverrideShow;
    public MeshCollider Collider;
    public MeshRenderer Renderer;

    void Update () {
        // If play mode is on, let it show
        if (Application.IsPlaying(gameObject) || OverrideShow) {
            Collider.enabled = true;
            Renderer.enabled = true;
        } else {
            // Disable the fog so it's easier to see in edit mode :-)
            Collider.enabled = false;
            Renderer.enabled = false;
        }
    }
}