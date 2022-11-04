# Breakout

A recreation of the popular game Atari Breakout. Made using Monogame as a framework for C#, and designed for a vertical display.

## Dependencies
This program has a few dependencies that allow it to run properly. The first dependency is `.NET 6.0`. This needs to be installed on a device before the program can run. It can be downloaded from [here](https://dotnet.microsoft.com/en-us/). Be sure to select the correct operating system and download version 6.0

The next dependency is Monogame. This can be installed in multiple ways. The first option is through the command line. To install it this way, open a new terminal and enter `dotnet new --install MonoGame.Templates.CSharp`. The other option is to install it through [Visual Studio](https://visualstudio.microsoft.com/). 

Although any IDE will work for this program, Visual Studio has many features that add convenience, such as an extension and NuGet package manager built in. To install MonoGame through Visual Studio, open the program and click `continue without code`. Then, through the navbar at the top,  open the extension manager, and search for MonoGame, and install the `MonoGame Framework C# Project Template`.

The last dependency is the NuGet package for the devcade library. This package needs to be added to the project on each device that the project is run on. The easiest way to install the package is through the NuGet package manager within Visual Studio. In the `Project` tab in the navbar, click on `Manage NuGet Packages`. Search for `devcade` and install the most recent version of the `devcade-library` package.

Once all of those dependencies are installed, clone this repository, and open it in Visual Studio. Simply click the run button in Visual Studio, and it will run.

____

## Creating with Monogame

In order to create a MonoGame project, the MonoGame extension and .NET 6.0 must be installed to the device. This can be done by following the steps above. Once those are installed, there are two ways to create a MonoGame project.

#### Command Line

The first option is through the command line. In a new terminal, run the command `dotnet new mgdesktopgl -o GameName`. Then, find the project folder in the file explorer, and open it in any code editor.

#### Visual Studio

The other option is through Visual Studio. To do this, open Visual Studio, and on the main menu, click `Create a New Project`. Next, search for the `MonoGame Cross-Platform Desktop Application` template and select it. Finally, enter a name for the project, and make sure the option to `Place solution and project in the same directory` is checked. Then, click create, and the project will be generated.

____

### Project Componenets

Each MonoGame project has a few basic componenets: the `Game1.cs` file, the `Program.cs` file, and the `Content` folder. Each of these items has an important function.

#### Game1 File

This is the primary file in the game. It contains the code for everything that happens within the game. It is pre-filled with a basic constructor, and 4 methods: Initialize, LoadContent, Update, and Draw. Each time the program is run, the `Initialize()` method and the `LoadContent()` method are called once, and then the program loops through the `Update()` and `Draw()` methods each frame.

#### Program File

This file contains only two lines, but is essential for making the program work. This file is generally left unchanged.

#### Content Folder

The content folder contains the Content Manager tool, which is how sprites and textures can be processed and loaded into the game. Once a file has been processed by the Content Manager, it gets place in this folder, giving the program access to the file.
