using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Audio.Api.OpenAL
{

	public class ALException : Exception
	{

		/// <summary>
		///   <see cref="AudioException"/> class の新しいインスタンスを初期化します
		/// </summary>
		public ALException()
		{
			
		}

		/// <summary>
		/// Constructs a new Veldridexception with the given message.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public ALException(string message) : base(message)
		{
		}

		/// <summary>
		/// Constructs a new Veldridexception with the given message and inner exception.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		public ALException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

	}


}
