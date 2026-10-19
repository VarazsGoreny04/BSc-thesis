BSc-thesis
==========
This was my BSc thesis when I was a student as Eötvös Loránd University. It is a scientific *calculator* built upon a math expression *parser* that is built upon a collection of *number types*. Everything was created from scratch.

The main point of this project was to eliminate every form of data loss of the built in **float** and **double** number types. So the strict rule while creating the number types was that it must not use any form of "binary floating point number" representation.

The workaround was to make a custom *Binary Coded Decimal (BCD)* number type that does not have a lower and upper limit, can represent rational numbers and implement every common operation and function in it.

ProjectReal
-----------
At uni we needed to take two classes about floating point numbers and methods to make the data loss acceptable while working with them. I remember when I realized that most of the things we learned in that one year would be useless if we could fix the couple problems of the currently used number representations. I was furious! Such a simple problem and we just accept it?!

In a couple months I came up with a decimal representation that could bypass most of the sources of the data loss. After a while, one of my friends pointed out that this data type was actually invented before me, which made me a little sad, but what can one do about it.

Implementing the operations and functions was lets just say: Interesting... I really struggled finding algorithms that could calculate **π**, **e**, **exp**, **sin**, **cos** and **ln**.

After lots of research it all came together and I had a *fairly fast floating point number type* that used *decimal representation* to bypass the decimal-to-binary conversion, that could be *any length* (the boundary is the maximum length of an array) to eliminate the loss of truncation and could handle *rational format* to cancel the data loss of division.

Calculators
-----------
This project uses the **ProjectReal** library because it needs the specific number set *interfaces* declared in that project.

Currently the following calculators are implemented in this library:
- **StandardCalculator**:<br>
  A calculator that understands the basic numbers, operations and trigonometric functions.
- **EuclideanSpaceCalculator**:<br>
  A calculator that understands matrices and can perform operations with them. Matrix and vector representations and operations are part of the library.
- **InterpolationCalculator**:<br>
  A calculator that understands 2×N matrices as (X, Y) coordinate pairs and can perform interpolations with them.

The calculator is implemented using generic types and can use any number representation that descends from the number sets of the **ProjectReal** library.

BullseyeCalculator
------------------
This is the final product: The calculator. But it consists of several projects to follow some architecture conventions.

To be able to cancel a calculation the program needed to be separated into two parts: A main program and an executable that can be ran by a process of the main program. When cancel is requested, the main program can call a *SIGKILL* on the process and exit the calculation.

It is implemented this way, so that no cancel checking is needed in any of the methods. Another solution would have been a to create a separate thread and to call **Thread.Kill** method when cancelled, but sadly that function is deprecated because it can leak memory. Doing it this way the operating system will take care of everything.

So the architecture used in this case is a weird version of the *MVVM (Model-View-ViewModel)* architecture because it is implemented the following way:
- **BullseyeCalculator**:<br>
  The *Model* part of the architecture. Stores the input, result and partial steps, allows options of editing and calculating the result.
- **BullseyeCalculator.Process**:<br>
  A console application that uses the **BullseyeCalculator** project. It accepts arguments, like the calculator type and the formula and outputs the result to the console.
- **BullseyeCalculator.WPF**:<br>
  The *View* and *ViewModel* part of the architecture. Uses the *Windows Presentation Foundation (WPF)* library to create an easy-to-use UI for the application. It creates a process to run the **BullseyeCalculator.Process** executable.

Caution:
--------
This is an experimental project implemented in C#.

The decimal types are super slow compared to the processor accelerated built in types and the memory usage is much higher than if it was implemented in C or C++. I chose this language, because I already moved comfortably in it and the built in garbage collector made the memory usage much simpler.

If I wanted to rewrite it, I would chose a different language.
