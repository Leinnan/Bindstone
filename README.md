# Bindstone

> Disclaimer- I am not the original creator of the framework but I intend to make it easier to use and have some improvements over the original.
> 
> There are already changes to the structure of the project so it is possible to use it as regular package and I did create a `BaseModelView` class that makes easier to create a new view classes.
> 
> I will try my best to improve the usabilty of the framework and maybe rework the inspectors to take advantage of the ToolkitUI.

*[MVVM-style](https://msdn.microsoft.com/en-us/library/hh848246.aspx) data-binding system for Unity.*

## Installation

Add the following line to *Packages/manifest.json*:
  - `"com.leinnan.unity-weld": "git+https://github.com/Leinnan/Bindstone",`

---
---
# Old description

Unity-Weld is a library for Unity 5+ that enables two-way data binding between Unity UI widgets and game/business logic code. This reduces boiler-plate code that would otherwise be necessary for things like updating the UI when a property changes, removes the need for messy links between objects in the scene that can be broken easily, and allows easier unit testing of code by providing a layer of abstraction between the UI and your core logic code.

A series of articles on Unity Weld has been published on [What Could Possibly Go Wrong](http://www.what-could-possibly-go-wrong.com/bringing-mvvm-to-unity-part-1-about-mvvm-and-unity-weld).

Example Unity project can be found here: [https://github.com/Real-Serious-Games/Unity-Weld-Examples](https://github.com/Real-Serious-Games/Unity-Weld-Examples).


## Getting started

Check out the [Unity-Weld-Examples](https://github.com/Real-Serious-Games/Unity-Weld-Examples) repository for some examples of how to use Unity-Weld.

[API docmentation](https://github.com/Real-Serious-Games/Unity-Weld/wiki) is on our wiki.

If you're interested in getting involved feel free to check out the [roadmap on Trello](https://trello.com/b/KVFUvGR0), or submit a pull request. Make sure to read our [contributing guide](CONTRIBUTING) first.
