using System;
using System.Collections.Generic;

namespace GK.Apps
{
    public class ArgumentManager
    {
        public Dictionary<string, List<Argument>> Arguments => arguments;
        public StringComparison NameComparison => nameComparison;

        private StringComparison nameComparison;
        private Dictionary<string, List<Argument>> arguments;
        private string stringArgs;

        public ArgumentManager(params string[] stringArgs) : this(StringComparison.Ordinal, stringArgs) { }
        public ArgumentManager(StringComparison nameComparison, params string[] stringArgs)
        {
            this.stringArgs = string.Join(' ', stringArgs);
            arguments = new Dictionary<string, List<Argument>>(StringComparer.FromComparison(this.nameComparison = nameComparison));
            ReadArguments();
        }

        protected virtual void ReadArguments()
        {
            for (int i = 0; i < stringArgs.Length; i++)
            {

            }
        }
    }
}