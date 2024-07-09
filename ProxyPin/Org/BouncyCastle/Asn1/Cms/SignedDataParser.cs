using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000121 RID: 289
	public class SignedDataParser
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00048138 File Offset: 0x00048138
		public static SignedDataParser GetInstance(object o)
		{
			if (o is Asn1Sequence)
			{
				return new SignedDataParser(((Asn1Sequence)o).Parser);
			}
			if (o is Asn1SequenceParser)
			{
				return new SignedDataParser((Asn1SequenceParser)o);
			}
			throw new IOException("unknown object encountered: " + Platform.GetTypeName(o));
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00048194 File Offset: 0x00048194
		public SignedDataParser(Asn1SequenceParser seq)
		{
			this._seq = seq;
			this._version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x000481B4 File Offset: 0x000481B4
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000481BC File Offset: 0x000481BC
		public Asn1SetParser GetDigestAlgorithms()
		{
			return (Asn1SetParser)this._seq.ReadObject();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000481D0 File Offset: 0x000481D0
		public ContentInfoParser GetEncapContentInfo()
		{
			return new ContentInfoParser((Asn1SequenceParser)this._seq.ReadObject());
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000481E8 File Offset: 0x000481E8
		public Asn1SetParser GetCertificates()
		{
			this._certsCalled = true;
			this._nextObject = this._seq.ReadObject();
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 0)
			{
				Asn1SetParser result = (Asn1SetParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(17, false);
				this._nextObject = null;
				return result;
			}
			return null;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0004825C File Offset: 0x0004825C
		public Asn1SetParser GetCrls()
		{
			if (!this._certsCalled)
			{
				throw new IOException("GetCerts() has not been called.");
			}
			this._crlsCalled = true;
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 1)
			{
				Asn1SetParser result = (Asn1SetParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(17, false);
				this._nextObject = null;
				return result;
			}
			return null;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000482F0 File Offset: 0x000482F0
		public Asn1SetParser GetSignerInfos()
		{
			if (!this._certsCalled || !this._crlsCalled)
			{
				throw new IOException("GetCerts() and/or GetCrls() has not been called.");
			}
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			return (Asn1SetParser)this._nextObject;
		}

		// Token: 0x0400072B RID: 1835
		private Asn1SequenceParser _seq;

		// Token: 0x0400072C RID: 1836
		private DerInteger _version;

		// Token: 0x0400072D RID: 1837
		private object _nextObject;

		// Token: 0x0400072E RID: 1838
		private bool _certsCalled;

		// Token: 0x0400072F RID: 1839
		private bool _crlsCalled;
	}
}
