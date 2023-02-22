using UnityEngine;

public class SaveSystem : MonoBehaviour {
    public static SaveSystem Instance { get; private set; }

    public void Awake () {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
}
