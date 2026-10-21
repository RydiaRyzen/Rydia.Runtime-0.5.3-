using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

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

            // ソリューションが存在しない場合のみ生成する
            if (!File.Exists(solutionPath))
            {
                RunDotNet(
                    Path,
                    "new",
                    "sln",
                    "--name",
                    Name,
                    "--format",
                    "sln");
            }

            var lines = File.ReadAllLines(solutionPath).ToList();

            // Shared Project と通常の C# プロジェクトの種類 GUID
            const string sharedProjectTypeGuid =
                "{D954291E-2A0B-460D-934E-DC6B0785DB48}";

            const string csharpProjectTypeGuid =
                "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";

            int globalIndex = lines.FindIndex(line =>
                line.Trim().Equals("Global", StringComparison.Ordinal));

            if (globalIndex < 0)
            {
                throw new InvalidOperationException(
                    "The solution file does not contain a Global section.");
            }

            foreach (var project in this._projects)
            {
                string extension =
                    project.ProjectType == ProjectType.Shared
                        ? ".shproj"
                        : ".csproj";

                string relativePath = System.IO.Path.Combine(
                    project.Name,
                    project.Name + extension);

                relativePath = relativePath.Replace('\\', '/');

                // 同じプロジェクトが既に登録されている場合はスキップ
                bool exists = lines.Any(line =>
                    line.StartsWith("Project(", StringComparison.Ordinal) &&
                    line.Contains(
                        $"\"{relativePath}\"",
                        StringComparison.OrdinalIgnoreCase));

                if (exists)
                    continue;

                string typeGuid =
                    project.ProjectType == ProjectType.Shared
                        ? sharedProjectTypeGuid
                        : csharpProjectTypeGuid;

                string projectGuid =
                    "{" + Guid.NewGuid().ToString().ToUpperInvariant() + "}";

                string projectBlock =
                    $"Project(\"{typeGuid}\") = " +
                    $"\"{project.Name}\", " +
                    $"\"{relativePath}\", " +
                    $"\"{projectGuid}\"";

                // Global セクションより前にプロジェクトブロックを追加
                lines.Insert(globalIndex, "EndProject");
                lines.Insert(globalIndex, projectBlock);

                globalIndex += 2;
            }

            File.WriteAllText(
                solutionPath,
                string.Join("\r\n", lines) + "\r\n",
                new System.Text.UTF8Encoding(true));
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

        internal static SolutionFile Load(string path)
        {
            return new SolutionFile(System.IO.Path.GetFileNameWithoutExtension(path))
            {
                Path = System.IO.Path.GetDirectoryName(path) ?? string.Empty
            };
        }
    }
}
