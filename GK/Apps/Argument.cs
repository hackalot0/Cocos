using System;
using System.Collections.Generic;
using System.Text;

namespace GK.Apps
{
    public class Argument
    {
        public ArgumentType Type => type;

        private ArgumentType type;

        public Argument(ArgumentType type)
        {
            this.type = type;
        }
    }
}