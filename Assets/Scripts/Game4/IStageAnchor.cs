using System.Collections.Generic;
using UnityEngine;

namespace Game4
{
    public interface IStageAnchor : IEnumerable<Vector2>
    {
        int GetRowAnchorsCount(int rowIndex);

        Vector2 this[int row, int col] { get; }

    }
}
