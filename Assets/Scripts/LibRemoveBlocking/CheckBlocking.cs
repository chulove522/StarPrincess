using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlocking : MonoBehaviour
{
    public Game3Flow flow;
    enum BLOCK_OP {
        ADD,
        REMOVE,
        UPDATE_STATE
    };
    private HashSet<GameObject> blockingSet;

    private void Start() {
        blockingSet = new HashSet<GameObject>();
        LogIsBlocking();
    }

    private void Update() {
    }

    public bool IsBlocking() {
        return blockingSet.Count != 0;
    }

    private void LogIsBlocking() {
        Debug.Log("Is blocking? " + IsBlocking());
    }

    private void UpdateBlocking(BLOCK_OP op, Collider2D other) {
        if (BLOCK_OP.ADD == op) {
            blockingSet.Add(other.gameObject);
        } else if (BLOCK_OP.REMOVE == op) {
            blockingSet.Remove(other.gameObject);
        } else if (BLOCK_OP.UPDATE_STATE == op) {
            // TODO: get blocking rate
            // Don't need calc by area, please calc by sampling with RayCasting
        }
        LogIsBlocking();
        if (!IsBlocking())
            flow.OnEventStartNotBlocking();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        UpdateBlocking(BLOCK_OP.ADD, other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        UpdateBlocking(BLOCK_OP.REMOVE, other);
    }

    private void OnTriggerStay2D(Collider2D other) {
        UpdateBlocking(BLOCK_OP.UPDATE_STATE, other);
    }
}
