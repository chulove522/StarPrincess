using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game4
{
    public class RandomBubble : MonoBehaviour
    {
        private Image _img;
        private DOTweenPath _movePathAnim;
        private Vector3 _defaultPos;

        public BubbleType BubbType { get; private set; }

        public void Awake()
        {
            _img = GetComponent<Image>();
            _movePathAnim = GetComponent<DOTweenPath>();
            _defaultPos = transform.localPosition;
            _movePathAnim.onComplete.AddListener(OnAnimEnd);

            void OnAnimEnd()
            {
                transform.localPosition = _defaultPos;
                gameObject.SetActive(false);
            }
        }

        public void Respawn()
        {
            gameObject.SetActive(true);
            var lvl = Manager.Instance.Level;
            var weight = Manager.Instance.GameCfg.LevelTunnings[lvl].WaitBubbWeights;
            BubbType = weight.SelectByWeight();
            _img.sprite = Manager.Instance.BubbSprites[(int)BubbType];
        }

        public YieldInstruction PlayMoveAnim()
        {
            _movePathAnim.tween.Restart();
            return _movePathAnim.tween.WaitForCompletion();
        }
    }
}
