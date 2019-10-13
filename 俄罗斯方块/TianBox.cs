using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class TianBox:Cell
    {
        public TianBox()
        {
            style1[0, 0] = style1[0, 1] = style1[1, 0] = style1[1, 1] = 1;
            base.Initial = style1;

        }
        public int[,] style1 = new int[4, 4];
     
    }
}
