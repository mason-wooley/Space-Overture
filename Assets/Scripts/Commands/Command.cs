using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand {
    public void Execute();
    public void Configure(Dictionary<string, object> config);
    public bool IsActive { get; set; }
}
