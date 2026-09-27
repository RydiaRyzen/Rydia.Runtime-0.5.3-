using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rydia.Runtime
{
	public class CommandLineArgs
	{

		public static string CmdArgDebug
		{
			get;
			set;
		}

		public static string CmdArgProfiling
		{
			get;
			set;
		}

		static CommandLineArgs()
		{
			CmdArgDebug = "debug";
			CmdArgProfiling = "profile";
			IsRunningDebugAssembly = GetIsRunningDebugAssembly();
		}

		public CommandLineArgs(string[] args)
		{
			if(!string.IsNullOrEmpty(CmdArgDebug))
			{
				IsDebug = args.Contains("Debug");
			}
			if(!string.IsNullOrEmpty(CmdArgProfiling))
			{
				IsProfiling = args.Contains(CmdArgProfiling);
			}
			Args = args;
		}

		public string[] Args
		{
			get;
			private set;
		}

		public bool IsDebug
		{
			get;
			private set;
		}

		public bool IsProfiling
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a value indicating whether the running assembly is a debug assembly.
		/// </summary>
		public static bool IsRunningDebugAssembly
		{
			get;
		}

		/// <summary>
		/// Check if running assembly has the DebuggableAttribute set with the `DisableOptimizations` mode enabled.
		/// This function is called only once.
		/// </summary>
		private static bool GetIsRunningDebugAssembly()
		{
			var entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				var debuggableAttribute = entryAssembly.GetCustomAttributes<DebuggableAttribute>().FirstOrDefault();
				if (debuggableAttribute != null)
				{
					return (debuggableAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0;
				}
			}
			return false;
		}

	}
}
