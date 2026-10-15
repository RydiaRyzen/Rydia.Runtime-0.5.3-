using System;
using System.Drawing;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    internal abstract class DockOutlineBase
    {
        public DockOutlineBase()
        {
			Init();
        }

        private void Init()
        {
			SetValues(Rectangle.Empty, null, DockStyle.None, -1);
			SaveOldValues();
        }

        private Rectangle m_oldFloatWindowBounds;
        protected Rectangle OldFloatWindowBounds
        {
            get { return this.m_oldFloatWindowBounds; }
        }

        private Control m_oldDockTo;
        protected Control OldDockTo
        {
            get { return this.m_oldDockTo; }
        }

        private DockStyle m_oldDock;
        protected DockStyle OldDock
        {
            get { return this.m_oldDock; }
        }

        private int m_oldContentIndex;
        protected int OldContentIndex
        {
            get { return this.m_oldContentIndex; }
        }

        protected bool SameAsOldValue
        {
            get
            {
                return FloatWindowBounds == OldFloatWindowBounds &&
					DockTo == OldDockTo &&
					Dock == OldDock &&
					ContentIndex == OldContentIndex;
            }
        }

        private Rectangle m_floatWindowBounds;
        public Rectangle FloatWindowBounds
        {
            get { return this.m_floatWindowBounds; }
        }

        private Control m_dockTo;
        public Control DockTo
        {
            get { return this.m_dockTo; }
        }

        private DockStyle m_dock;
        public DockStyle Dock
        {
            get { return this.m_dock; }
        }

        private int m_contentIndex;
        public int ContentIndex
        {
            get { return this.m_contentIndex; }
        }

        public bool FlagFullEdge
        {
            get { return this.m_contentIndex != 0; }
        }

        private bool m_flagTestDrop = false;
        public bool FlagTestDrop
        {
            get { return this.m_flagTestDrop; }
            set { this.m_flagTestDrop = value; }
        }

        private void SaveOldValues()
        {
			this.m_oldDockTo = this.m_dockTo;
			this.m_oldDock = this.m_dock;
			this.m_oldContentIndex = this.m_contentIndex;
			this.m_oldFloatWindowBounds = this.m_floatWindowBounds;
        }

        protected abstract void OnShow();

        protected abstract void OnClose();

        private void SetValues(Rectangle floatWindowBounds, Control dockTo, DockStyle dock, int contentIndex)
        {
			this.m_floatWindowBounds = floatWindowBounds;
			this.m_dockTo = dockTo;
			this.m_dock = dock;
			this.m_contentIndex = contentIndex;
			FlagTestDrop = true;
        }

        private void TestChange()
        {
            if (this.m_floatWindowBounds != this.m_oldFloatWindowBounds ||
				this.m_dockTo != this.m_oldDockTo ||
				this.m_dock != this.m_oldDock ||
				this.m_contentIndex != this.m_oldContentIndex)
				OnShow();
        }

        public void Show()
        {
			SaveOldValues();
			SetValues(Rectangle.Empty, null, DockStyle.None, -1);
			TestChange();
        }

        public void Show(DockPane pane, DockStyle dock)
        {
			SaveOldValues();
			SetValues(Rectangle.Empty, pane, dock, -1);
			TestChange();
        }

        public void Show(DockPane pane, int contentIndex)
        {
			SaveOldValues();
			SetValues(Rectangle.Empty, pane, DockStyle.Fill, contentIndex);
			TestChange();
        }

        public void Show(DockPanel dockPanel, DockStyle dock, bool fullPanelEdge)
        {
			SaveOldValues();
			SetValues(Rectangle.Empty, dockPanel, dock, fullPanelEdge ? -1 : 0);
			TestChange();
        }

        public void Show(Rectangle floatWindowBounds)
        {
			SaveOldValues();
			SetValues(floatWindowBounds, null, DockStyle.None, -1);
			TestChange();
        }

        public void Close()
        {
			OnClose();
        }
    }
}
