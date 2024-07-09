using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000886 RID: 2182
	[ComVisible(true)]
	public enum VariantType : uint
	{
		// Token: 0x040027EB RID: 10219
		Empty,
		// Token: 0x040027EC RID: 10220
		None = 0U,
		// Token: 0x040027ED RID: 10221
		Null,
		// Token: 0x040027EE RID: 10222
		I2,
		// Token: 0x040027EF RID: 10223
		I4,
		// Token: 0x040027F0 RID: 10224
		R4,
		// Token: 0x040027F1 RID: 10225
		R8,
		// Token: 0x040027F2 RID: 10226
		CY,
		// Token: 0x040027F3 RID: 10227
		Date,
		// Token: 0x040027F4 RID: 10228
		BStr,
		// Token: 0x040027F5 RID: 10229
		Dispatch,
		// Token: 0x040027F6 RID: 10230
		Error,
		// Token: 0x040027F7 RID: 10231
		Bool,
		// Token: 0x040027F8 RID: 10232
		Variant,
		// Token: 0x040027F9 RID: 10233
		Unknown,
		// Token: 0x040027FA RID: 10234
		Decimal,
		// Token: 0x040027FB RID: 10235
		I1 = 16U,
		// Token: 0x040027FC RID: 10236
		UI1,
		// Token: 0x040027FD RID: 10237
		UI2,
		// Token: 0x040027FE RID: 10238
		UI4,
		// Token: 0x040027FF RID: 10239
		I8,
		// Token: 0x04002800 RID: 10240
		UI8,
		// Token: 0x04002801 RID: 10241
		Int,
		// Token: 0x04002802 RID: 10242
		UInt,
		// Token: 0x04002803 RID: 10243
		Void,
		// Token: 0x04002804 RID: 10244
		HResult,
		// Token: 0x04002805 RID: 10245
		Ptr,
		// Token: 0x04002806 RID: 10246
		SafeArray,
		// Token: 0x04002807 RID: 10247
		CArray,
		// Token: 0x04002808 RID: 10248
		UserDefined,
		// Token: 0x04002809 RID: 10249
		LPStr,
		// Token: 0x0400280A RID: 10250
		LPWStr,
		// Token: 0x0400280B RID: 10251
		Record = 36U,
		// Token: 0x0400280C RID: 10252
		IntPtr,
		// Token: 0x0400280D RID: 10253
		UIntPtr,
		// Token: 0x0400280E RID: 10254
		FileTime = 64U,
		// Token: 0x0400280F RID: 10255
		Blob,
		// Token: 0x04002810 RID: 10256
		Stream,
		// Token: 0x04002811 RID: 10257
		Storage,
		// Token: 0x04002812 RID: 10258
		StreamedObject,
		// Token: 0x04002813 RID: 10259
		StoredObject,
		// Token: 0x04002814 RID: 10260
		BlobObject,
		// Token: 0x04002815 RID: 10261
		CF,
		// Token: 0x04002816 RID: 10262
		CLSID,
		// Token: 0x04002817 RID: 10263
		VersionedStream,
		// Token: 0x04002818 RID: 10264
		BStrBlob = 4095U,
		// Token: 0x04002819 RID: 10265
		Vector,
		// Token: 0x0400281A RID: 10266
		Array = 8192U,
		// Token: 0x0400281B RID: 10267
		ByRef = 16384U,
		// Token: 0x0400281C RID: 10268
		Reserved = 32768U,
		// Token: 0x0400281D RID: 10269
		Illegal = 65535U,
		// Token: 0x0400281E RID: 10270
		IllegalMasked = 4095U,
		// Token: 0x0400281F RID: 10271
		TypeMask = 4095U,
		// Token: 0x04002820 RID: 10272
		NotInitialized = 4294967295U
	}
}
