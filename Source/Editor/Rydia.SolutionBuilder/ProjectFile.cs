using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// C# プロジェクトの構成とソースファイル、参照情報を管理します。
    /// </summary>
    public sealed class ProjectFile
    {

        private readonly List<ClassFile> _classes = new();
        private readonly List<ProjectFile> _references = new();
        private readonly ProjectType _projectType;
        private readonly List<AssemblyReference> _assemblyReferences = new();

        /// <summary>
        /// プロジェクトの種類を取得します。
        /// </summary>
        public ProjectType ProjectType
        {
            get
            {
                return this._projectType;
            }
        }

        /// <summary>
        /// プロジェクト名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// プロジェクトの出力先ディレクトリのパスを取得または設定します。
        /// </summary>
        public string DirectoryPath { get; internal set; }

        /// <summary>
        /// プロジェクトに含まれるクラスファイルの一覧を取得します。
        /// </summary>
        public IReadOnlyList<ClassFile> Classes
        {
            get
            {
                return this._classes;
            }
        }

        /// <summary>
        /// プロジェクト参照の一覧を取得します。
        /// </summary>
        public IReadOnlyList<ProjectFile> References
        {
            get
            {
                return this._references;
            }
        }

        /// <summary>
        /// アセンブリ参照の一覧を取得します。
        /// </summary>
        public IReadOnlyList<AssemblyReference> AssemblyReferences
        {
            get
            {
                return this._assemblyReferences;
            }
        }

        /// <summary>
        /// 指定した名前とプロジェクトの種類を使用して、
        /// <see cref="ProjectFile"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">プロジェクト名。</param>
        /// <param name="projectType">
        /// プロジェクトの種類。省略した場合は
        /// <see cref="ProjectType.ClassLibrary"/> が使用されます。
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> が <see langword="null"/>、空、または空白文字のみの場合にスローされます。
        /// </exception>
        public ProjectFile(string name, ProjectType projectType = ProjectType.ClassLibrary)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
            this._projectType = projectType;
        }

        /// <summary>
        /// プロジェクトにクラスファイルを追加します。
        /// </summary>
        /// <param name="classFile">追加するクラスファイル。</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="classFile"/> が <see langword="null"/> の場合にスローされます。
        /// </exception>
        public void AddClass(ClassFile classFile)
        {
            ArgumentNullException.ThrowIfNull(classFile);

            this._classes.Add(classFile);
        }

        /// <summary>
        /// プロジェクト参照を追加します。
        /// </summary>
        /// <param name="project">参照するプロジェクト。</param>
        /// <remarks>
        /// 同一のインスタンスが既に登録されている場合、重複して追加しません。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="project"/> が <see langword="null"/> の場合にスローされます。
        /// </exception>
        public void AddReference(ProjectFile project)
        {
            ArgumentNullException.ThrowIfNull(project);

            if (!this._references.Contains(project))
            {
                this._references.Add(project);
            }
        }

        /// <summary>
        /// アセンブリ参照を追加します。
        /// </summary>
        /// <param name="reference">追加するアセンブリ参照。</param>
        /// <remarks>
        /// 同一のインスタンスが既に登録されている場合、重複して追加しません。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reference"/> が <see langword="null"/> の場合にスローされます。
        /// </exception>
        public void AddAssemblyReference(AssemblyReference reference)
        {
            ArgumentNullException.ThrowIfNull(reference);

            if (!this._assemblyReferences.Contains(reference))
            {
                this._assemblyReferences.Add(reference);
            }
        }

        /// <summary>
        /// 指定したアセンブリ名と DLL ファイルへのパスを使用して、
        /// アセンブリ参照を追加します。
        /// </summary>
        /// <param name="name">アセンブリ名。</param>
        /// <param name="hintPath">DLL ファイルへのパス。</param>
        /// <remarks>
        /// <see cref="AssemblyReference"/> のインスタンスを生成して追加します。
        /// </remarks>
        public void AddAssemblyReference(string name, string hintPath)
        {
            AddAssemblyReference(new AssemblyReference(name, hintPath));
        }

        /// <summary>
        /// プロジェクトのディレクトリを作成し、
        /// ソースファイルとプロジェクトファイルを生成します。
        /// </summary>
        /// <param name="rootDirectory">プロジェクトのルートディレクトリ。</param>
        /// <remarks>
        /// プロジェクト名をディレクトリ名として使用し、
        /// 登録されているクラスファイルとプロジェクトファイルを出力します。
        /// </remarks>
        internal void Generate(string rootDirectory)
        {
            DirectoryPath = Path.Combine(
                rootDirectory,
                Name);

            Directory.CreateDirectory(DirectoryPath);

            foreach (var classFile in this._classes)
            {
                string path = Path.Combine(
                    DirectoryPath,
                    classFile.Name + ".cs");

                File.WriteAllText(
                    path,
                    classFile.Generate());
            }

            GenerateProjectFile();
        }

        /// <summary>
        /// プロジェクトの種類に応じたプロジェクトファイルを生成します。
        /// </summary>
        private void GenerateProjectFile()
        {
            switch (ProjectType)
            {
                case ProjectType.ClassLibrary:
                    GenerateClassLibrary();
                    break;

                case ProjectType.Shared:
                    GenerateSharedProject();
                    break;

                case ProjectType.WindowsApplication:
                    GenerateWindowsApplication();
                    break;
            }
        }

        /// <summary>
        /// クラスライブラリのプロジェクトファイル（.csproj）を生成します。
        /// </summary>
        /// <remarks>
        /// ターゲットフレームワーク、ビルド設定、プロジェクト参照、
        /// アセンブリ参照をプロジェクトファイルに出力します。
        /// </remarks>
        private void GenerateClassLibrary()
        {
            var sb = new StringBuilder();

            sb.AppendLine("""
            <Project Sdk="Microsoft.NET.Sdk">

              <PropertyGroup>
                <TargetFramework>net9.0</TargetFramework>
                <ImplicitUsings>enable</ImplicitUsings>
                <Nullable>enable</Nullable>
              </PropertyGroup>
            """);

            if (this._references.Count > 0)
            {
                GenerateProjectReference(sb);
            }

            if (this._assemblyReferences.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("  <ItemGroup>");

                foreach (var reference in this._assemblyReferences)
                {
                    sb.AppendLine(
                        $"    <Reference Include=\"{System.Security.SecurityElement.Escape(reference.Name)}\">");

                    sb.AppendLine(
                        $"      <HintPath>{System.Security.SecurityElement.Escape(reference.HintPath)}</HintPath>");

                    sb.AppendLine("    </Reference>");
                }

                sb.AppendLine("  </ItemGroup>");
            }

            sb.AppendLine();
            sb.AppendLine("</Project>");

            File.WriteAllText(
                Path.Combine(
                    DirectoryPath,
                    Name + ".csproj"),
                sb.ToString());
        }

        /// <summary>
        /// Windows Forms アプリケーションのプロジェクトファイル（.csproj）を生成します。
        /// </summary>
        private void GenerateWindowsApplication()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"""
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net9.0-windows</TargetFramework>
        <UseWindowsForms>true</UseWindowsForms>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    <StartupObject>Rydia.{Name}.Program</StartupObject>
      </PropertyGroup>
    """);

            if (this._references.Count > 0)
            {
                GenerateProjectReference(sb);
            }

            if (this._assemblyReferences.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("  <ItemGroup>");

                foreach (var reference in this._assemblyReferences)
                {
                    sb.AppendLine(
                        $"    <Reference Include=\"{System.Security.SecurityElement.Escape(reference.Name)}\">");

                    sb.AppendLine(
                        $"      <HintPath>{System.Security.SecurityElement.Escape(reference.HintPath)}</HintPath>");

                    sb.AppendLine("    </Reference>");
                }

                sb.AppendLine("  </ItemGroup>");
            }

            sb.AppendLine();
            sb.AppendLine("</Project>");

            File.WriteAllText(
                Path.Combine(
                    DirectoryPath,
                    Name + ".csproj"),
                sb.ToString());
        }

        /// <summary>
        /// 共有プロジェクトのファイル（.shproj および .projitems）を生成します。
        /// </summary>
        private void GenerateSharedProject()
        {
            string projectItemsPath = Path.Combine(DirectoryPath, Name + ".projitems");

            string sharedProjectPath = Path.Combine(
                DirectoryPath,
                Name + ".shproj");

            // .projitems
            var items = new StringBuilder();

            items.AppendLine("""<?xml version="1.0" encoding="utf-8"?>""");
            items.AppendLine("""<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">""");
            items.AppendLine();

            items.AppendLine("<PropertyGroup>");
            items.AppendLine("<MSBuildAllProjects Condition=\"'$(MSBuildVersion)' == '' Or '$(MSBuildVersion)' &lt; '16.0'\">$(MSBuildAllProjects);$(MSBuildThisFileFullPath)</MSBuildAllProjects>");
            items.AppendLine("<HasSharedItems>true</HasSharedItems>");
            items.AppendLine("<SharedGUID>5e295dfa-21c3-487e-8e3e-b2957e1d354a</SharedGUID>");
            items.AppendLine("</PropertyGroup>");
            items.AppendLine("<PropertyGroup Label=\"Configuration\">");
            items.AppendLine($"<Import_RootNamespace>{Name}</Import_RootNamespace>");
            items.AppendLine("</PropertyGroup>");

            items.AppendLine("  <ItemGroup>");

            foreach (var classFile in this._classes)
            {
                items.AppendLine(
                    $"  <Compile Include=\"$(MSBuildThisFileDirectory){classFile.Name}.cs\" />");
            }
            items.AppendLine("  </ItemGroup>");
            items.AppendLine();
            items.AppendLine("</Project>");

            File.WriteAllText(
                projectItemsPath,
                items.ToString());

            // .shproj
            var project = new StringBuilder();

            project.AppendLine("""<?xml version="1.0" encoding="utf-8"?>""");
            project.AppendLine("""<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">""");
            project.AppendLine();

            project.AppendLine("<PropertyGroup Label=\"Globals\">");
            project.AppendLine("<ProjectGuid>5e295dfa-21c3-487e-8e3e-b2957e1d354a</ProjectGuid>");
            project.AppendLine("<MinimumVisualStudioVersion>14.0</MinimumVisualStudioVersion>");
            project.AppendLine("</PropertyGroup>");
            project.AppendLine("<Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />");
            project.AppendLine("<Import Project=\"$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v$(VisualStudioVersion)\\CodeSharing\\Microsoft.CodeSharing.Common.Default.props\" />");
            project.AppendLine("<Import Project=\"$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v$(VisualStudioVersion)\\CodeSharing\\Microsoft.CodeSharing.Common.props\" />");
            project.AppendLine("<PropertyGroup />");
            project.AppendLine(
                $"  <Import Project=\"{Name}.projitems\" Label=\"Shared\" />");
            project.AppendLine("<Import Project=\"$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v$(VisualStudioVersion)\\CodeSharing\\Microsoft.CodeSharing.CSharp.targets\" />");
            project.AppendLine();
            project.AppendLine("</Project>");

            File.WriteAllText(
                sharedProjectPath,
                project.ToString());
        }

        private void GenerateProjectReference(StringBuilder sb)
        {
            var count = 0;
            foreach (var reference in this._references)
            {
                if (reference.ProjectType != ProjectType.Shared)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("  <ItemGroup>");

                foreach (var reference in this._references)
                {
                    string relativePath = Path.Combine(
                        "..",
                        reference.Name,
                        reference.Name);
                    if (reference.ProjectType != ProjectType.Shared)
                    {
                        relativePath += ".csproj";
                        sb.AppendLine(
                            $"    <ProjectReference Include=\"{relativePath}\" />");
                    }
                }

                sb.AppendLine("  </ItemGroup>");
            }
            foreach (var reference in this._references)
            {
                string relativePath = Path.Combine(
                    "..",
                    reference.Name,
                    reference.Name);
                if (reference.ProjectType == ProjectType.Shared)
                {
                    relativePath += ".projitems";
                    sb.AppendLine($"<Import Project=\"{relativePath}\" Label=\"Shared\"/>");
                }
            }
        }


    }

}
