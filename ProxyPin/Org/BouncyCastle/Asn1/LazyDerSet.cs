using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200027E RID: 638
	internal class LazyDerSet : DerSet
	{
		// Token: 0x0600143C RID: 5180 RVA: 0x0006D00C File Offset: 0x0006D00C
		internal LazyDerSet(byte[] encoded)
		{
			this.encoded = encoded;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0006D01C File Offset: 0x0006D01C
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

		// Token: 0x1700049B RID: 1179
		public override Asn1Encodable this[int index]
		{
			get
			{
				this.Parse();
				return base[index];
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0006D0A4 File Offset: 0x0006D0A4
		public override IEnumerator GetEnumerator()
		{
			this.Parse();
			return base.GetEnumerator();
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0006D0B4 File Offset: 0x0006D0B4
		public override int Count
		{
			get
			{
				this.Parse();
				return base.Count;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0006D0C4 File Offset: 0x0006D0C4
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
					derOut.WriteEncoded(49, this.encoded);
				}
			}
		}

		// Token: 0x04000DC7 RID: 3527
		private byte[] encoded;
	}
}
