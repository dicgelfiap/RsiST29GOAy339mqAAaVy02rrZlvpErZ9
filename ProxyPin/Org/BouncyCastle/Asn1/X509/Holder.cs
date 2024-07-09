using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FC RID: 508
	public class Holder : Asn1Encodable
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x0005FB7C File Offset: 0x0005FB7C
		public static Holder GetInstance(object obj)
		{
			if (obj is Holder)
			{
				return (Holder)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Holder((Asn1Sequence)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return new Holder((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0005FBE8 File Offset: 0x0005FBE8
		public Holder(Asn1TaggedObject tagObj)
		{
			switch (tagObj.TagNo)
			{
			case 0:
				this.baseCertificateID = IssuerSerial.GetInstance(tagObj, false);
				break;
			case 1:
				this.entityName = GeneralNames.GetInstance(tagObj, false);
				break;
			default:
				throw new ArgumentException("unknown tag in Holder");
			}
			this.version = 0;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0005FC50 File Offset: 0x0005FC50
		private Holder(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.baseCertificateID = IssuerSerial.GetInstance(instance, false);
					break;
				case 1:
					this.entityName = GeneralNames.GetInstance(instance, false);
					break;
				case 2:
					this.objectDigestInfo = ObjectDigestInfo.GetInstance(instance, false);
					break;
				default:
					throw new ArgumentException("unknown tag in Holder");
				}
			}
			this.version = 1;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0005FD18 File Offset: 0x0005FD18
		public Holder(IssuerSerial baseCertificateID) : this(baseCertificateID, 1)
		{
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0005FD24 File Offset: 0x0005FD24
		public Holder(IssuerSerial baseCertificateID, int version)
		{
			this.baseCertificateID = baseCertificateID;
			this.version = version;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0005FD3C File Offset: 0x0005FD3C
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0005FD44 File Offset: 0x0005FD44
		public Holder(GeneralNames entityName) : this(entityName, 1)
		{
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0005FD50 File Offset: 0x0005FD50
		public Holder(GeneralNames entityName, int version)
		{
			this.entityName = entityName;
			this.version = version;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0005FD68 File Offset: 0x0005FD68
		public Holder(ObjectDigestInfo objectDigestInfo)
		{
			this.objectDigestInfo = objectDigestInfo;
			this.version = 1;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0005FD80 File Offset: 0x0005FD80
		public IssuerSerial BaseCertificateID
		{
			get
			{
				return this.baseCertificateID;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0005FD88 File Offset: 0x0005FD88
		public GeneralNames EntityName
		{
			get
			{
				return this.entityName;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0005FD90 File Offset: 0x0005FD90
		public ObjectDigestInfo ObjectDigestInfo
		{
			get
			{
				return this.objectDigestInfo;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0005FD98 File Offset: 0x0005FD98
		public override Asn1Object ToAsn1Object()
		{
			if (this.version == 1)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
				asn1EncodableVector.AddOptionalTagged(false, 0, this.baseCertificateID);
				asn1EncodableVector.AddOptionalTagged(false, 1, this.entityName);
				asn1EncodableVector.AddOptionalTagged(false, 2, this.objectDigestInfo);
				return new DerSequence(asn1EncodableVector);
			}
			if (this.entityName != null)
			{
				return new DerTaggedObject(false, 1, this.entityName);
			}
			return new DerTaggedObject(false, 0, this.baseCertificateID);
		}

		// Token: 0x04000BED RID: 3053
		internal readonly IssuerSerial baseCertificateID;

		// Token: 0x04000BEE RID: 3054
		internal readonly GeneralNames entityName;

		// Token: 0x04000BEF RID: 3055
		internal readonly ObjectDigestInfo objectDigestInfo;

		// Token: 0x04000BF0 RID: 3056
		private readonly int version;
	}
}
