using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    public class GameConstant
    {
        public const int StageRowCount = 14;
        public const int RowBubbMaxNum = 10;
        public const int RowBubbMinNum = RowBubbMaxNum - 1;
        public const float BubbRadius = 0.5f;
        public const int BubbWipeThreshold = 3;
        public const int MoveDownRowNum = 2;
        public static readonly float RowHeight;
    }
}
