using UnityEngine;

[ExecuteAlways]
public class ToggleFoggle : MonoBehaviour {
    public bool OverrideShow;

    void Update () {
        // If play mode is on, let it show
        if (Application.IsPlaying(gameObject) || OverrideShow) {
            gameObject.GetComponent<MeshCollider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        } else {
            // Disable the fog so it's easier to see in edit mode :-)
            gameObject.GetComponent<MeshCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}