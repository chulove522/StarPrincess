using System;
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

    public void StartGame01() {
        m = StartCoroutine(MoveTo(this.gameObject, this.transform.position, target , speed));
    }


    //這什麼爛東西
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
        StopCoroutine(m);
    }

    private IEnumerator MoveTo(GameObject obj, Vector3 currentPos, Vector3 targetPos, float speed) {


        var duration = 20 / speed;

        var timePassed = 0f;
        while (timePassed < duration) {
            // always a factor between 0 and 1
            var factor = timePassed / duration;

            obj.transform.position = Vector3.Lerp(currentPos, targetPos, factor);

            // increase timePassed with Mathf.Min to avoid overshooting
            timePassed += Math.Min(Time.deltaTime, duration - timePassed);

            // "Pause" the routine here, render this frame and continue
            // from here in the next frame
            yield return null;
        }

        // move done!
        yield return null;
    }
}
