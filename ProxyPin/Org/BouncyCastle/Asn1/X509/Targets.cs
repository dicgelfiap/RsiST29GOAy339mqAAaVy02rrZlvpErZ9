using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000212 RID: 530
	public class Targets : Asn1Encodable
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x00061D5C File Offset: 0x00061D5C
		public static Targets GetInstance(object obj)
		{
			if (obj is Targets)
			{
				return (Targets)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Targets((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00061DB0 File Offset: 0x00061DB0
		private Targets(Asn1Sequence targets)
		{
			this.targets = targets;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00061DC0 File Offset: 0x00061DC0
		public Targets(Target[] targets)
		{
			this.targets = new DerSequence(targets);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00061DD4 File Offset: 0x00061DD4
		public virtual Target[] GetTargets()
		{
			Target[] array = new Target[this.targets.Count];
			for (int i = 0; i < this.targets.Count; i++)
			{
				array[i] = Target.GetInstance(this.targets[i]);
			}
			return array;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00061E28 File Offset: 0x00061E28
		public override Asn1Object ToAsn1Object()
		{
			return this.targets;
		}

		// Token: 0x04000C40 RID: 3136
		private readonly Asn1Sequence targets;
	}
}
