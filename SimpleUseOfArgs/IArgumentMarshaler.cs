using System.Collections.Generic;

namespace SimpleUseOfArgs
{
    public interface IArgumentMarshaler
    {
        void set(IEnumerator<string> currentArgument);
    }
}