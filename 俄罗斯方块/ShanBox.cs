using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class ShanBox:Cell
    {
        public ShanBox()
        {
            style1[0, 1] = style1[1, 0] = style1[1, 1] = style1[1, 2] = 1;
            style2[1, 0] = style2[0, 1] = style2[1, 1] = style2[2, 1] = 1;
            style3[0, 0] = style3[0, 1] = style3[0, 2] = style3[1, 1] = 1;
            style4[0, 0] = style4[1, 0] = style4[2, 0] = style4[1, 1] = 1;
            base.Initial = style1;
        }
        public int[,] style1 = new int[4, 4];
        public int[,] style2 = new int[4, 4];
        public int[,] style3 = new int[4, 4];
        public int[,] style4 = new int[4, 4];
        public override void Change()
        {
            if (base.Initial == style1)
            {
                base.Initial = style2;
            }
            else if (base.Initial == style2)
            {
                base.Initial = style3;
            }
            else if (base.Initial == style3)
            {
                base.Initial = style4;
            }
            else if (base.Initial == style4)
            {
                base.Initial = style1;
            }
        }
    }
}
