# UtK2 Text Editor
Allows the modification of text found in Under the Knife 2.

# Running
Executables for Windows are available in the releases page. A dotnet 9 runtime installation is required to run the program. When converting a text file from the game using the tool, a "rom.nds" file is generated in the same directory where the executable has been run.

# Building
Project is buildable using a dotnet 9 Environment on Windows, with no extra dependencies not available in a standard Desktop runtime. As of now, it is untested on both Linux and MacOS environments. The ROM loader project [available here](https://github.com/levathian1/DSROMLoader) is a required dependency to build and run the project

# Resources used
- [Tinke](https://github.com/pleonex/tinke)
- [gbatek](https://problemkaputt.de/gbatek.htm#dscartridgeheader)
- [.NET documentation](https://learn.microsoft.com/en-gb/dotnet/)
