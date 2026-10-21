using System.Diagnostics;

namespace Rydia.SolutionBuilder.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // テスト中のため、ディレクトリを削除
            string solutionDirectory = Path.Combine(Environment.CurrentDirectory, "Source", "Code", "TestSolution");

            if (Directory.Exists(solutionDirectory))
            {
                Directory.Delete(
                    solutionDirectory,
                    true);
            }

            // ソリューションを生成
            var solutionFile = new SolutionFile("TestSolution");

            // ソリューションのパスを設定
            solutionFile.Path = Path.Combine(Environment.CurrentDirectory, "Source", "Code", solutionFile.Name);

            // プロジェクトAを生成
            var projectA = new ProjectFile("ProjectA", ProjectType.ClassLibrary);
            {
                // クラスAを生成
                var classA = new ClassFile("ClassA");
                // System.Numerics名前空間を追加
                classA.AddUsing("System.Numerics");

                // クラスAにプロパティとメソッドを追加
                classA.AddProperty("Name", "string");
                classA.AddProperty("Age", "int");
                classA.AddProperty("Enabled", "bool");
                classA.AddProperty("Position", "Vector3");
                classA.AddMethod("Initialize", "");
                // classA.AddMethod("GetName", "string");

                // プロジェクトAにクラスAを追加
                projectA.AddClass(classA);

                var classB = new ClassFile("ClassB");
                projectA.AddClass(classB);

                var classC = new ClassFile("ClassC");
                projectA.AddClass(classC);

                solutionFile.AddProject(projectA);
            }

            var projectB = new ProjectFile("ProjectB");
            {
                var classD = new ClassFile("ClassD");
                classD.AddProperty("Name", "string");
                classD.AddMethod("Initialize", "");

                projectB.AddClass(classD);

                var classE = new ClassFile("ClassE");
                projectB.AddClass(classE);

                var classF = new ClassFile("ClassF");
                projectB.AddClass(classF);

                solutionFile.AddProject(projectB);
            }

            var projectC = new ProjectFile("ProjectC");
            {
                projectC.AddReference(projectA);
                projectC.AddReference(projectB);

                solutionFile.AddProject(projectC);
            }

            // ソリューションを生成
            solutionFile.Generate();
            // ビルド
            //solutionFile.Build();

            // ソリューションを開く
            solutionFile.Open();
        }
    }
}