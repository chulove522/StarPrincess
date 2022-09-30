using Logic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class StartPanel : Panel
    {
        [SerializeField, LabelText("开始游戏Btn")]
        private Button _startBtn;

        private void Awake()
        {
            _startBtn.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            Manager.Instance.StartGame();
        }

    }
}