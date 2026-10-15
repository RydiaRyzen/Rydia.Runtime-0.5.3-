using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

using IList = System.Collections.IList;

using AdamsLair.WinForms.Drawing;
using AdamsLair.WinForms.PropertyEditing.Templates;
using Rydia.Windows.Forms.Core.Properties;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class IListPropertyEditor : GroupedPropertyEditor
	{
		public delegate void IndexValueSetter(PropertyInfo indexer, IEnumerable<object> targetObjects, IEnumerable<object> values, int index);
		public delegate bool IListResizer(IList<IList> targetLists, int size, Type elementType);

		// To avoid memory problems from entering insane size values, we need some kind of limit.
		// In the context of this specific PropertyEditor, it makes sense to have a rather low
		// limit, as there is no way to reasonably edit much larger arrays with this. Instead,
		// specialized editors for these amounts of data should be considered.
		private static readonly int MaxAllowedListSize = 100000;

		private	bool					buttonIsCreate	= false;
		private	NumericPropertyEditor	sizeEditor		= null;
		private	NumericPropertyEditor	offsetEditor	= null;
		private	int						offset			= 0;
		private	int						internalEditors	= 0;
		private	IndexValueSetter		listIndexSetter	= null;
		private IListResizer			listResizer		= null;

		public override object DisplayedValue
		{
			get { return GetValue(); }
		}
		public IndexValueSetter ListIndexSetter
		{
			get { return this.listIndexSetter; }
			set
			{
				if (value == null) value = DefaultPropertySetter;
				this.listIndexSetter = value;
			}
		}

		public IListResizer ListResizer
		{
			get { return this.listResizer; }
			set
			{
				if (value == null) value = DefaultListResizer;
				this.listResizer = value;
			}
		}

		public IListPropertyEditor()
		{
			Hints |= HintFlags.HasButton | HintFlags.ButtonEnabled;

			this.listIndexSetter = DefaultPropertySetter;
			this.listResizer = DefaultListResizer;

			this.sizeEditor = new NumericPropertyEditor();
			this.sizeEditor.EditedType = typeof(int);
			this.sizeEditor.Minimum = 0;
			this.sizeEditor.PropertyName = "Size";
			this.sizeEditor.Getter = SizeValueGetter;
			this.sizeEditor.Setter = SizeValueSetter;

			this.offsetEditor = new NumericPropertyEditor();
			this.offsetEditor.EditedType = typeof(uint);
			this.offsetEditor.Minimum = 0;
			this.offsetEditor.PropertyName = "Offset";
			this.offsetEditor.Getter = OffsetValueGetter;
			this.offsetEditor.Setter = OffsetValueSetter;
			this.offsetEditor.ValueMutable = true;
		}

		public override void InitContent()
		{
			base.InitContent();

			if (EditedType != null)
				PerformGetValue();
			else
				ClearContent();
		}
		public override void ClearContent()
		{
			base.ClearContent();
			this.offset = 0;
		}

		protected override void OnGetValue()
		{
			base.OnGetValue();
			IList[] values = GetValue().Cast<IList>().ToArray();

			string valString = null;
			if (!values.Any() || values.All(o => o == null))
			{
				ClearContent();

				Hints &= ~HintFlags.ExpandEnabled;
				ButtonIcon = AdamsLair.WinForms.Properties.ResourcesCache.ImageAdd;
				this.buttonIsCreate = true;
				Expanded = false;
					
				valString = "null";
			}
			else
			{
				if (ContentInitialized)
				{
					if (Expanded)
						UpdateElementEditors(values, false);
					else
						ClearContent();
				}
				
				Hints |= HintFlags.ExpandEnabled;
				if (!CanExpand) Expanded = false;
				ButtonIcon = AdamsLair.WinForms.Properties.ResourcesCache.ImageDelete;
				this.buttonIsCreate = false;

				IList firstValue = values.First();
				int valueCount = values.Count();
				
				if (valueCount == 1 && firstValue != null)
				{
					valString = string.Format(
						"{0}, Count = {1}", 
						EditedType.GetTypeCSCodeName(true), 
						firstValue.Count);
				}
				else
				{
					valString = string.Format(
						CoreResources.PropertyGrid_N_Objects, 
						valueCount);
				}
			}

			HeaderValueText = valString;

			foreach (PropertyEditor e in ChildEditors)
				e.PerformGetValue();
		}
		protected override void OnSetValue()
		{
			if (ReadOnly) return;
			if (!ChildEditors.Any()) return;
			base.OnSetValue();

			foreach (PropertyEditor e in ChildEditors)
				e.PerformSetValue();
		}
		protected override void VerifyReflectedTypeEditors(IEnumerable<object> values)
		{
			base.VerifyReflectedTypeEditors(values);
			if (!ContentInitialized) return;
			if (!Expanded) return;

			IList[] valuesCast = values.Cast<IList>().ToArray();
			if (values.Any() && values.Any(o => o != null))
			{
				UpdateElementEditors(valuesCast, true);
			}
		}

		protected void UpdateElementEditors(IList[] values, bool getValueOnNewEditors)
		{
			PropertyInfo indexer = typeof(IList).GetProperty("Item");
			IEnumerable<IList> valuesNotNull = values.Where(v => v != null);
			int visibleElementCount = valuesNotNull.Min(o => (int)o.Count);
			bool showOffset = false;
			if (visibleElementCount > 10)
			{
				this.offset = Math.Min(this.offset, visibleElementCount - 10);
				this.offsetEditor.Maximum = visibleElementCount - 10;
				this.offsetEditor.ValueBarMaximum = this.offsetEditor.Maximum;
				visibleElementCount = 10;
				showOffset = true;
			}
			else
			{
				this.offset = 0;
			}

			if (this.sizeEditor.ParentEditor == null) AddPropertyEditor(this.sizeEditor, 0);
			if (showOffset && this.offsetEditor.ParentEditor == null) AddPropertyEditor(this.offsetEditor, 1);
			else if (!showOffset && this.offsetEditor.ParentEditor != null) RemovePropertyEditor(this.offsetEditor);

			this.internalEditors = showOffset ? 2 : 1;

			BeginUpdate();


			// Add missing editors
			Type elementType = GetIListElementType(EditedType);
			Type reflectedArrayType = PropertyEditor.ReflectDynamicType(elementType, valuesNotNull.Select(a => GetIListElementType(a.GetType())));
			for (int i = this.internalEditors; i < visibleElementCount + this.internalEditors; i++)
			{
				int elementIndex = i - this.internalEditors + this.offset;
				Type reflectedElementType = PropertyEditor.ReflectDynamicType(
					reflectedArrayType, 
					valuesNotNull.Select(v => indexer.GetValue(v, new object[] { elementIndex })));
				bool elementEditorIsNew = false;
				PropertyEditor elementEditor;

				// Retrieve and Update existing editor
				if (i < ChildEditors.Count())
				{
					elementEditor = ChildEditors.ElementAt(i);
					if (elementEditor.EditedType != reflectedElementType)
					{
						// If the editor has the wrong type, we'll need to create a new one
						PropertyEditor oldEditor = elementEditor;
						elementEditor = ParentGrid.CreateEditor(reflectedElementType, this);
						elementEditorIsNew = true;

						AddPropertyEditor(elementEditor, oldEditor);
						RemovePropertyEditor(oldEditor);
						ParentGrid.ConfigureEditor(elementEditor);
					}
				}
				// Create a new editor
				else
				{
					elementEditor = ParentGrid.CreateEditor(reflectedElementType, this);
					elementEditorIsNew = true;

					AddPropertyEditor(elementEditor);
					ParentGrid.ConfigureEditor(elementEditor);
				}

				elementEditor.Getter = CreateElementValueGetter(indexer, elementIndex);
				elementEditor.Setter = CreateElementValueSetter(indexer, elementIndex);
				elementEditor.PropertyName = "[" + elementIndex + "]";

				// Immediately retrieve a valid value for the newly created editor when requested
				if (elementEditorIsNew && getValueOnNewEditors)
					elementEditor.PerformGetValue();
			}

			// Remove overflowing editors
			for (int i = ChildEditors.Count() - (this.internalEditors + 1); i >= visibleElementCount; i--)
			{
				PropertyEditor child = ChildEditors.Last();
				RemovePropertyEditor(child);
			}

			EndUpdate();
		}

		protected IEnumerable<object> SizeValueGetter()
		{
			return GetValue().Select(o => o != null ? (object)((IList)o).Count : null);
		}
		protected void SizeValueSetter(IEnumerable<object> values)
		{
			IEnumerator<object> valuesEnum = values.GetEnumerator();
			IList<IList> targetLists = GetValue().Cast<IList>().ToList();
			Type elementType = GetIListElementType(EditedType);
			IEnumerable<Type> targetListElementTypes = targetLists.Where(list => list != null).Select(a => GetIListElementType(a.GetType()));
			Type reflectedElementType = PropertyEditor.ReflectDynamicType(elementType, targetListElementTypes);

			int maxSize = 0;
			while (valuesEnum.MoveNext())
			{
				int cur = (int) valuesEnum.Current;
				if (cur > maxSize) maxSize = cur;
			}

			if (maxSize > MaxAllowedListSize) maxSize = MaxAllowedListSize;

			bool writeBack = this.listResizer(targetLists, maxSize, reflectedElementType);

			if (writeBack || ForceWriteBack) SetValues(targetLists);
			PerformGetValue();
		}
		protected IEnumerable<object> OffsetValueGetter()
		{
			yield return (uint)this.offset;
		}
		protected void OffsetValueSetter(IEnumerable<object> values)
		{
			this.offset = (int)Convert.ChangeType(values.First(), typeof(int));
			PerformGetValue();
		}
		protected Func<IEnumerable<object>> CreateElementValueGetter(PropertyInfo indexer, int index)
		{
			return () => GetValue().Select(o => o != null ? indexer.GetValue(o, new object[] {index}) : null);
		}
		protected Action<IEnumerable<object>> CreateElementValueSetter(PropertyInfo indexer, int index)
		{
			return delegate(IEnumerable<object> values)
			{
				object[] targetArray = GetValue().ToArray();
				this.listIndexSetter(indexer, targetArray, values, index);
				if (ForceWriteBack) SetValues(targetArray);
			};
		}

		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			Expanded = false;
			ClearContent();
		}
		protected override void OnButtonPressed()
		{
			base.OnButtonPressed();

			if (this.buttonIsCreate)
			{
				bool anyCreated = false;

				int objectsToCreate = GetValue().Count();
				IList[] createdObjects = new IList[objectsToCreate];
				for (int i = 0; i < createdObjects.Length; i++)
				{
					createdObjects[i] = ParentGrid.CreateObjectInstance(EditedType) as IList;
					if (createdObjects[i] == null)
					{
						Type elementType = GetIListElementType(EditedType);
						Type listType = elementType != null ? elementType.MakeArrayType() : null;
						if (listType != null && EditedType.IsAssignableFrom(listType))
						{
							createdObjects[i] = ParentGrid.CreateObjectInstance(listType) as IList;
						}
					}
					if (createdObjects[i] != null)
					{
						anyCreated = true;
					}
				}

				if (anyCreated)
				{
					SetValues(createdObjects);
					Expanded = true;
				}
			}
			else
			{
				SetValue(null);
			}

			PerformGetValue();
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if ((Hints & HintFlags.HasButton) != HintFlags.None &&
				(Hints & HintFlags.ButtonEnabled) != HintFlags.None)
			{
				if (!this.buttonIsCreate && e.KeyCode == Keys.Delete)
				{
					OnButtonPressed();
					e.Handled = true;
				}
				else if (this.buttonIsCreate && e.KeyCode == Keys.Return)
				{
					OnButtonPressed();
					e.Handled = true;
				}
			}
		}

		protected internal override void ConfigureEditor(object configureData)
		{
			base.ConfigureEditor(configureData);
		}

		protected static Type GetIListElementType(Type listType)
		{
			Type ilistInterface = null;
			if (listType.HasElementType)
				return listType.GetElementType();
			else if ((ilistInterface = listType.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>)).FirstOrDefault()) != null)
				return ilistInterface.GetGenericArguments()[0];
			else if (listType.IsGenericType)
				return listType.GetGenericArguments()[0];
			else
				return typeof(object);
		}
		protected static void DefaultPropertySetter(PropertyInfo property, IEnumerable<object> targetObjects, IEnumerable<object> values, int index)
		{
			IEnumerator<object> valuesEnum = values.GetEnumerator();
			object curValue = null;

			if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
			foreach (object target in targetObjects)
			{
				if (target != null) property.SetValue(target, curValue, new object[] { index });
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
			}
		}
		protected static bool DefaultListResizer(IList<IList> targetLists, int size, Type elementType)
		{
			bool writeback = false;

			for (int i = 0; i < targetLists.Count; i++)
			{
				IList target = targetLists[i];
				if (target == null) continue;
				if (!target.IsFixedSize && !target.IsReadOnly)
				{
					while (target.Count < size)
					{
						object added = elementType.IsValueType ? elementType.CreateInstanceOf() : null;
						target.Add(added);
					}
					while (target.Count > size)
						target.RemoveAt(target.Count - 1);
				}
				else if (target is Array)
				{
					Array newTarget = Array.CreateInstance(elementType, size);
					for (int j = 0; j < Math.Min(size, target.Count); j++) newTarget.SetValue(target[j], j);
					targetLists[i] = newTarget;
					writeback = true;
				}
				else
				{
					// a read only container we can't do anything about
				}
			}

			return writeback;
		}
	}
}
