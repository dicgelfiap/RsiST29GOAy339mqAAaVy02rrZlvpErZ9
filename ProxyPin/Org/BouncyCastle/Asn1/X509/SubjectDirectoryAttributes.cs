using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200020D RID: 525
	public class SubjectDirectoryAttributes : Asn1Encodable
	{
		// Token: 0x060010DC RID: 4316 RVA: 0x00061730 File Offset: 0x00061730
		public static SubjectDirectoryAttributes GetInstance(object obj)
		{
			if (obj == null || obj is SubjectDirectoryAttributes)
			{
				return (SubjectDirectoryAttributes)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SubjectDirectoryAttributes((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0006178C File Offset: 0x0006178C
		private SubjectDirectoryAttributes(Asn1Sequence seq)
		{
			this.attributes = Platform.CreateArrayList();
			foreach (object obj in seq)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(obj);
				this.attributes.Add(AttributeX509.GetInstance(instance));
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00061808 File Offset: 0x00061808
		[Obsolete]
		public SubjectDirectoryAttributes(ArrayList attributes) : this(attributes)
		{
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00061814 File Offset: 0x00061814
		public SubjectDirectoryAttributes(IList attributes)
		{
			this.attributes = Platform.CreateArrayList(attributes);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00061828 File Offset: 0x00061828
		public override Asn1Object ToAsn1Object()
		{
			AttributeX509[] array = new AttributeX509[this.attributes.Count];
			for (int i = 0; i < this.attributes.Count; i++)
			{
				array[i] = (AttributeX509)this.attributes[i];
			}
			return new DerSequence(array);
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00061884 File Offset: 0x00061884
		public IEnumerable Attributes
		{
			get
			{
				return new EnumerableProxy(this.attributes);
			}
		}

		// Token: 0x04000C39 RID: 3129
		private readonly IList attributes;
	}
}
