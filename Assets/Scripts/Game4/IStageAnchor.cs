using System.Collections.Generic;
using UnityEngine;

namespace Game4
{
    public interface IStageAnchor : IEnumerable<Vector2>
    {
        float TopEdge { get; }
        float BottomEdge { get; }
        float LeftEdge { get; }
        float RightEdge { get; }

        int GetRowAnchorsCount(int rowIndex);

        Vector2 this[int row, int col] { get; }

    }
}
