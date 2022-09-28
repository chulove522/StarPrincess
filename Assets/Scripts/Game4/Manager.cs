using GameFramework.GameStructure.Levels.ObjectModel;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game4
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance { get; private set; }

        [SerializeField]
        public Transform StageBubbleParent;
        [SerializeField]
        public Sprite[] BubbSprites;
        [SerializeField]
        public GameObject StageBubblePrefab;
        [SerializeField, LabelText("游戏配置")]
        private GameCfg _gameCfg;

        public int Level { get; private set; }
        public int FlyCount { get; private set; }
        public IList<IList<StageNode>> StageNodes { get; private set; }
        public IStageAnchor Anchors { get; private set; }
        public GameCfg GameCfg => _gameCfg;

        private Dictionary<StageNode, HashSet<StageNode>> _parentRecords;

        protected void Awake()
        {
            Anchors = new StageAnchor(StageType.EvenStage);
            Instance = this;
            StageNodes = new List<IList<StageNode>>(GameConstant.StageRowCount);
            for (var i = 0; i < GameConstant.StageRowCount; ++i)
                StageNodes.Add(new List<StageNode>(GameConstant.RowBubbMaxNum));
            _parentRecords = new Dictionary<StageNode, HashSet<StageNode>>();
        }

        void Start()
        {
            InitLevelData(0);
        }

        void Update()
        {
           
        }

        public void InitLevelData(int lvl)
        {
            Level = lvl;
            FlyCount = 0;
            foreach (Transform child in StageBubbleParent)
                Destroy(child.gameObject);
            var tunning = GameCfg.LevelTunnings[lvl];
            Anchors = new StageAnchor(tunning.StageType);
            InitLevelStage();
        }

        private void SpawnStageBubble(StageNode node)
        {
            if (node.BubbleType == BubbleType.Empty || node.BubbleType == BubbleType.Random) return;

            // 遍历周围的泡泡,重设parent
            foreach (var sideNode in node)
            {
                // 同色的合并
                if (sideNode?.BubbleType == node.BubbleType)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    if (sideNode.ParentNode == null && node.ParentNode == null)
                    {
                        node.ParentNode = node;
                        sideNode.ParentNode = node;
                        _parentRecords[node] = new HashSet<StageNode> { node, sideNode };
                    }
                    else if (sideNode.ParentNode != null && node.ParentNode == null)
                    {
                        node.ParentNode = sideNode.ParentNode;
                        _parentRecords[sideNode.ParentNode].Add(node);
                    }
                    else if (sideNode.ParentNode == null && node.ParentNode != null)
                    {
                        sideNode.ParentNode = node.ParentNode;
                        _parentRecords[node.ParentNode].Add(sideNode);
                    }
                    else if (sideNode.ParentNode != node.ParentNode)
                        CombineParentSet(sideNode.ParentNode, node.ParentNode);
                }
            }

            // 如果周围没同色的
            if (node.ParentNode == null)
            {
                node.ParentNode = node;
                _parentRecords[node] = new HashSet<StageNode> { node };
            }

            var stageBubble = Instantiate(StageBubblePrefab, node.AnchorPos, Quaternion.identity, StageBubbleParent).GetComponent<StageBubble>();
            stageBubble.Respawn(node);
        }

        private void CombineParentSet(StageNode parent1, StageNode parent2)
        {
            var parent = parent2.Row < parent1.Row ? parent2 : parent1;
            var child = parent2.Row < parent1.Row ? parent1 : parent2;
            foreach (var node in _parentRecords[child])
                node.ParentNode = parent;
            _parentRecords[parent].UnionWith(_parentRecords[child]);
            _parentRecords.Remove(child);
        }

        private void InitLevelStage()
        {
            // 数据
            foreach (var row in StageNodes)
                row.Clear();
            var lvlTunning = GameCfg.LevelTunnings[Level];
            var initBubbs = lvlTunning.InitBubles;
            for (var row = 0; row < StageNodes.Count; ++row)
            {
                var rowCount = Anchors.GetRowAnchorsCount(row);
                for (var col = 0; col < rowCount; ++col)
                {
                    var cfgClr = row < initBubbs.GetLength(0) ? initBubbs[row, col] : BubbleType.Empty;
                    if (cfgClr == BubbleType.Random)
                        cfgClr = BubbTypeUtil.GetRandomStageType();
                    var node = new StageNode(Anchors, StageNodes,row, col, cfgClr, Anchors[row, col], null );
                    StageNodes[row].Add(node);
                }
            }

            foreach (var rowNodes in StageNodes)
            {
                foreach (var node in rowNodes)
                    SpawnStageBubble(node);
            }

        }
    }

}