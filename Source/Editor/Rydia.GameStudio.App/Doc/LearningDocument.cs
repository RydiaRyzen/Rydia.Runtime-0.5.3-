using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking.Skins;

namespace Rydia.Doc
{

    /// <summary>
    /// Rydia Engine の学習用ドキュメントを管理するクラスです。
    /// 各学習項目の HTML コンテンツと、共通のスタイル定義を提供します。
    /// </summary>
    internal static class LearningDocument
    {

        /// <summary>
        /// 学習ドキュメントのカテゴリを定義します。
        /// </summary>
        public static class Category
        {
            /// <summary>
            /// すべてのカテゴリを表します。
            /// </summary>
            public const string All = "すべて";

            /// <summary>
            /// 入門カテゴリを表します。
            /// </summary>
            public const string Intro = "入門";

            /// <summary>
            /// グラフィックスカテゴリを表します。
            /// </summary>
            public const string Graphics = "グラフィックス";

            /// <summary>
            /// プログラミングカテゴリを表します。
            /// </summary>
            public const string Programming = "プログラミング";

            /// <summary>
            /// リソースカテゴリを表します。
            /// </summary>
            public const string Resource = "リソース";

        }

        /// <summary>
        /// アプリケーションの実行ディレクトリを取得します。
        /// </summary>
        private static string basePath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 実行ディレクトリを URI 形式に変換した文字列を取得します。
        /// HTML 内で画像などのリソースを参照する際の基準として使用します。
        /// </summary>
        private static string baseUrl = new Uri(basePath).AbsoluteUri;

        /// <summary>
        /// 学習ドキュメントで共通して使用する HTML ヘッダーと CSS を取得します。
        /// フォント、文字色、背景色、見出し、リスト、コードブロックなどの
        /// 表示スタイルを定義します。
        /// </summary>
        public static string Style = """
                    <!DOCTYPE html>
                <html lang="ja">
                <head>
                    <meta charset="utf-8">
                    <style>
                        body {
                            font-family: "Yu Gothic UI", "Meiryo", sans-serif;
                            font-size: 14px;
                            line-height: 1.8;
                            color: #f0f0f0;
                            background-color: rgb(38, 41, 49);
                            margin: 20px;
                        }

                        h1 {
                            font-size: 24px;
                            margin-bottom: 16px;
                        }

                        h2 {
                            font-size: 18px;
                            margin-top: 24px;
                            border-bottom: 1px solid #50545e;
                            padding-bottom: 6px;
                        }

                        h3 {
                            font-size: 15px;
                            margin-top: 18px;
                            margin-bottom: 4px;
                            color: #00a0f0;
                        }

                        p {
                            margin: 8px 0;
                        }

                        ul {
                            padding-left: 24px;
                        }

                        li {
                            margin-bottom: 6px;
                        }

                        .folder {
                            margin-left: 16px;
                        }
                                        /* コードブロック */
                pre {
                    background-color: #1e1e1e;
                    border: 1px solid #3c3c3c;
                    border-radius: 8px;
                    padding: 16px;
                    margin: 16px 0;
                    overflow-x: auto;
                    white-space: pre;
                    font-family: Consolas, "Cascadia Code", "Courier New", monospace;
                    font-size: 13px;
                    line-height: 1.6;
                    color: #d4d4d4;
                }
                        /* コード部分 */
                pre code {
                    font-family: inherit;
                    font-size: inherit;
                    color: inherit;
                    background: none;
                    padding: 0;
                    white-space: inherit;
                }
                                /* インラインコード */
                p code,
                li code {
                    background-color: #30343d;
                    color: #8ed6ff;
                    border-radius: 4px;
                    padding: 2px 5px;
                    font-family: Consolas, "Cascadia Code", monospace;
                    font-size: 0.95em;
                }
                    </style>
                <base href="
                """ + baseUrl + """
                ">
                </head>
                """;

        /// <summary>
        /// 学習ドキュメントのホームページを取得します。
        /// ドキュメントの概要と、学習項目の選択方法を説明します。
        /// </summary>
        public static string Home = Style + """
            <body>
            <h1>開発者ドキュメントへようこそ！</h1>
            <p>
                このドキュメントは、Rydia Engine の開発者向けの情報を提供するためのものです。
                ここでは、エンジンの基本的な使い方を学ぶことができます。
            </p>
            <p>
                さらに詳しいAPIリファレンスや高度なトピックは左ペインの　コミュニティ > ドキュメント　からご覧いただけます
            </p>
                左側の学習項目または上のカテゴリから学習したい項目を選択してください。
            </body>
            </html>
            """;

        /// <summary>
        /// Rydia Engine の基本構成に関する入門ドキュメントを取得します。
        /// ランチャーとエディターの要件、各コンポーネントの役割、
        /// プロジェクトのフォルダー構成について説明します。
        /// </summary>
        public static string Introduction = Style + """
            <body>
                <h1>学習内容</h1>
                <p>
                    Rydia Engine の基本構成や、GameStudio の役割を学びます。
                </p>
                <h3>ランチャー要件</h3>
                <p>
                    Rydia Engine Launcherを起動するには、
                    .NET 9.0 + OpenGL ES 2.0 または 3.0 が必要です。

                    グラフィクスバックエンドは今後追加する予定です
                </p>

                <h3>エディター要件</h3>
                <p>
                    エディターは現在 Windows 専用ですが、
                    macOS、Linux での開発も視野に入れています。
                </p>
                <p>
                    ランチャーと同じく、
                    .NET 9.0 + OpenGL ES 2.0 または 3.0 が必要です。
                </p>

                <h3>エンジンとエディターの役割</h3>
                <p>
                    Rydia Engine は、エディター・ランチャーの
                    どちらでも必要となるクラスを用意しています。
                </p>
                <p>
                    Rydia GameStudio は、Rydia Engine Launcher で使用する
                    ゲームオブジェクトを管理・作成するためのツールです。
                </p>

                <h3>プロジェクトの構成</h3>
                <ul>
                    <li>
                        <strong>Assets フォルダー</strong>
                        <p class="folder">
                            Rydia Engine Launcher で使用する
                            すべてのゲームオブジェクトが格納されているフォルダーです。
                        </p>
                    </li>
                    <li>
                        <strong>Source フォルダー</strong>
                        <p class="folder">
                            Media フォルダーと Code フォルダーが格納されています。
                        </p>
                    </li>
                    <li>
                        <strong>Media フォルダー</strong>
                        <p class="folder">
                            Rydia GameStudio でインポートした音楽ファイル、
                            画像、フォントなどが格納されている場所です。
                        </p>
                    </li>
                    <li>
                        <strong>Code フォルダー</strong>
                        <p class="folder">
                            ソリューションファイルが配置されています。
                            独自の Component を作成するには、
                            C# コードを書く必要があります。
                        </p>
                    </li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// 開発環境の準備に関するドキュメントを取得します。
        /// 必要な開発ツール、プロジェクトの作成、ビルドと実行について説明します。
        /// </summary>
        public static string Setup = Style + """
            <body>
                <h1>開発環境の準備</h1>
                <p>
                    C# による開発環境とプロジェクトの準備について学びます。
                </p>
                <h2>学習内容</h2>
                <ul>
                    <li>必要な開発ツール</li>
                    <li>プロジェクトの作成</li>
                    <li>ビルドと実行</li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// グラフィックスの描画処理に関する基礎ドキュメントを取得します。
        /// レンダリング、頂点バッファ、インデックスバッファ、
        /// 描画コマンドの基本的な流れについて説明します。
        /// </summary>
        public static string RenderingBasics = Style + """
            <body>
                <h1>描画処理の基礎</h1>
                <p>ゲーム画面の描画に必要な基本概念を学びます。</p>
                <h2>学習内容</h2>
                <ul>
                    <li>レンダリングの基本</li>
                    <li>頂点バッファとインデックスバッファ</li>
                    <li>描画コマンドの流れ</li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// シェーダーに関するドキュメントを取得します。
        /// 頂点シェーダー、フラグメントシェーダー、
        /// ユニフォーム変数の基本について説明します。
        /// </summary>
        public static string Shaders = Style + """
            <body>
                <h1>シェーダー</h1>
                <p>GPU 上で実行されるシェーダーの基本を学びます。</p>
                <h2>学習内容</h2>
                <ul>
                    <li>頂点シェーダー</li>
                    <li>フラグメントシェーダー</li>
                    <li>ユニフォーム変数</li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// ゲームループに関するドキュメントを取得します。
        /// 更新処理と描画処理、経過時間の管理、
        /// フレームレートの基本について説明します。
        /// </summary>
        public static string GameLoop = Style + """
            <body>
                <h1>ゲームループ</h1>
                <p>ゲームの更新処理と描画処理の基本を学びます。</p>
                <h2>学習内容</h2>
                <ul>
                    <li>Update と Render</li>
                    <li>経過時間の管理</li>
                    <li>フレームレート</li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// 入力処理に関するドキュメントを取得します。
        /// キーボード、マウス、タッチイベントの基本的な扱い方を説明します。
        /// </summary>
        public static string InputHandling = Style + """
            <body>
                <h1>入力処理</h1>
                <p>キーボード、マウス、タッチ入力の扱い方を学びます。</p>
                <h2>学習内容</h2>
                <ul>
                    <li>キー入力の取得</li>
                    <li>マウス操作</li>
                    <li>タッチイベント</li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// テクスチャとフォントに関するドキュメントを取得します。
        /// 画像リソースの読み込み、テクスチャの利用、
        /// 文字の描画について説明します。
        /// </summary>
        public static string TexturesAndFonts = Style + """
            <body>
                <h1>テクスチャとフォント</h1>
                <p>画像やフォントなどのリソースの基本を学びます。</p>
                <h2>学習内容</h2>
                <ul>
                    <li>画像リソースの読み込み</li>
                    <li>テクスチャの利用</li>
                    <li>文字の描画</li>
                </ul>
            </body>
            </html>
            """;

        /// <summary>
        /// [要再編集]　音声処理に関するドキュメントを取得します。
        /// 現在は Duality の Audio Handling に関する記事を
        /// テスト用コンテンツとして表示します。
        /// </summary>
        public static string Audio = Style + $"""
            <body>
            <h1>テスト用でDualityのAudio Handlingの記事を表示しています</h1>
                <hr>
            <h2>title: 'Audio Handling'<br>
            category: 'basics'<br>
            displayOrder: 30<br>
            version: 'v3'</h2>
            <p>Visuals are just half of what makes a game. Audio is the other half that completes the experience. Audio gives feedback to the player, adds to the immersion of the game and helps ground visuals. In this article we will take a look at how audio handling works in Duality.</p>
            <h1>Audio terminology</h1>
            <p>Before we dive in let's take a step back and go through some of the terminology that is commonly used in game audio and game development in general. It's important for us to have a decent understanding of these concepts before moving onto other topics in the article.</p>
            <h2>Sound Listener</h2>
            <p>A Sound Listener listens for oncoming sounds and acts as the &quot;ears&quot; of the player. It is what makes 3D audio in games possible. Duality provides a SoundListener component which we can attach to a game object, typically the Camera to act as the reference point for 3D audio.</p>
            <h2>Sound Emitters</h2>
            <p>A Sound Emitter is a component that enables a game object to emit sound. These sounds will then be picked up by our Sound Listener and the respective 3D audio settings will be applied. That is, sounds emitting from the left side of the listener will be panned to the left speaker and sounds emitting from the right side will be panned to the right speaker, creating the effect of 3D audio. Duality provides a SoundEmitter component which can be attached to a game object to have that object emit sound.</p>
            <h2>2D Sounds vs 3D Sounds</h2>
            <p>The distinction between 2D sounds and 3D sounds can be confusing to a lot of people at first, so let's clarify that next. A 3D sound is a sound instance that is attached to an object in the game world. For example, this could be a character in the game. When the character moves forward, a footstep sound could be emitted. This footstep sound is attached to the character and therefore it is emitted from the character's position and has a presence in the game world. A 2D sound on the other hand is a sound instance that is not attached to a game object in the world. A good example for a 2D sound would be background music. Background music essentially plays out of nowhere (ie. in the background).</p>
            <h1>Setting up the Sound Listener</h1>
            <p>Before playing sounds we must first correctly set up the Sound Listener in our scene. A Sound Listener can be attached to any game object and that game object will then act as the reference point for 3D audio. Typically it is attached to the Camera.</p>
            <p>Right click on your camera, select New, select Sound and click on the SoundListener component.</p>
            <p><img src="img/AudioHandling/settingUpTheSoundListener.png" alt=""></p>
            <p>The SoundListener will now be attached to our camera and act as the reference point for 3D audio!</p>
            <h1>Playing sounds</h1>
            <p>Now that we have the listener set up it's time to play some sounds!</p>
            <h2>Playing a simple sound</h2>
            <p>Let's now play a simple sound using Duality's SoundEmitter component. If you read through the previous topics you should now have a good understanding of how the SoundEmitter relates to the SoundListener.</p>
            <p>Drag any sound file to your project view. The sound file has to be in .ogg format. For 3D audio to kick in it must also be a mono sound file. Upon dragging in the sound file the Duality editor will create a new AudioData resource file for us. In this case my sound file was called &quot;pulsefire&quot;.</p>
            <p><img src="img/AudioHandling/audioDataResource.PNG" alt=""></p>
            <p>Create a new game object (or use an existing game object) that wishes to emit sound. Add a new SoundEmitter component to the object.</p>
            <p><img src="img/AudioHandling/addingASoundEmitter.png" alt=""></p>
            <p>Drag the AudioData resource file into the SoundEmitter component in the object inspector. Your SoundEmitter component will look like this (in this case &quot;pulsefire&quot; is the filename of the sound I used. It will be different in your case.).</p>
            <p><img src="img/AudioHandling/SoundEmitterAfterAddingSound.PNG" alt=""></p>
            <p>Also, if you take a look at the project view you will notice that the Duality editor has created a Sound resource out of the AudioData resource we provided.</p>
            <p><img src="img/AudioHandling/soundResource.PNG" alt=""></p>
            <p>Now, when you run your game the sound should start playing and when you move your game object around the listener it should pan accordingly between the left and right speakers. Once again, your sound file must be in mono for 3D audio to work!</p>
            <h2>Playing ambience sounds</h2>
            <p>Ambience sounds such as wind or rain usually must not be attached to a single game object in the world because it is meant to be a 2D sound (ie. plays in the background). You could always try to simulate it through point emitters but that would be way too overkill for most games. So in this case, we can simply attach a SoundEmitter component directly to the camera (to which the SoundListener is attached to). As both the emitter and the listener have the same position at any given time the sound will effectively be treated as a 2D sound with no presence in the game world.</p>
            <h2>Playing music</h2>
            <p>So far we have only worked with the editor to play sounds so let's jump into some code. A good demonstration of this would be playing background music which typically requires some manual work by the programmer such as fading in/out music in a way that is reactive to the game state and applying various effects.</p>
            <p>Here's how you'd play some background music:</p>
            <pre><code class="language-csharp">// Declare fields in some Component class
            private ContentRef&lt;Sound&gt; sound = null;
            private SoundInstance instance = null;
            </code></pre>
            <pre><code class="language-csharp">private void StartAudio()
            """ + """
            {
            	// Grab a reference to the Sound resource (our music loop)
            	this.sound = ContentProvider.RequestContent&lt;Sound&gt;(@&quot;Data\MusicLoop.Sound.Res&quot;);

            	// Start playing
            	this.instance = DualityApp.Sound.PlaySound(this.sound);

            	// Set the Looped property to true as we want the music to loop
            	this.instance.Looped = true;
            }
            </code></pre>
            <h3>Fading in and out</h3>
            <p>Starting and stopping music immediately with no fading can be quite abrupt and often distract the player which ultimately leads to a poor overall player experience. To help with this we can smooth out transitions with a simple fade out or a fade in. Duality provides helper methods that allows you to do this quite effortlessly.</p>
            <pre><code class="language-csharp">// start the ambience music
            // ambience music plays regardless of which state the player is in
            this.ambienceLoop.BeginFadeIn(5.0f);
            </code></pre>
            <pre><code class="language-csharp">// The player is in combat: Fade out the Exploration music and fade in the Combat music
            if (Player.IsInCombat())
            {
            	this.ambienceLoop.FadeOut(5.0f);
            	this.combatLoop.FadeIn(5.0f);
            }
            // The player is not in combat: Fade out the Combat music and fade in the Exploration music
            else
            {
            	// The player is not in combat: Fade out the Combat music and fade in the Exploration music
            	this.combatLoop.FadeOut(5.0f);
            	this.ambienceLoop.FadeIn(5.0f);
            }
            </code></pre>
            <p>In this example <code>ambienceLoop</code> and <code>combatLoop</code> are SoundInstance objects and we call the <code>BeginFadeIn</code>, <code>FadeIn</code> and <code>FadeOut</code> helper methods with a fade time of 5 seconds.</p>
            <h1>Applying audio effects</h1>
            <p>Effects are another way to make your game's audio feel more dynamic and reactive. Duality offers some standard audio effects straight out of the box. Additional effects can also be accessed through the audio backend.</p>
            <h2>Low pass filter</h2>
            <p>You can think of a low pass filter as essentially &quot;audio blur&quot;. It can be used in many ways to enhance the player experience (eg. drown out the audio to signal to the player that they are low on health).</p>
            <pre><code class="language-csharp">// Change the low pass factor. Lower values cut off more frequencies
            this.soundInstance.Lowpass = 0.5f;
            </code></pre>
            <h2>Pitch shift</h2>
            <p>Changing the pitch can be useful for slow-mo effects!</p>
            <pre><code class="language-csharp">// Change the pitch factor
            this.soundInstance.Pitch = 1.0f;
            </code></pre>
            
            </body>
            </html>
            """;

        /// <summary>
        /// 音声ドキュメントで使用する画像の表示を検証するための
        /// テスト用 HTML コンテンツを取得します。
        /// 実行ディレクトリと画像の参照先を表示します。
        /// </summary>
        public static string Audio2 = Style + $"""
    <body>
        <p>{basePath}</p>
        <img src="{new Uri(basePath).AbsoluteUri}/img/AudioHandling/settingUpTheSoundListener.png" alt="テスト画像">
    </body>
    </html>
    """;

        /// <summary>
        /// 音声ドキュメントで使用する画像の表示を検証するための
        /// テスト用 HTML コンテンツを取得します。
        /// 実行ディレクトリと画像の参照先を表示します。
        /// </summary>
        public static string Audio3 = Style + $"""
    <body>
        <p>{basePath}</p>
        <img src="{new Uri(basePath).AbsoluteUri}/img/AudioHandling/settingUpTheSoundListener.png" alt="テスト画像">
    </body>
    </html>
    """;

    }


}
