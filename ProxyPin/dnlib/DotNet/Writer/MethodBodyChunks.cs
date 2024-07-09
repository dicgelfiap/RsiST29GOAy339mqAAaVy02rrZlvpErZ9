using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008BA RID: 2234
	[ComVisible(true)]
	public sealed class MethodBodyChunks : IChunk
	{
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06005648 RID: 22088 RVA: 0x001A5E54 File Offset: 0x001A5E54
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x001A5E5C File Offset: 0x001A5E5C
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600564A RID: 22090 RVA: 0x001A5E64 File Offset: 0x001A5E64
		public uint SavedBytes
		{
			get
			{
				return this.savedBytes;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600564B RID: 22091 RVA: 0x001A5E6C File Offset: 0x001A5E6C
		// (set) Token: 0x0600564C RID: 22092 RVA: 0x001A5E74 File Offset: 0x001A5E74
		internal bool CanReuseOldBodyLocation { get; set; }

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600564D RID: 22093 RVA: 0x001A5E80 File Offset: 0x001A5E80
		internal bool ReusedAllMethodBodyLocations
		{
			get
			{
				return this.tinyMethods.Count == 0 && this.fatMethods.Count == 0;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x001A5EA4 File Offset: 0x001A5EA4
		internal bool HasReusedMethods
		{
			get
			{
				return this.reusedMethods.Count > 0;
			}
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x001A5EB4 File Offset: 0x001A5EB4
		public MethodBodyChunks(bool shareBodies)
		{
			this.shareBodies = shareBodies;
			this.alignFatBodies = true;
			if (shareBodies)
			{
				this.tinyMethodsDict = new Dictionary<MethodBody, MethodBody>();
				this.fatMethodsDict = new Dictionary<MethodBody, MethodBody>();
			}
			this.tinyMethods = new List<MethodBody>();
			this.fatMethods = new List<MethodBody>();
			this.reusedMethods = new List<MethodBodyChunks.ReusedMethodInfo>();
			this.rvaToReusedMethod = new Dictionary<uint, MethodBody>();
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x001A5F24 File Offset: 0x001A5F24
		public MethodBody Add(MethodBody methodBody)
		{
			return this.Add(methodBody, (RVA)0U, 0U);
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x001A5F30 File Offset: 0x001A5F30
		internal MethodBody Add(MethodBody methodBody, RVA origRva, uint origSize)
		{
			if (this.setOffsetCalled)
			{
				throw new InvalidOperationException("SetOffset() has already been called");
			}
			if (this.CanReuseOldBodyLocation && origRva != (RVA)0U && origSize != 0U && methodBody.CanReuse(origRva, origSize))
			{
				MethodBody methodBody2;
				if (!this.rvaToReusedMethod.TryGetValue((uint)origRva, out methodBody2))
				{
					this.rvaToReusedMethod.Add((uint)origRva, methodBody);
					this.reusedMethods.Add(new MethodBodyChunks.ReusedMethodInfo(methodBody, origRva));
					return methodBody;
				}
				if (methodBody.Equals(methodBody2))
				{
					return methodBody2;
				}
			}
			if (this.shareBodies)
			{
				Dictionary<MethodBody, MethodBody> dictionary = methodBody.IsFat ? this.fatMethodsDict : this.tinyMethodsDict;
				MethodBody result;
				if (dictionary.TryGetValue(methodBody, out result))
				{
					this.savedBytes += (uint)methodBody.GetApproximateSizeOfMethodBody();
					return result;
				}
				dictionary[methodBody] = methodBody;
			}
			(methodBody.IsFat ? this.fatMethods : this.tinyMethods).Add(methodBody);
			return methodBody;
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x001A6034 File Offset: 0x001A6034
		public bool Remove(MethodBody methodBody)
		{
			if (methodBody == null)
			{
				throw new ArgumentNullException("methodBody");
			}
			if (this.setOffsetCalled)
			{
				throw new InvalidOperationException("SetOffset() has already been called");
			}
			if (this.CanReuseOldBodyLocation)
			{
				throw new InvalidOperationException("Reusing old body locations is enabled. Can't remove bodies.");
			}
			return (methodBody.IsFat ? this.fatMethods : this.tinyMethods).Remove(methodBody);
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x001A60A4 File Offset: 0x001A60A4
		internal void InitializeReusedMethodBodies(Func<RVA, FileOffset> getNewFileOffset)
		{
			foreach (MethodBodyChunks.ReusedMethodInfo reusedMethodInfo in this.reusedMethods)
			{
				FileOffset fileOffset = getNewFileOffset(reusedMethodInfo.RVA);
				reusedMethodInfo.MethodBody.SetOffset(fileOffset, reusedMethodInfo.RVA);
			}
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x001A6118 File Offset: 0x001A6118
		internal void WriteReusedMethodBodies(DataWriter writer, long destStreamBaseOffset)
		{
			foreach (MethodBodyChunks.ReusedMethodInfo reusedMethodInfo in this.reusedMethods)
			{
				if (reusedMethodInfo.MethodBody.RVA != reusedMethodInfo.RVA)
				{
					throw new InvalidOperationException();
				}
				writer.Position = destStreamBaseOffset + (long)((ulong)reusedMethodInfo.MethodBody.FileOffset);
				reusedMethodInfo.MethodBody.VerifyWriteTo(writer);
			}
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x001A61A8 File Offset: 0x001A61A8
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.setOffsetCalled = true;
			this.offset = offset;
			this.rva = rva;
			this.tinyMethodsDict = null;
			this.fatMethodsDict = null;
			RVA rva2 = rva;
			foreach (MethodBody methodBody in this.tinyMethods)
			{
				methodBody.SetOffset(offset, rva2);
				uint fileLength = methodBody.GetFileLength();
				rva2 += fileLength;
				offset += fileLength;
			}
			foreach (MethodBody methodBody2 in this.fatMethods)
			{
				if (this.alignFatBodies)
				{
					uint num = rva2.AlignUp(4U) - rva2;
					rva2 += num;
					offset += num;
				}
				methodBody2.SetOffset(offset, rva2);
				uint fileLength2 = methodBody2.GetFileLength();
				rva2 += fileLength2;
				offset += fileLength2;
			}
			this.length = rva2 - rva;
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x001A62B8 File Offset: 0x001A62B8
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x001A62C0 File Offset: 0x001A62C0
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x001A62C8 File Offset: 0x001A62C8
		public void WriteTo(DataWriter writer)
		{
			RVA rva = this.rva;
			foreach (MethodBody methodBody in this.tinyMethods)
			{
				methodBody.VerifyWriteTo(writer);
				rva += methodBody.GetFileLength();
			}
			foreach (MethodBody methodBody2 in this.fatMethods)
			{
				if (this.alignFatBodies)
				{
					int num = (int)(rva.AlignUp(4U) - rva);
					writer.WriteZeroes(num);
					rva += (uint)num;
				}
				methodBody2.VerifyWriteTo(writer);
				rva += methodBody2.GetFileLength();
			}
		}

		// Token: 0x0400295C RID: 10588
		private const uint FAT_BODY_ALIGNMENT = 4U;

		// Token: 0x0400295D RID: 10589
		private Dictionary<MethodBody, MethodBody> tinyMethodsDict;

		// Token: 0x0400295E RID: 10590
		private Dictionary<MethodBody, MethodBody> fatMethodsDict;

		// Token: 0x0400295F RID: 10591
		private readonly List<MethodBody> tinyMethods;

		// Token: 0x04002960 RID: 10592
		private readonly List<MethodBody> fatMethods;

		// Token: 0x04002961 RID: 10593
		private readonly List<MethodBodyChunks.ReusedMethodInfo> reusedMethods;

		// Token: 0x04002962 RID: 10594
		private readonly Dictionary<uint, MethodBody> rvaToReusedMethod;

		// Token: 0x04002963 RID: 10595
		private readonly bool shareBodies;

		// Token: 0x04002964 RID: 10596
		private FileOffset offset;

		// Token: 0x04002965 RID: 10597
		private RVA rva;

		// Token: 0x04002966 RID: 10598
		private uint length;

		// Token: 0x04002967 RID: 10599
		private bool setOffsetCalled;

		// Token: 0x04002968 RID: 10600
		private readonly bool alignFatBodies;

		// Token: 0x04002969 RID: 10601
		private uint savedBytes;

		// Token: 0x0200101F RID: 4127
		private readonly struct ReusedMethodInfo
		{
			// Token: 0x06008F67 RID: 36711 RVA: 0x002AC0D4 File Offset: 0x002AC0D4
			public ReusedMethodInfo(MethodBody methodBody, RVA rva)
			{
				this.MethodBody = methodBody;
				this.RVA = rva;
			}

			// Token: 0x040044B8 RID: 17592
			public readonly MethodBody MethodBody;

			// Token: 0x040044B9 RID: 17593
			public readonly RVA RVA;
		}
	}
}
