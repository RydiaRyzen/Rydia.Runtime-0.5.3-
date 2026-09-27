using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rydia.Resources;
using Rydia.Serialization;
using SampleEngine.Resources;

namespace Rydia.ResourceConverter
{
    public partial class TTFFontConverter : Form
    {


        // ユーザーが選択した最終的なFontを保持するプロパティ
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font SelectedFont { get; private set; }

        public TTFFontConverter()
        {
            InitializeComponent();
            LoadFontFamilies();
            this.listBox1.SelectedIndexChanged += UpdatePreview;
            this.nudFontSize.ValueChanged += UpdatePreview;
            this.chkBold.CheckedChanged += UpdatePreview;
            this.chkItalic.CheckedChanged += UpdatePreview;
            this.listBox1.DragEnter += ListBoxFiles_DragEnter;
            this.listBox1.DragDrop += ListBoxFiles_DragDrop;
            this.nudFontSize.Value = 70;
            this.listBox1.SelectedItem = "Arial";
            UpdatePreview(null, EventArgs.Empty);
        }

        private void ListBoxFiles_DragDrop(object? sender, DragEventArgs e)
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
                ".ttf"
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
            LoadFontFamilies();

            addedCount++;
        }

        private void ListBoxFiles_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // インストールされているフォント名をロードする
        private void LoadFontFamilies()
        {
            this.listBox1.Items.Clear();
            var files = Directory.GetFiles(MainForm.MediaDir, "*.ttf");
            var pfc = new PrivateFontCollection();
            foreach (var file in files)
            {
                pfc.AddFontFile(file);
            }
            foreach (FontFamily family in pfc.Families)
            {
                // 一部のシステムフォントはRegularスタイルをサポートしていない場合があるため、チェックする
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    this.listBox1.Items.Add(family.Name);
                }
            }
        }

        // 選択肢が変わるたびにプレビューとSelectedFontを更新する
        private void UpdatePreview(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null) return;

            try
            {
                string fontName = this.listBox1.SelectedItem.ToString();
                float fontSize = (float)this.nudFontSize.Value;
                FontStyle style = FontStyle.Regular;

                if (this.chkBold.Checked)
                {
                    style |= FontStyle.Bold;
                }
                if (this.chkItalic.Checked)
                {
                    style |= FontStyle.Italic;
                }

                // 新しいFontオブジェクトを作成
                Font newFont = new Font(fontName, fontSize, style);

                // プレビューテキストボックスに適用
                this.txtPreview.Font = newFont;
                this.txtPreview.Text = $"{fontName}, {fontSize}pt";

                // 外部に渡すためのプロパティに格納
                SelectedFont = newFont;
            }
            catch (Exception ex)
            {
                // フォントの組み合わせが無効な場合にエラーを無視またはログに記録
                MessageBox.Show($"フォントの作成中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SelectedFont == null)
            {
                MessageBox.Show("有効なフォントが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 描画設定を取得
            string fontName = SelectedFont.Name;
            float fontSize = SelectedFont.Size * 1f; // WinFormsのPointをImageSharpのPixelサイズに近似;
            System.Drawing.FontStyle style = SelectedFont.Style;

            var charSet = FontLoader.Load(SelectedFont);

            var fontFileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", $"{fontName}_{(int)fontSize}px_{style}.ryres");
            byte[] data = new BinarySerializer().Serialize(charSet);
            var temp = Convert.ToBase64String(data);
            File.WriteAllText(fontFileName, temp);
            MessageBox.Show($"フォントリソースを保存しました:\n{fontFileName}", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var deserialized = new BinarySerializer().Deserialize<FontCharSet>(data);
            deserialized.CreateCharSet();
            Console.WriteLine($"文字数: {deserialized.Chars.Count}");
            var str = Convert.FromBase64String(temp);
            deserialized = new BinarySerializer().Deserialize<FontCharSet>(str);
            deserialized.CreateCharSet();
            Console.WriteLine($"文字数: {deserialized.Chars.Count}");
        }
    }
}
