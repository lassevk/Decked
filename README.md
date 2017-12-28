# Decked

A .NET application for making it easy to write advanced interfaces
for the [Elgato Stream Deck](https://www.elgato.com/en/gaming/stream-deck).

The aim of the project is to create an application framework for which "plugins" can be made
in .NET. A plugin would have its focus on how to talk to other programs, such as streaming programs
or games, and not need to have code that actually talks to the Stream Deck.

Instead, Decked would provide the bridge between such a plugin and the actual Stream Deck hardware,
letting plugin developers focus on the plugin and not the nuances of the Stream Deck itself.

Configuration will be through JSON files which will combine the Decked application with the
plugins in order to make "Screens", combinations of buttons for display on the Stream Deck. The
aim is to make it fully customizable so that people wanting to build these screens can pick
and choose between all the buttons that the plugins make available and compose their screens
through simple configuration files.

The current short term goal is to complete a prototype in order to see if this project is viable.

---

* Icons used in example projects are courtesy of [Elgato Key Creator](https://www.elgato.com/gaming/keycreator).