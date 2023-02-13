using System;
using System.Collections.Generic;
using UnityEngine;
using Battle;

public enum BattleState {
    IDLE,
    BEGIN,
    ACTIVE,
    PAUSE,
    END
}

public class BattleSystem : MonoBehaviour {
    public BattleState State;

    public 
    private static Dictionary<string, Unit> units;
    private static Dictionary<int, BattleAction> actions;

    public static void ActOnUnits(string sourceId, string targetId, int actionId) {
        Unit source = units[sourceId];
        Unit target = units[targetId];
        BattleAction action = actions[actionId];

        int damageAmount = Mathf.FloorToInt((source.Attack * action.DamageModifier) / (target.Defense + 100 / 100));
        Console.WriteLine(String.Format("{0} did {1} damage to {2}.", source.Name, damageAmount, target.Name));
    }

    // Takes in a unit, and adds it to the list
    public static void RegisterUnit(Unit unit) {
        units.Add(unit.Id, unit);
    }

    public static void RegisterBattleAction(BattleAction action) {
        actions.Add(action.Id, action);
    }

    public static void SaveBattleActions() {
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Save(Application.dataPath + "/Data/BattleActions");
    }

    public static void LoadBattleActions() {

    }
}
