using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200023A RID: 570
	public class Asn1InputStream : FilterStream
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x00067B68 File Offset: 0x00067B68
		internal static int FindLimit(Stream input)
		{
			if (input is LimitedInputStream)
			{
				return ((LimitedInputStream)input).Limit;
			}
			if (input is Asn1InputStream)
			{
				return ((Asn1InputStream)input).Limit;
			}
			if (input is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)input;
				return (int)(memoryStream.Length - memoryStream.Position);
			}
			return int.MaxValue;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00067BD0 File Offset: 0x00067BD0
		public Asn1InputStream(Stream inputStream) : this(inputStream, Asn1InputStream.FindLimit(inputStream))
		{
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00067BE0 File Offset: 0x00067BE0
		public Asn1InputStream(Stream inputStream, int limit) : base(inputStream)
		{
			this.limit = limit;
			this.tmpBuffers = new byte[16][];
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00067C00 File Offset: 0x00067C00
		public Asn1InputStream(byte[] input) : this(new MemoryStream(input, false), input.Length)
		{
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00067C14 File Offset: 0x00067C14
		private Asn1Object BuildObject(int tag, int tagNo, int length)
		{
			bool flag = (tag & 32) != 0;
			DefiniteLengthInputStream definiteLengthInputStream = new DefiniteLengthInputStream(this.s, length, this.limit);
			if ((tag & 64) != 0)
			{
				return new DerApplicationSpecific(flag, tagNo, definiteLengthInputStream.ToArray());
			}
			if ((tag & 128) != 0)
			{
				return new Asn1StreamParser(definiteLengthInputStream).ReadTaggedObject(flag, tagNo);
			}
			if (!flag)
			{
				return Asn1InputStream.CreatePrimitiveDerObject(tagNo, definiteLengthInputStream, this.tmpBuffers);
			}
			if (tagNo == 4)
			{
				Asn1EncodableVector asn1EncodableVector = this.ReadVector(definiteLengthInputStream);
				Asn1OctetString[] array = new Asn1OctetString[asn1EncodableVector.Count];
				for (int num = 0; num != array.Length; num++)
				{
					Asn1Encodable asn1Encodable = asn1EncodableVector[num];
					if (!(asn1Encodable is Asn1OctetString))
					{
						throw new Asn1Exception("unknown object encountered in constructed OCTET STRING: " + Platform.GetTypeName(asn1Encodable));
					}
					array[num] = (Asn1OctetString)asn1Encodable;
				}
				return new BerOctetString(array);
			}
			if (tagNo == 8)
			{
				return new DerExternal(this.ReadVector(definiteLengthInputStream));
			}
			switch (tagNo)
			{
			case 16:
				return this.CreateDerSequence(definiteLengthInputStream);
			case 17:
				return this.CreateDerSet(definiteLengthInputStream);
			default:
				throw new IOException("unknown tag " + tagNo + " encountered");
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00067D58 File Offset: 0x00067D58
		internal virtual Asn1EncodableVector ReadVector(DefiniteLengthInputStream dIn)
		{
			if (dIn.Remaining < 1)
			{
				return new Asn1EncodableVector(0);
			}
			Asn1InputStream asn1InputStream = new Asn1InputStream(dIn);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			Asn1Object element;
			while ((element = asn1InputStream.ReadObject()) != null)
			{
				asn1EncodableVector.Add(element);
			}
			return asn1EncodableVector;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00067DA0 File Offset: 0x00067DA0
		internal virtual DerSequence CreateDerSequence(DefiniteLengthInputStream dIn)
		{
			return DerSequence.FromVector(this.ReadVector(dIn));
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00067DB0 File Offset: 0x00067DB0
		internal virtual DerSet CreateDerSet(DefiniteLengthInputStream dIn)
		{
			return DerSet.FromVector(this.ReadVector(dIn), false);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00067DC0 File Offset: 0x00067DC0
		public Asn1Object ReadObject()
		{
			int num = this.ReadByte();
			if (num <= 0)
			{
				if (num == 0)
				{
					throw new IOException("unexpected end-of-contents marker");
				}
				return null;
			}
			else
			{
				int num2 = Asn1InputStream.ReadTagNumber(this.s, num);
				bool flag = (num & 32) != 0;
				int num3 = Asn1InputStream.ReadLength(this.s, this.limit, false);
				if (num3 >= 0)
				{
					Asn1Object result;
					try
					{
						result = this.BuildObject(num, num2, num3);
					}
					catch (ArgumentException exception)
					{
						throw new Asn1Exception("corrupted stream detected", exception);
					}
					return result;
				}
				if (!flag)
				{
					throw new IOException("indefinite-length primitive encoding encountered");
				}
				IndefiniteLengthInputStream inStream = new IndefiniteLengthInputStream(this.s, this.limit);
				Asn1StreamParser parser = new Asn1StreamParser(inStream, this.limit);
				if ((num & 64) != 0)
				{
					return new BerApplicationSpecificParser(num2, parser).ToAsn1Object();
				}
				if ((num & 128) != 0)
				{
					return new BerTaggedObjectParser(true, num2, parser).ToAsn1Object();
				}
				int num4 = num2;
				if (num4 == 4)
				{
					return new BerOctetStringParser(parser).ToAsn1Object();
				}
				if (num4 == 8)
				{
					return new DerExternalParser(parser).ToAsn1Object();
				}
				switch (num4)
				{
				case 16:
					return new BerSequenceParser(parser).ToAsn1Object();
				case 17:
					return new BerSetParser(parser).ToAsn1Object();
				default:
					throw new IOException("unknown BER object encountered");
				}
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00067F20 File Offset: 0x00067F20
		internal virtual int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00067F28 File Offset: 0x00067F28
		internal static int ReadTagNumber(Stream s, int tag)
		{
			int num = tag & 31;
			if (num == 31)
			{
				num = 0;
				int num2 = s.ReadByte();
				if ((num2 & 127) == 0)
				{
					throw new IOException("corrupted stream - invalid high tag number found");
				}
				while (num2 >= 0 && (num2 & 128) != 0)
				{
					num |= (num2 & 127);
					num <<= 7;
					num2 = s.ReadByte();
				}
				if (num2 < 0)
				{
					throw new EndOfStreamException("EOF found inside tag value.");
				}
				num |= (num2 & 127);
			}
			return num;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00067FA0 File Offset: 0x00067FA0
		internal static int ReadLength(Stream s, int limit, bool isParsing)
		{
			int num = s.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException("EOF found when length expected");
			}
			if (num == 128)
			{
				return -1;
			}
			if (num > 127)
			{
				int num2 = num & 127;
				if (num2 > 4)
				{
					throw new IOException("DER length more than 4 bytes: " + num2);
				}
				num = 0;
				for (int i = 0; i < num2; i++)
				{
					int num3 = s.ReadByte();
					if (num3 < 0)
					{
						throw new EndOfStreamException("EOF found reading length");
					}
					num = (num << 8) + num3;
				}
				if (num < 0)
				{
					throw new IOException("corrupted stream - negative length found");
				}
				if (num >= limit && !isParsing)
				{
					throw new IOException(string.Concat(new object[]
					{
						"corrupted stream - out of bounds length found: ",
						num,
						" >= ",
						limit
					}));
				}
			}
			return num;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0006808C File Offset: 0x0006808C
		private static byte[] GetBuffer(DefiniteLengthInputStream defIn, byte[][] tmpBuffers)
		{
			int remaining = defIn.Remaining;
			if (remaining >= tmpBuffers.Length)
			{
				return defIn.ToArray();
			}
			byte[] array = tmpBuffers[remaining];
			if (array == null)
			{
				array = (tmpBuffers[remaining] = new byte[remaining]);
			}
			defIn.ReadAllIntoByteArray(array);
			return array;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000680D8 File Offset: 0x000680D8
		private static char[] GetBmpCharBuffer(DefiniteLengthInputStream defIn)
		{
			int i = defIn.Remaining;
			if ((i & 1) != 0)
			{
				throw new IOException("malformed BMPString encoding encountered");
			}
			char[] array = new char[i / 2];
			int num = 0;
			byte[] array2 = new byte[8];
			while (i >= 8)
			{
				if (Streams.ReadFully(defIn, array2, 0, 8) != 8)
				{
					throw new EndOfStreamException("EOF encountered in middle of BMPString");
				}
				array[num] = (char)((int)array2[0] << 8 | (int)(array2[1] & byte.MaxValue));
				array[num + 1] = (char)((int)array2[2] << 8 | (int)(array2[3] & byte.MaxValue));
				array[num + 2] = (char)((int)array2[4] << 8 | (int)(array2[5] & byte.MaxValue));
				array[num + 3] = (char)((int)array2[6] << 8 | (int)(array2[7] & byte.MaxValue));
				num += 4;
				i -= 8;
			}
			if (i > 0)
			{
				if (Streams.ReadFully(defIn, array2, 0, i) != i)
				{
					throw new EndOfStreamException("EOF encountered in middle of BMPString");
				}
				int num2 = 0;
				do
				{
					int num3 = (int)array2[num2++] << 8;
					int num4 = (int)(array2[num2++] & byte.MaxValue);
					array[num++] = (char)(num3 | num4);
				}
				while (num2 < i);
			}
			if (defIn.Remaining != 0 || array.Length != num)
			{
				throw new InvalidOperationException();
			}
			return array;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00068204 File Offset: 0x00068204
		internal static Asn1Object CreatePrimitiveDerObject(int tagNo, DefiniteLengthInputStream defIn, byte[][] tmpBuffers)
		{
			if (tagNo <= 6)
			{
				if (tagNo == 1)
				{
					return DerBoolean.FromOctetString(Asn1InputStream.GetBuffer(defIn, tmpBuffers));
				}
				if (tagNo == 6)
				{
					return DerObjectIdentifier.FromOctetString(Asn1InputStream.GetBuffer(defIn, tmpBuffers));
				}
			}
			else
			{
				if (tagNo == 10)
				{
					return DerEnumerated.FromOctetString(Asn1InputStream.GetBuffer(defIn, tmpBuffers));
				}
				if (tagNo == 30)
				{
					return new DerBmpString(Asn1InputStream.GetBmpCharBuffer(defIn));
				}
			}
			byte[] array = defIn.ToArray();
			switch (tagNo)
			{
			case 2:
				return new DerInteger(array, false);
			case 3:
				return DerBitString.FromAsn1Octets(array);
			case 4:
				return new DerOctetString(array);
			case 5:
				return DerNull.Instance;
			case 12:
				return new DerUtf8String(array);
			case 18:
				return new DerNumericString(array);
			case 19:
				return new DerPrintableString(array);
			case 20:
				return new DerT61String(array);
			case 21:
				return new DerVideotexString(array);
			case 22:
				return new DerIA5String(array);
			case 23:
				return new DerUtcTime(array);
			case 24:
				return new DerGeneralizedTime(array);
			case 25:
				return new DerGraphicString(array);
			case 26:
				return new DerVisibleString(array);
			case 27:
				return new DerGeneralString(array);
			case 28:
				return new DerUniversalString(array);
			}
			throw new IOException("unknown tag " + tagNo + " encountered");
		}

		// Token: 0x04000D5D RID: 3421
		private readonly int limit;

		// Token: 0x04000D5E RID: 3422
		private readonly byte[][] tmpBuffers;
	}
}
