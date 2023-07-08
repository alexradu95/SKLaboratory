// File: SKLaboratory.Infrastructure.Tests/Services/WidgetFactoryTests.cs
using NUnit.Framework;
using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Widgets;
using StereoKit;

namespace SKLaboratory.Infrastructure.Tests.Services
{
    [TestFixture]
    public class WidgetFactoryTests
    {
        private WidgetFactory _widgetFactory;

        [SetUp]
        public void SetUp()
        {
            _widgetFactory = new WidgetFactory();
        }

        [Test]
        public void CreateWidget_WhenRegisteredWidgetType_ShouldReturnWidgetInstance()
        {
            // Arrange
            var expectedWidgetType = typeof(MockNullWidget);
            _widgetFactory.RegisterWidget<MockNullWidget>();

            // Act
            var result = _widgetFactory.CreateWidget(expectedWidgetType);

            // Assert
            Assert.IsInstanceOf(expectedWidgetType, result);
        }

        [Test]
        public void CreateWidget_WhenUnknownWidgetType_ShouldThrowUnknownWidgetTypeException()
        {
            // Arrange
            var unknownWidgetType = typeof(UnknownWidget);

            // Act and Assert
            Assert.Throws<UnknownWidgetTypeException>(() => _widgetFactory.CreateWidget(unknownWidgetType));
        }

        [Test]
        public void RegisterWidget_WhenWidgetTypeAlreadyRegistered_ShouldThrowArgumentException()
        {
            // Arrange
            _widgetFactory.RegisterWidget<MockNullWidget>();

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _widgetFactory.RegisterWidget<MockNullWidget>());
        }
    }
}

public class UnknownWidget : IWidget
{
    // Implement the IWidget interface...
    public Guid Id => throw new NotImplementedException();

    public Matrix Transform => throw new NotImplementedException();

    public Pose Pose => throw new NotImplementedException();

    public void Draw()
    {
        throw new NotImplementedException();
    }

    public bool Initialize()
    {
        throw new NotImplementedException();
    }

    public void Shutdown()
    {
        throw new NotImplementedException();
    }
}
