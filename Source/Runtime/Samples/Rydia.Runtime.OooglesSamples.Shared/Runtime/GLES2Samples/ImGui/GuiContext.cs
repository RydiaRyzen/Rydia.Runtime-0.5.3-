using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.GLES2Samples.ImGui
{
    public class GuiContext
    {

        public Vector2 MousePosition;

        public bool MouseDown;
        public bool MousePressed;

        public uint HotItem;
        public uint ActiveItem;

        public float CursorX;
        public float CursorY;

        public readonly List<GuiVertex> Vertices = new List<GuiVertex>();
        public readonly List<ushort> Indices = new List<ushort>();

        public IGuiRenderer Renderer
        {
            get;
            set;
        }

        public void BeginFrame(Vector2 mousePos, bool mouseDown)
        {
            this.MousePressed = !this.MouseDown && mouseDown;
            this.MouseDown = mouseDown;
            this.MousePosition = mousePos;

            this.HotItem = 0;

            this.CursorX = 10;
            this.CursorY = 10;

            this.Vertices.Clear();
            this.Indices.Clear();
        }

        public bool Button(string text)
        {
            float width = 120;
            float height = 28;
            
            var rect = new RectF(this.CursorX, this.CursorY, width, height);
            
            uint id = GuiId.Get(text);

            bool hover = rect.Contains(this.MousePosition.X, this.MousePosition.Y);

            if (hover)
            {
                this.HotItem = id;
                if(this.MousePressed)
                {
                    this.ActiveItem = id;
                }
            }

            bool clicked = false;
            if(!this.MouseDown)
            {
                if(this.ActiveItem == id && this.HotItem == id)
                {
                    clicked = true;
                    if(this.ActiveItem == id)
                    {
                        this.ActiveItem = 0;
                    }
                }
            }
            byte color = 0;
            if (this.HotItem == id)
            {
                color = 140;
            }
            if(this.ActiveItem == id)
            {
                color = 70;
            }
            AddRect(rect, color, color, color, 255);

            this.CursorY += height + 4;
            return clicked;
        }

        private void AddRect(RectF rect, byte r, byte g, byte b, byte a)
        {
            int baseIndex = this.Vertices.Count;
            this.Vertices.Add(new GuiVertex(rect.LeftX, rect.TopY, r, g, b, a));
            this.Vertices.Add(new GuiVertex(rect.RightX, rect.TopY, r, g, b, a));
            this.Vertices.Add(new GuiVertex(rect.RightX, rect.BottomY, r, g, b, a));
            this.Vertices.Add(new GuiVertex(rect.LeftX, rect.BottomY, r, g, b, a));
            this.Indices.Add((ushort)(baseIndex + 0));
            this.Indices.Add((ushort)(baseIndex + 1));
            this.Indices.Add((ushort)(baseIndex + 2));

            this.Indices.Add((ushort)(baseIndex + 0));
            this.Indices.Add((ushort)(baseIndex + 2));
            this.Indices.Add((ushort)(baseIndex + 3));
        }

        public void EndFrame(Matrix4 projection)
        {
            if (Renderer != null)
            {
                Renderer.Draw(this, projection);
            }
        }

    }
}
