using Moq;
using NUnit.Framework;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Widgets;
using System;
using System.Collections.Generic;

namespace YourNamespace.Tests
{
    [TestFixture]
    public class WidgetManagerTests
    {
        private Mock<IWidgetFactory> _widgetFactoryMock;
        private WidgetManager _widgetManager;

        [SetUp]
        public void SetUp()
        {
            _widgetFactoryMock = new Mock<IWidgetFactory>();
            _widgetManager = new WidgetManager(_widgetFactoryMock.Object);
        }

        [Test]
        public void ActivateWidget_WhenWidgetTypeAlreadyActive_ShouldReturnFalse()
        {
            // Arrange
            var widgetType = typeof(MockNullWidget);
            _widgetFactoryMock.Setup(factory => factory.CreateWidget(widgetType)).Returns(new Mock<IWidget>().Object);
            _widgetManager.ActivateWidget(widgetType); // Activate the widget once

            // Act
            var result = _widgetManager.ActivateWidget(widgetType); // Try to activate it again

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ActivateWidget_WhenWidgetInitializationFails_ShouldReturnFalse()
        {
            // Arrange
            var widgetType = typeof(MockNullWidget);
            var widgetMock = new Mock<IWidget>();
            widgetMock.Setup(widget => widget.Initialize()).Returns(false);
            _widgetFactoryMock.Setup(factory => factory.CreateWidget(widgetType)).Returns(widgetMock.Object);

            // Act
            var result = _widgetManager.ActivateWidget(widgetType);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ActivateWidget_WhenWidgetActivationSuccessful_ShouldReturnTrueAndAddToActiveWidgets()
        {
            // Arrange
            var widgetType = typeof(MockNullWidget);
            var widgetMock = new Mock<IWidget>();
            widgetMock.Setup(widget => widget.Initialize()).Returns(true);
            _widgetFactoryMock.Setup(factory => factory.CreateWidget(widgetType)).Returns(widgetMock.Object);

            // Act
            var result = _widgetManager.ActivateWidget(widgetType);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_widgetManager.ActiveWidgetsList.ContainsKey(widgetType), Is.True);
            Assert.That(_widgetManager.ActiveWidgetsList[widgetType], Is.SameAs(widgetMock.Object));
        }

        [Test]
        public void DeactivateWidget_WhenWidgetTypeNotActive_ShouldReturnFalse()
        {
            // Arrange
            var widgetType = typeof(MockNullWidget);

            // Act
            var result = _widgetManager.DeactivateWidget(widgetType);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DeactivateWidget_WhenWidgetShutdownThrowsException_ShouldReturnFalse()
        {
            // Arrange
            var widgetType = typeof(MockNullWidget);
            var widgetMock = new Mock<IWidget>();
            widgetMock.Setup(widget => widget.Shutdown()).Throws<Exception>();
            _widgetManager.ActivateWidget(widgetType);

            // Act
            var result = _widgetManager.DeactivateWidget(widgetType);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DeactivateWidget_WhenWidgetShutdownSuccessful_ShouldReturnTrueAndRemoveFromActiveWidgets()
        {
            // Arrange
            var widgetType = typeof(MockNullWidget);
            var widgetMock = new Mock<IWidget>();
            widgetMock.Setup(widget => widget.Shutdown());
            widgetMock.Setup(widget => widget.Initialize()).Returns(true);
            _widgetFactoryMock.Setup(factory => factory.CreateWidget(widgetType)).Returns(widgetMock.Object);

            // Act
            _widgetManager.ActivateWidget(widgetType);
            var result = _widgetManager.DeactivateWidget(widgetType); 

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_widgetManager.ActiveWidgetsList.ContainsKey(widgetType), Is.False);
        }
    }
}
