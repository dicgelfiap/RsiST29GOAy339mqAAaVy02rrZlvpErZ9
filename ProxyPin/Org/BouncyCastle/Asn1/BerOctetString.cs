using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000252 RID: 594
	public class BerOctetString : DerOctetString, IEnumerable
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x00069C28 File Offset: 0x00069C28
		public static BerOctetString FromSequence(Asn1Sequence seq)
		{
			int count = seq.Count;
			Asn1OctetString[] array = new Asn1OctetString[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = Asn1OctetString.GetInstance(seq[i]);
			}
			return new BerOctetString(array);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00069C70 File Offset: 0x00069C70
		private static byte[] ToBytes(Asn1OctetString[] octs)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (Asn1OctetString asn1OctetString in octs)
			{
				byte[] octets = asn1OctetString.GetOctets();
				memoryStream.Write(octets, 0, octets.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00069CC0 File Offset: 0x00069CC0
		private static Asn1OctetString[] ToOctetStringArray(IEnumerable e)
		{
			IList list = Platform.CreateArrayList(e);
			int count = list.Count;
			Asn1OctetString[] array = new Asn1OctetString[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = Asn1OctetString.GetInstance(list[i]);
			}
			return array;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00069D0C File Offset: 0x00069D0C
		[Obsolete("Will be removed")]
		public BerOctetString(IEnumerable e) : this(BerOctetString.ToOctetStringArray(e))
		{
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00069D1C File Offset: 0x00069D1C
		public BerOctetString(byte[] str) : this(str, BerOctetString.DefaultChunkSize)
		{
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00069D2C File Offset: 0x00069D2C
		public BerOctetString(Asn1OctetString[] octs) : this(octs, BerOctetString.DefaultChunkSize)
		{
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00069D3C File Offset: 0x00069D3C
		public BerOctetString(byte[] str, int chunkSize) : this(str, null, chunkSize)
		{
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00069D48 File Offset: 0x00069D48
		public BerOctetString(Asn1OctetString[] octs, int chunkSize) : this(BerOctetString.ToBytes(octs), octs, chunkSize)
		{
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00069D58 File Offset: 0x00069D58
		private BerOctetString(byte[] str, Asn1OctetString[] octs, int chunkSize) : base(str)
		{
			this.octs = octs;
			this.chunkSize = chunkSize;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00069D70 File Offset: 0x00069D70
		public IEnumerator GetEnumerator()
		{
			if (this.octs == null)
			{
				return this.GenerateOcts().GetEnumerator();
			}
			return this.octs.GetEnumerator();
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00069D94 File Offset: 0x00069D94
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00069D9C File Offset: 0x00069D9C
		private IList GenerateOcts()
		{
			IList list = Platform.CreateArrayList();
			for (int i = 0; i < this.str.Length; i += this.chunkSize)
			{
				int num = Math.Min(this.str.Length, i + this.chunkSize);
				byte[] array = new byte[num - i];
				Array.Copy(this.str, i, array, 0, array.Length);
				list.Add(new DerOctetString(array));
			}
			return list;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00069E0C File Offset: 0x00069E0C
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(36);
				derOut.WriteByte(128);
				foreach (object obj in this)
				{
					Asn1OctetString obj2 = (Asn1OctetString)obj;
					derOut.WriteObject(obj2);
				}
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x04000D8F RID: 3471
		private static readonly int DefaultChunkSize = 1000;

		// Token: 0x04000D90 RID: 3472
		private readonly int chunkSize;

		// Token: 0x04000D91 RID: 3473
		private readonly Asn1OctetString[] octs;
	}
}
