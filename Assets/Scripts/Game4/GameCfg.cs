using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Game4
{
    [TypeInfoBox("關卡難度設定")]
    public class LevelTunning
    {
        [LabelText("類型")]
        public StageType StageType;

        [LabelText("初始填充泡泡"), TableMatrix(HorizontalTitle = "列数:固定10", VerticalTitle = "行:按需填", Transpose = true)]
        public BubbleType[,] InitBubles;

        [LabelText("待發射泡泡生成權重")]
        public Dictionary<BubbleType, int> WaitBubbWeights;

        private Dictionary<BubbleType, int> _stageBubbWeights;

        public Dictionary<BubbleType, int> StageBubbWeights

        {
            get
            {
                if (_stageBubbWeights == null)
                {
                    _stageBubbWeights = new Dictionary<BubbleType, int>(WaitBubbWeights);
                    _stageBubbWeights.Remove(BubbleType.Random);
                }

                return _stageBubbWeights;
            }
        }


    }

    public class GameCfg : SerializedScriptableObject
    {

        [TabGroup("關卡配置"), LabelText("關卡配置"), ListDrawerSettings(ShowIndexLabels = true)]
        public LevelTunning[] LevelTunnings;


        [TabGroup("通用配置"), LabelText("飛行氣泡速度")]
        public float FlyBubbleSpeed;

#if UNITY_EDITOR

        [Button("保存", ButtonSizes.Medium)]
        private void Save()
        {
            AssetDatabase.SaveAssets();
        }
#endif
    }
}
