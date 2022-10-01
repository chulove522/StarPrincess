using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //省時間 作法不好 懶得改 就這樣用

    public bool stop = false;
    public float speed = 0.3f;
    public Vector3 target;

    Coroutine m;
    // Update is called once per frame

    public void StartGame() {
        m = StartCoroutine(Move_Routine(this.transform, new List<Vector3> { this.transform.position, target }));
    }

    private IEnumerator Move_Routine(Transform transform, List<Vector3> vectors) {
        var duration = 20 / speed;
        for (int i = 0; i < vectors.Count - 1; i++) {
            float t = 0f;

            Vector3 a = vectors[i];
            Vector3 b = vectors[i + 1];

            while (t < 1f) {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(a, b, Mathf.SmoothStep(t*speed , 0 , 0));
                yield return null;
            }

        }
    }
    public void stopall() {
        stop = true;
    }
}
