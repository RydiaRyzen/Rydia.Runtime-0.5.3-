using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{
    public sealed class ProjectFile
    {
        private readonly List<ClassFile> _classes = new();
        private readonly List<ProjectFile> _references = new();
        private readonly ProjectType _projectType;

        public ProjectType ProjectType
        {
            get
            {
                return this._projectType;
            }
        }

        public string Name { get; }

        public string DirectoryPath { get; internal set; }

        public IReadOnlyList<ClassFile> Classes
        {
            get
            {
                return this._classes;
            }
        }

        public IReadOnlyList<ProjectFile> References
        {
            get
            {
                return this._references;
            }
        }

        public ProjectFile(string name, ProjectType projectType = ProjectType.ClassLibrary)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
            this._projectType = projectType;
        }

        public void AddClass(ClassFile classFile)
        {
            ArgumentNullException.ThrowIfNull(classFile);

            this._classes.Add(classFile);
        }

        public void AddReference(ProjectFile project)
        {
            ArgumentNullException.ThrowIfNull(project);

            if (!this._references.Contains(project))
            {
                this._references.Add(project);
            }
        }

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
            }
        }

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
                sb.AppendLine();
                sb.AppendLine("  <ItemGroup>");

                foreach (var reference in this._references)
                {
                    string relativePath = Path.Combine(
                        "..",
                        reference.Name,
                        reference.Name + ".csproj");

                    sb.AppendLine(
                        $"    <ProjectReference Include=\"{relativePath}\" />");
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
    }

}
