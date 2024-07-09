using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000268 RID: 616
	public class DerExternal : Asn1Object
	{
		// Token: 0x06001391 RID: 5009 RVA: 0x0006B1F0 File Offset: 0x0006B1F0
		public DerExternal(Asn1EncodableVector vector)
		{
			int num = 0;
			Asn1Object objFromVector = DerExternal.GetObjFromVector(vector, num);
			if (objFromVector is DerObjectIdentifier)
			{
				this.directReference = (DerObjectIdentifier)objFromVector;
				num++;
				objFromVector = DerExternal.GetObjFromVector(vector, num);
			}
			if (objFromVector is DerInteger)
			{
				this.indirectReference = (DerInteger)objFromVector;
				num++;
				objFromVector = DerExternal.GetObjFromVector(vector, num);
			}
			if (!(objFromVector is Asn1TaggedObject))
			{
				this.dataValueDescriptor = objFromVector;
				num++;
				objFromVector = DerExternal.GetObjFromVector(vector, num);
			}
			if (vector.Count != num + 1)
			{
				throw new ArgumentException("input vector too large", "vector");
			}
			if (!(objFromVector is Asn1TaggedObject))
			{
				throw new ArgumentException("No tagged object found in vector. Structure doesn't seem to be of type External", "vector");
			}
			Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)objFromVector;
			this.Encoding = asn1TaggedObject.TagNo;
			if (this.encoding < 0 || this.encoding > 2)
			{
				throw new InvalidOperationException("invalid encoding value");
			}
			this.externalContent = asn1TaggedObject.GetObject();
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0006B2F0 File Offset: 0x0006B2F0
		public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference, Asn1Object dataValueDescriptor, DerTaggedObject externalData) : this(directReference, indirectReference, dataValueDescriptor, externalData.TagNo, externalData.ToAsn1Object())
		{
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0006B318 File Offset: 0x0006B318
		public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference, Asn1Object dataValueDescriptor, int encoding, Asn1Object externalData)
		{
			this.DirectReference = directReference;
			this.IndirectReference = indirectReference;
			this.DataValueDescriptor = dataValueDescriptor;
			this.Encoding = encoding;
			this.ExternalContent = externalData.ToAsn1Object();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0006B35C File Offset: 0x0006B35C
		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerExternal.WriteEncodable(memoryStream, this.directReference);
			DerExternal.WriteEncodable(memoryStream, this.indirectReference);
			DerExternal.WriteEncodable(memoryStream, this.dataValueDescriptor);
			DerExternal.WriteEncodable(memoryStream, new DerTaggedObject(8, this.externalContent));
			derOut.WriteEncoded(32, 8, memoryStream.ToArray());
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0006B3B8 File Offset: 0x0006B3B8
		protected override int Asn1GetHashCode()
		{
			int num = this.externalContent.GetHashCode();
			if (this.directReference != null)
			{
				num ^= this.directReference.GetHashCode();
			}
			if (this.indirectReference != null)
			{
				num ^= this.indirectReference.GetHashCode();
			}
			if (this.dataValueDescriptor != null)
			{
				num ^= this.dataValueDescriptor.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0006B424 File Offset: 0x0006B424
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			if (this == asn1Object)
			{
				return true;
			}
			DerExternal derExternal = asn1Object as DerExternal;
			return derExternal != null && (object.Equals(this.directReference, derExternal.directReference) && object.Equals(this.indirectReference, derExternal.indirectReference) && object.Equals(this.dataValueDescriptor, derExternal.dataValueDescriptor)) && this.externalContent.Equals(derExternal.externalContent);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0006B4A4 File Offset: 0x0006B4A4
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0006B4AC File Offset: 0x0006B4AC
		public Asn1Object DataValueDescriptor
		{
			get
			{
				return this.dataValueDescriptor;
			}
			set
			{
				this.dataValueDescriptor = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0006B4B8 File Offset: 0x0006B4B8
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0006B4C0 File Offset: 0x0006B4C0
		public DerObjectIdentifier DirectReference
		{
			get
			{
				return this.directReference;
			}
			set
			{
				this.directReference = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0006B4CC File Offset: 0x0006B4CC
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0006B4D4 File Offset: 0x0006B4D4
		public int Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (this.encoding < 0 || this.encoding > 2)
				{
					throw new InvalidOperationException("invalid encoding value: " + this.encoding);
				}
				this.encoding = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0006B510 File Offset: 0x0006B510
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x0006B518 File Offset: 0x0006B518
		public Asn1Object ExternalContent
		{
			get
			{
				return this.externalContent;
			}
			set
			{
				this.externalContent = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0006B524 File Offset: 0x0006B524
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x0006B52C File Offset: 0x0006B52C
		public DerInteger IndirectReference
		{
			get
			{
				return this.indirectReference;
			}
			set
			{
				this.indirectReference = value;
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0006B538 File Offset: 0x0006B538
		private static Asn1Object GetObjFromVector(Asn1EncodableVector v, int index)
		{
			if (v.Count <= index)
			{
				throw new ArgumentException("too few objects in input vector", "v");
			}
			return v[index].ToAsn1Object();
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0006B564 File Offset: 0x0006B564
		private static void WriteEncodable(MemoryStream ms, Asn1Encodable e)
		{
			if (e != null)
			{
				byte[] derEncoded = e.GetDerEncoded();
				ms.Write(derEncoded, 0, derEncoded.Length);
			}
		}

		// Token: 0x04000DAA RID: 3498
		private DerObjectIdentifier directReference;

		// Token: 0x04000DAB RID: 3499
		private DerInteger indirectReference;

		// Token: 0x04000DAC RID: 3500
		private Asn1Object dataValueDescriptor;

		// Token: 0x04000DAD RID: 3501
		private int encoding;

		// Token: 0x04000DAE RID: 3502
		private Asn1Object externalContent;
	}
}
