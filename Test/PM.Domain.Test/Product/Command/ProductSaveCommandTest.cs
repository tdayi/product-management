using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using PM.Domain.Product.Command;

namespace PM.Domain.Test.Product.Command;

public class ProductSaveCommandTest
{
    [TestFixture]
    public class ValidateMethod : ProductSaveCommandTest
    {
        [Test]
        public void ValidInputs_ReturnsNoError()
        {
            // Arrange
            var command = new ProductSaveCommand {Title = "Test"};

            var results = new List<ValidationResult>();
            var context = new ValidationContext(command, null, null);

            // Act
            Validator.TryValidateObject(command, context, results, true);

            // Assert
            CollectionAssert.IsEmpty(results);
        }

        [Test]
        public void InvalidCategoryId_ReturnsOneError()
        {
            // Arrange
            var command = new ProductSaveCommand {CategoryId = Guid.Empty, Title = "Test"};

            var results = new List<ValidationResult>();
            var context = new ValidationContext(command, null, null);

            // Act
            Validator.TryValidateObject(command, context, results, true);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("CategoryId invalid", results[0].ErrorMessage);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void InvalidTitle_ReturnsOneError(string title)
        {
            // Arrange
            var command = new ProductSaveCommand {Title = title};

            var results = new List<ValidationResult>();
            var context = new ValidationContext(command, null, null);

            // Act
            Validator.TryValidateObject(command, context, results, true);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Title is required", results[0].ErrorMessage);
        }
    }
}