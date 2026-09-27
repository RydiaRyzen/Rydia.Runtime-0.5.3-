using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Text;
using System;


using Rydia.Drawing;
using Rydia.IO;
using Rydia.Diagnostics.Profiling;

namespace Rydia
{
	/// <summary>
	/// This class houses several performance counters and performance measurement utility
	/// </summary>
	public static class Profile
	{
        private static Dictionary<string, ProfileCounter> counterMap = new Dictionary<string, ProfileCounter>();

		public static readonly TimeCounter TimeFrame;
		public static readonly TimeCounter TimeUpdate;
		public static readonly TimeCounter TimeUpdateScene;
		public static readonly TimeCounter TimeUpdateSceneComponents;
		public static readonly TimeCounter TimeUpdateAudio;
		public static readonly TimeCounter TimeUpdatePhysics;
		public static readonly TimeCounter TimeUpdatePhysicsContacts;
		public static readonly TimeCounter TimeUpdatePhysicsController;
		public static readonly TimeCounter TimeUpdatePhysicsContinous;
		public static readonly TimeCounter TimeUpdatePhysicsAddRemove;
		public static readonly TimeCounter TimeUpdatePhysicsSolve;
		public static readonly TimeCounter TimeRender;
		public static readonly TimeCounter TimeSwapBuffers;
		public static readonly TimeCounter TimeQueryVisibleRenderers;
		public static readonly TimeCounter TimeCollectDrawcalls;
		public static readonly TimeCounter TimeOptimizeDrawcalls;
		public static readonly TimeCounter TimeProcessDrawcalls;
		public static readonly TimeCounter TimeLog;
		public static readonly TimeCounter TimeVisualPicking;
		public static readonly TimeCounter TimeUnaccounted;

		public static readonly StatCounter StatNumPlaying2D;
		public static readonly StatCounter StatNumPlaying3D;
		public static readonly StatCounter StatNumDrawcalls;
		public static readonly StatCounter StatNumRawBatches;
		public static readonly StatCounter StatNumMergedBatches;
		public static readonly StatCounter StatNumOptimizedBatches;
		public static readonly StatCounter StatMemoryTotalUsage;
		public static readonly StatCounter StatMemoryGarbageCollect0;
		public static readonly StatCounter StatMemoryGarbageCollect1;
		public static readonly StatCounter StatMemoryGarbageCollect2;

		static Profile()
		{
            var root = new ProfilingKey("Rydia");
            var frame = root.AddChild("Frame");
                var update = frame.AddChild("Update");
                    var physics = update.AddChild("Physics");
                        var contacts = physics.AddChild("Contacts");
                        var controller = physics.AddChild("Controller");
                        var continous = physics.AddChild("Continous");
                        var addRemove = physics.AddChild("AddRemove");
                        var solve = physics.AddChild("Solve");
                        var scene = update.AddChild("Scene");
                        var allComponents = scene.AddChild("All Components");
                    var audio = update.AddChild("Audio");
                var render = frame.AddChild("Render");
                    var swapBuffers = render.AddChild("SwapBuffers");
                    var queryVisibleRenderers = render.AddChild("QueryVisibleRenderers");
            TimeFrame                   = Request<TimeCounter>(@"Rydia\Frame");
			TimeUpdate                  = Request<TimeCounter>(@"Rydia\Frame\Update");
			TimeUpdatePhysics           = Request<TimeCounter>(@"Rydia\Frame\Update\Physics");
			TimeUpdatePhysicsContacts   = Request<TimeCounter>(@"Rydia\Frame\Update\Physics\Contacts");
			TimeUpdatePhysicsController = Request<TimeCounter>(@"Rydia\Frame\Update\Physics\Controller");
			TimeUpdatePhysicsContinous  = Request<TimeCounter>(@"Rydia\Frame\Update\Physics\Continous");
			TimeUpdatePhysicsAddRemove  = Request<TimeCounter>(@"Rydia\Frame\Update\Physics\AddRemove");
			TimeUpdatePhysicsSolve      = Request<TimeCounter>(@"Rydia\Frame\Update\Physics\Solve");
			TimeUpdateScene             = Request<TimeCounter>(@"Rydia\Frame\Update\Scene");
			TimeUpdateSceneComponents   = Request<TimeCounter>(@"Rydia\Frame\Update\Scene\All Components");
			TimeUpdateAudio             = Request<TimeCounter>(@"Rydia\Frame\Update\Audio");
			TimeRender                  = Request<TimeCounter>(@"Rydia\Frame\Render");
			TimeSwapBuffers             = Request<TimeCounter>(@"Rydia\Frame\Render\SwapBuffers");
			TimeQueryVisibleRenderers   = Request<TimeCounter>(@"Rydia\Frame\Render\QueryVisibleRenderers");
			TimeCollectDrawcalls        = Request<TimeCounter>(@"Rydia\Frame\Render\CollectDrawcalls");
			TimeOptimizeDrawcalls       = Request<TimeCounter>(@"Rydia\Frame\Render\OptimizeDrawcalls");
			TimeProcessDrawcalls        = Request<TimeCounter>(@"Rydia\Frame\Render\ProcessDrawcalls");
			TimeLog                     = Request<TimeCounter>(@"Rydia\Frame\Log");
			TimeVisualPicking           = Request<TimeCounter>(@"Rydia\Frame\VisualPicking");
			TimeUnaccounted             = Request<TimeCounter>(@"Rydia\Frame\Unaccounted");

			StatNumPlaying2D            = Request<StatCounter>(@"Rydia\Stats\Audio\NumPlaying2D");
			StatNumPlaying3D            = Request<StatCounter>(@"Rydia\Stats\Audio\NumPlaying3D");
			StatNumDrawcalls            = Request<StatCounter>(@"Rydia\Stats\Render\NumDrawcalls");
			StatNumRawBatches           = Request<StatCounter>(@"Rydia\Stats\Render\NumRawBatches");
			StatNumMergedBatches        = Request<StatCounter>(@"Rydia\Stats\Render\NumMergedBatches");
			StatNumOptimizedBatches     = Request<StatCounter>(@"Rydia\Stats\Render\NumOptimizedBatches");
			StatMemoryTotalUsage        = Request<StatCounter>(@"Rydia\Stats\Memory\TotalUsage");
			StatMemoryGarbageCollect0   = Request<StatCounter>(@"Rydia\Stats\Memory\GarbageCollect0");
			StatMemoryGarbageCollect1   = Request<StatCounter>(@"Rydia\Stats\Memory\GarbageCollect1");
			StatMemoryGarbageCollect2   = Request<StatCounter>(@"Rydia\Stats\Memory\GarbageCollect2");

			StatMemoryGarbageCollect0.IsSingleValue = true;
			StatMemoryGarbageCollect1.IsSingleValue = true;
			StatMemoryGarbageCollect2.IsSingleValue = true;
		}

		/// <summary>
		/// Completely resets all <see cref="ProfileCounter"/> instances, discarding
		/// all data that has been collected so far and starting over.
		/// </summary>
		public static void ResetCounters()
		{
			foreach (var pair in counterMap)
			{
				pair.Value.ResetAll();
			}
		}

		/// <summary>
		/// Returns an existing <see cref="ProfileCounter"/> with the specified name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		/// <returns></returns>
		public static T GetCounter<T>(string name) where T : ProfileCounter
		{
			if (name == null) return null;

			ProfileCounter c;
			if (!counterMap.TryGetValue(name, out c)) return null;

			T cc = c as T;
			if (cc == null) 
                throw new InvalidOperationException(
                    string.Format("The specified performance counter '{0}' is not a {1}.", name, typeof(T).Name));
			return cc;
		}
		/// <summary>
		/// Returns an existing <see cref="ProfileCounter"/> with the specified name, or creates one if none is found.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		/// <returns></returns>
		public static T Request<T>(string name) where T : ProfileCounter, new()
		{
			if (name == null) return null;

			T c = GetCounter<T>(name);
			if (c != null) return c;
			
			c = new T();
			c.FullName = name;
			counterMap[name] = c;
			return c;
		}
		/// <summary>
		/// Enumerates all <see cref="ProfileCounter"/> objects that have been actively used this frame.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<ProfileCounter> GetUsedCounters()
		{
			return counterMap.Values.Where(p => p.WasUsed);
		}

		/// <summary>
		/// Begins time measurement using a new or existing <see cref="ProfileCounter"/> with the specified name.
		/// </summary>
		/// <param name="counter">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		/// <returns></returns>
		public static TimeCounter BeginMeasure(string counter)
		{
			TimeCounter tc = Request<TimeCounter>(counter);
			tc.BeginMeasure();
			return tc;
		}
		/// <summary>
		/// Ends time measurement using an existing <see cref="ProfileCounter"/> with the specified name.
		/// </summary>
		/// <param name="counter">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		public static void EndMeasure(string counter)
		{
			TimeCounter tc = Request<TimeCounter>(counter);
			tc.EndMeasure();
		}
		/// <summary>
		/// Queries this frames time measurement value from an existing <see cref="ProfileCounter"/> with the specified name.
		/// </summary>
		/// <param name="counter">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		/// <returns></returns>
		public static float GetMeasure(string counter)
		{
			TimeCounter tc = GetCounter<TimeCounter>(counter);
			if (tc != null)
				return tc.LastValue;
			else
				return 0.0f;
		}

		/// <summary>
		/// Accumulates a statistical information value to a new or existing <see cref="ProfileCounter"/> with the specified name.
		/// </summary>
		/// <param name="counter">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		/// <param name="value"></param>
		public static void AddToStat(string counter, int value)
		{
			StatCounter sc = Request<StatCounter>(counter);
			sc.Add(value);
		}
		/// <summary>
		/// Queries a statistical information value from an existing <see cref="ProfileCounter"/> with the specified name.
		/// </summary>
		/// <param name="counter">The <see cref="ProfileCounter"/> name to use for this measurement. For nested measurements, use path strings, e.g. "ParentCounter\ChildCounter"</param>
		/// <param name="value"></param>
		public static int GetStat(string counter)
		{
			StatCounter sc = Request<StatCounter>(counter);
			if (sc != null)
				return sc.LastValue;
			else
				return 0;
		}

		/// <summary>
		/// Saves a text report of the current profiling data to the specified file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void SaveTextReport(string filePath)
		{
			using (Stream str = File.Create(filePath))
			{
				SaveTextReport(str);
			}
		}

		/// <summary>
		/// Saves a text report of the current profiling data to the specified stream.
		/// </summary>
		/// <param name="filePath"></param>
		public static void SaveTextReport(Stream stream)
		{
			string report = GetTextReport(counterMap.Values.Where(c => c.HasData), 
				ProfileReportOptions.GroupHeader | 
				ProfileReportOptions.Header | 
				ProfileReportOptions.AverageValue |
				ProfileReportOptions.MaxValue | 
				ProfileReportOptions.MinValue |
				ProfileReportOptions.SampleCount);
			using (StreamWriter writer = new StreamWriter(stream))
			{
				writer.Write(report);
			}
		}
		/// <summary>
		/// Creates a text report of the current profiling data and returns it as string.
		/// </summary>
		/// <param name="filePath"></param>
		public static string GetTextReport(IEnumerable<ProfileCounter> reportCounters, ProfileReportOptions options = ProfileReportOptions.LastValue)
		{
			bool omitMinor = (options & ProfileReportOptions.OmitMinorValues) != ProfileReportOptions.None;

			// Group Counters by Type
			Dictionary<Type,List<ProfileCounter>> countersByType = new Dictionary<Type,List<ProfileCounter>>();
			Type[] existingTypes = reportCounters.Select(c => c.GetType()).Distinct().ToArray();
			foreach (Type type in existingTypes)
			{
				countersByType[type] = reportCounters.Where(c => c.GetType() == type).ToList();
			}

			// Prepare text building
			StringBuilder reportBuilder = new StringBuilder(countersByType.Count * 256);

			// Handle each group separately
			foreach (var pair in countersByType)
			{
				IEnumerable<ProfileCounter> counters = pair.Value;
				int minDepth = counters.Min(c => c.ParentDepth);
				IEnumerable<ProfileCounter> rootCounters = counters.Where(c => c.ParentDepth == minDepth);

				int maxNameLen	= counters.Max(c => c.DisplayName.Length + c.ParentDepth * 2);
					
				if (options.HasFlag(ProfileReportOptions.GroupHeader))
				{
					reportBuilder.Append(Environment.NewLine);
					reportBuilder.AppendLine(("[ " + pair.Key.Name + " ]").PadLeft(35, '-').PadRight(50,'-'));
					reportBuilder.Append(Environment.NewLine);
				}
				else if (reportBuilder.Length > 0)
				{
					reportBuilder.Append(Environment.NewLine);
				}

				if (options.HasFlag(ProfileReportOptions.Header))
				{
					reportBuilder.Append("Name");
					reportBuilder.Append(' ', 1 + Math.Max((1 + maxNameLen) - "Name".Length, 0));

					if (options.HasFlag(ProfileReportOptions.LastValue))
						reportBuilder.Append("   Last Value ");
					if (options.HasFlag(ProfileReportOptions.AverageValue))
						reportBuilder.Append("   Avg. Value ");
					if (options.HasFlag(ProfileReportOptions.MinValue))
						reportBuilder.Append("   Min. Value ");
					if (options.HasFlag(ProfileReportOptions.MaxValue))
						reportBuilder.Append("   Max. Value ");
					if (options.HasFlag(ProfileReportOptions.SampleCount))
						reportBuilder.Append("        Samples ");

					reportBuilder.Append(Environment.NewLine);

				}
				Stack<ProfileCounter> appendStack = new Stack<ProfileCounter>(rootCounters.Reverse());
				while (appendStack.Count > 0)
				{
					ProfileCounter current = appendStack.Pop();

					ProfileReportCounterData data;
					current.GetReportData(out data);
					if (omitMinor && data.Severity <= 0.005f)
						continue;
					
					reportBuilder.Append(' ', current.ParentDepth * 2);
					reportBuilder.Append(current.DisplayName);
					reportBuilder.Append(':');
					reportBuilder.Append(' ', 1 + Math.Max((1 + maxNameLen) - (current.ParentDepth * 2 + current.DisplayName.Length + 1), 0));

					if (options.HasFlag(ProfileReportOptions.LastValue))
					{
						string valStr = data.LastValue ?? "-";
						reportBuilder.Append(' ', Math.Max(13 - valStr.Length, 0));
						reportBuilder.Append(valStr);
						reportBuilder.Append(' ');
					}
					if (options.HasFlag(ProfileReportOptions.AverageValue))
					{
						string valStr = data.AverageValue ?? "-";
						reportBuilder.Append(' ', Math.Max(13 - valStr.Length, 0));
						reportBuilder.Append(valStr);
						reportBuilder.Append(' ');
					}
					if (options.HasFlag(ProfileReportOptions.MinValue))
					{
						string valStr = data.MinValue ?? "-";
						reportBuilder.Append(' ', Math.Max(13 - valStr.Length, 0));
						reportBuilder.Append(valStr);
						reportBuilder.Append(' ');
					}
					if (options.HasFlag(ProfileReportOptions.MaxValue))
					{
						string valStr = data.MaxValue ?? "-";
						reportBuilder.Append(' ', Math.Max(13 - valStr.Length, 0));
						reportBuilder.Append(valStr);
						reportBuilder.Append(' ');
					}
					if (options.HasFlag(ProfileReportOptions.SampleCount))
					{
						string valStr = data.SampleCount ?? "-";
						reportBuilder.Append(' ', Math.Max(15 - valStr.Length, 0));
						reportBuilder.Append(valStr);
						reportBuilder.Append(' ');
					}
					reportBuilder.Append(Environment.NewLine);

					IEnumerable<ProfileCounter> childCounters = counters.Where(c => c.Parent == current);
					foreach (ProfileCounter child in childCounters.Reverse())
						appendStack.Push(child);
				}
			}


			return reportBuilder.ToString();;
		}

		internal static void FrameTick()
		{
			// Calculate unaccounted for frame time
			TimeUnaccounted.Add(TimeFrame.Value
				- TimeUpdate.Value
				- TimeRender.Value
				- TimeLog.Value
				- TimeVisualPicking.Value);

			// Collect more globally available data
			StatMemoryTotalUsage.Add((int)(GC.GetTotalMemory(false) / 1024L));
			StatMemoryGarbageCollect0.Add(GC.CollectionCount(0));
			StatMemoryGarbageCollect1.Add(GC.CollectionCount(1));
			StatMemoryGarbageCollect2.Add(GC.CollectionCount(2));

			// Run frametick counter operations
			foreach (ProfileCounter c in counterMap.Values.ToArray())
			{
				c.TickFrame();
			}
		}
	}
}
