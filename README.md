# FSADDER

This is a program with one job. To add compile lines to your .fsproj files without doing it manually. It looks at your file, finds the <Compile Include="thing.fs"> ItemGroup and appends the provided text to it.

Example command

` ./FSAdder hello\testing\testing.fs `

Will result in

` <Compile Include="hello\testing\testing.fs" /> `

Simply include the .exe file in your folder with the .fsproj file and it will do its thing when you run commands at it.