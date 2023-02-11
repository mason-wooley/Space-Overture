using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;

    [Range(0, 100)]
    public int CameraHeight;
    [Range(-50,50)]
    public int CameraDistance;

    // Update is called once per frame
    void Update() {
        transform.position = player.transform.position + new Vector3(0, CameraHeight, -CameraDistance);
    }
}