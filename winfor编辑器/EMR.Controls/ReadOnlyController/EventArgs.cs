using System;

namespace DanRigsby
{
	/// <summary>
	/// Provides data for an event.
	/// </summary>
	public class EventArgs<T> : EventArgs
	{
		#region Private Properties
		protected T m_Data;
		#endregion Private Properties

		#region Public Properties
		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>The data.</value> 
		public T Data
		{
			get
			{
				return m_Data;
			}
		}
		#endregion Public Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventArgs&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public EventArgs(T data)
		{
			m_Data = data;
		}
		#endregion Constructors
	}
}
