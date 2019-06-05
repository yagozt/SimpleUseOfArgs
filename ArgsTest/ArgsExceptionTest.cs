using SimpleUseOfArgs;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimplesUseOfArgs.Test
{
    public class ArgsExceptionTest
    {
        [Fact]
        public void testUnexpectedMessage()
        {
            ArgsException e = new ArgsException(ErrorCode.UNEXPECTED_ARGUMENT, 'x', null);
            Assert.Equal("Argument -x unexpected.", e.errorMessage());
        }
        [Fact]
        public void testMissingStringMessage()
        {
            ArgsException e = new ArgsException(ErrorCode.MISSING_STRING, 'x', null);
            Assert.Equal("Could not find string parameter for -x.", e.errorMessage());
        }
        [Fact]
        public void testInvalidIntegerMessage()
        {
            ArgsException e = new ArgsException(ErrorCode.INVALID_INTEGER, 'x', "Forty two");
            Assert.Equal("Argument -x expects an integer but was Forty two.", e.errorMessage());
        }
        [Fact]
        public void testMissingIntegerMessage()
        {
            ArgsException e = new ArgsException(ErrorCode.MISSING_INTEGER, 'x', null);
            Assert.Equal("Could not find integer parameter for -x.", e.errorMessage());
        }
        [Fact]
        public void testInvalidDoubleMessage()
        {
            ArgsException e = new ArgsException(ErrorCode.INVALID_DOUBLE, 'x', "Forty two");
            Assert.Equal("Argument -x expects a double but was Forty two.", e.errorMessage());
        }
        [Fact]
        public void testMissingDoubleMessage()
        {
            ArgsException e = new ArgsException(ErrorCode.MISSING_DOUBLE, 'x', null);
            Assert.Equal("Could not find double parameter for -x.", e.errorMessage());
        }
    }
}
