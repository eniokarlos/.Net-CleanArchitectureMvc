using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact]
    public void CreateCategory_WithValidParameters_ResultObjetcValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_ResultExceptionInvalidState()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid Id value.");
    }
}