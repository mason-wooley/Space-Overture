using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCommandPool : MonoBehaviour {
    private List<PunchCommand> commandObjects = new List<PunchCommand>();

    public PunchCommand GetCommandObject () {
        PunchCommand commandObject = null;

        for (int i = 0; i < commandObjects.Count; i++) {
            if (!commandObjects[i].IsActive) {
                commandObject = commandObjects[i];
                break;
            }
        }

        if (commandObject == null) {
            commandObject = new PunchCommand(Object.FindObjectOfType<Enemy>(), 0);
            commandObjects.Add(commandObject);
        }

        commandObject.IsActive = true;
        return commandObject;
    }

    public void ReturnCommandObject (PunchCommand commandObject) {
        commandObject.IsActive = false;
    }
}
