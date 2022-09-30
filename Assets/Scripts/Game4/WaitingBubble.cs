using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game4;


namespace Game4
{
    public class WaitingBubble : MonoBehaviour
                               , IBeginDragHandler
                               , IDragHandler
                               , IEndDragHandler
    {
        [SerializeField, LabelText("發射線")]
        private LineRenderer LineRenderer;

        [SerializeField, LabelText("拖動泡泡")]
        private Image DragBubbleImage;

        private const float _punchDuration = 0.2f;
        private const float _lineMaterialScale = 11.8f;
        private Tweener _punchAnim;
        private Image _image;
        public BubbleType BubbType { get; private set; }
        public Vector2 FlyDirection { get; private set; }

        private void Awake()
        {
            _image = GetComponent<Image>();
            LineRenderer.material.SetTextureScale("_MainTex", new Vector2(_lineMaterialScale, 1f));
            _punchAnim = _image.rectTransform.DOPunchAnchorPos(Vector2.down * 20, _punchDuration, 5, 0).SetRelative(true).Pause().SetAutoKill(false);
        }

        public YieldInstruction Respawn(BubbleType type)
        {
            gameObject.SetActive(true);
            BubbType = type;
            _image.sprite = Manager.Instance.BubbSprites[(int)BubbType];
            DragBubbleImage.sprite = _image.sprite;
            _punchAnim.Restart();
            return _punchAnim.WaitForCompletion();
        }

        #region 拖动操作相关

        public void OnBeginDrag(PointerEventData eventData)
        {
            var startPos = Vector3.Scale(transform.position, new Vector3(0, 1, 0));
            _image.sprite = Manager.Instance.BubbSprites[(int)BubbType];
            LineRenderer.positionCount = 1;
            LineRenderer.SetPosition(0, startPos);
            DragBubbleImage.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var panelRect = (RectTransform)transform.parent;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, eventData.position, eventData.pressEventCamera, out var pos);
            var selfPos = ((RectTransform)transform).anchoredPosition;

            // if (pos.y > selfPos.y) return;

            DragBubbleImage.rectTransform.anchoredPosition = pos;
            var ray = Physics2D.Raycast(transform.position, (selfPos - pos));
            var anchors = Manager.Instance.Anchors;

            // if (ray.point.y < anchors.BottomEdge) return; // 不能出下边界

            FlyDirection = (selfPos - pos).normalized;
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(1, ray.point);

            if (ray.rigidbody == null) return;

            // 碰到墙壁
            if (ray.rigidbody.bodyType == RigidbodyType2D.Static && ray.point.y < anchors.TopEdge)
            {
                var offestY = GameConstant.BubbRadius * (FlyDirection.y / FlyDirection.x); // tanθ = y/x
                var rayPoint = new Vector2(ray.point.x - Mathf.Sign(offestY) * GameConstant.BubbRadius, ray.point.y - Mathf.Abs(offestY));
                LineRenderer.SetPosition(1, rayPoint);

                var rayDir = new Vector2(-FlyDirection.x, FlyDirection.y);
                ray = Physics2D.Raycast(rayPoint, rayDir);
                rayPoint = ray.point;
                if (ray.rigidbody.bodyType == RigidbodyType2D.Static)
                    rayPoint = new Vector2(ray.point.x - Mathf.Sign(rayDir.x) * GameConstant.BubbRadius, ray.point.y - Mathf.Abs(offestY));

                LineRenderer.positionCount = 3;
                LineRenderer.SetPosition(2, rayPoint);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           gameObject.SetActive(false);
           DragBubbleImage.gameObject.SetActive(false);
           LineRenderer.positionCount = 0;
           Manager.Instance.SpawnFlyBubble(BubbType, FlyDirection, transform.position);
        }

        #endregion
    }
}