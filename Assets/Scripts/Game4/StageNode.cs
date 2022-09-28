using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game4
{
    public class StageNode : IEnumerable<StageNode>
    {
        private readonly IList<IList<StageNode>> nodes_;
        private readonly IStageAnchor anchors_;
        public int Row { get; private set; }
        public int Column { get; private set; }
        public Vector2 AnchorPos;
        public BubbleType BubbleType { get; private set; }
        public StageNode ParentNode { get; set; }

        public StageNode(IStageAnchor anchors, IList<IList<StageNode>> nodes, int row, int column, BubbleType bubbleType, Vector2 anchorPos, StageNode parentNode)
        {
            anchors_ = anchors;
            nodes_ = nodes;
            Row = row;
            Column = column;
            BubbleType = bubbleType;
            AnchorPos = anchorPos;
            ParentNode = parentNode;
        }

        // 左上相邻
        public StageNode GetUpLeft()
        {
            if (Row == 0) return null;

            var rowCount = anchors_.GetRowAnchorsCount(Row);

            if (rowCount == GameConstant.RowBubbMaxNum)
                return Column == 0 ? null : nodes_[Row - 1][Column - 1];

            return nodes_[Row - 1][Column];
        }

        public StageNode GetUpRight()
        {
            if (Row == 0) return null;

            var rowCount = anchors_.GetRowAnchorsCount(Row);

            if (rowCount == GameConstant.RowBubbMaxNum)
                return Column == rowCount - 1 ? null : nodes_[Row - 1][Column];
            return nodes_[Row - 1][Column + 1];
        }

        public StageNode GetLeft()
        {
            return Column == 0 ? null : nodes_[Row][Column - 1];
        }

        public StageNode GetRight()
        {
            var rowCount = anchors_.GetRowAnchorsCount(Row);
            return Column == rowCount - 1 ? null : nodes_[Row][Column + 1];
        }

        public StageNode GetDownLeft()
        {
            if (Row == GameConstant.StageRowCount - 1) return null;

            var rowCount = anchors_.GetRowAnchorsCount(Row);

            if (rowCount == GameConstant.RowBubbMaxNum)
                return Column == 0 ? null : nodes_[Row + 1][Column - 1];
            return nodes_[Row + 1][Column];
        }

        public StageNode GetDownRight()
        {
            if (Row == GameConstant.StageRowCount - 1) return null;

            var rowCount = anchors_.GetRowAnchorsCount(Row);

            if (rowCount == GameConstant.RowBubbMaxNum)
                return Column == rowCount - 1 ? null : nodes_[Row + 1][Column];
            return nodes_[Row + 1][Column + 1];
        }

        public IEnumerator<StageNode> GetEnumerator()
        {
            yield return GetUpLeft();
            yield return GetUpRight();
            yield return GetLeft();
            yield return GetRight();
            yield return GetDownLeft();
            yield return GetDownRight();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
