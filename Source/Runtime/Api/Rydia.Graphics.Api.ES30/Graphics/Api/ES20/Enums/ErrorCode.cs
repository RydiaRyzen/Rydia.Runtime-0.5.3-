using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{


	public enum ErrorCode
	{
		//
		// 概要:
		//     Original was GL_NO_ERROR = 0
		NoError = ESAllEnum.NoError,
		//
		// 概要:
		//     Original was GL_INVALID_ENUM = 0x0500
		InvalidEnum = 1280,
		//
		// 概要:
		//     Original was GL_INVALID_VALUE = 0x0501
		InvalidValue = 1281,
		//
		// 概要:
		//     Original was GL_INVALID_OPERATION = 0x0502
		InvalidOperation = 1282,
		//
		// 概要:
		//     Original was GL_STACK_OVERFLOW = 0x0503
		StackOverflow = 1283,
		//
		// 概要:
		//     Original was GL_STACK_UNDERFLOW = 0x0504
		StackUnderflow = 1284,
		//
		// 概要:
		//     Original was GL_OUT_OF_MEMORY = 0x0505
		OutOfMemory = 1285,
		//
		// 概要:
		//     Original was GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506
		InvalidFramebufferOperation = 1286,
		//
		// 概要:
		//     Original was GL_INVALID_FRAMEBUFFER_OPERATION_EXT = 0x0506
		InvalidFramebufferOperationExt = 1286,
		//
		// 概要:
		//     Original was GL_INVALID_FRAMEBUFFER_OPERATION_OES = 0x0506
		InvalidFramebufferOperationOes = 1286,
		//
		// 概要:
		//     Original was GL_CONTEXT_LOST = 0x0507
		ContextLost = 1287,
		//
		// 概要:
		//     Original was GL_TABLE_TOO_LARGE = 0x8031
		TableTooLarge = 32817,
		//
		// 概要:
		//     Original was GL_TABLE_TOO_LARGE_EXT = 0x8031
		TableTooLargeExt = 32817,
		//
		// 概要:
		//     Original was GL_TEXTURE_TOO_LARGE_EXT = 0x8065
		TextureTooLargeExt = 32869
	}

}
