using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Supported data types for attributes
	/// </summary>
	public enum AttributeDataType
	{
		/// <summary>
		/// Single-precision floating-point type.
		/// Corresponds to C#'s float type.
		/// </summary>
		Float = ESAllEnum.Float,

		/// <summary>
		/// A vector of 2 floats.
		/// Corresponds to OpenTK's Vector2 type.
		/// </summary>
		Vector2 = ESAllEnum.FloatVec2,

		/// <summary>
		/// A vector of 3 floats.
		/// Corresponds to OpenTK's Vector3 type.
		/// </summary>
		Vector3 = ESAllEnum.FloatVec3,

		/// <summary>
		/// A vector of 4 floats.
		/// Corresponds to OpenTK's Vector4 type.
		/// </summary>
		Vector4 = ESAllEnum.FloatVec4,

		/// <summary>
		/// A 2x2 matrix of floats.
		/// Corresponds to OpenTK's Matrix2 type.
		/// </summary>
		Matrix2 = ESAllEnum.FloatMat2,

		/// <summary>
		/// A 3x3 matrix of floats.
		/// Corresponds to OpenTK's Matrix3 type.
		/// </summary>
		Matrix3 = ESAllEnum.FloatMat3,

		/// <summary>
		/// A 4x4 matrix of floats.
		/// Corresponds to OpenTK's Matrix4 type.
		/// </summary>
		Matrix4 = ESAllEnum.FloatMat4,
	}

}
