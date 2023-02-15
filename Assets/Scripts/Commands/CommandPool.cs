using System.Collections.Generic;
using UnityEngine;

/*
public class CommandPool<T> where T : ICommand {
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T commandPrefab;

    public CommandPool(T prefab, int poolSize) {
        commandPrefab = prefab;
        for (int i = 0; i < poolSize; i++) {
            T command = Object.Instantiate(prefab);
            command.gameObject.SetActive(false);
            pool.Enqueue(command);
        }
    }

    public T GetCommand() {
        T command = pool.Count > 0 ? pool.Dequeue() : Object.Instantiate(commandPrefab);
        return command;
    }

    public void ReturnCommand(T command) {
        command.gameObject.SetActive(false);
        pool.Enqueue(command);
    }
}
*/
