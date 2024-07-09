using System;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x02000170 RID: 368
	public class DataGroupHash : Asn1Encodable
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00050270 File Offset: 0x00050270
		public static DataGroupHash GetInstance(object obj)
		{
			if (obj is DataGroupHash)
			{
				return (DataGroupHash)obj;
			}
			if (obj != null)
			{
				return new DataGroupHash(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00050298 File Offset: 0x00050298
		private DataGroupHash(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.dataGroupNumber = DerInteger.GetInstance(seq[0]);
			this.dataGroupHashValue = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000502F0 File Offset: 0x000502F0
		public DataGroupHash(int dataGroupNumber, Asn1OctetString dataGroupHashValue)
		{
			this.dataGroupNumber = new DerInteger(dataGroupNumber);
			this.dataGroupHashValue = dataGroupHashValue;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0005030C File Offset: 0x0005030C
		public int DataGroupNumber
		{
			get
			{
				return this.dataGroupNumber.IntValueExact;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0005031C File Offset: 0x0005031C
		public Asn1OctetString DataGroupHashValue
		{
			get
			{
				return this.dataGroupHashValue;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00050324 File Offset: 0x00050324
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.dataGroupNumber,
				this.dataGroupHashValue
			});
		}

		// Token: 0x040008A1 RID: 2209
		private readonly DerInteger dataGroupNumber;

		// Token: 0x040008A2 RID: 2210
		private readonly Asn1OctetString dataGroupHashValue;
	}
}
