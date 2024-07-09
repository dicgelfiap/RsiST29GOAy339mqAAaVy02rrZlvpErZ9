using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000202 RID: 514
	public class NameConstraints : Asn1Encodable
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x0006081C File Offset: 0x0006081C
		public static NameConstraints GetInstance(object obj)
		{
			if (obj == null || obj is NameConstraints)
			{
				return (NameConstraints)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NameConstraints((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00060878 File Offset: 0x00060878
		public NameConstraints(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.permitted = Asn1Sequence.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.excluded = Asn1Sequence.GetInstance(asn1TaggedObject, false);
					break;
				}
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00060910 File Offset: 0x00060910
		public NameConstraints(ArrayList permitted, ArrayList excluded) : this(permitted, excluded)
		{
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0006091C File Offset: 0x0006091C
		public NameConstraints(IList permitted, IList excluded)
		{
			if (permitted != null)
			{
				this.permitted = this.CreateSequence(permitted);
			}
			if (excluded != null)
			{
				this.excluded = this.CreateSequence(excluded);
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0006094C File Offset: 0x0006094C
		private DerSequence CreateSequence(IList subtrees)
		{
			GeneralSubtree[] array = new GeneralSubtree[subtrees.Count];
			for (int i = 0; i < subtrees.Count; i++)
			{
				array[i] = (GeneralSubtree)subtrees[i];
			}
			return new DerSequence(array);
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00060998 File Offset: 0x00060998
		public Asn1Sequence PermittedSubtrees
		{
			get
			{
				return this.permitted;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x000609A0 File Offset: 0x000609A0
		public Asn1Sequence ExcludedSubtrees
		{
			get
			{
				return this.excluded;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000609A8 File Offset: 0x000609A8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this.permitted);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.excluded);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C17 RID: 3095
		private Asn1Sequence permitted;

		// Token: 0x04000C18 RID: 3096
		private Asn1Sequence excluded;
	}
}
