using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000211 RID: 529
	public class TargetInformation : Asn1Encodable
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x00061C78 File Offset: 0x00061C78
		public static TargetInformation GetInstance(object obj)
		{
			if (obj is TargetInformation)
			{
				return (TargetInformation)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TargetInformation((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00061CCC File Offset: 0x00061CCC
		private TargetInformation(Asn1Sequence targets)
		{
			this.targets = targets;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00061CDC File Offset: 0x00061CDC
		public virtual Targets[] GetTargetsObjects()
		{
			Targets[] array = new Targets[this.targets.Count];
			for (int i = 0; i < this.targets.Count; i++)
			{
				array[i] = Targets.GetInstance(this.targets[i]);
			}
			return array;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00061D30 File Offset: 0x00061D30
		public TargetInformation(Targets targets)
		{
			this.targets = new DerSequence(targets);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00061D44 File Offset: 0x00061D44
		public TargetInformation(Target[] targets) : this(new Targets(targets))
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00061D54 File Offset: 0x00061D54
		public override Asn1Object ToAsn1Object()
		{
			return this.targets;
		}

		// Token: 0x04000C3F RID: 3135
		private readonly Asn1Sequence targets;
	}
}
