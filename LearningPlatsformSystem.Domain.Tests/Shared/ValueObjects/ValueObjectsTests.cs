using LearningPlatformSystem.Domain.Shared.ValueObjects;

namespace LearningPlatsformSystem.Domain.Tests.Shared.ValueObjects;

public class ValueObjectsTests
{
    // fake valueObject eftersom ValueObject är en abstrakt basklass och inte kan instansieras direkt. 
    private class TestValueObject : ValueObject
    {
        public string Name { get; }

        public TestValueObject(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Name;
        }
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        TestValueObject first = new TestValueObject("Test");
        TestValueObject second = new TestValueObject("Test");

        // Act
        bool result = first.Equals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenValuesAreDifferent()
    {
        // Arrange
        TestValueObject first = new TestValueObject("Test1");
        TestValueObject second = new TestValueObject("Test2");

        // Act
        bool result = first.Equals(second);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void OperatorEquality_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        TestValueObject first = new TestValueObject("Test");
        TestValueObject second = new TestValueObject("Test");

        // Act
        bool result = first == second;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void OperatorInequality_ShouldReturnTrue_WhenValuesAreDifferent()
    {
        // Arrange
        TestValueObject first = new TestValueObject("Test1");
        TestValueObject second = new TestValueObject("Test2");

        // Act
        bool result = first != second;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenObjectsAreEqual()
    {
        // Arrange
        TestValueObject first = new TestValueObject("Test");
        TestValueObject second = new TestValueObject("Test");

        // Act
        int firstHashCode = first.GetHashCode();
        int secondHashCode = second.GetHashCode();

        // Assert
        Assert.Equal(firstHashCode, secondHashCode);
    }
}
