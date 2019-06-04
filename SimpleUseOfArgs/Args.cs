using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleUseOfArgs
{
    public class Args
    {
        private Dictionary<Char, IArgumentMarshaler> marshalers;
        private ISet<Char> argsFound;
        private IEnumerator<string> currentArgument;

        public int cardinality() => argsFound.Count;

        public Args(string schema, string[] args)
        {
            marshalers = new Dictionary<Char, IArgumentMarshaler>();
            argsFound = new HashSet<Char>();

            parseSchema(schema);
            parseArgumentStrings(args.ToList());
        }

        private void parseSchema(string schema)
        {
            foreach (var element in schema.Split(','))
                if (element.Length > 0)
                    parseSchemaElement(element.Trim());
        }

        private void parseSchemaElement(string element)
        {
            char elementId = element.ElementAt(0);
            string elementTail = element.Substring(1);
            validadeSchemaElementId(elementId);
            if (elementTail.Length == 0)
                marshalers.Add(elementId, new BooleanArgumentMarshaler());
            else if (elementTail.Equals("*"))
                marshalers.Add(elementId, new StringArgumentMarshaler());
            else if (elementTail.Equals("#"))
                marshalers.Add(elementId, new IntegerArgumentMarshaler());
            else if (elementTail.Equals("##"))
                marshalers.Add(elementId, new DoubleArgumentMarshaler());
            else if (elementTail.Equals("[*]"))
                marshalers.Add(elementId, new StringArrayArgumentMarshaler());
            else
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_FORMAT, elementId, elementTail);
        }

        private void validadeSchemaElementId(char elementId)
        {
            if(!char.IsLetter(elementId))
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_NAME, elementId, null);
        }
        private void parseArgumentStrings(List<string> argsList)
        {
            for(currentArgument = argsList.GetEnumerator(); currentArgument.MoveNext();)
            {
                string argString = currentArgument.Current;
                if (argString.StartsWith("-"))
                {
                    parseArgumentCharacters(argString.Substring(1));
                } else
                {
                    currentArgument.Reset();
                    break;
                }
            }
        }

        private void parseArgumentCharacters(string argChars)
        {
            for (int i = 0; i < argChars.Length; i++)
                parseArgumentCharacter(argChars.ElementAt(i));
        }

        private void parseArgumentCharacter(char argChar)
        {
            IArgumentMarshaler m = marshalers.GetValueOrDefault(argChar);
            if(m == null)
                throw new ArgsException(ErrorCode.UNEXPECTED_ARGUMENT, argChar, null);
            else
            {
                argsFound.Add(argChar);
                try
                {
                    m.set(currentArgument);
                }
                catch (ArgsException e)
                {
                    e.setErrorArgumentId(argChar);
                    throw e;
                }
            }
        }
        public bool has(char arg) => argsFound.Contains(arg);
        public int nextArgument()
        {
            currentArgument.MoveNext();
            return currentArgument.GetHashCode();
        }
        public bool getBoolean(char arg) => BooleanArgumentMarshaler.getValue(marshalers[arg]);
        public string getString(char arg) => StringArgumentMarshaler.getValue(marshalers[arg]);
        public int getInt(char arg) => IntegerArgumentMarshaler.getValue(marshalers[arg]);
        public double getDouble(char arg) => DoubleArgumentMarshaler.getValue(marshalers[arg]);
        public string[] getStringArray(char arg) => StringArrayArgumentMarshaler.getValue(marshalers[arg]);
    }
}