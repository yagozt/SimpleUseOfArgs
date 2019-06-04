using SimpleUseOfArgs;
using System;
using Xunit;

namespace SimplesUseOfArgs.Test
{
    public class ArgsTest
    {
        [Fact]
        public void testCreateWithNoSchemaOrArguments()
        {
            Args args = new Args("", new string[0]);
            Assert.Equal(0, args.cardinality());
        }
        [Fact]
        public void testWithNoSchemaButWithOneArgument()
        {
                var e = Assert.Throws<ArgsException>(() => new Args("", new string[] { "-x" }));
            Assert.Equal(ErrorCode.UNEXPECTED_ARGUMENT, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
        }

        [Fact]
        public void testWithNoSchemaButWithMultipleArguments()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("", new string[] { "-x", "-y" }));
            Assert.Equal(ErrorCode.UNEXPECTED_ARGUMENT, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
        }

        [Fact]
        public void testNonLetterSchema()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("*", new string[] { }));
            Assert.Equal(ErrorCode.INVALID_ARGUMENT_NAME, e.GetErrorCode());
            Assert.Equal('*', e.getErrorArgumentId());
        }
    }
}
