using System;
using System.IO;
using MassTransit;
using Moq;
using NUnit.Framework;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Repository;
using PM.Domain.Product.Command;
using PM.Domain.Product.CommandHandler;
using PM.Domain.Product.Repository;

namespace PM.Domain.Test.Product.CommandHandler;

public class ProductSaveCommandHandlerTest
{
    private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private ProductSaveCommandHandler _productSaveCommandHandler;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();

        _productSaveCommandHandler = new ProductSaveCommandHandler(_unitOfWorkFactoryMock.Object,
            _productRepositoryMock.Object, _categoryRepositoryMock.Object);
    }

    public class HandleMethod : ProductSaveCommandHandlerTest
    {
        [Test]
        public void Valid_Inputs_ReturnsNoError()
        {
            // Arrange
            var command = new ProductSaveCommand {Title = "Test"};

            var context = Mock.Of<ConsumeContext<ProductSaveCommand>>(c => c.Message == command);

            // Act and Assert
            Assert.DoesNotThrow(() => _productSaveCommandHandler.Consume(context));
        }

        [Test]
        public void Invalid_CategoryId_ThrowsException()
        {
            // Arrange
            var command = new ProductSaveCommand {CategoryId = Guid.Empty, Title = "Test"};

            var context = Mock.Of<ConsumeContext<ProductSaveCommand>>(c => c.Message == command);

            Assert.ThrowsAsync<InvalidDataException>(async () => await _productSaveCommandHandler.Consume(context));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Invalid_Title_ThrowsException(string fileName)
        {
            // Arrange
            var command = new ProductSaveCommand {CategoryId = Guid.Empty};

            var context = Mock.Of<ConsumeContext<ProductSaveCommand>>(c => c.Message == command);

            Assert.ThrowsAsync<InvalidDataException>(async () => await _productSaveCommandHandler.Consume(context));
        }
    }
}