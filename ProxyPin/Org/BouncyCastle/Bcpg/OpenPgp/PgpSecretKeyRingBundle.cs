using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000663 RID: 1635
	public class PgpSecretKeyRingBundle
	{
		// Token: 0x0600390A RID: 14602 RVA: 0x00133218 File Offset: 0x00133218
		private PgpSecretKeyRingBundle(IDictionary secretRings, IList order)
		{
			this.secretRings = secretRings;
			this.order = order;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x00133230 File Offset: 0x00133230
		public PgpSecretKeyRingBundle(byte[] encoding) : this(new MemoryStream(encoding, false))
		{
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x00133240 File Offset: 0x00133240
		public PgpSecretKeyRingBundle(Stream inputStream) : this(new PgpObjectFactory(inputStream).AllPgpObjects())
		{
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x00133254 File Offset: 0x00133254
		public PgpSecretKeyRingBundle(IEnumerable e)
		{
			this.secretRings = Platform.CreateHashtable();
			this.order = Platform.CreateArrayList();
			foreach (object obj in e)
			{
				PgpSecretKeyRing pgpSecretKeyRing = obj as PgpSecretKeyRing;
				if (pgpSecretKeyRing == null)
				{
					throw new PgpException(Platform.GetTypeName(obj) + " found where PgpSecretKeyRing expected");
				}
				long keyId = pgpSecretKeyRing.GetPublicKey().KeyId;
				this.secretRings.Add(keyId, pgpSecretKeyRing);
				this.order.Add(keyId);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x00133318 File Offset: 0x00133318
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x00133328 File Offset: 0x00133328
		public int Count
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x00133338 File Offset: 0x00133338
		public IEnumerable GetKeyRings()
		{
			return new EnumerableProxy(this.secretRings.Values);
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x0013334C File Offset: 0x0013334C
		public IEnumerable GetKeyRings(string userId)
		{
			return this.GetKeyRings(userId, false, false);
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x00133358 File Offset: 0x00133358
		public IEnumerable GetKeyRings(string userId, bool matchPartial)
		{
			return this.GetKeyRings(userId, matchPartial, false);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x00133364 File Offset: 0x00133364
		public IEnumerable GetKeyRings(string userId, bool matchPartial, bool ignoreCase)
		{
			IList list = Platform.CreateArrayList();
			if (ignoreCase)
			{
				userId = Platform.ToUpperInvariant(userId);
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)obj;
				foreach (object obj2 in pgpSecretKeyRing.GetSecretKey().UserIds)
				{
					string text = (string)obj2;
					string text2 = text;
					if (ignoreCase)
					{
						text2 = Platform.ToUpperInvariant(text2);
					}
					if (matchPartial)
					{
						if (Platform.IndexOf(text2, userId) > -1)
						{
							list.Add(pgpSecretKeyRing);
						}
					}
					else if (text2.Equals(userId))
					{
						list.Add(pgpSecretKeyRing);
					}
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x0013347C File Offset: 0x0013347C
		public PgpSecretKey GetSecretKey(long keyId)
		{
			foreach (object obj in this.GetKeyRings())
			{
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)obj;
				PgpSecretKey secretKey = pgpSecretKeyRing.GetSecretKey(keyId);
				if (secretKey != null)
				{
					return secretKey;
				}
			}
			return null;
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x001334F4 File Offset: 0x001334F4
		public PgpSecretKeyRing GetSecretKeyRing(long keyId)
		{
			if (this.secretRings.Contains(keyId))
			{
				return (PgpSecretKeyRing)this.secretRings[keyId];
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)obj;
				PgpSecretKey secretKey = pgpSecretKeyRing.GetSecretKey(keyId);
				if (secretKey != null)
				{
					return pgpSecretKeyRing;
				}
			}
			return null;
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x0013359C File Offset: 0x0013359C
		public bool Contains(long keyID)
		{
			return this.GetSecretKey(keyID) != null;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x001335AC File Offset: 0x001335AC
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x001335D0 File Offset: 0x001335D0
		public void Encode(Stream outStr)
		{
			BcpgOutputStream outStr2 = BcpgOutputStream.Wrap(outStr);
			foreach (object obj in this.order)
			{
				long num = (long)obj;
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)this.secretRings[num];
				pgpSecretKeyRing.Encode(outStr2);
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x00133654 File Offset: 0x00133654
		public static PgpSecretKeyRingBundle AddSecretKeyRing(PgpSecretKeyRingBundle bundle, PgpSecretKeyRing secretKeyRing)
		{
			long keyId = secretKeyRing.GetPublicKey().KeyId;
			if (bundle.secretRings.Contains(keyId))
			{
				throw new ArgumentException("Collection already contains a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = Platform.CreateHashtable(bundle.secretRings);
			IList list = Platform.CreateArrayList(bundle.order);
			dictionary[keyId] = secretKeyRing;
			list.Add(keyId);
			return new PgpSecretKeyRingBundle(dictionary, list);
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x001336CC File Offset: 0x001336CC
		public static PgpSecretKeyRingBundle RemoveSecretKeyRing(PgpSecretKeyRingBundle bundle, PgpSecretKeyRing secretKeyRing)
		{
			long keyId = secretKeyRing.GetPublicKey().KeyId;
			if (!bundle.secretRings.Contains(keyId))
			{
				throw new ArgumentException("Collection does not contain a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = Platform.CreateHashtable(bundle.secretRings);
			IList list = Platform.CreateArrayList(bundle.order);
			dictionary.Remove(keyId);
			list.Remove(keyId);
			return new PgpSecretKeyRingBundle(dictionary, list);
		}

		// Token: 0x04001DEA RID: 7658
		private readonly IDictionary secretRings;

		// Token: 0x04001DEB RID: 7659
		private readonly IList order;
	}
}
