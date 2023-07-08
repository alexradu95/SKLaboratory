using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Widgets;

namespace SKLaboratory.Infrastructure.Tests.Services;

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
        var expectedWidget = new MockNullWidget();
        _widgetFactory.RegisterWidget(expectedWidget.GetType());

        // Act
        var result = _widgetFactory.CreateWidget(expectedWidget.GetType());

        // Assert
        Assert.IsInstanceOf(expectedWidget.GetType(), result);
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
        var widgetType = typeof(MockNullWidget);
        _widgetFactory.RegisterWidget(widgetType);

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _widgetFactory.RegisterWidget(widgetType));
    }
}

public class UnknownWidget { }