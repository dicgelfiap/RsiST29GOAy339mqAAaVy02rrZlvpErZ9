using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000241 RID: 577
	public abstract class Asn1Sequence : Asn1Object, IEnumerable
	{
		// Token: 0x0600129B RID: 4763 RVA: 0x000687A8 File Offset: 0x000687A8
		public static Asn1Sequence GetInstance(object obj)
		{
			if (obj == null || obj is Asn1Sequence)
			{
				return (Asn1Sequence)obj;
			}
			if (obj is Asn1SequenceParser)
			{
				return Asn1Sequence.GetInstance(((Asn1SequenceParser)obj).ToAsn1Object());
			}
			if (obj is byte[])
			{
				try
				{
					return Asn1Sequence.GetInstance(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (IOException ex)
				{
					throw new ArgumentException("failed to construct sequence from byte[]: " + ex.Message);
				}
			}
			if (obj is Asn1Encodable)
			{
				Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
				if (asn1Object is Asn1Sequence)
				{
					return (Asn1Sequence)asn1Object;
				}
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00068878 File Offset: 0x00068878
		public static Asn1Sequence GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly)
			{
				if (!obj.IsExplicit())
				{
					throw new ArgumentException("object implicit - explicit expected.");
				}
				return (Asn1Sequence)@object;
			}
			else if (obj.IsExplicit())
			{
				if (obj is BerTaggedObject)
				{
					return new BerSequence(@object);
				}
				return new DerSequence(@object);
			}
			else
			{
				if (@object is Asn1Sequence)
				{
					return (Asn1Sequence)@object;
				}
				throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00068904 File Offset: 0x00068904
		protected internal Asn1Sequence()
		{
			this.elements = Asn1EncodableVector.EmptyElements;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00068918 File Offset: 0x00068918
		protected internal Asn1Sequence(Asn1Encodable element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.elements = new Asn1Encodable[]
			{
				element
			};
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00068958 File Offset: 0x00068958
		protected internal Asn1Sequence(params Asn1Encodable[] elements)
		{
			if (Arrays.IsNullOrContainsNull(elements))
			{
				throw new NullReferenceException("'elements' cannot be null, or contain null");
			}
			this.elements = Asn1EncodableVector.CloneElements(elements);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00068984 File Offset: 0x00068984
		protected internal Asn1Sequence(Asn1EncodableVector elementVector)
		{
			if (elementVector == null)
			{
				throw new ArgumentNullException("elementVector");
			}
			this.elements = elementVector.TakeElements();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000689AC File Offset: 0x000689AC
		public virtual IEnumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000689BC File Offset: 0x000689BC
		public virtual Asn1SequenceParser Parser
		{
			get
			{
				return new Asn1Sequence.Asn1SequenceParserImpl(this);
			}
		}

		// Token: 0x17000478 RID: 1144
		public virtual Asn1Encodable this[int index]
		{
			get
			{
				return this.elements[index];
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000689D4 File Offset: 0x000689D4
		public virtual int Count
		{
			get
			{
				return this.elements.Length;
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000689E0 File Offset: 0x000689E0
		public virtual Asn1Encodable[] ToArray()
		{
			return Asn1EncodableVector.CloneElements(this.elements);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000689F0 File Offset: 0x000689F0
		protected override int Asn1GetHashCode()
		{
			int num = this.elements.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= this.elements[num].ToAsn1Object().CallAsn1GetHashCode();
			}
			return num2;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00068A40 File Offset: 0x00068A40
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1Sequence asn1Sequence = asn1Object as Asn1Sequence;
			if (asn1Sequence == null)
			{
				return false;
			}
			int count = this.Count;
			if (asn1Sequence.Count != count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				Asn1Object asn1Object2 = this.elements[i].ToAsn1Object();
				Asn1Object asn1Object3 = asn1Sequence.elements[i].ToAsn1Object();
				if (asn1Object2 != asn1Object3 && !asn1Object2.CallAsn1Equals(asn1Object3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00068AC4 File Offset: 0x00068AC4
		public override string ToString()
		{
			return CollectionUtilities.ToString(this.elements);
		}

		// Token: 0x04000D60 RID: 3424
		internal Asn1Encodable[] elements;

		// Token: 0x02000DD3 RID: 3539
		private class Asn1SequenceParserImpl : Asn1SequenceParser, IAsn1Convertible
		{
			// Token: 0x06008B58 RID: 35672 RVA: 0x0029D408 File Offset: 0x0029D408
			public Asn1SequenceParserImpl(Asn1Sequence outer)
			{
				this.outer = outer;
				this.max = outer.Count;
			}

			// Token: 0x06008B59 RID: 35673 RVA: 0x0029D424 File Offset: 0x0029D424
			public IAsn1Convertible ReadObject()
			{
				if (this.index == this.max)
				{
					return null;
				}
				Asn1Encodable asn1Encodable = this.outer[this.index++];
				if (asn1Encodable is Asn1Sequence)
				{
					return ((Asn1Sequence)asn1Encodable).Parser;
				}
				if (asn1Encodable is Asn1Set)
				{
					return ((Asn1Set)asn1Encodable).Parser;
				}
				return asn1Encodable;
			}

			// Token: 0x06008B5A RID: 35674 RVA: 0x0029D494 File Offset: 0x0029D494
			public Asn1Object ToAsn1Object()
			{
				return this.outer;
			}

			// Token: 0x0400405C RID: 16476
			private readonly Asn1Sequence outer;

			// Token: 0x0400405D RID: 16477
			private readonly int max;

			// Token: 0x0400405E RID: 16478
			private int index;
		}
	}
}
