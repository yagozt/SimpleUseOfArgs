using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleUseOfArgs
{
    public class ArgsException : Exception
    {
        private char errorArgumentId = '\0';
        private string errorParameter = null;
        private ErrorCode errorCode = ErrorCode.OK;

        public ArgsException() { }
        public ArgsException(string message) : base(message) { }
        public ArgsException(ErrorCode errorCode) => this.errorCode = errorCode;
        public ArgsException(ErrorCode errorCode, string errorParameter)
        {
            this.errorCode = errorCode;
            this.errorParameter = errorParameter;
        }
        public ArgsException(ErrorCode errorCode, char errorArgumentId, string errorParameter)
        {
            this.errorCode = errorCode;
            this.errorParameter = errorParameter;
            this.errorArgumentId = errorArgumentId;
        }
        public char getErrorArgumentId()
        {
            return errorArgumentId;
        }
        public void setErrorArgumentId(char errorArgumentId)
        {
            this.errorArgumentId = errorArgumentId;
        }
        public string getErrorParameter()
        {
            return errorParameter;
        }
        public void setErrorParameter(string errorParameter)
        {
            this.errorParameter = errorParameter;
        }
        public ErrorCode GetErrorCode()
        {
            return errorCode;
        }
        public void setErrorCode(ErrorCode errorCode) => this.errorCode = errorCode;
        public string errorMessage()
        {
            switch (errorCode)
            {
                case ErrorCode.OK:
                    return "TILT:Should not get here.";
                case ErrorCode.INVALID_ARGUMENT_FORMAT:
                    return $"{errorParameter} is not a valid argument format.";
                case ErrorCode.UNEXPECTED_ARGUMENT:
                    return $"Argument {errorArgumentId} unexpected.";
                case ErrorCode.INVALID_ARGUMENT_NAME:
                    return $"{errorArgumentId} is not a validi argument name.";
                case ErrorCode.MISSING_STRING:
                    return $"Could not find string parameter for {errorArgumentId}.";
                case ErrorCode.MISSING_INTEGER:
                    return $"Could not find integer parameter for {errorArgumentId}.";
                case ErrorCode.INVALID_INTEGER:
                    return $"Argument {errorArgumentId} expects an integer but was {errorParameter}.";
                case ErrorCode.MISSING_DOUBLE:
                    return $"Could not find double parameter for {errorArgumentId}.";
                case ErrorCode.INVALID_DOUBLE:
                    return $"Argument {errorArgumentId} expects a double but was {errorParameter}.";
            }
            return string.Empty;
        }

    }
}
