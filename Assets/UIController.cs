using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button attackButton01;
    public Button attackButton02;
    public Button attackButton03;

    void Start() {
        var root = GetComponent<UIDocument>().rootVisualElement;

        attackButton01 = root.Q<Button>("AttackButton01");
        attackButton02 = root.Q<Button>("AttackButton02");
        attackButton03 = root.Q<Button>("AttackButton03");

        // Attach the events
        attackButton01.clicked += attack1Pressed;
        attackButton02.clicked += attack2Pressed;
        attackButton03.clicked += attack3Pressed;
    }

    void attack1Pressed () {
        var punchCommand = Object.FindObjectOfType<PunchCommandPool>().GetCommandObject();
        var config = new Dictionary<string, object>();
        config.Add("enemy", Object.FindObjectOfType<Enemy>());
        config.Add("damage", 10);
        punchCommand.Configure(config);
        punchCommand.Execute();
    }

    void attack2Pressed () {
        Debug.Log("I kick!");
    }
    void attack3Pressed () {
        Debug.Log("I have a fucking gun!");
    }
}
