using DnDH.Repo;
using System;

namespace DnDH.Test
{
    [TestClass]
    public class DataValidationTests
    {
        [TestMethod]
        public void Validated_ValidString_ReturnsTrimmedString()
        {
            // Act & Assert
            Assert.AreEqual("valid string", "  valid string  ".Validated());
            Assert.AreEqual("test", "test".Validated());
        }

        [TestMethod]
        public void Validated_NullOrWhiteSpace_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => ((string?)null).Validated());
            Assert.Throws<ArgumentException>(() => "".Validated());
            Assert.Throws<ArgumentException>(() => "   ".Validated());
        }
    }
}