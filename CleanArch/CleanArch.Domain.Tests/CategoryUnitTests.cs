using System;

using Xunit;

using FluentAssertions;

using CleanArch.Domain.Entities;
using CleanArch.Domain.Validation;

namespace CleanArch.Domain.Tests
{
    public class CategoryUnitTests
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid name. Too short, minimum 3 characthers");
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");

            action.Should()
                .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid name. Name is required");
        }
    }
}