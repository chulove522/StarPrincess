using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class MouseDrag : MonoBehaviour
{
    // https://stackoverflow.com/questions/64481530/unity-drag-with-right-mouse-button
    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mosePoint = Input.mousePosition;
        mosePoint.z = mZCoord;
        var result = Camera.main.ScreenToWorldPoint(mosePoint);
        return result;
    }

    /*
    如果移動位置測一下感覺很怪可以試試看下面這個座標系

        private Vector3 screenPoint;
        private Vector3 offset;

        void OnMouseDown() {

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        void OnMouseDrag() {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }*/
    
}
