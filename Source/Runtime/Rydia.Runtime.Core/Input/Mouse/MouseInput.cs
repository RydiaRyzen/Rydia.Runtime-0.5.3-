using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    /// <summary>
	/// Provides access to user mouse input.
	/// </summary>
	public sealed class MouseInput : IUserInput
    {
        private class State
        {
            public bool IsAvailable = false;
            public Point2 WindowPos = Point2.Zero;
            public Vector2 ViewPos = Vector2.Zero;
            public float Wheel = 0.0f;
            public bool[] ButtonPressed = new bool[(int)MouseButton.Last + 1];

            /// <summary>
            /// [GET / SET] When set to a non-zero value, the game's viewport will be adjusted to fit this size within the constraints
            /// of the user-defined or default window size.
            /// </summary>
            public Point2 ForcedRenderSize
            {
                get;
                set;
            }

            /// <summary>
            /// [GET / SET] Specifies how <see cref="ForcedRenderSize"/> will adjust the image to fit window constraints.
            /// </summary>
            public TargetResize ForcedRenderResizeMode
            {
                get;
                set;
            }

            public State()
            {
                
            }

            public State(State baseState)
            {
                baseState.CopyTo(this);
            }
            public void CopyTo(State other)
            {
                other.IsAvailable = this.IsAvailable;
                other.WindowPos = this.WindowPos;
                other.ViewPos = this.ViewPos;
                other.Wheel = this.Wheel;
                this.ButtonPressed.CopyTo(other.ButtonPressed, 0);
            }

            public void UpdateFromSource(IMouseInputSource source, int width, int height)
            {
                this.IsAvailable = source != null ? source.IsAvailable : false;
                if (source == null) return;

                this.WindowPos = source.Pos;
                this.Wheel = source.Wheel;
                for (int i = 0; i < this.ButtonPressed.Length; i++)
                {
                    this.ButtonPressed[i] = source[(MouseButton)i];
                }

                // Map window position to game view position
                RectF viewportRect;
                Vector2 gameViewSize;
                CalculateGameViewport(new Point2(width, height), out viewportRect, out gameViewSize);
                Vector2 relativePos = new Vector2(
                    MathFR.Clamp((this.WindowPos.X - viewportRect.X) / viewportRect.W, 0.0f, 1.0f),
                    MathFR.Clamp((this.WindowPos.Y - viewportRect.Y) / viewportRect.H, 0.0f, 1.0f));
                this.ViewPos = relativePos * gameViewSize;
            }

            /// <summary>
            /// Given the specified window size, this method calculates the window rectangle of the rendered
            /// viewport, as well as the game's rendered image size while taking into account application settings
            /// regarding forced rendering sizes.
            /// </summary>
            /// <param name="windowSize"></param>
            /// <param name="windowViewport"></param>
            /// <param name="renderTargetSize"></param>
            public void CalculateGameViewport(Point2 windowSize, out RectF windowViewport, out Vector2 renderTargetSize)
            {
                Point2 forcedSize = ForcedRenderSize;
                TargetResize forcedResizeMode = ForcedRenderResizeMode;

                renderTargetSize = windowSize;
                windowViewport = new RectF(renderTargetSize);

                bool forcedResizeActive =
                    forcedResizeMode != TargetResize.None &&
                    forcedSize.X > 0 && forcedSize.Y > 0 &&
                    forcedSize != renderTargetSize;
                if (forcedResizeActive)
                {
                    Vector2 adjustedViewportSize = forcedResizeMode.Apply(forcedSize, windowViewport.Size);

                    // Clip viewport and target size, so they don't exceed the window size.
                    // This, strictly speaking, violates the forced rendering size, but for
                    // resize modes like Fill, there is no other way to solve this.
                    if (adjustedViewportSize.X > windowSize.X)
                    {
                        forcedSize.X = MathFR.RoundToInt((float)forcedSize.X * (float)windowSize.X / (float)adjustedViewportSize.X);
                        adjustedViewportSize.X = windowSize.X;
                    }
                    if (adjustedViewportSize.Y > windowSize.Y)
                    {
                        forcedSize.Y = MathFR.RoundToInt((float)forcedSize.Y * (float)windowSize.Y / (float)adjustedViewportSize.Y);
                        adjustedViewportSize.Y = windowSize.Y;
                    }

                    renderTargetSize = forcedSize;
                    windowViewport = RectF.Align(
                        Alignment.Center,
                        windowViewport.Size.X * 0.5f,
                        windowViewport.Size.Y * 0.5f,
                        adjustedViewportSize.X,
                        adjustedViewportSize.Y);
                }
            }

        }

        private IMouseInputSource source = null;
        private State currentState = new State();
        private State lastState = new State();


        /// <summary>
        /// [GET / SET] The mouse inputs data source.
        /// </summary>
        public IMouseInputSource Source
        {
            get { return this.source; }
            set { this.source = value; }
        }
        IUserInputSource IUserInput.Source
        {
            get { return Source; }
            set { Source = value as IMouseInputSource; }
        }
        /// <summary>
        /// [GET] The unique id of this input.
        /// </summary>
        public string Id
        {
            get { return "Mouse"; }
        }
        /// <summary>
        /// [GET] The unique ID of the product that is providing this input.
        /// </summary>
        public Guid ProductId
        {
            get { return Guid.Empty; }
        }
        /// <summary>
        /// [GET] The name of the product that is providing this input.
        /// </summary>
        public string ProductName
        {
            get { return "Mouse"; }
        }
        /// <summary>
        /// [GET] Returns whether this input is currently available.
        /// </summary>
        public bool IsAvailable
        {
            get { return this.currentState.IsAvailable; }
        }
        /// <summary>
        /// [GET / SET] The current window-local cursor position in native window coordinates.
        /// </summary>
        public Point2 WindowPos
        {
            get { return this.currentState.WindowPos; }
            set
            {
                if (this.source != null)
                    this.source.Pos = value;
            }
        }
        /// <summary>
        /// [GET] The current viewport-local cursor position.
        /// </summary>
        public Vector2 Pos
        {
            get { return this.currentState.ViewPos; }
        }
        /// <summary>
        /// [GET] The viewport-local cursor position change since last frame.
        /// </summary>
        public Vector2 Vel
        {
            get
            {
                return (this.currentState.IsAvailable && this.lastState.IsAvailable) ?
                    this.currentState.ViewPos - this.lastState.ViewPos :
                    Vector2.Zero;
            }
        }
        /// <summary>
        /// [GET] The current mouse wheel value
        /// </summary>
        public float Wheel
        {
            get { return this.currentState.Wheel; }
        }
        /// <summary>
        /// [GET] Returns the change of the mouse wheel value since last frame.
        /// </summary>
        public float WheelSpeed
        {
            get { return (this.currentState.IsAvailable && this.lastState.IsAvailable) ? this.currentState.Wheel - this.lastState.Wheel : 0.0f; }
        }
        /// <summary>
        /// [GET] Returns whether a specific <see cref="MouseButton"/> is currently pressed.
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public bool this[MouseButton btn]
        {
            get { return this.currentState.ButtonPressed[(int)btn]; }
        }

        /// <summary>
        /// Fired when a <see cref="MouseButton"/> is no longer pressed.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonUp;
        /// <summary>
        /// Fired once when a <see cref="MouseButton"/> is pressed.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonDown;
        /// <summary>
        /// Fired when the cursor moves.
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> Move;
        /// <summary>
        /// Fired when the cursor leaves the viewport area.
        /// </summary>
        public event EventHandler NoLongerAvailable;
        /// <summary>
        /// Fired when the cursor enters the viewport area.
        /// </summary>
        public event EventHandler BecomesAvailable;
        /// <summary>
        /// Fired when the mouse wheel value changes.
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> WheelChanged;


        public MouseInput() { }

        public void Update(int width, int height)
        {
            // Memorize last state
            this.currentState.CopyTo(this.lastState);

            if (this.source != null)
            {
                // Update source state
                this.source.UpdateState();

                // Obtain new state
                this.currentState.UpdateFromSource(this.source, width, height);
            }

            // Fire events
            if (this.currentState.IsAvailable && !this.lastState.IsAvailable)
            {
                if (BecomesAvailable != null)
                    BecomesAvailable(this, EventArgs.Empty);
            }
            if (!this.currentState.IsAvailable && this.lastState.IsAvailable)
            {
                if (NoLongerAvailable != null)
                    NoLongerAvailable(this, EventArgs.Empty);
            }
            if (this.currentState.ViewPos != this.lastState.ViewPos)
            {
                if (Move != null)
                    Move(this, new MouseMoveEventArgs(
                        this,
                        Pos,
                        Vel));
            }
            if (this.currentState.Wheel != this.lastState.Wheel)
            {
                if (WheelChanged != null)
                    WheelChanged(this, new MouseWheelEventArgs(
                        this,
                        Pos,
                        Wheel,
                        WheelSpeed));
            }
            for (int i = 0; i < this.currentState.ButtonPressed.Length; i++)
            {
                if (this.currentState.ButtonPressed[i] && !this.lastState.ButtonPressed[i])
                {
                    if (ButtonDown != null)
                        ButtonDown(this, new MouseButtonEventArgs(
                            this,
                            Pos,
                            (MouseButton)i,
                            this.currentState.ButtonPressed[i]));
                }
                if (!this.currentState.ButtonPressed[i] && this.lastState.ButtonPressed[i])
                {
                    if (ButtonUp != null)
                        ButtonUp(this, new MouseButtonEventArgs(
                            this,
                            Pos,
                            (MouseButton)i,
                            this.currentState.ButtonPressed[i]));
                }
            }
        }

        /// <summary>
        /// Returns whether the specified button is currently pressed.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonPressed(MouseButton button)
        {
            return this.currentState.ButtonPressed[(int)button];
        }
        /// <summary>
        /// Returns whether the specified button was hit this frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonHit(MouseButton button)
        {
            return this.currentState.ButtonPressed[(int)button] && !this.lastState.ButtonPressed[(int)button];
        }
        /// <summary>
        /// Returns whether the specified button was released this frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonReleased(MouseButton button)
        {
            return !this.currentState.ButtonPressed[(int)button] && this.lastState.ButtonPressed[(int)button];
        }
    }
}
