using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleUseOfArgs
{
    public class StringArgumentMarshaler : IArgumentMarshaler
    {
        private string stringValue = string.Empty;
        public void set(IEnumerator<string> currentArgument)
        {
            try
            {
                if(!currentArgument.MoveNext())
                    throw new ArgsException(ErrorCode.MISSING_STRING);
                stringValue = currentArgument.Current;
            }
            catch (InvalidOperationException)
            {
                throw new ArgsException(ErrorCode.MISSING_STRING);
            }
        }

        public static string getValue(IArgumentMarshaler am)
        {
            if (am != null && am is StringArgumentMarshaler)
                return ((StringArgumentMarshaler)am).stringValue;
            else
                return "";
        }
    }
}
