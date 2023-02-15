using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCommand : MonoBehaviour, ICommand {
    public bool IsActive { get; set; }

    private Enemy _enemy;
    private int _damage;

    public PunchCommand (Enemy enemy, int damage) {
        _enemy = enemy;
        _damage = damage;
    }

    public void Execute () {
        _enemy.TakeDamage(_damage);
    }

    public void Configure (Dictionary<string, object> configValues) {
        // In-line define the output object
        if (configValues.TryGetValue("damage",  out object value)) {
            int damage = (int)value;
            _damage = damage;
        }

        if (configValues.TryGetValue("enemy", out value)) {
            Enemy enemy = (Enemy)value;
            _enemy = enemy;
        }
    }
}
