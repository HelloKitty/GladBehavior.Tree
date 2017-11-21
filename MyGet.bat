%NUGET% restore GladBehavior.Tree.sln -NoCache -NonInteractive -ConfigFile Nuget.config
msbuild GladBehavior.Tree.sln /p:Configuration=Release