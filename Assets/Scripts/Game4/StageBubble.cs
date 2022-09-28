﻿using DG.Tweening;
using UnityEngine;

namespace Game4
{
    public class StageBubble : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        public StageNode StageNode { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Respawn(StageNode node)
        {
            StageNode = node;
            _renderer.sprite = Manager.Instance.BubbSprites[(int)node.BubbleType];
        }

        public void PlayWipeAnim()
        {
            _renderer.DOFade(0, 0.6f).SetEase(Ease.InQuad);
            transform.DOMove(Vector3.down * 2, 0.6f).SetRelative(true).OnComplete(() => Destroy(gameObject));
        }
    }
}
