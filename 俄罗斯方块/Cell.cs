using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
   public class Cell
    {
        public int[,] Initial = new int[4, 4];
        //极端
        public int ColIndex = 3;
        public int RowIndex = 0;

        public virtual void Change()
        {

        }
        public void Down()
        {

            RowIndex++;


        }
        public void Left()
        {

            ColIndex--;


        }
        public void Right()
        {

            ColIndex++;


        }
    }
}
