# UtK2 Text Editor
Allows the modification of text found in Under the Knife 2. Requires using text files extracted from the game. This tool, for now, only allows the modification of these text files, not finding and modifying the game directly

# Running
Executables for Windows are available in the releases page. A dotnet 9 runtime installation is required to run the program. When converting a text file from the game using the tool, a "rom.nds" file is generated in the same directory where the executable has been run.

# Building
Project is buildable using a dotnet 9 Environment on Windows, with no extra dependencies not available in a standard Desktop runtime. As of now, it is untested on both Linux and MacOS environments. The ROM loader project [available here](https://github.com/levathian1/DSROMLoader) is a required dependency to build and run the project
