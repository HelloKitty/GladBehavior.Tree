dotnet restore GladBehavior.Tree.sln
%NUGET% restore GladBehavior.Tree.sln -NoCache -NonInteractive -ConfigFile Nuget.config
dotnet build GladBehavior.Tree.sln -c Release