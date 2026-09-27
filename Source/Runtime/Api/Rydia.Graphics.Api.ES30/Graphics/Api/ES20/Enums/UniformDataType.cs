using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Supported data types for uniforms
	/// </summary>
	public enum UniformDataType
	{
		/// <summary>
		/// Single-precision floating-point type.
		/// Corresponds to C#'s float type.
		/// </summary>
		Float = 5126,

		/// <summary>
		/// A vector of 2 floats.
		/// Corresponds to OpenTK's Vector2 type.
		/// </summary>
		Vector2 = 35664,

		/// <summary>
		/// A vector of 3 floats.
		/// Corresponds to OpenTK's Vector3 type.
		/// </summary>
		Vector3 = 35665,

		/// <summary>
		/// A vector of 4 floats.
		/// Corresponds to OpenTK's Vector4 type.
		/// </summary>
		Vector4 = 35666,

		/// <summary>
		/// 32-bit integer type.
		/// Corresponds to C#'s int type.
		/// </summary>
		Int = 5124,

		/// <summary>
		/// A vector of 2 integers.
		/// </summary>
		IVector2 = 35667,

		/// <summary>
		/// A vector of 3 integers.
		/// </summary>
		IVector3 = 35668,

		/// <summary>
		/// A vector of 4 integers.
		/// </summary>
		IVector4 = 35669,

		/// <summary>
		/// Boolean type.
		/// Corresponds to C#'s bool type.
		/// </summary>
		Bool = 35670,

		/// <summary>
		/// A vector of 2 booleans.
		/// </summary>
		BVector2 = 35671,

		/// <summary>
		/// A vector of 3 booleans.
		/// </summary>
		BVector3 = 35672,

		/// <summary>
		/// A vector of 4 booleans.
		/// </summary>
		BVector4 = 35673,

		/// <summary>
		/// A 2x2 matrix of floats.
		/// Corresponds to OpenTK's Matrix2 type.
		/// </summary>
		Matrix2 = 35674,

		/// <summary>
		/// A 3x3 matrix of floats.
		/// Corresponds to OpenTK's Matrix3 type.
		/// </summary>
		Matrix3 = 35675,

		/// <summary>
		/// A 4x4 matrix of floats.
		/// Corresponds to OpenTK's Matrix4 type.
		/// </summary>
		Matrix4 = 35676,

		/// <summary>
		/// A 2D texture sampler.
		/// </summary>
		Sampler2D = 35678,

		/// <summary>
		/// A cubetexture sampler.
		/// </summary>
		SamplerCube = 35680
	}

}
