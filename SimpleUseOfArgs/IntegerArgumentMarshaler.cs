using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleUseOfArgs
{
    public class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        private int intValue = 0;
        public void set(IEnumerator<string> currentArgument)
        {
            string parameter = null;
            try
            {
                currentArgument.MoveNext();
                parameter = currentArgument.Current;
                intValue = int.Parse(parameter);
            }
            catch (ArgumentNullException)
            {
                throw new ArgsException(ErrorCode.MISSING_INTEGER);
            }
            catch (FormatException)
            {
                throw new ArgsException(ErrorCode.INVALID_INTEGER, parameter);
            }
        }
        public static int getValue(IArgumentMarshaler am)
        {
            if (am != null && am is IntegerArgumentMarshaler)
                return ((IntegerArgumentMarshaler)am).intValue;
            else
                return 0;
        }
    }
}
