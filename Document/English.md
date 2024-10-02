# What's RipeConsole?

RipeConsole is a console tool designed to make it easy for users to Debug, Test and Execute their JavaScript code. Allows sorting files into Categories, Groups, SubGroups and Functions for selection from its Menu. It uses the V8Script engine for this, and ZCore, a Library made also by me (without that the Program won't work, most of the Dependencies and TypeDefinitions are there)

The project is open source, which means that you can take what you need from it whenever you want.

## Platform Support

The Tool supports the following OS, i'm planing to add Support to Mobile and also to make a GUI version per each Platform:

- `Windows x64+`
- `Linux x64+`
- `Macintosh x64+`

## Installation

To install the program, no complex steps are required, just do the following:

1. Download VisualStudio (if you want a higher performance you can use VisualStudio Code, no problem) and the latest version of the .NET Framework (RipeConsole uses ver 8.0 for optimization reasons).

2. Once VS is downloaded and installed, open this repository (which you will have to download to your device).

3. Once opened you can compile the project as follows: if you are in VisualStudio click on `Run/Run and Debug`, if you are in VS Code you must open a terminal and enter the command `dotnen run` to start compiling the project. With that you should have the executable at hand, by default it is generated in “Compiled”, inside the same folder of the project (if desired, you can change the path in the .csproj file).

4. If you desire, you can download this assets I made, they serve for handling different kind of files: [here](https://github.com/RaFranVJ/RipeUtils)

Once download is finished, extract file's content and copy it to the Compiled Project, the Tool will load this Assets at runtime.

## Usage

The use of the program is simple, it allows to load Scripts from pure JavaScript or with injected code (C# class types that are defined in the function Entry along with other parameters to customize the loading and compilation of the JS code before executing it). By default the program generates templates with generic code of the serializable classes that work to make this possible, here is a brief description of each of them:

- GroupCategory: represents a category of groups, each group is loaded from the same folder. You can customize certain parameters like its ID, name or path to the groups that need to be loaded from it (jsons must exist in that folder and should match the structure of the FuncGroup class that I am going to define below

- FuncGroup: represents a group of functions or other groups. Each function/group is loaded from a single folder. You can customize parameters like ID, Name and the Path to SubGroups/Funcs dir, note that the group cannot load functions and subgroups simultaneously, you have to specify one of both.

- Function: represents a JavaScript function. Functions can be filtered by certain criteria (such as by extension or by file name when selecting them in the menu). You can customize the ID or name of the function, a path must be assigned to the Script entry in order to correctly identify the necessary parameters. If the function requires arguments such as I/O paths or the name of a method to call at runtime, you will have to set them in the json (args should be in the same order they are passed in the ScriptMethod)

- JSCriptEntry: represents an access point for a JS file. In it you can define how the document should be compiled, among other functions such as its name or the path to the file in question. If you want to inject code to the Script you must map the variables that represent the C# types together with the name they have in the JS Source, in a Dictionary called TypesToExpose (or NamedItems for object instances), note that C# types must be declared as dependencies  in the App (such as Libraries or other Projects) so the Injection works fine.

- JS File: is the JavaScript file in question, it can be a direct expression such as `2 + 1`, for example, or a set of expressions that can be separated into functions. These functions are called from C# by name and may or may not take arguments (of the direct type or by accessing properties from the UserParams class).

All of this clases are loaded in program execution and can be Selected through Keyboard in Menu (enter a Number or Navigate using UP or DOWN, then press ENTER, you can change Keybinds in the Settings file). UserParams is used for specifying the arguments the methods require, change them in the JSON or through the App menu).

## Special thanks

-   [TwinStar](https://github.com/twinkles-twinstar/): Some of his code was taken from [TwinStar.ToolKit](https://github.com/twinkles-twinstar/TwinStar.ToolKit) and translated to C#.

- [Haruma](https://github.com/Haruma-VN/): helped me a lot thanks to his project [Sen](https://github.com/Haruma-VN/Sen) and feedback. 

- [YingFengTingYu](https://github.com/YingFengTingYu/): I started implementing some of his code from [PopStudio](https://github.com/YingFengTingYu/PopStudio_Old) and giving improvements to it in my tool.