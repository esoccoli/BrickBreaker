# Breakout

A recreation of the popular game Atari Breakout. Made using Monogame as a framework for C#, and designed for a vertical display.

There are two ways to install Monogame on your machine.

## Through Visual Studio
The first method is to install the monogame extension in [Visual Studio](https://visualstudio.microsoft.com/vs/). To do this, download [Visual Studio](https://visualstudio.microsoft.com/vs/). When you open the program, you will see a screen that propts you to create or open a project. In the right column, underneath the button to create a project, there will be an option to continue without code. Click on that. Next, go to the navbar at the top of the program, and click on the extensions tab. In the dropdown, click on manage extensions. Use the search bar in the top right corner to search for Monogame. Find the result called MonoGame Framework C# Project Templates. Click on it, and click download.

## Command Line
The second method is to install it through the command line. To do this, open a new terminal, and run `dotnet new --install MonoGame.Templates.CSharp`. Then, you can open Visual Studio, and create a new project. When it propmts you to select a project type, search for **MonoGame Cross-Platform Desktop Application**, Select that, then name your project and create it.

## Project Components
There are a few main components to a MonoGame project, being the Game1.cs file and the Content.mgcb editor. The Game1.cs file is the main program. This file has pre-defined methods to allow developers to make games, without having to worry about making everything run and draw and update every frame. The Content.mgcb editor is the primary tool for adding sprites to the game. This allows items in the game to have custom images displayed in the game window.

For more information about installing, using, or understanding MonoGame, see the [MonoGame Documentation](https://docs.monogame.net/)
