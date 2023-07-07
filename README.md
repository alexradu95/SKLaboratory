# SKLaboratoryHere's a high-level overview of the architecture:

Interfaces: The project defines several interfaces in the SKLaboratory.Infrastructure.Interfaces namespace. The IWidget interface is particularly important as it defines the basic behavior of a widget, including initialization, drawing, shutdown, and position handling. Other interfaces like IWidgetFactory are used for creating widgets.

Widget Factory: The WidgetFactory class, which implements the IWidgetFactory interface, is responsible for creating widgets. It currently supports the creation of CubeWidget and FloorWidget.

Widget Manager: The WidgetManager class is responsible for managing the lifecycle of widgets. It uses the WidgetFactory to create widgets and maintains a list of active widgets. It provides methods to activate and deactivate widgets.

Widgets: Widgets are the main components of the application. Each widget implements the IWidget interface. The CubeWidget and FloorWidget are examples of widgets in this project. They define their own initialization, drawing, and shutdown behavior.

Main Program: The Program class is the entry point of the application. It initializes the WidgetManager and activates the CubeWidget and FloorWidget. It also sets up the main loop for drawing active widgets.

Android Activity: The MainActivity class is the entry point for the Android application. It sets up the Android activity and starts the main program in a separate thread.

This architecture allows for a modular design where new types of widgets can be easily added by implementing the IWidget interface and extending the WidgetFactory to support the new widget type. The WidgetManager provides a centralized place to manage the lifecycle of all widgets.