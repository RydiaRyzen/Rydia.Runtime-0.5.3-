using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Rydia.Resources;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixFonts = SixLabors.Fonts;

namespace SampleEngine.Resources
{
    public class FontLoader
    {

        public const string Hiragana =
            "あいうえおかきくけこさしすせそたちつてとなにぬねの" +
            "はひふへほまみむめもやゆよらりるれろわをん" +
            "がぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽっ　";
        public const string Katakana =
            "アイウエオカキクケコサシスセソタチツテトナニヌネノ" +
            "ハヒフヘホマミムメモヤユヨラリルレロワヲン" +
            "ガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポッ";
        public const string Symbol = @"? ,;.:-_<>|#'+*~@^°!""§$%&/()=`²³{[]}\´öäüÖÄÜß";
        public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string DigitChars = "1234567890";
        public const string JoyoKanji2136 =
            "亜哀愛悪握圧扱安案暗以衣位囲医依委威為畏胃尉異移萎偉椅彙意違維慰遺緯域育一壱逸茨芋引印因姻員院淫陰飲隠韻右宇羽雨渦浦運雲営影映栄永泳英衛詠鋭" +
            "液疫益駅悦謁越閲円延沿炎宴援園煙猿遠鉛塩演縁艶汚王凹央応往押旺欧殴桜翁奥横岡屋億憶臆虞乙俺卸音恩温穏下化火加可仮何花佳価果河苛科架夏家荷華菓" +
            "貨渦過嫁暇禍靴寡歌箇稼課蚊牙瓦我画芽賀雅餓介回灰会快戒改怪拐悔海界皆械絵開階塊楷解潰壊懐諧貝外劾害崖涯街慨蓋該概骸垣柿各角拡革格核殻郭覚較隔" +
            "閣確獲嚇穫学岳楽額顎掛潟括活喝渇割葛滑褐轄且株釜鎌刈干刊甘汗缶完肝官冠巻看陥乾勘患貫寒喚堪換敢棺款間閑勧寛幹感漢慣管関歓監緩憾還館環簡観艦鑑" +
            "丸含岸岩玩眼頑顔願企伎危机気岐希忌汽奇祈軌既紀記起飢鬼帰基寄規亀喜幾揮期棋貴棄毀旗器畿輝機騎技宜偽欺義疑儀戯擬犠議菊吉喫詰却客脚逆虐九久及弓" +
            "丘旧休吸朽臼求究泣急級糾宮救球給嗅窮牛去巨居拒拠挙虚許距魚御漁凶共叫狂京享供協況峡挟狭恐恭胸脅強教郷境橋矯鏡競響驚仰暁業凝曲局極玉巾斤均近金" +
            "菌勤琴筋僅禁緊錦謹襟吟銀区句苦駆具惧愚空偶遇隅屈掘靴繰桑勲君訓軍郡群兄刑形系径茎係型契計恵啓掲渓経蛍敬景軽傾携継慶憩警鶏芸迎鯨隙劇撃激桁欠穴" +
            "血決結傑潔月犬件見券肩建研県倹兼剣拳軒健険圏堅検嫌献絹遣権憲賢謙鍵繭顕験懸元幻玄言弦限原現減源厳己戸古呼固股虎孤弧故枯個庫湖雇誇鼓錮顧五互午" +
            "呉後娯悟碁語誤護口工公勾孔功巧広甲交光向后好江考行坑孝抗攻更効幸拘肯侯厚恒洪皇紅荒郊香候校耕航貢降高康控梗黄喉慌港硬絞項溝鉱構綱酵稿興衡鋼講" +
            "購乞号合拷剛傲豪克告谷刻国黒穀酷獄骨駒込頃今困昆恨根混痕紺魂墾懇左佐沙査砂唆差詐鎖座挫才再災妻采砕宰栽彩採済祭斎細菜最裁債催塞歳載際埼在材剤" +
            "財罪崎作削昨柵索策酢搾錯咲冊札刷刹拶殺察撮擦雑皿三山参桟蚕惨産傘散算酸賛残斬暫士子支止氏仕史司四市矢旨死糸至伺志私使刺始姉枝祉肢姿思指施師恣" +
            "紙脂視紫詞歯嗣試詩資飼誌雌摯賜諮示字寺次耳自似児事侍治持時滋慈辞磁餌璽式識軸七叱失室疾執湿漆質実芝写社車舎者射捨赦斜煮遮謝邪蛇尺借酌釈爵若弱" +
            "寂手主守朱取狩首殊珠酒腫種趣寿受授需儒樹収州舟秀周宗拾秋臭修袖終羞習週就衆集愁酬醜蹴襲十汁充住柔重従渋銃獣縦叔祝宿淑粛縮塾熟出述術俊春瞬旬巡" +
            "盾准殉純循順準潤遵処初所書庶暑署緒諸女如助序叙徐除小升少召匠床抄肖尚招承昇松沼昭宵将消症祥称笑唱商渉章紹訟勝掌晶焼焦硝粧詔証象傷奨照詳彰障衝" +
            "賞償礁鐘上丈冗条状乗城浄剰常情場畳蒸縄壌嬢錠譲醸色拭食植殖飾触嘱織職辱尻心申伸臣芯身辛侵信津神唇娠振浸真針深紳進森診寝慎新審震薪親人刃仁尽迅" +
            "甚陣尋腎須図水吹垂炊帥粋衰推酔遂睡穂随髄枢崇数量杉裾寸瀬是井世正生成西声制姓征性青斉政星牲省清盛婿晴勢聖誠精製誓静請整醒税夕斥石赤昔析席脊隻" +
            "惜戚責跡積績籍切折拙窃接設雪摂節説舌絶千川仙占先宣専泉浅洗染扇栓旋船戦煎羨腺詮践箋銭潜線遷選薦繊鮮全前善然禅漸膳繕狙阻祖租素措粗組疎訴塑遡礎" +
            "双壮早争走奏相荘草送倉捜挿桑巣掃曹曽爽窓創喪痩葬装僧想層総遭槽踪操燥霜騒藻造像増憎蔵贈臓即束足促則息捉速側測俗族属賊続卒率存村孫尊損他多打妥" +
            "唾堕惰駄太対体耐待怠胎退帯泰袋逮替貸隊滞態戴大代台第題滝卓宅択拓沢濯託濁諾但達奪脱棚谷丹担単炭胆探淡短嘆端誕鍛団男段断弾暖談壇地池知値恥致遅" +
            "痴稚置緻竹畜逐蓄築秩窒茶着嫡中仲虫沖宙忠抽注昼柱衷酎鋳駐著貯丁弔庁兆町長挑帳張彫眺釣頂鳥朝貼超腸跳徴嘲潮澄調聴懲直勅捗沈珍朕陳賃鎮追椎墜通痛" +
            "塚漬坪爪鶴低呈廷弟定底抵邸亭貞帝訂庭逓停偵堤提程艇締諦泥的笛摘滴適敵溺迭哲鉄徹撤天典店点展添転田伝殿電斗吐妬徒途都渡塗賭土奴努度怒刀冬灯当投" +
            "豆東到逃倒凍唐島桃討透党悼盗陶塔搭棟湯痘登答等筒統稲踏糖頭謄藤闘騰同洞胴動堂童道働銅導瞳峠匿特得督徳篤毒独読突届屯豚頓貪鈍曇丼那奈内梨謎鍋南" +
            "軟難二尼弐匂肉虹日入乳尿任妊忍認寧熱年念燃粘悩納能脳農濃把波派破覇馬婆罵拝杯背肺俳配排敗廃輩売倍梅培陪媒買賠白伯拍泊迫剝舶博薄麦漠縛爆箱箸畑" +
            "肌八鉢発髪伐抜罰閥反半氾犯帆汎伴判坂板版班畔般販斑飯搬煩頒範繁藩晩番蛮盤比皮妃否批彼披肥非卑飛疲秘被悲扉費碑罷避尾眉美備微鼻膝肘匹必泌筆姫百" +
            "氷表俵票評漂標苗秒病描猫品浜貧賓頻敏瓶不夫父付布扶府怖阜附訃負赴浮婦符富普腐敷膚賦譜侮武部舞封風伏服副幅復福腹複覆払沸仏物粉紛雰噴墳憤奮分文" +
            "聞丙平兵併並柄陛閉塀幣弊蔽餅米壁璧別蔑片辺返変偏遍編弁便勉歩保捕補舗母募墓慕暮簿方包芳邦奉宝抱放法泡胞俸倣峰砲崩訪報豊飽褒縫亡乏忙坊妨忘防房" +
            "肪某冒剖紡望傍帽棒貿暴膨謀北木朴牧没本奔翻凡盆麻摩磨魔毎妹枚昧埋幕膜枕又末抹万満慢漫未味魅密蜜脈妙民眠務夢無矛霧婿娘名命明迷冥盟銘鳴滅免面綿" +
            "麺茂模毛妄盲耗猛網目黙門紋問冶夜野弥厄役約訳躍柳愉油癒諭輸唯優勇友有由誘遊郵雄融夕予余与誉預幼用羊妖洋要容庸揚揺葉陽溶腰様瘍踊窯養擁謡曜抑浴" +
            "欲翌翼拉裸羅来雷頼絡落酪辣乱卵覧濫藍欄吏利里理痢裏履璃離陸立律慄略柳流留竜粒隆硫侶旅虜慮了両良料涼猟陵量僚領寮療瞭糧力緑林厘倫輪隣臨瑠涙累塁" +
            "類令礼冷励戻例鈴零霊隷齢麗暦歴列劣烈裂恋連廉練錬呂炉賂路露老労弄郎朗浪廊楼漏牢六録論和話賄脇惑枠湾腕";
        public const string AllCharSet = Hiragana + Katakana + Symbol + Lowercase + Uppercase + DigitChars + JoyoKanji2136;

        public static FontCharSet Load(System.Drawing.Font targetFont)
        {
            if (targetFont == null)
            {
                MessageBox.Show("有効なフォントが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            // 描画設定を取得
            string fontName = targetFont.Name;
            float fontSize = targetFont.Size * 1.333333f; // WinFormsのPointをImageSharpのPixelサイズに近似;
            System.Drawing.FontStyle style = targetFont.Style;

            string fontPath = GetFontFilePath(targetFont.FontFamily.Name);
            if (fontPath == null || !File.Exists(fontPath))
            {
                MessageBox.Show("フォントファイルを取得できません。");
                return null;
            }

            // ImageSharpのFontオブジェクトをロード
            SixFonts.FontCollection fontCollection = new SixFonts.FontCollection();
            SixLabors.Fonts.FontFamily family = fontCollection.Add(fontPath);
            SixLabors.Fonts.FontStyle slStyle = ConvertWinFormsStyle(style);
            SixLabors.Fonts.Font slFont = family.CreateFont(fontSize, slStyle);
            var charSet = new FontCharSet();
            foreach (var item in FontLoader.AllCharSet)
            {
                // 文字列サイズの測定
                var options = new SixFonts.TextOptions(slFont);
                var textSize = SixFonts.TextMeasurer.MeasureBounds(item.ToString(), options);
                int width = (int)Math.Ceiling(textSize.Width) + 1;
                int height = (int)Math.Ceiling(textSize.Height) + 10;

                // サイズがゼロだとテクスチャ作成に失敗するので対処
                if (width <= 0) width = 1;
                if (height <= 0) height = 1;

                // ImageSharpで文字列を描画
                var img = new SixLabors.ImageSharp.Image<Rgba32>(width + 10, height + 5);
                img.Mutate(ctx =>
                {
                    ctx.Clear(SixLabors.ImageSharp.Color.Transparent);
                    ctx.DrawText(item.ToString(), slFont, SixLabors.ImageSharp.Color.White, new SixLabors.ImageSharp.PointF(5, -3));
                });

                // BGRA → RGBA byte[]
                var pixels = new byte[img.Width * img.Height * 4];
                img.CopyPixelDataTo(pixels);

                PixelData pixelData = new PixelData();
                pixelData.Width = img.Width;
                pixelData.Height = img.Height;
                pixelData.Data = pixels;

                charSet.Add(item, pixelData);

                //img.Save(item.ToString() + ".png");
                img.Dispose();
            }
            charSet.CreateCharEntries();
            return charSet;
        }

        private static string GetFontFilePath(string fontName)
        {
            using var key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");

            foreach (var valueName in key.GetValueNames())
            {
                if (valueName.StartsWith(fontName, StringComparison.OrdinalIgnoreCase))
                {
                    var fileName = key.GetValue(valueName)?.ToString();
                    return System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                        fileName);
                }
            }
            return null;
        }


        /// <summary>
        /// System.Drawing.FontStyleをSixLabors.Fonts.FontStyleに変換するヘルパーメソッド
        /// </summary>
        private static SixLabors.Fonts.FontStyle ConvertWinFormsStyle(System.Drawing.FontStyle winFormsStyle)
        {
            SixLabors.Fonts.FontStyle slStyle = SixLabors.Fonts.FontStyle.Regular;
            if (winFormsStyle.HasFlag(System.Drawing.FontStyle.Bold))
            {
                slStyle |= SixLabors.Fonts.FontStyle.Bold;
            }
            if (winFormsStyle.HasFlag(System.Drawing.FontStyle.Italic))
            {
                slStyle |= SixLabors.Fonts.FontStyle.Italic;
            }
            // Underline, StrikeoutはImageSharpでは通常、個別に制御が必要だが、ここではシンプルにBold/Italicのみ考慮
            return slStyle;
        }

    }
}
