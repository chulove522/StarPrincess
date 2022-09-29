using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Game4
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField, LabelText("随机泡泡")]
        private RandomBubble _randomBubble;

        [SerializeField, LabelText("待发射的泡泡")]
        private WaitingBubble _waitingBubble;

        private void Awake()
        {
        }

        private void Start()
        {
            this.SpawnRandomBubble();
            this.SpawnWaitBubble();
        }


        public void Reset()
        {

        }


        private void SpawnRandomBubble()
        {
            _randomBubble.Respawn();
        }

        public void SpawnWaitBubble()
        {
            StartCoroutine(WaitAndSpawnRandomBubb());

            IEnumerator WaitAndSpawnRandomBubb()
            {
                yield return _randomBubble.PlayMoveAnim();
                yield return _waitingBubble.Respawn(_randomBubble.BubbType);

                SpawnRandomBubble();
            }
        }
    }
}
