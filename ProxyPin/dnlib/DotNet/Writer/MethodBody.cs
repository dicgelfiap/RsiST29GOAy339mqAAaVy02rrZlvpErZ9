using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B9 RID: 2233
	[ComVisible(true)]
	public sealed class MethodBody : IChunk
	{
		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06005635 RID: 22069 RVA: 0x001A5BF8 File Offset: 0x001A5BF8
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06005636 RID: 22070 RVA: 0x001A5C00 File Offset: 0x001A5C00
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06005637 RID: 22071 RVA: 0x001A5C08 File Offset: 0x001A5C08
		public byte[] Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06005638 RID: 22072 RVA: 0x001A5C10 File Offset: 0x001A5C10
		public byte[] ExtraSections
		{
			get
			{
				return this.extraSections;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06005639 RID: 22073 RVA: 0x001A5C18 File Offset: 0x001A5C18
		public uint LocalVarSigTok
		{
			get
			{
				return this.localVarSigTok;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x0600563A RID: 22074 RVA: 0x001A5C20 File Offset: 0x001A5C20
		public bool IsFat
		{
			get
			{
				return !this.isTiny;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x001A5C2C File Offset: 0x001A5C2C
		public bool IsTiny
		{
			get
			{
				return this.isTiny;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600563C RID: 22076 RVA: 0x001A5C34 File Offset: 0x001A5C34
		public bool HasExtraSections
		{
			get
			{
				return this.extraSections != null && this.extraSections.Length != 0;
			}
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x001A5C50 File Offset: 0x001A5C50
		public MethodBody(byte[] code) : this(code, null, 0U)
		{
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x001A5C5C File Offset: 0x001A5C5C
		public MethodBody(byte[] code, byte[] extraSections) : this(code, extraSections, 0U)
		{
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x001A5C68 File Offset: 0x001A5C68
		public MethodBody(byte[] code, byte[] extraSections, uint localVarSigTok)
		{
			this.isTiny = ((code[0] & 3) == 2);
			this.code = code;
			this.extraSections = extraSections;
			this.localVarSigTok = localVarSigTok;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x001A5C94 File Offset: 0x001A5C94
		public int GetApproximateSizeOfMethodBody()
		{
			int num = this.code.Length;
			if (this.extraSections != null)
			{
				num = Utils.AlignUp(num, 4U);
				num += this.extraSections.Length;
				num = Utils.AlignUp(num, 4U);
			}
			return num;
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x001A5CD8 File Offset: 0x001A5CD8
		internal bool CanReuse(RVA origRva, uint origSize)
		{
			uint num;
			if (this.HasExtraSections)
			{
				num = (origRva + (uint)this.code.Length).AlignUp(4U) + (uint)this.extraSections.Length - origRva;
			}
			else
			{
				num = (uint)this.code.Length;
			}
			return num <= origSize;
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x001A5D28 File Offset: 0x001A5D28
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
			if (this.HasExtraSections)
			{
				RVA rva2 = rva + (uint)this.code.Length;
				rva2 = rva2.AlignUp(4U);
				rva2 += (uint)this.extraSections.Length;
				this.length = rva2 - rva;
				return;
			}
			this.length = (uint)this.code.Length;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x001A5D88 File Offset: 0x001A5D88
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x001A5D90 File Offset: 0x001A5D90
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x001A5D98 File Offset: 0x001A5D98
		public void WriteTo(DataWriter writer)
		{
			writer.WriteBytes(this.code);
			if (this.HasExtraSections)
			{
				RVA rva = this.rva + (uint)this.code.Length;
				writer.WriteZeroes((int)(rva.AlignUp(4U) - rva));
				writer.WriteBytes(this.extraSections);
			}
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x001A5DEC File Offset: 0x001A5DEC
		public override int GetHashCode()
		{
			return Utils.GetHashCode(this.code) + Utils.GetHashCode(this.extraSections);
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x001A5E08 File Offset: 0x001A5E08
		public override bool Equals(object obj)
		{
			MethodBody methodBody = obj as MethodBody;
			return methodBody != null && Utils.Equals(this.code, methodBody.code) && Utils.Equals(this.extraSections, methodBody.extraSections);
		}

		// Token: 0x04002954 RID: 10580
		private const uint EXTRA_SECTIONS_ALIGNMENT = 4U;

		// Token: 0x04002955 RID: 10581
		private readonly bool isTiny;

		// Token: 0x04002956 RID: 10582
		private readonly byte[] code;

		// Token: 0x04002957 RID: 10583
		private readonly byte[] extraSections;

		// Token: 0x04002958 RID: 10584
		private uint length;

		// Token: 0x04002959 RID: 10585
		private FileOffset offset;

		// Token: 0x0400295A RID: 10586
		private RVA rva;

		// Token: 0x0400295B RID: 10587
		private readonly uint localVarSigTok;
	}
}
