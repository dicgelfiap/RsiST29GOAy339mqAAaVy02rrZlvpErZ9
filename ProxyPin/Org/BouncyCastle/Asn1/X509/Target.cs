using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000210 RID: 528
	public class Target : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x00061B74 File Offset: 0x00061B74
		public static Target GetInstance(object obj)
		{
			if (obj is Target)
			{
				return (Target)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new Target((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00061BC8 File Offset: 0x00061BC8
		private Target(Asn1TaggedObject tagObj)
		{
			switch (tagObj.TagNo)
			{
			case 0:
				this.targetName = GeneralName.GetInstance(tagObj, true);
				return;
			case 1:
				this.targetGroup = GeneralName.GetInstance(tagObj, true);
				return;
			default:
				throw new ArgumentException("unknown tag: " + tagObj.TagNo);
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00061C30 File Offset: 0x00061C30
		public Target(Target.Choice type, GeneralName name) : this(new DerTaggedObject((int)type, name))
		{
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00061C40 File Offset: 0x00061C40
		public virtual GeneralName TargetGroup
		{
			get
			{
				return this.targetGroup;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00061C48 File Offset: 0x00061C48
		public virtual GeneralName TargetName
		{
			get
			{
				return this.targetName;
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00061C50 File Offset: 0x00061C50
		public override Asn1Object ToAsn1Object()
		{
			if (this.targetName != null)
			{
				return new DerTaggedObject(true, 0, this.targetName);
			}
			return new DerTaggedObject(true, 1, this.targetGroup);
		}

		// Token: 0x04000C3D RID: 3133
		private readonly GeneralName targetName;

		// Token: 0x04000C3E RID: 3134
		private readonly GeneralName targetGroup;

		// Token: 0x02000DBA RID: 3514
		public enum Choice
		{
			// Token: 0x04004042 RID: 16450
			Name,
			// Token: 0x04004043 RID: 16451
			Group
		}
	}
}
