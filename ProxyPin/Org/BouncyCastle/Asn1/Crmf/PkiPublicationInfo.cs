using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000137 RID: 311
	public class PkiPublicationInfo : Asn1Encodable
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x00049F44 File Offset: 0x00049F44
		private PkiPublicationInfo(Asn1Sequence seq)
		{
			this.action = DerInteger.GetInstance(seq[0]);
			this.pubInfos = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00049F70 File Offset: 0x00049F70
		public static PkiPublicationInfo GetInstance(object obj)
		{
			if (obj is PkiPublicationInfo)
			{
				return (PkiPublicationInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiPublicationInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00049FC4 File Offset: 0x00049FC4
		public virtual DerInteger Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00049FCC File Offset: 0x00049FCC
		public virtual SinglePubInfo[] GetPubInfos()
		{
			if (this.pubInfos == null)
			{
				return null;
			}
			SinglePubInfo[] array = new SinglePubInfo[this.pubInfos.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = SinglePubInfo.GetInstance(this.pubInfos[num]);
			}
			return array;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0004A028 File Offset: 0x0004A028
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.action,
				this.pubInfos
			});
		}

		// Token: 0x04000780 RID: 1920
		private readonly DerInteger action;

		// Token: 0x04000781 RID: 1921
		private readonly Asn1Sequence pubInfos;
	}
}
