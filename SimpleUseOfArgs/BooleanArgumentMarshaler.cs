using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleUseOfArgs
{
    public class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool booleanValue = false;
        public void set(IEnumerator<string> currentArgument)
        {
            booleanValue = true;
        }
        public static bool getValue(IArgumentMarshaler am)
        {
            if (am != null && am is BooleanArgumentMarshaler)
                return ((BooleanArgumentMarshaler)am).booleanValue;
            else
                return false;
        }
    }
}
