using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace DanRigsby.Windows.Forms
{
	/// <summary>
	/// Providers an extender control that sets controls to read only or not.
	/// </summary>
	[DesignerCategory("Behavior")]	
	[ProvideProperty("EnableReadOnlyControl", typeof(Control))]
	public class ReadOnlyController : Component, IExtenderProvider
	{
		#region Private Properties
		private Dictionary<Control, bool> m_Extendees = new Dictionary<Control, bool>();
		private bool m_ReadOnly;
		private EventHandler<EventArgs<bool>> m_ReadOnlyChanged;
		#endregion Private Properties

		#region Public Events
		/// <summary>
		/// Occurs when [read only changed].
		/// </summary>
		public event EventHandler<EventArgs<bool>> ReadOnlyChanged
		{
			add
			{
				m_ReadOnlyChanged += value;
			}
			remove
			{
				ReadOnlyChanged -= value;
			}
		}
		#endregion Public Events

		#region Private Properties
		/// <summary>
		/// Gets or sets a value indicating whether the controller is set to read only.
		/// </summary>
		/// <value><c>true</c> if [read only]; otherwise, <c>false</c>.</value>
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the controller is set to read only.")]
		public bool ReadOnly
		{
			get
			{
				return m_ReadOnly;
			}
			set
			{
				if (m_ReadOnly != value)
				{
					m_ReadOnly = value;

					OnReadOnlyChanged();
					ApplyReadOnly(m_ReadOnly);
				}
			}
		}
		#endregion Private Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReadOnlyController"/> class.
		/// </summary>
		public ReadOnlyController()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReadOnlyController"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public ReadOnlyController(
			IContainer container)
			: this()
		{
			container.Add(this);
		}
		#endregion Constructors

		/// <summary>
		/// Called when [read only changed].
		/// </summary>
		protected virtual void OnReadOnlyChanged()
		{
			EventHandler<EventArgs<bool>> handler = m_ReadOnlyChanged;
			if (handler != null)
			{
				handler(this, new EventArgs<bool>(m_ReadOnly));
			}
		}

		/// <summary>
		/// Specifies whether this object can provide its extender properties to the specified object.
		/// </summary>
		/// <param name="extendee">The <see cref="T:System.Object"/> to receive the extender properties.</param>
		/// <returns>
		/// true if this object can provide extender properties to the specified object; otherwise, false.
		/// </returns>
		public bool CanExtend(
			object extendee)
		{
			if (extendee.GetType().GetProperty("ReadOnly") != null
			  || extendee.GetType().GetProperty("Enabled") != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the custom EnableReadOnly extender property added to extended controls.
		/// </summary>
		/// <param name="source">Control being extended.</param>
		[Category("Behavior")]
		public bool GetEnableReadOnlyControl(
			Control source)
		{
			if (m_Extendees.ContainsKey(source))
			{
				return m_Extendees[source];
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Sets the custom EnableReadOnly extender property added to extended controls.
		/// </summary>
		/// <param name="source">Control being extended.</param>
		/// <param name="value">The new value of property.</param>
		[Category("Behavior")]
		public void SetEnableReadOnlyControl(
			Control source,
			bool value)
		{
			if (m_Extendees.ContainsKey(source))
			{
				m_Extendees[source] = value;
			}
			else
			{
				m_Extendees.Add(source, value);
			}
		}

		/// <summary>
		/// Applies ReadOnly based on the value.
		/// </summary>
		/// <param name="value">if set to <c>true</c> [value].</param>
		private void ApplyReadOnly(
			bool value)
		{
			foreach (KeyValuePair<Control, bool> item in m_Extendees)
			{
				if (item.Value)
				{
					ApplyReadOnly(item.Key, value);
				}
			}
		}

		/// <summary>
		/// Applies ReadOnly based on the value.
		/// </summary>
		/// <param name="control">The control.</param>
		private void ApplyReadOnly(
			Control control,
			bool value)
		{
			PropertyInfo propertyInfo = control.GetType().GetProperty("ReadOnly");
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(control, value, null);
			}
			else
			{
				propertyInfo = control.GetType().GetProperty("Enabled");
				if (propertyInfo != null)
				{
					propertyInfo.SetValue(control, !value, null);
				}
			}
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Clear events
				m_ReadOnlyChanged = null;
			}

			base.Dispose(disposing);
		}
	}
}
