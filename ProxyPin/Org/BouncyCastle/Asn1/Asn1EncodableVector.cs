using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000236 RID: 566
	public class Asn1EncodableVector : IEnumerable
	{
		// Token: 0x06001248 RID: 4680 RVA: 0x00067660 File Offset: 0x00067660
		public static Asn1EncodableVector FromEnumerable(IEnumerable e)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in e)
			{
				Asn1Encodable element = (Asn1Encodable)obj;
				asn1EncodableVector.Add(element);
			}
			return asn1EncodableVector;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000676C4 File Offset: 0x000676C4
		public Asn1EncodableVector() : this(10)
		{
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000676D0 File Offset: 0x000676D0
		public Asn1EncodableVector(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentException("must not be negative", "initialCapacity");
			}
			this.elements = ((initialCapacity == 0) ? Asn1EncodableVector.EmptyElements : new Asn1Encodable[initialCapacity]);
			this.elementCount = 0;
			this.copyOnWrite = false;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00067728 File Offset: 0x00067728
		public Asn1EncodableVector(params Asn1Encodable[] v) : this()
		{
			this.Add(v);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00067738 File Offset: 0x00067738
		public void Add(Asn1Encodable element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			int num = this.elements.Length;
			int num2 = this.elementCount + 1;
			if (num2 > num | this.copyOnWrite)
			{
				this.Reallocate(num2);
			}
			this.elements[this.elementCount] = element;
			this.elementCount = num2;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0006779C File Offset: 0x0006779C
		public void Add(params Asn1Encodable[] objs)
		{
			foreach (Asn1Encodable element in objs)
			{
				this.Add(element);
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000677D0 File Offset: 0x000677D0
		public void AddOptional(params Asn1Encodable[] objs)
		{
			if (objs != null)
			{
				foreach (Asn1Encodable asn1Encodable in objs)
				{
					if (asn1Encodable != null)
					{
						this.Add(asn1Encodable);
					}
				}
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00067810 File Offset: 0x00067810
		public void AddOptionalTagged(bool isExplicit, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				this.Add(new DerTaggedObject(isExplicit, tagNo, obj));
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00067828 File Offset: 0x00067828
		public void AddAll(Asn1EncodableVector other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			int count = other.Count;
			if (count < 1)
			{
				return;
			}
			int num = this.elements.Length;
			int num2 = this.elementCount + count;
			if (num2 > num | this.copyOnWrite)
			{
				this.Reallocate(num2);
			}
			int num3 = 0;
			for (;;)
			{
				Asn1Encodable asn1Encodable = other[num3];
				if (asn1Encodable == null)
				{
					break;
				}
				this.elements[this.elementCount + num3] = asn1Encodable;
				if (++num3 >= count)
				{
					goto Block_5;
				}
			}
			throw new NullReferenceException("'other' elements cannot be null");
			Block_5:
			this.elementCount = num2;
		}

		// Token: 0x1700046D RID: 1133
		public Asn1Encodable this[int index]
		{
			get
			{
				if (index >= this.elementCount)
				{
					throw new IndexOutOfRangeException(index + " >= " + this.elementCount);
				}
				return this.elements[index];
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00067900 File Offset: 0x00067900
		public int Count
		{
			get
			{
				return this.elementCount;
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00067908 File Offset: 0x00067908
		public IEnumerator GetEnumerator()
		{
			return this.CopyElements().GetEnumerator();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00067918 File Offset: 0x00067918
		internal Asn1Encodable[] CopyElements()
		{
			if (this.elementCount == 0)
			{
				return Asn1EncodableVector.EmptyElements;
			}
			Asn1Encodable[] array = new Asn1Encodable[this.elementCount];
			Array.Copy(this.elements, 0, array, 0, this.elementCount);
			return array;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0006795C File Offset: 0x0006795C
		internal Asn1Encodable[] TakeElements()
		{
			if (this.elementCount == 0)
			{
				return Asn1EncodableVector.EmptyElements;
			}
			if (this.elements.Length == this.elementCount)
			{
				this.copyOnWrite = true;
				return this.elements;
			}
			Asn1Encodable[] array = new Asn1Encodable[this.elementCount];
			Array.Copy(this.elements, 0, array, 0, this.elementCount);
			return array;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000679C0 File Offset: 0x000679C0
		private void Reallocate(int minCapacity)
		{
			int val = this.elements.Length;
			int num = Math.Max(val, minCapacity + (minCapacity >> 1));
			Asn1Encodable[] destinationArray = new Asn1Encodable[num];
			Array.Copy(this.elements, 0, destinationArray, 0, this.elementCount);
			this.elements = destinationArray;
			this.copyOnWrite = false;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00067A10 File Offset: 0x00067A10
		internal static Asn1Encodable[] CloneElements(Asn1Encodable[] elements)
		{
			if (elements.Length >= 1)
			{
				return (Asn1Encodable[])elements.Clone();
			}
			return Asn1EncodableVector.EmptyElements;
		}

		// Token: 0x04000D56 RID: 3414
		private const int DefaultCapacity = 10;

		// Token: 0x04000D57 RID: 3415
		internal static readonly Asn1Encodable[] EmptyElements = new Asn1Encodable[0];

		// Token: 0x04000D58 RID: 3416
		private Asn1Encodable[] elements;

		// Token: 0x04000D59 RID: 3417
		private int elementCount;

		// Token: 0x04000D5A RID: 3418
		private bool copyOnWrite;
	}
}
