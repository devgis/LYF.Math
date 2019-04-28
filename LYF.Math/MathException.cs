using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYF.Math
{
    public class MathException:Exception
    {
        public MathException(string ErrorMessage)
        {
            base.Message.Insert(0,ErrorMessage);
        }
    }
}
