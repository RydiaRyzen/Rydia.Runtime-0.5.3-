using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rydia.ResourceConverter
{
    public partial class ResourceConverter : Form
    {
        public ResourceConverter()
        {
            InitializeComponent();
            LoadMediaFiles();
        }

        private void LoadMediaFiles()
        {
            this.listBoxFiles.Items.Clear();

            if (!Directory.Exists(MainForm.MediaDir))
                return;

            var files = Directory.GetFiles(MainForm.MediaDir);

            foreach (var file in files)
            {
                this.listBoxFiles.Items.Add(System.IO.Path.GetFileName(file));
            }

            //this.toolStripStatusLabel1.Text = $"{files.Length} 件のファイルを読み込みました。";
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            // Media 内のファイルをすべて読み込む
            var mediaFiles = Directory.GetFiles(MainForm.MediaDir);

            if (mediaFiles.Length == 0)
            {
                MessageBox.Show("Media フォルダにファイルがありません。");
                return;
            }
            /*
            // Assets 内の全ファイルを削除
            foreach (var file in Directory.GetFiles(this.assetsDir))
            {
                try
                {
                    Application.DoEvents();
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Assets ファイル削除エラー: {ex.Message}");
                    return;
                }
            }
            */
            this.btnConvert.Enabled = false;
            this.progressBar.Visible = true;
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = mediaFiles.Length;
            this.progressBar.Value = 0;

            //this.toolStripStatusLabel1.Text = $"準備中です";
            var manifest = new List<MediaEntry>();
            //var serializer = new BinarySerializer();
            MediaEntry entry;

            for (int i = 0; i < mediaFiles.Length; i++)
            {
                Application.DoEvents();
                string path = mediaFiles[i];

                try
                {
                    FileInfo fi = new FileInfo(path);
                    string md5 = await ComputeMD5Async(path);
                    entry = new MediaEntry()
                    {
                        FileName = fi.Name,
                        RelativePath =
                        System.IO.Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, path),
                        SizeBytes = fi.Length,
                        CreatedUtc = fi.CreationTimeUtc,
                        LastWriteUtc = fi.LastWriteTimeUtc,
                        MD5 = md5,
                        FileAllBytes = File.ReadAllBytes(path)
                    };
                    manifest.Add(entry);
                    //this.toolStripStatusLabel1.Text = $"{manifest.Count} 件目 : 変換準備";
                    this.progressBar.Value = i + 1;
                    Thread.Sleep(1000);
                    entry.Serialize();
                    //this.toolStripStatusLabel1.Text = $"{manifest.Count} 件目 : {entry.FileName} を生成しました";
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エラー: {ex.Message}");
                }
                //
            }
            this.btnConvert.Enabled = true;
            this.progressBar.Value = 0;
            MessageBox.Show("シリアライズ完了！");
        }

        private static System.Threading.Tasks.Task<string> ComputeMD5Async(string filePath)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                using var md5 = MD5.Create();
                using var stream = File.OpenRead(filePath);
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            });
        }

        private void ListBoxFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ListBoxFiles_DragDrop(object sender, DragEventArgs e)
        {
            var dropped = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dropped == null || dropped.Length == 0) return;

            int added = 0;

            foreach (var path in dropped)
            {
                if (Directory.Exists(path))
                {
                    foreach (var f in Directory.GetFiles(path))
                        CopyToMediaAndAdd(f, ref added);
                }
                else if (File.Exists(path))
                {
                    CopyToMediaAndAdd(path, ref added);
                }
            }

            //this.toolStripStatusLabel1.Text = $"{added} 件コピーしました。";
        }

        private void CopyToMediaAndAdd(string sourcePath, ref int addedCount)
        {
            string ext = System.IO.Path.GetExtension(sourcePath).ToLower();
            // ★ JPEG, PNG, BMP, TGA, WAV, OGG, MP3 以外のファイルはコピーしない
            // 許可される拡張子一覧
            string[] allowed =
            {
                ".jpg", ".jpeg", ".png", ".bmp", ".tga",
                ".wav", ".ogg", ".mp3"
            };

            // ★ 許可された拡張子でなければ拒否
            if (!allowed.Contains(ext))
            {
                MessageBox.Show(
                    $"このファイル形式はコピーできません。\n\n" +
                    $"対象: {System.IO.Path.GetFileName(sourcePath)}",
                    "コピー不可",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            string fileName = System.IO.Path.GetFileName(sourcePath);
            string destPath = System.IO.Path.Combine(MainForm.MediaDir, fileName);
            // ★ 名前衝突があればコピーせず中断
            if (File.Exists(destPath))
            {
                MessageBox.Show(
                    $"同名ファイルが既に存在するためコピーを中断しました。\n\n" +
                    $"指定されたファイル : {fileName}",
                    "コピー中断",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            // 通常コピー
            File.Copy(sourcePath, destPath);

            // リストに追加
            this.listBoxFiles.Items.Add(System.IO.Path.GetFileName(destPath));

            addedCount++;
        }

        private async void btnConvert_Click_1(object sender, EventArgs e)
        {
            // Media 内のファイルをすべて読み込む
            var mediaFiles = Directory.GetFiles(MainForm.MediaDir);

            if (mediaFiles.Length == 0)
            {
                MessageBox.Show("Media フォルダにファイルがありません。");
                return;
            }
            /*
            // Assets 内の全ファイルを削除
            foreach (var file in Directory.GetFiles(this.assetsDir))
            {
                try
                {
                    Application.DoEvents();
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Assets ファイル削除エラー: {ex.Message}");
                    return;
                }
            }
            */
            this.btnConvert.Enabled = false;
            this.progressBar.Visible = true;
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = mediaFiles.Length;
            this.progressBar.Value = 0;

            //this.toolStripStatusLabel1.Text = $"準備中です";
            var manifest = new List<MediaEntry>();
            //var serializer = new BinarySerializer();
            MediaEntry entry;

            for (int i = 0; i < mediaFiles.Length; i++)
            {
                Application.DoEvents();
                string path = mediaFiles[i];

                try
                {
                    FileInfo fi = new FileInfo(path);
                    string md5 = await ComputeMD5Async(path);
                    entry = new MediaEntry()
                    {
                        FileName = fi.Name,
                        RelativePath =
                        System.IO.Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, path),
                        SizeBytes = fi.Length,
                        CreatedUtc = fi.CreationTimeUtc,
                        LastWriteUtc = fi.LastWriteTimeUtc,
                        MD5 = md5,
                        FileAllBytes = File.ReadAllBytes(path)
                    };
                    manifest.Add(entry);
                    //this.toolStripStatusLabel1.Text = $"{manifest.Count} 件目 : 変換準備";
                    this.progressBar.Value = i + 1;
                    Thread.Sleep(1000);
                    entry.Serialize();
                    //this.toolStripStatusLabel1.Text = $"{manifest.Count} 件目 : {entry.FileName} を生成しました";
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エラー: {ex.Message}");
                }
                //
            }
            this.btnConvert.Enabled = true;
            this.progressBar.Value = 0;
            MessageBox.Show("シリアライズ完了！");
        }

    }
}
