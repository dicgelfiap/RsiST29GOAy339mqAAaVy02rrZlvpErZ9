using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000243 RID: 579
	public abstract class Asn1Set : Asn1Object, IEnumerable
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x00068AD4 File Offset: 0x00068AD4
		public static Asn1Set GetInstance(object obj)
		{
			if (obj == null || obj is Asn1Set)
			{
				return (Asn1Set)obj;
			}
			if (obj is Asn1SetParser)
			{
				return Asn1Set.GetInstance(((Asn1SetParser)obj).ToAsn1Object());
			}
			if (obj is byte[])
			{
				try
				{
					return Asn1Set.GetInstance(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (IOException ex)
				{
					throw new ArgumentException("failed to construct set from byte[]: " + ex.Message);
				}
			}
			if (obj is Asn1Encodable)
			{
				Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
				if (asn1Object is Asn1Set)
				{
					return (Asn1Set)asn1Object;
				}
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00068BA4 File Offset: 0x00068BA4
		public static Asn1Set GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly)
			{
				if (!obj.IsExplicit())
				{
					throw new ArgumentException("object implicit - explicit expected.");
				}
				return (Asn1Set)@object;
			}
			else
			{
				if (obj.IsExplicit())
				{
					return new DerSet(@object);
				}
				if (@object is Asn1Set)
				{
					return (Asn1Set)@object;
				}
				if (@object is Asn1Sequence)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
					Asn1Sequence asn1Sequence = (Asn1Sequence)@object;
					foreach (object obj2 in asn1Sequence)
					{
						Asn1Encodable element = (Asn1Encodable)obj2;
						asn1EncodableVector.Add(element);
					}
					return new DerSet(asn1EncodableVector, false);
				}
				throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00068C90 File Offset: 0x00068C90
		protected internal Asn1Set()
		{
			this.elements = Asn1EncodableVector.EmptyElements;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00068CA4 File Offset: 0x00068CA4
		protected internal Asn1Set(Asn1Encodable element)
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

		// Token: 0x060012AE RID: 4782 RVA: 0x00068CE4 File Offset: 0x00068CE4
		protected internal Asn1Set(params Asn1Encodable[] elements)
		{
			if (Arrays.IsNullOrContainsNull(elements))
			{
				throw new NullReferenceException("'elements' cannot be null, or contain null");
			}
			this.elements = Asn1EncodableVector.CloneElements(elements);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00068D10 File Offset: 0x00068D10
		protected internal Asn1Set(Asn1EncodableVector elementVector)
		{
			if (elementVector == null)
			{
				throw new ArgumentNullException("elementVector");
			}
			this.elements = elementVector.TakeElements();
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00068D38 File Offset: 0x00068D38
		public virtual IEnumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x1700047A RID: 1146
		public virtual Asn1Encodable this[int index]
		{
			get
			{
				return this.elements[index];
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00068D58 File Offset: 0x00068D58
		public virtual int Count
		{
			get
			{
				return this.elements.Length;
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00068D64 File Offset: 0x00068D64
		public virtual Asn1Encodable[] ToArray()
		{
			return Asn1EncodableVector.CloneElements(this.elements);
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00068D74 File Offset: 0x00068D74
		public Asn1SetParser Parser
		{
			get
			{
				return new Asn1Set.Asn1SetParserImpl(this);
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00068D7C File Offset: 0x00068D7C
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

		// Token: 0x060012B6 RID: 4790 RVA: 0x00068DCC File Offset: 0x00068DCC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1Set asn1Set = asn1Object as Asn1Set;
			if (asn1Set == null)
			{
				return false;
			}
			int count = this.Count;
			if (asn1Set.Count != count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				Asn1Object asn1Object2 = this.elements[i].ToAsn1Object();
				Asn1Object asn1Object3 = asn1Set.elements[i].ToAsn1Object();
				if (asn1Object2 != asn1Object3 && !asn1Object2.CallAsn1Equals(asn1Object3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00068E50 File Offset: 0x00068E50
		protected internal void Sort()
		{
			if (this.elements.Length < 2)
			{
				return;
			}
			int num = this.elements.Length;
			byte[][] array = new byte[num][];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.elements[i].GetEncoded("DER");
			}
			Array.Sort(array, this.elements, new Asn1Set.DerComparer());
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00068EC0 File Offset: 0x00068EC0
		public override string ToString()
		{
			return CollectionUtilities.ToString(this.elements);
		}

		// Token: 0x04000D61 RID: 3425
		internal Asn1Encodable[] elements;

		// Token: 0x02000DD4 RID: 3540
		private class Asn1SetParserImpl : Asn1SetParser, IAsn1Convertible
		{
			// Token: 0x06008B5B RID: 35675 RVA: 0x0029D49C File Offset: 0x0029D49C
			public Asn1SetParserImpl(Asn1Set outer)
			{
				this.outer = outer;
				this.max = outer.Count;
			}

			// Token: 0x06008B5C RID: 35676 RVA: 0x0029D4B8 File Offset: 0x0029D4B8
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

			// Token: 0x06008B5D RID: 35677 RVA: 0x0029D528 File Offset: 0x0029D528
			public virtual Asn1Object ToAsn1Object()
			{
				return this.outer;
			}

			// Token: 0x0400405F RID: 16479
			private readonly Asn1Set outer;

			// Token: 0x04004060 RID: 16480
			private readonly int max;

			// Token: 0x04004061 RID: 16481
			private int index;
		}

		// Token: 0x02000DD5 RID: 3541
		private class DerComparer : IComparer
		{
			// Token: 0x06008B5E RID: 35678 RVA: 0x0029D530 File Offset: 0x0029D530
			public int Compare(object x, object y)
			{
				byte[] array = (byte[])x;
				byte[] array2 = (byte[])y;
				int num = (int)array[0] & -33;
				int num2 = (int)array2[0] & -33;
				if (num == num2)
				{
					int num3 = Math.Min(array.Length, array2.Length);
					int i = 1;
					while (i < num3)
					{
						byte b = array[i];
						byte b2 = array2[i];
						if (b != b2)
						{
							if (b >= b2)
							{
								return 1;
							}
							return -1;
						}
						else
						{
							i++;
						}
					}
					return 0;
				}
				if (num >= num2)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
