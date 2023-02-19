using System;

using Xunit;

using FluentAssertions;

using CleanArch.Domain.Entities;
using CleanArch.Domain.Validation;

namespace CleanArch.Domain.Tests
{
    public class ProductUnitTests
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product Image");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product Image");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid name. Too short, minimum 3 characthers");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, 
                "Proooooooooooooooooduuuuuuuuuuuuuuuuuuucttttttttttttttttttttttttttttttttttt tooooooooooooooooooooooooooooooooooooooooo " +
                "loooooonnnnnnnnnnnnnnnnnnnnnnggggggggggggggg Imaaaaaaaaaaaaaaaaaaaaaaaaaggggggggggggggggggeeeeeeeeeeeeeeeeeeeee " +
                "Naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid image name. Too long, maximum 250 characters");
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoNullReferencenException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);

            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "Product Image");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product Image");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid stock value");
        }
    }
}
