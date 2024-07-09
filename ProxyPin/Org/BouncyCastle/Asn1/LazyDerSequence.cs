using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200027D RID: 637
	internal class LazyDerSequence : DerSequence
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0006CEFC File Offset: 0x0006CEFC
		internal LazyDerSequence(byte[] encoded)
		{
			this.encoded = encoded;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0006CF0C File Offset: 0x0006CF0C
		private void Parse()
		{
			lock (this)
			{
				if (this.encoded != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
					Asn1InputStream asn1InputStream = new LazyAsn1InputStream(this.encoded);
					Asn1Object element;
					while ((element = asn1InputStream.ReadObject()) != null)
					{
						asn1EncodableVector.Add(element);
					}
					this.elements = asn1EncodableVector.TakeElements();
					this.encoded = null;
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		public override Asn1Encodable this[int index]
		{
			get
			{
				this.Parse();
				return base[index];
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0006CF94 File Offset: 0x0006CF94
		public override IEnumerator GetEnumerator()
		{
			this.Parse();
			return base.GetEnumerator();
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0006CFA4 File Offset: 0x0006CFA4
		public override int Count
		{
			get
			{
				this.Parse();
				return base.Count;
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0006CFB4 File Offset: 0x0006CFB4
		internal override void Encode(DerOutputStream derOut)
		{
			lock (this)
			{
				if (this.encoded == null)
				{
					base.Encode(derOut);
				}
				else
				{
					derOut.WriteEncoded(48, this.encoded);
				}
			}
		}

		// Token: 0x04000DC6 RID: 3526
		private byte[] encoded;
	}
}
