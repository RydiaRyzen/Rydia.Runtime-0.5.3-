using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Rydia.Graphics.Api.ES30;
using Rydia.Runtime;

namespace Rydia.Graphics.Api.ES20
{

    public static class RenderbufferTargetExtensions
    {

        private static IGLES30 GL
        {
            get
            {
                return RuntimeHost.GLES30;
            }
        }

        [Conditional("DEBUG")]
        public static void CheckBinding(this RenderbufferTarget target, object instance, [CallerMemberName] string memberName = null)
        {
            Debug.Assert(instance != null);
            GL.GetInteger(GetTargetBinding(target), out int currentBinding);
            if (currentBinding == 0)
                Debug.WriteLine($"The method {instance.ToString()}.{memberName} requires an object bound to {GetTargetName(target)}, but no object is bound to that target");
        }

        public static void CheckBinding(this RenderbufferTarget target, int expectedBinding, object instance, [CallerMemberName] string memberName = null)
        {
            Debug.Assert(instance != null);
            GL.GetInteger(GetTargetBinding(target), out int currentBinding);
            if (currentBinding != expectedBinding)
                Debug.WriteLine($"The method {instance.ToString()}.{memberName} requires the current object bound to {GetTargetName(target)}, but another object is bound to that target");
        }

        public static GetPName GetTargetBinding(this RenderbufferTarget target)
        {
            switch (target)
            {
                case RenderbufferTarget.Renderbuffer:
                    return GetPName.RenderbufferBinding;

                default:
                    Debug.Assert(false);
                    return (GetPName)0;
            }
        }

        private static string GetTargetName(RenderbufferTarget target)
        {
            switch (target)
            {
                case RenderbufferTarget.Renderbuffer:
                    return "Renderbuffer";
                default:
                    Debug.Assert(false);
                    return $"(unknown {target})";
            }
        }

    }

}
