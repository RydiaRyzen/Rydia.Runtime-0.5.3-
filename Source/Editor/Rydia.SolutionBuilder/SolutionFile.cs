using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// ソリューションファイルと、それに含まれるプロジェクトを管理するクラスです。
    /// </summary>
    public sealed class SolutionFile
    {

        private readonly List<ProjectFile> _projects = new();

        /// <summary>
        /// ソリューション名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ソリューションの出力先ディレクトリのパスを取得または設定します。
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// ソリューションに登録されているプロジェクトの一覧を取得します。
        /// </summary>
        public IReadOnlyList<ProjectFile> Projects
        {
            get
            {
                return this._projects;
            }
        }

        /// <summary>
        /// 指定した名前で <see cref="SolutionFile"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">ソリューション名。</param>
        public SolutionFile(string name)
        {
            Name = name;
        }

        /// <summary>
        /// ソリューションにプロジェクトを追加します。
        /// </summary>
        /// <param name="project">追加するプロジェクト。</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="project"/> が <see langword="null"/> の場合にスローされます。
        /// </exception>
        public void AddProject(ProjectFile project)
        {
            ArgumentNullException.ThrowIfNull(project);

            if (!this._projects.Contains(project))
            {
                this._projects.Add(project);
            }
        }

        /// <summary>
        /// 登録されているプロジェクトとソリューションファイルを生成します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// ソリューションの出力先ディレクトリが指定されていない場合にスローされます。
        /// </exception>
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

        /// <summary>
        /// .NET CLI を使用してソリューションファイルを生成し、
        /// 登録されている各プロジェクトをソリューションに追加します。
        /// </summary>
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
                var ext = project.ProjectType == ProjectType.Shared ? ".shproj" : ".csproj";
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

        /// <summary>
        /// ソリューションを Debug 構成でビルドします。
        /// </summary>
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

        /// <summary>
        /// 指定した引数で .NET CLI コマンドを実行します。
        /// </summary>
        /// <param name="workingDirectory">コマンドの作業ディレクトリ。</param>
        /// <param name="arguments">.NET CLI に渡すコマンドライン引数。</param>
        /// <exception cref="InvalidOperationException">
        /// .NET CLI プロセスを起動できない場合、または実行結果の終了コードが 0 以外の場合にスローされます。
        /// </exception>
        private static void RunDotNet(
            string workingDirectory,
            params string[] arguments)
        {
            Console.WriteLine(
                "dotnet " + string.Join(" ", arguments));

            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = workingDirectory,
                FileName = "dotnet",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
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

            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
        $"dotnet exited with code {process.ExitCode}.\n" +
        $"標準出力:\n{standardOutput}\n" +
        $"標準エラー:\n{standardError}");
            }
        }

        /// <summary>
        /// 生成したソリューションファイルを既定のアプリケーションで開きます。
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
