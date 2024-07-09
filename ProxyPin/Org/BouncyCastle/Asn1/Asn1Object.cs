using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000E2 RID: 226
	public abstract class Asn1Object : Asn1Encodable
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x00042418 File Offset: 0x00042418
		public static Asn1Object FromByteArray(byte[] data)
		{
			Asn1Object result;
			try
			{
				MemoryStream memoryStream = new MemoryStream(data, false);
				Asn1InputStream asn1InputStream = new Asn1InputStream(memoryStream, data.Length);
				Asn1Object asn1Object = asn1InputStream.ReadObject();
				if (memoryStream.Position != memoryStream.Length)
				{
					throw new IOException("extra data found after object");
				}
				result = asn1Object;
			}
			catch (InvalidCastException)
			{
				throw new IOException("cannot recognise object in byte array");
			}
			return result;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00042480 File Offset: 0x00042480
		public static Asn1Object FromStream(Stream inStr)
		{
			Asn1Object result;
			try
			{
				result = new Asn1InputStream(inStr).ReadObject();
			}
			catch (InvalidCastException)
			{
				throw new IOException("cannot recognise object in stream");
			}
			return result;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000424BC File Offset: 0x000424BC
		public sealed override Asn1Object ToAsn1Object()
		{
			return this;
		}

		// Token: 0x0600086A RID: 2154
		internal abstract void Encode(DerOutputStream derOut);

		// Token: 0x0600086B RID: 2155
		protected abstract bool Asn1Equals(Asn1Object asn1Object);

		// Token: 0x0600086C RID: 2156
		protected abstract int Asn1GetHashCode();

		// Token: 0x0600086D RID: 2157 RVA: 0x000424C0 File Offset: 0x000424C0
		internal bool CallAsn1Equals(Asn1Object obj)
		{
			return this.Asn1Equals(obj);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000424CC File Offset: 0x000424CC
		internal int CallAsn1GetHashCode()
		{
			return this.Asn1GetHashCode();
		}
	}
}
