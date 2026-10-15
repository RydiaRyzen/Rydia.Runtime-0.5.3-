using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

using Aga.Controls.Tree.NodeControls;

namespace Aga.Controls.Tree
{
	public partial class TreeViewAdv
	{
		private Cursor _innerCursor = null;
		private IHeaderLayout _headerLayout;

		public override Cursor Cursor
		{
			get
			{
                if (this._innerCursor != null)
                    return this._innerCursor;
                else
					return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		#region Internal Properties

		private IRowLayout _rowLayout;

		private bool _dragMode;
		private bool DragMode
		{
			get { return this._dragMode; }
			set
			{
				this._dragMode = value;
				if (!value)
				{
					StopDragTimer();
					if (this._dragBitmap != null)
						this._dragBitmap.Dispose();
					this._dragBitmap = null;
				}
				else
					StartDragTimer();
			}
		}

		[DefaultValue(20), Category("Appearance")]
		public int ColumnHeaderHeight
		{
			get
			{
				if (UseColumns)
					return this._headerLayout.PreferredHeaderHeight;
				return 0;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("value");
				this._headerLayout.PreferredHeaderHeight = value;
				FullUpdate();
			}
		}

		/// <summary>
		/// returns all nodes, which parent is expanded
		/// </summary>
		private IEnumerable<TreeNodeAdv> VisibleNodes
		{
			get
			{
				TreeNodeAdv node = Root;
				while (node != null)
				{
					node = node.NextVisibleNode;
					if (node != null)
						yield return node;
				}
			}
		}

		private bool _suspendSelectionEvent;
		internal bool SuspendSelectionEvent
		{
			get { return this._suspendSelectionEvent; }
			set
			{
				if (value != this._suspendSelectionEvent)
				{
					this._suspendSelectionEvent = value;
					if (!this._suspendSelectionEvent && this._fireSelectionEvent)
						OnSelectionChanged();
				}
			}
		}

		private List<TreeNodeAdv> _rowMap;
		internal List<TreeNodeAdv> RowMap
		{
			get { return this._rowMap; }
		}

		private TreeNodeAdv _selectionStart;
		internal TreeNodeAdv SelectionStart
		{
			get { return this._selectionStart; }
			set { this._selectionStart = value; }
		}

		private InputState _input;
		internal InputState Input
		{
			get { return this._input; }
			set
			{
				this._input = value;
			}
		}

		private bool _itemDragMode;
		internal bool ItemDragMode
		{
			get { return this._itemDragMode; }
			set { this._itemDragMode = value; }
		}

		private Point _itemDragStart;
		internal Point ItemDragStart
		{
			get { return this._itemDragStart; }
			set { this._itemDragStart = value; }
		}


		/// <summary>
		/// Number of rows fits to the current page
		/// </summary>
		internal int CurrentPageSize
		{
			get
			{
				return this._rowLayout.CurrentPageSize;
			}
		}

		/// <summary>
		/// Number of all visible nodes (which parent is expanded)
		/// </summary>
		internal int RowCount
		{
			get
			{
				return RowMap.Count;
			}
		}

		/// <summary>
		/// Number of all visible, non-hidden nodes (which parent is expanded)
		/// </summary>
		internal int VisibleRowCount
		{
			get
			{
				int visibleCount = 0;
				for (int i = 0; i < RowMap.Count; i++)
				{
					if (RowMap[i].IsHidden)
						continue;

					visibleCount++;
				}
				return visibleCount;
			}
		}

		private int _contentWidth = 0;
		private int ContentWidth
		{
			get
			{
				return this._contentWidth;
			}
		}

		private int _firstVisibleRow;
		internal int FirstVisibleRow
		{
			get { return this._firstVisibleRow; }
			set
			{
				HideEditor();
				this._firstVisibleRow = value;
				UpdateView();
			}
		}

		private int _offsetX;
		public int OffsetX
		{
			get { return this._offsetX; }
			private set
			{
				HideEditor();
				this._offsetX = value;
				UpdateView();
			}
		}

		public override Rectangle DisplayRectangle
		{
			get
			{
				Rectangle r = ClientRectangle;
				//r.Y += ColumnHeaderHeight;
				//r.Height -= ColumnHeaderHeight;
				int w = this._vScrollBar.Visible ? this._vScrollBar.Width : 0;
				int h = this._hScrollBar.Visible ? this._hScrollBar.Height : 0;
				return new Rectangle(r.X, r.Y, r.Width - w, r.Height - h);
			}
		}

		private List<TreeNodeAdv> _selection;
		internal List<TreeNodeAdv> Selection
		{
			get { return this._selection; }
		}

		#endregion

		#region Public Properties

		#region DesignTime

		private Color _fullRowSelectActiveColor;
		[Category("Appearance")]
		public Color FullRowSelectActiveColor
		{
			get { return this._fullRowSelectActiveColor; }
			set { this._fullRowSelectActiveColor = value; }
		}

		private Color _fullRowSelectInactiveColor;
		[Category("Appearance")]
		public Color FullRowSelectInactiveColor
		{
			get { return this._fullRowSelectInactiveColor; }
			set { this._fullRowSelectInactiveColor = value; }
		}

		private bool _shiftFirstNode;
		[DefaultValue(false), Category("Behavior")]
		public bool ShiftFirstNode
		{
			get { return this._shiftFirstNode; }
			set { this._shiftFirstNode = value; }
		}

		private bool _displayDraggingNodes;
		[DefaultValue(false), Category("Behavior")]
		public bool DisplayDraggingNodes
		{
			get { return this._displayDraggingNodes; }
			set { this._displayDraggingNodes = value; }
		}

		private bool _fullRowSelect;
		[DefaultValue(false), Category("Behavior")]
		public bool FullRowSelect
		{
			get { return this._fullRowSelect; }
			set
			{
				this._fullRowSelect = value;
				UpdateView();
			}
		}

		private bool _useColumns;
		[DefaultValue(false), Category("Behavior")]
		public bool UseColumns
		{
			get { return this._useColumns; }
			set
			{
				this._useColumns = value;
				FullUpdate();
			}
		}

		private bool _allowColumnReorder;
		[DefaultValue(false), Category("Behavior")]
		public bool AllowColumnReorder
		{
			get { return this._allowColumnReorder; }
			set { this._allowColumnReorder = value; }
		}

		private bool _showLines = true;
		[DefaultValue(true), Category("Behavior")]
		public bool ShowLines
		{
			get { return this._showLines; }
			set
			{
				this._showLines = value;
				UpdateView();
			}
		}

		private bool _showPlusMinus = true;
		[DefaultValue(true), Category("Behavior")]
		public bool ShowPlusMinus
		{
			get { return this._showPlusMinus; }
			set
			{
				this._showPlusMinus = value;
				FullUpdate();
			}
		}

		private bool _showNodeToolTips = false;
		[DefaultValue(false), Category("Behavior")]
		public bool ShowNodeToolTips
		{
			get { return this._showNodeToolTips; }
			set { this._showNodeToolTips = value; }
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), DefaultValue(true), Category("Behavior"), Obsolete("No longer used")]
		public bool KeepNodesExpanded
		{
			get { return true; }
			set {}
		}

		private ITreeModel _model;
        /// <Summary>
        /// The model associated with this <see cref="TreeViewAdv"/>.
        /// </Summary>
        /// <seealso cref="ITreeModel"/>
        /// <seealso cref="TreeModel"/>
        [Browsable(false)]
		public ITreeModel Model
		{
			get { return this._model; }
			set
			{
				if (this._model != value)
				{
					AbortBackgroundExpandingThreads();
					if (this._model != null)
						UnbindModelEvents();
					this._model = value;
					CreateNodes();
					FullUpdate();
					if (this._model != null)
						BindModelEvents();
				}
			}
		}

        // Tahoma is the default font
        private static Font _font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)), false);
        /// <summary>
        /// The font to render <see cref="TreeViewAdv"/> content in.
        /// </summary>
        [Category("Appearance"), Description("The font to render TreeViewAdv content in.")]
        public override Font Font
        {
            get
            {
                return (base.Font);
            }
            set
            {
                if (value == null)
                    base.Font = _font;
                else
                {
                    if (value == DefaultFont)
                        base.Font = _font;
                    else
                        base.Font = value;
                }
            }
        }
        public override void ResetFont()
        {
			Font = null;
        }
        private bool ShouldSerializeFont()
        {
            return (!Font.Equals(_font));
        }
        // End font property

		private BorderStyle _borderStyle = BorderStyle.Fixed3D;
		[DefaultValue(BorderStyle.Fixed3D), Category("Appearance")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		private bool _autoRowHeight = false;
		/// <summary>
		/// Set to true to expand each row's height to fit the text of it's largest column.
		/// </summary>
		[DefaultValue(false), Category("Appearance"), Description("Expand each row's height to fit the text of it's largest column.")]
		public bool AutoRowHeight
		{
			get
			{
				return this._autoRowHeight;
			}
			set
			{
				this._autoRowHeight = value;
				if (value)
					this._rowLayout = new AutoRowHeightLayout(this, RowHeight);
				else
					this._rowLayout = new FixedRowHeightLayout(this, RowHeight);
				FullUpdate();
			}
		}

		private bool _autoHeaderHeight = false;
		/// <summary>
		/// Set to true to expand header height to fit the text of it's largest column.
		/// </summary>
		[DefaultValue(false), Category("Appearance"), Description("Expand each header height to fit the text of it's largest column.")]
		public bool AutoHeaderHeight
		{
			get
			{
				return this._autoHeaderHeight;
			}
			set
			{
				this._autoHeaderHeight = value;
				if (value)
					this._headerLayout = new AutoHeaderHeightLayout(this, ColumnHeaderHeight);
				else
					this._headerLayout = new FixedHeaderHeightLayout(this, ColumnHeaderHeight);
				FullUpdate();
			}
		}

        private GridLineStyle _gridLineStyle = GridLineStyle.None;
        [DefaultValue(GridLineStyle.None), Category("Appearance")]
        public GridLineStyle GridLineStyle
        {
            get
            {
                return this._gridLineStyle;
            }
            set
            {
				if (value != this._gridLineStyle)
				{
					this._gridLineStyle = value;
					UpdateView();
					OnGridLineStyleChanged();
				}
            }
        }

		private int _rowHeight = 16;
		[DefaultValue(16), Category("Appearance")]
		public int RowHeight
		{
			get
			{
				return this._rowHeight;
			}
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException("value");

				this._rowHeight = value;
				this._rowLayout.PreferredRowHeight = value;
				FullUpdate();
			}
		}

		private TreeSelectionMode _selectionMode = TreeSelectionMode.Single;
		[DefaultValue(TreeSelectionMode.Single), Category("Behavior")]
		public TreeSelectionMode SelectionMode
		{
			get { return this._selectionMode; }
			set { this._selectionMode = value; }
		}

		private bool _hideSelection;
		[DefaultValue(false), Category("Behavior")]
		public bool HideSelection
		{
			get { return this._hideSelection; }
			set
			{
				this._hideSelection = value;
				UpdateView();
			}
		}

		bool _inactiveSelection = true;
		[DefaultValue(true), Category("Behavior")]
		public bool InactiveSelection
		{
			get { return this._inactiveSelection; }
			set
			{
				this._inactiveSelection = value;
				UpdateView();
			}
		}

		private float _topEdgeSensivity = 0.3f;
		[DefaultValue(0.3f), Category("Behavior")]
		public float TopEdgeSensivity
		{
			get { return this._topEdgeSensivity; }
			set
			{
				if (value < 0 || value > 1)
					throw new ArgumentOutOfRangeException();
				this._topEdgeSensivity = value;
			}
		}

		private float _bottomEdgeSensivity = 0.3f;
		[DefaultValue(0.3f), Category("Behavior")]
		public float BottomEdgeSensivity
		{
			get { return this._bottomEdgeSensivity; }
			set
			{
				if (value < 0 || value > 1)
					throw new ArgumentOutOfRangeException("value should be from 0 to 1");
				this._bottomEdgeSensivity = value;
			}
		}

		private bool _loadOnDemand;
		[DefaultValue(false), Category("Behavior")]
		public bool LoadOnDemand
		{
			get { return this._loadOnDemand; }
			set { this._loadOnDemand = value; }
		}

		private bool _unloadCollapsedOnReload = false;
		[DefaultValue(false), Category("Behavior")]
		public bool UnloadCollapsedOnReload
		{
			get { return this._unloadCollapsedOnReload; }
			set { this._unloadCollapsedOnReload = value; }
		}

		private int _indent = 19;
		[DefaultValue(19), Category("Behavior")]
		public int Indent
		{
			get { return this._indent; }
			set
			{
				this._indent = value;
				UpdateView();
			}
		}

		private Color _lineColor = SystemColors.ControlDark;
		[Category("Behavior")]
		public Color LineColor
		{
			get { return this._lineColor; }
			set
			{
				this._lineColor = value;
				CreateLinePen();
				UpdateView();
			}
		}

		private Color _dragDropMarkColor = Color.Black;
		[Category("Behavior")]
		public Color DragDropMarkColor
		{
			get { return this._dragDropMarkColor; }
			set
			{
				this._dragDropMarkColor = value;
				CreateMarkPen();
			}
		}

		private float _dragDropMarkWidth = 3.0f;
		[DefaultValue(3.0f), Category("Behavior")]
		public float DragDropMarkWidth
		{
			get { return this._dragDropMarkWidth; }
			set
			{
				this._dragDropMarkWidth = value;
				CreateMarkPen();
			}
		}

		private bool _highlightDropPosition = true;
		[DefaultValue(true), Category("Behavior")]
		public bool HighlightDropPosition
		{
			get { return this._highlightDropPosition; }
			set { this._highlightDropPosition = value; }
		}

		private TreeColumnCollection _columns;
		[Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Collection<TreeColumn> Columns
		{
			get { return this._columns; }
		}

		private NodeControlsCollection _controls;
		[Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(NodeControlCollectionEditor), typeof(UITypeEditor))]
		public Collection<NodeControl> NodeControls
		{
			get
			{
				return this._controls;
			}
		}

		private bool _asyncExpanding;
		/// <summary>
		/// When set to true, node contents will be read in background thread.
		/// </summary>
		[Category("Behavior"), DefaultValue(false), Description("Read children in a background thread when expanding.")]
		public bool AsyncExpanding
		{
			get { return this._asyncExpanding; }
			set { this._asyncExpanding = value; }
		}

		#endregion

		#region RunTime

		private IToolTipProvider _defaultToolTipProvider = null;
		[Browsable(false)]
		public IToolTipProvider DefaultToolTipProvider
		{
			get { return this._defaultToolTipProvider; }
			set { this._defaultToolTipProvider = value; }
		}

		[Browsable(false)]
		public IEnumerable<TreeNodeAdv> AllNodes
		{
			get
			{
				if (this._root.Nodes.Count > 0)
				{
					TreeNodeAdv node = this._root.Nodes[0];
					while (node != null)
					{
						yield return node;
						if (node.Nodes.Count > 0)
							node = node.Nodes[0];
						else if (node.NextNode != null)
							node = node.NextNode;
						else
							node = node.BottomNode;
					}
				}
			}
		}

		[Browsable(false)]
		public Predicate<TreeNodeAdv> NodeFilter
		{
			get { return this._viewNodeFilter; }
			set
			{
				this._viewNodeFilter = value;
				UpdateNodeFilter();
			}
		}

		private DropPosition _dropPosition;
		[Browsable(false)]
		public DropPosition DropPosition
		{
			get { return this._dropPosition; }
			set { this._dropPosition = value; }
		}

		private TreeNodeAdv _root;
		[Browsable(false)]
		public TreeNodeAdv Root
		{
			get { return this._root; }
		}

		private ReadOnlyCollection<TreeNodeAdv> _readonlySelection;
		[Browsable(false)]
		public ReadOnlyCollection<TreeNodeAdv> SelectedNodes
		{
			get
			{
				return this._readonlySelection;
			}
		}

		[Browsable(false)]
		public TreeNodeAdv SelectedNode
		{
			get
			{
				if (Selection.Count > 0)
				{
					if (CurrentNode != null && CurrentNode.IsSelected)
						return CurrentNode;
					else
						return Selection[0];
				}
				else
					return null;
			}
			set
			{
				if (SelectedNode == value)
					return;

				BeginUpdate();
				try
				{
					if (value == null)
					{
						ClearSelectionInternal();
					}
					else
					{
						if (!IsMyNode(value))
							throw new ArgumentException();

						ClearSelectionInternal();
						value.IsSelected = true;
						CurrentNode = value;
						EnsureVisible(value);
					}
				}
				finally
				{
					EndUpdate();
				}
			}
		}

		private TreeNodeAdv _currentNode;
		[Browsable(false)]
		public TreeNodeAdv CurrentNode
		{
			get { return this._currentNode; }
			internal set { this._currentNode = value; }
		}

        [Browsable(false)]
        public int ItemCount
        {
            get { return RowMap.Count; }
        }

		/// <summary>
		/// Indicates the distance the content is scrolled to the left
		/// </summary>
		[Browsable(false)]
		public int HorizontalScrollPosition
		{
			get
			{
				if (this._hScrollBar.Visible)
					return this._hScrollBar.Value;
				else
					return 0;
			}
		}

		#endregion

		#endregion

	}
}
