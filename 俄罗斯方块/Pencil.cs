using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    //一字型
    class Pencil:Cell
    {
        public Pencil()
        {
            style1[0, 0] = style1[0, 1] = style1[0, 2] = style1[0, 3] = 1;
            style2[0, 0] = style2[1, 0] = style2[2, 0] = style2[3, 0] = 1;
            base.Initial = style1;
        }
        private int[,] style1 = new int[4, 4];

        public int[,] Style1
        {
            get { return style1; }
            set { style1 = value; }
        }
        public int[,] style2 = new int[4, 4];
        public override void Change()
        {
            if (base.Initial == style1)
            {
                base.Initial = style2;
            }
            else if (base.Initial == style2)
            {
                base.Initial = style1;
            }

        }
    }
}
