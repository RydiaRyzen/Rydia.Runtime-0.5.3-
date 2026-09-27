using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{
    public sealed class SolutionFile
    {
        private readonly List<ProjectFile> _projects = new();

        public string Name { get; }

        public string Path { get; set; }

        public IReadOnlyList<ProjectFile> Projects
        {
            get
            {
                return this._projects;
            }
        }

        public SolutionFile(string name)
        {
            Name = name;
        }

        public void AddProject(ProjectFile project)
        {
            ArgumentNullException.ThrowIfNull(project);

            if (!this._projects.Contains(project))
            {
                this._projects.Add(project);
            }
        }

        public void Generate()
        {
            if (string.IsNullOrWhiteSpace(Path))
                throw new InvalidOperationException(
                    "Solution path has not been specified.");

            Directory.CreateDirectory(Path);

            // プロジェクトを生成
            foreach (var project in this._projects)
            {
                project.Generate(Path);
            }

            // ソリューションを生成
            GenerateSolutionFile();
        }

        private void GenerateSolutionFile()
        {
            string solutionPath = System.IO.Path.Combine(
                Path,
                Name + ".sln");

            // .sln を明示的に指定
            RunDotNet(
                Path,
                "new",
                "sln",
                "--name",
                Name,
                "--format",
                "sln");

            // プロジェクトをソリューションへ追加
            foreach (var project in this._projects)
            {
                var ext = project.ProjectType == ProjectType.ClassLibrary ? ".csproj" : ".shproj";
                string projectPath = System.IO.Path.Combine(
                    Path,
                    project.Name,
                    project.Name + ext);

                RunDotNet(
                    Path,
                    "sln",
                    solutionPath,
                    "add",
                    projectPath);
            }
        }

        public void Build()
        {
            

            string solutionPath = System.IO.Path.Combine(
                Path,
                Name + ".sln");

            RunDotNet(
                Path,
                "build",
                solutionPath,
                "--configuration",
                "Debug");
        }

        private static void RunDotNet(
            string workingDirectory,
            params string[] arguments)
        {
            Console.WriteLine(
                "dotnet " + string.Join(" ", arguments));

            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                WorkingDirectory = workingDirectory,

                UseShellExecute = false,
                CreateNoWindow = true,

                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            foreach (string argument in arguments)
            {
                startInfo.ArgumentList.Add(argument);
            }

            using var process = Process.Start(startInfo)
                ?? throw new InvalidOperationException(
                    "Failed to start dotnet.");

            string output =
                process.StandardOutput.ReadToEnd();

            string error =
                process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
                Console.WriteLine(output);

            if (!string.IsNullOrEmpty(error))
                Debug.WriteLine(error);

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    $"dotnet exited with code {process.ExitCode}.");
            }
        }

        /// <summary>
        /// ソリューションファイルを開きます
        /// </summary>
        public void Open()
        {
            string solutionPath = System.IO.Path.Combine(Path, Name + ".sln");

            Process.Start(new ProcessStartInfo
            {
                FileName = solutionPath,
                UseShellExecute = true
            });
        }

    }
}
