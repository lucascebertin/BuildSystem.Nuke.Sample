using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using System.Linq;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace build
{
    public class Build : NukeBuild
    {
        [Solution] readonly Solution Solution;

        private Project ConsoleApp => Solution.GetProject("*.App").NotNull();

        public static int Main() =>
            Execute<Build>(x => x.Publish);

        private Target Clean => _ => _
            .Executes(() =>
            {
                DeleteDirectories(GlobDirectories(SourceDirectory, "*/bin", "*/obj"));
                EnsureCleanDirectory(OutputDirectory);
            });

        private Target Restore => _ => _
            .DependsOn(Clean)
            .Executes(() =>
                DotNetRestore(s => s
                    .SetProjectFile(Solution))
            );

        private Target Compile => _ => _
            .DependsOn(Restore)
            .Executes(() =>
                DotNetBuild(s => s
                    .SetProjectFile(Solution)
                    .EnableNoRestore()
                    .SetConfiguration(Configuration)
                    .SetAssemblyVersion("1.0.0")
                    .SetFileVersion("1.0.0")
                    .SetInformationalVersion("1.0.0")));

        private Target Publish => _ => _
            .DependsOn(Test)
            .Executes(() =>
            {
                var publishSettings = new DotNetPublishSettings()
                    .EnableNoRestore()
                    .SetConfiguration(Configuration)
                    .SetOutput(OutputDirectory);

                DotNetPublish(s => publishSettings
                    .SetProject(ConsoleApp)
                    .SetFramework("netcoreapp2.1"));
            });

        private Target Test => _ => _
           .DependsOn(Compile)
           .Executes(() =>
           {
               foreach (var project in Solution.Projects.Where(x => x.Name.Contains("Tests")))
               {
                   var command =
                       $"test {project.Path} /p:CollectCoverage=true /p:Threshold=80 /p:CoverletOutputFormat=opencover /p:CoverletOutput=\"{OutputDirectory}\\result.{project.Name}.xml\"";

                   DotNet(command);
               }
           });
    }
}
