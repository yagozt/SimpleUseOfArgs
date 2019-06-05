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
        [Fact]
        public void testInvalidArgumentFormat()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("f-", new string[] { }));
            Assert.Equal(ErrorCode.INVALID_ARGUMENT_FORMAT, e.GetErrorCode());
            Assert.Equal('f', e.getErrorArgumentId());
        }
        [Fact]
        public void testSimpleBooleanPresent()
        {
            var args = new Args("x", new string[] { "-x" });
            Assert.Equal(1, args.cardinality());
            Assert.True(args.getBoolean('x'));
        }
        [Fact]
        public void testSimpleStringPresent()
        {
            var args = new Args("x*", new string[] { "-x", "param" });
            Assert.Equal(1, args.cardinality());
            Assert.True(args.has('x'));
            Assert.Equal("param",args.getString('x'));
        }
        [Fact]
        public void testMissingStringArgument()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("x*", new string[] { "-x" }));
            Assert.Equal(ErrorCode.MISSING_STRING, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
        }
        [Fact]
        public void testSpacesInFormat()
        {
            var args = new Args("x, y", new string[] { "-xy" });
            Assert.Equal(2, args.cardinality());
            Assert.True(args.has('x'));
            Assert.True(args.has('y'));
        }
        [Fact]
        public void testSimpleIntPresent()
        {
            var args = new Args("x#", new string[] { "-x", "42" });
            Assert.Equal(1, args.cardinality());
            Assert.True(args.has('x'));
            Assert.Equal(42, args.getInt('x'));
        }
        [Fact]
        public void testInvalidInteger()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("x#", new string[] { "-x", "Forty two" }));
            Assert.Equal(ErrorCode.INVALID_INTEGER, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
            Assert.Equal("Forty two", e.getErrorParameter());
        }
        [Fact]
        public void testMissingInteger()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("x#", new string[] { "-x" }));
            Assert.Equal(ErrorCode.MISSING_INTEGER, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
        }
        [Fact]
        public void testSimpleDoublePresent()
        {
            var args = new Args("x##", new string[] { "-x", "42.3" });
            Assert.Equal(1, args.cardinality());
            Assert.True(args.has('x'));
            Assert.Equal(42.3, args.getDouble('x'),1);
        }
        [Fact]
        public void testInvalidDouble()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("x##", new string[] { "-x", "Forty two" }));
            Assert.Equal(ErrorCode.INVALID_DOUBLE, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
            Assert.Equal("Forty two", e.getErrorParameter());
        }
        [Fact]
        public void testMissingDouble()
        {
            var e = Assert.Throws<ArgsException>(() => new Args("x##", new string[] { "-x" }));
            Assert.Equal(ErrorCode.MISSING_DOUBLE, e.GetErrorCode());
            Assert.Equal('x', e.getErrorArgumentId());
        }
    }
}
