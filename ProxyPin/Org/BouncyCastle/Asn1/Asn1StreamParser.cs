using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000245 RID: 581
	public class Asn1StreamParser
	{
		// Token: 0x060012BA RID: 4794 RVA: 0x00068ED0 File Offset: 0x00068ED0
		public Asn1StreamParser(Stream inStream) : this(inStream, Asn1InputStream.FindLimit(inStream))
		{
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00068EE0 File Offset: 0x00068EE0
		public Asn1StreamParser(Stream inStream, int limit)
		{
			if (!inStream.CanRead)
			{
				throw new ArgumentException("Expected stream to be readable", "inStream");
			}
			this._in = inStream;
			this._limit = limit;
			this.tmpBuffers = new byte[16][];
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00068F20 File Offset: 0x00068F20
		public Asn1StreamParser(byte[] encoding) : this(new MemoryStream(encoding, false), encoding.Length)
		{
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00068F34 File Offset: 0x00068F34
		internal IAsn1Convertible ReadIndef(int tagValue)
		{
			int num = tagValue;
			if (num == 4)
			{
				return new BerOctetStringParser(this);
			}
			if (num == 8)
			{
				return new DerExternalParser(this);
			}
			switch (num)
			{
			case 16:
				return new BerSequenceParser(this);
			case 17:
				return new BerSetParser(this);
			default:
				throw new Asn1Exception("unknown BER object encountered: 0x" + tagValue.ToString("X"));
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00068FA4 File Offset: 0x00068FA4
		internal IAsn1Convertible ReadImplicit(bool constructed, int tag)
		{
			if (!(this._in is IndefiniteLengthInputStream))
			{
				if (constructed)
				{
					if (tag == 4)
					{
						return new BerOctetStringParser(this);
					}
					switch (tag)
					{
					case 16:
						return new DerSequenceParser(this);
					case 17:
						return new DerSetParser(this);
					}
				}
				else
				{
					if (tag == 4)
					{
						return new DerOctetStringParser((DefiniteLengthInputStream)this._in);
					}
					switch (tag)
					{
					case 16:
						throw new Asn1Exception("sets must use constructed encoding (see X.690 8.11.1/8.12.1)");
					case 17:
						throw new Asn1Exception("sequences must use constructed encoding (see X.690 8.9.1/8.10.1)");
					}
				}
				throw new Asn1Exception("implicit tagging not implemented");
			}
			if (!constructed)
			{
				throw new IOException("indefinite-length primitive encoding encountered");
			}
			return this.ReadIndef(tag);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00069068 File Offset: 0x00069068
		internal Asn1Object ReadTaggedObject(bool constructed, int tag)
		{
			if (!constructed)
			{
				DefiniteLengthInputStream definiteLengthInputStream = (DefiniteLengthInputStream)this._in;
				return new DerTaggedObject(false, tag, new DerOctetString(definiteLengthInputStream.ToArray()));
			}
			Asn1EncodableVector asn1EncodableVector = this.ReadVector();
			if (this._in is IndefiniteLengthInputStream)
			{
				if (asn1EncodableVector.Count != 1)
				{
					return new BerTaggedObject(false, tag, BerSequence.FromVector(asn1EncodableVector));
				}
				return new BerTaggedObject(true, tag, asn1EncodableVector[0]);
			}
			else
			{
				if (asn1EncodableVector.Count != 1)
				{
					return new DerTaggedObject(false, tag, DerSequence.FromVector(asn1EncodableVector));
				}
				return new DerTaggedObject(true, tag, asn1EncodableVector[0]);
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00069108 File Offset: 0x00069108
		public virtual IAsn1Convertible ReadObject()
		{
			int num = this._in.ReadByte();
			if (num == -1)
			{
				return null;
			}
			this.Set00Check(false);
			int num2 = Asn1InputStream.ReadTagNumber(this._in, num);
			bool flag = (num & 32) != 0;
			int num3 = Asn1InputStream.ReadLength(this._in, this._limit, num2 == 4 || num2 == 16 || num2 == 17 || num2 == 8);
			if (num3 < 0)
			{
				if (!flag)
				{
					throw new IOException("indefinite-length primitive encoding encountered");
				}
				IndefiniteLengthInputStream inStream = new IndefiniteLengthInputStream(this._in, this._limit);
				Asn1StreamParser asn1StreamParser = new Asn1StreamParser(inStream, this._limit);
				if ((num & 64) != 0)
				{
					return new BerApplicationSpecificParser(num2, asn1StreamParser);
				}
				if ((num & 128) != 0)
				{
					return new BerTaggedObjectParser(true, num2, asn1StreamParser);
				}
				return asn1StreamParser.ReadIndef(num2);
			}
			else
			{
				DefiniteLengthInputStream definiteLengthInputStream = new DefiniteLengthInputStream(this._in, num3, this._limit);
				if ((num & 64) != 0)
				{
					return new DerApplicationSpecific(flag, num2, definiteLengthInputStream.ToArray());
				}
				if ((num & 128) != 0)
				{
					return new BerTaggedObjectParser(flag, num2, new Asn1StreamParser(definiteLengthInputStream));
				}
				if (flag)
				{
					int num4 = num2;
					if (num4 == 4)
					{
						return new BerOctetStringParser(new Asn1StreamParser(definiteLengthInputStream));
					}
					if (num4 == 8)
					{
						return new DerExternalParser(new Asn1StreamParser(definiteLengthInputStream));
					}
					switch (num4)
					{
					case 16:
						return new DerSequenceParser(new Asn1StreamParser(definiteLengthInputStream));
					case 17:
						return new DerSetParser(new Asn1StreamParser(definiteLengthInputStream));
					default:
						throw new IOException("unknown tag " + num2 + " encountered");
					}
				}
				else
				{
					int num4 = num2;
					if (num4 == 4)
					{
						return new DerOctetStringParser(definiteLengthInputStream);
					}
					IAsn1Convertible result;
					try
					{
						result = Asn1InputStream.CreatePrimitiveDerObject(num2, definiteLengthInputStream, this.tmpBuffers);
					}
					catch (ArgumentException exception)
					{
						throw new Asn1Exception("corrupted stream detected", exception);
					}
					return result;
				}
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000692F8 File Offset: 0x000692F8
		private void Set00Check(bool enabled)
		{
			if (this._in is IndefiniteLengthInputStream)
			{
				((IndefiniteLengthInputStream)this._in).SetEofOn00(enabled);
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0006931C File Offset: 0x0006931C
		internal Asn1EncodableVector ReadVector()
		{
			IAsn1Convertible asn1Convertible = this.ReadObject();
			if (asn1Convertible == null)
			{
				return new Asn1EncodableVector(0);
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			do
			{
				asn1EncodableVector.Add(asn1Convertible.ToAsn1Object());
			}
			while ((asn1Convertible = this.ReadObject()) != null);
			return asn1EncodableVector;
		}

		// Token: 0x04000D62 RID: 3426
		private readonly Stream _in;

		// Token: 0x04000D63 RID: 3427
		private readonly int _limit;

		// Token: 0x04000D64 RID: 3428
		private readonly byte[][] tmpBuffers;
	}
}
