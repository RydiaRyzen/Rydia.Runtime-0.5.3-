using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// キーボード上の物理的なキー位置を表します。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 各列挙値は仮想キーコードではなく、キーボード上の物理的なキー位置を表します。
    /// そのため、キーに印字されている文字そのものではなく、
    /// 押されたキーの位置を識別するために使用します。
    /// </para>
    /// <para>
    /// キー名は、US配列のASCII / QWERTYキーボードを基準として命名されています。
    /// </para>
    /// </remarks>
    public enum Key
    {

        /// 不明なキーを表します。
        /// </summary>
        Unknown,

        /// <summary>
        /// 左Shiftキーを表します。
        /// </summary>
        ShiftLeft,

        /// <summary>
        /// 右Shiftキーを表します。
        /// </summary>
        ShiftRight,

        /// <summary>
        /// 左Controlキーを表します。
        /// </summary>
        ControlLeft,

        /// <summary>
        /// 右Controlキーを表します。
        /// </summary>
        ControlRight,

        /// <summary>
        /// 左Altキーを表します。
        /// </summary>
        AltLeft,

        /// <summary>
        /// 右Altキーを表します。
        /// </summary>
        AltRight,

        /// <summary>
        /// 左Windowsキーを表します。
        /// </summary>
        WinLeft,

        /// <summary>
        /// 右Windowsキーを表します。
        /// </summary>
        WinRight,

        /// <summary>
        /// Menuキーを表します。
        /// </summary>
        Menu,

        /// <summary>
        /// F1キーを表します。
        /// </summary>
        F1,

        /// <summary>
        /// F2キーを表します。
        /// </summary>
        F2,

        /// <summary>
        /// F3キーを表します。
        /// </summary>
        F3,

        /// <summary>
        /// F4キーを表します。
        /// </summary>
        F4,

        /// <summary>
        /// F5キーを表します。
        /// </summary>
        F5,

        /// <summary>
        /// F6キーを表します。
        /// </summary>
        F6,

        /// <summary>
        /// F7キーを表します。
        /// </summary>
        F7,

        /// <summary>
        /// F8キーを表します。
        /// </summary>
        F8,

        /// <summary>
        /// F9キーを表します。
        /// </summary>
        F9,

        /// <summary>
        /// F10キーを表します。
        /// </summary>
        F10,

        /// <summary>
        /// F11キーを表します。
        /// </summary>
        F11,

        /// <summary>
        /// F12キーを表します。
        /// </summary>
        F12,

        /// <summary>
        /// F13キーを表します。
        /// </summary>
        F13,

        /// <summary>
        /// F14キーを表します。
        /// </summary>
        F14,

        /// <summary>
        /// F15キーを表します。
        /// </summary>
        F15,

        /// <summary>
        /// F16キーを表します。
        /// </summary>
        F16,

        /// <summary>
        /// F17キーを表します。
        /// </summary>
        F17,

        /// <summary>
        /// F18キーを表します。
        /// </summary>
        F18,

        /// <summary>
        /// F19キーを表します。
        /// </summary>
        F19,

        /// <summary>
        /// F20キーを表します。
        /// </summary>
        F20,

        /// <summary>
        /// F21キーを表します。
        /// </summary>
        F21,

        /// <summary>
        /// F22キーを表します。
        /// </summary>
        F22,

        /// <summary>
        /// F23キーを表します。
        /// </summary>
        F23,

        /// <summary>
        /// F24キーを表します。
        /// </summary>
        F24,

        /// <summary>
        /// F25キーを表します。
        /// </summary>
        F25,

        /// <summary>
        /// F26キーを表します。
        /// </summary>
        F26,

        /// <summary>
        /// F27キーを表します。
        /// </summary>
        F27,

        /// <summary>
        /// F28キーを表します。
        /// </summary>
        F28,

        /// <summary>
        /// F29キーを表します。
        /// </summary>
        F29,

        /// <summary>
        /// F30キーを表します。
        /// </summary>
        F30,

        /// <summary>
        /// F31キーを表します。
        /// </summary>
        F31,

        /// <summary>
        /// F32キーを表します。
        /// </summary>
        F32,

        /// <summary>
        /// F33キーを表します。
        /// </summary>
        F33,

        /// <summary>
        /// F34キーを表します。
        /// </summary>
        F34,

        /// <summary>
        /// F35キーを表します。
        /// </summary>
        F35,

        /// <summary>
        /// 上矢印キーを表します。
        /// </summary>
        Up,

        /// <summary>
        /// 下矢印キーを表します。
        /// </summary>
        Down,

        /// <summary>
        /// 左矢印キーを表します。
        /// </summary>
        Left,

        /// <summary>
        /// 右矢印キーを表します。
        /// </summary>
        Right,

        /// <summary>
        /// Enterキーを表します。
        /// </summary>
        Enter,

        /// <summary>
        /// Escapeキーを表します。
        /// </summary>
        Escape,

        /// <summary>
        /// Spaceキーを表します。
        /// </summary>
        Space,

        /// <summary>
        /// Tabキーを表します。
        /// </summary>
        Tab,

        /// <summary>
        /// BackSpaceキーを表します。
        /// </summary>
        BackSpace,

        /// <summary>
        /// Insertキーを表します。
        /// </summary>
        Insert,

        /// <summary>
        /// Deleteキーを表します。
        /// </summary>
        Delete,

        /// <summary>
        /// PageUpキーを表します。
        /// </summary>
        PageUp,

        /// <summary>
        /// PageDownキーを表します。
        /// </summary>
        PageDown,

        /// <summary>
        /// Homeキーを表します。
        /// </summary>
        Home,

        /// <summary>
        /// Endキーを表します。
        /// </summary>
        End,

        /// <summary>
        /// CapsLockキーを表します。
        /// </summary>
        CapsLock,

        /// <summary>
        /// ScrollLockキーを表します。
        /// </summary>
        ScrollLock,

        /// <summary>
        /// PrintScreenキーを表します。
        /// </summary>
        PrintScreen,

        /// <summary>
        /// Pauseキーを表します。
        /// </summary>
        Pause,

        /// <summary>
        /// NumLockキーを表します。
        /// </summary>
        NumLock,

        /// <summary>
        /// Clearキーを表します。
        /// </summary>
        Clear,

        /// <summary>
        /// Sleepキーを表します。
        /// </summary>
        Sleep,

        /// <summary>
        /// テンキーの0キーを表します。
        /// </summary>
        Keypad0,

        /// <summary>
        /// テンキーの1キーを表します。
        /// </summary>
        Keypad1,

        /// <summary>
        /// テンキーの2キーを表します。
        /// </summary>
        Keypad2,

        /// <summary>
        /// テンキーの3キーを表します。
        /// </summary>
        Keypad3,

        /// <summary>
        /// テンキーの4キーを表します。
        /// </summary>
        Keypad4,

        /// <summary>
        /// テンキーの5キーを表します。
        /// </summary>
        Keypad5,

        /// <summary>
        /// テンキーの6キーを表します。
        /// </summary>
        Keypad6,

        /// <summary>
        /// テンキーの7キーを表します。
        /// </summary>
        Keypad7,

        /// <summary>
        /// テンキーの8キーを表します。
        /// </summary>
        Keypad8,

        /// <summary>
        /// テンキーの9キーを表します。
        /// </summary>
        Keypad9,

        /// <summary>
        /// テンキーの除算キーを表します。
        /// </summary>
        KeypadDivide,

        /// <summary>
        /// テンキーの乗算キーを表します。
        /// </summary>
        KeypadMultiply,

        /// <summary>
        /// テンキーの減算キーを表します。
        /// </summary>
        KeypadSubtract,

        /// <summary>
        /// テンキーの加算キーを表します。
        /// </summary>
        KeypadAdd,

        /// <summary>
        /// テンキーの小数点キーを表します。
        /// </summary>
        KeypadDecimal,

        /// <summary>
        /// テンキーのEnterキーを表します。
        /// </summary>
        KeypadEnter,

        /// <summary>
        /// Aキーを表します。
        /// </summary>
        A,

        /// <summary>
        /// Bキーを表します。
        /// </summary>
        B,

        /// <summary>
        /// Cキーを表します。
        /// </summary>
        C,

        /// <summary>
        /// Dキーを表します。
        /// </summary>
        D,

        /// <summary>
        /// Eキーを表します。
        /// </summary>
        E,

        /// <summary>
        /// Fキーを表します。
        /// </summary>
        F,

        /// <summary>
        /// Gキーを表します。
        /// </summary>
        G,

        /// <summary>
        /// Hキーを表します。
        /// </summary>
        H,

        /// <summary>
        /// Iキーを表します。
        /// </summary>
        I,

        /// <summary>
        /// Jキーを表します。
        /// </summary>
        J,

        /// <summary>
        /// Kキーを表します。
        /// </summary>
        K,

        /// <summary>
        /// Lキーを表します。
        /// </summary>
        L,

        /// <summary>
        /// Mキーを表します。
        /// </summary>
        M,

        /// <summary>
        /// Nキーを表します。
        /// </summary>
        N,

        /// <summary>
        /// Oキーを表します。
        /// </summary>
        O,

        /// <summary>
        /// Pキーを表します。
        /// </summary>
        P,

        /// <summary>
        /// Qキーを表します。
        /// </summary>
        Q,

        /// <summary>
        /// Rキーを表します。
        /// </summary>
        R,

        /// <summary>
        /// Sキーを表します。
        /// </summary>
        S,

        /// <summary>
        /// Tキーを表します。
        /// </summary>
        T,

        /// <summary>
        /// Uキーを表します。
        /// </summary>
        U,

        /// <summary>
        /// Vキーを表します。
        /// </summary>
        V,

        /// <summary>
        /// Wキーを表します。
        /// </summary>
        W,

        /// <summary>
        /// Xキーを表します。
        /// </summary>
        X,

        /// <summary>
        /// Yキーを表します。
        /// </summary>
        Y,

        /// <summary>
        /// Zキーを表します。
        /// </summary>
        Z,

        /// <summary>
        /// 数字キーの0を表します。
        /// </summary>
        Number0,

        /// <summary>
        /// 数字キーの1を表します。
        /// </summary>
        Number1,

        /// <summary>
        /// 数字キーの2を表します。
        /// </summary>
        Number2,

        /// <summary>
        /// 数字キーの3を表します。
        /// </summary>
        Number3,

        /// <summary>
        /// 数字キーの4を表します。
        /// </summary>
        Number4,

        /// <summary>
        /// 数字キーの5を表します。
        /// </summary>
        Number5,

        /// <summary>
        /// 数字キーの6を表します。
        /// </summary>
        Number6,

        /// <summary>
        /// 数字キーの7を表します。
        /// </summary>
        Number7,

        /// <summary>
        /// 数字キーの8を表します。
        /// </summary>
        Number8,

        /// <summary>
        /// 数字キーの9を表します。
        /// </summary>
        Number9,

        /// <summary>
        /// チルダキーを表します。
        /// </summary>
        Tilde,

        /// <summary>
        /// マイナスキーを表します。
        /// </summary>
        Minus,

        /// <summary>
        /// プラスキーを表します。
        /// </summary>
        Plus,

        /// <summary>
        /// 左角括弧キーを表します。
        /// </summary>
        BracketLeft,

        /// <summary>
        /// 右角括弧キーを表します。
        /// </summary>
        BracketRight,

        /// <summary>
        /// セミコロンキーを表します。
        /// </summary>
        Semicolon,

        /// <summary>
        /// クォートキーを表します。
        /// </summary>
        Quote,

        /// <summary>
        /// カンマキーを表します。
        /// </summary>
        Comma,

        /// <summary>
        /// ピリオドキーを表します。
        /// </summary>
        Period,

        /// <summary>
        /// スラッシュキーを表します。
        /// </summary>
        Slash,

        /// <summary>
        /// バックスラッシュキーを表します。
        /// </summary>
        BackSlash,

        /// <summary>
        /// US配列とは異なる位置にあるバックスラッシュキーを表します。
        /// </summary>
        NonUSBackSlash,

        /// <summary>
        /// 列挙値の終端を表します。
        /// </summary>
        Last
    }
}
