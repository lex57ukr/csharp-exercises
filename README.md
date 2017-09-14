# C# Exercism Exercises

1. Download and install [.NET Core](https://www.microsoft.com/net/core)
1. Download and install [VSCode](https://code.visualstudio.com/)

After cloning a repo, execute the below command to restore all dependencies at once (in bash):

```bash
find . -path "*/*.csproj" -exec dotnet restore {} \;
```