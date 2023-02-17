using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Battle {
    public static class BattleSystem {
        private static Dictionary<string, Unit> units;
        private static Dictionary<int, BattleAction> actions;

        public static void ActOnUnits (string sourceId, string targetId, int actionId) {
            Unit source = units[sourceId];
            Unit target = units[targetId];
            BattleAction action = actions[actionId];

            int damageAmount = Mathf.FloorToInt((source.Attack * action.DamageModifier) / (target.Defense + 100 / 100));
            Console.WriteLine(String.Format("{0} did {1} damage to {2}.", source.Name, damageAmount, target.Name));
        }

        // Takes in a unit, and adds it to the list
        public static void RegisterUnit (Unit unit) {
            units.Add(unit.Id, unit);
        }

        public static void RegisterBattleAction (BattleAction action) {
            actions.Add(action.Id, action);
        }

        public static void SaveBattleActions () {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Save(Application.dataPath + "/Data/BattleActions");
        }

        public static void LoadBattleActions () {

        }
    }

    public class Unit {
        # region Unit Variables

        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int HitPoints { get; private set;  }
        
        // Not sure if this is actually needed here for physical-based action/targeting
        public Vector3 Position { get; set; }

        private List<BattleAction> actions;

        #endregion

        #region Public classes

        // Struct constructors have to contain data
        public Unit (GameObject parent, string name, int attack, int defense, int hitPoints) {
            // Generate a new GUID string for the Unit id
            Id = System.Guid.NewGuid().ToString();
            
            Name = name;
            Attack = attack;
            Defense = defense;
            HitPoints = hitPoints;
            Position = parent.transform.position;

            actions = new List<BattleAction>();
        }

        public void ActOnUnits (string target, int id) {
            BattleSystem.ActOnUnits(Id, target, id);
        }

        #endregion
    }

    // These need to somehow have a unique id, but I like having it countable vs. the guid approach
    // Iterate on instantiation?
    public class BattleAction {
        // Base properties
        public int Id;
        public string Name;
        public string Description;
        
        // Combat properties
        public float DamageModifier;
        public int MaxTargets;
        public float Range;
    }
}
