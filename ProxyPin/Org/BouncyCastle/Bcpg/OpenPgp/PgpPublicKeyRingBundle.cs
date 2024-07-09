using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000660 RID: 1632
	public class PgpPublicKeyRingBundle
	{
		// Token: 0x060038B6 RID: 14518 RVA: 0x001310CC File Offset: 0x001310CC
		private PgpPublicKeyRingBundle(IDictionary pubRings, IList order)
		{
			this.pubRings = pubRings;
			this.order = order;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x001310E4 File Offset: 0x001310E4
		public PgpPublicKeyRingBundle(byte[] encoding) : this(new MemoryStream(encoding, false))
		{
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x001310F4 File Offset: 0x001310F4
		public PgpPublicKeyRingBundle(Stream inputStream) : this(new PgpObjectFactory(inputStream).AllPgpObjects())
		{
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00131108 File Offset: 0x00131108
		public PgpPublicKeyRingBundle(IEnumerable e)
		{
			this.pubRings = Platform.CreateHashtable();
			this.order = Platform.CreateArrayList();
			foreach (object obj in e)
			{
				PgpPublicKeyRing pgpPublicKeyRing = obj as PgpPublicKeyRing;
				if (pgpPublicKeyRing == null)
				{
					throw new PgpException(Platform.GetTypeName(obj) + " found where PgpPublicKeyRing expected");
				}
				long keyId = pgpPublicKeyRing.GetPublicKey().KeyId;
				this.pubRings.Add(keyId, pgpPublicKeyRing);
				this.order.Add(keyId);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x001311CC File Offset: 0x001311CC
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x001311DC File Offset: 0x001311DC
		public int Count
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x001311EC File Offset: 0x001311EC
		public IEnumerable GetKeyRings()
		{
			return new EnumerableProxy(this.pubRings.Values);
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x00131200 File Offset: 0x00131200
		public IEnumerable GetKeyRings(string userId)
		{
			return this.GetKeyRings(userId, false, false);
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0013120C File Offset: 0x0013120C
		public IEnumerable GetKeyRings(string userId, bool matchPartial)
		{
			return this.GetKeyRings(userId, matchPartial, false);
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00131218 File Offset: 0x00131218
		public IEnumerable GetKeyRings(string userId, bool matchPartial, bool ignoreCase)
		{
			IList list = Platform.CreateArrayList();
			if (ignoreCase)
			{
				userId = Platform.ToUpperInvariant(userId);
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)obj;
				foreach (object obj2 in pgpPublicKeyRing.GetPublicKey().GetUserIds())
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
							list.Add(pgpPublicKeyRing);
						}
					}
					else if (text2.Equals(userId))
					{
						list.Add(pgpPublicKeyRing);
					}
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x00131330 File Offset: 0x00131330
		public PgpPublicKey GetPublicKey(long keyId)
		{
			foreach (object obj in this.GetKeyRings())
			{
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)obj;
				PgpPublicKey publicKey = pgpPublicKeyRing.GetPublicKey(keyId);
				if (publicKey != null)
				{
					return publicKey;
				}
			}
			return null;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x001313A8 File Offset: 0x001313A8
		public PgpPublicKeyRing GetPublicKeyRing(long keyId)
		{
			if (this.pubRings.Contains(keyId))
			{
				return (PgpPublicKeyRing)this.pubRings[keyId];
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)obj;
				PgpPublicKey publicKey = pgpPublicKeyRing.GetPublicKey(keyId);
				if (publicKey != null)
				{
					return pgpPublicKeyRing;
				}
			}
			return null;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x0013144C File Offset: 0x0013144C
		public bool Contains(long keyID)
		{
			return this.GetPublicKey(keyID) != null;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x0013145C File Offset: 0x0013145C
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x00131480 File Offset: 0x00131480
		public void Encode(Stream outStr)
		{
			BcpgOutputStream outStr2 = BcpgOutputStream.Wrap(outStr);
			foreach (object obj in this.order)
			{
				long num = (long)obj;
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)this.pubRings[num];
				pgpPublicKeyRing.Encode(outStr2);
			}
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x00131504 File Offset: 0x00131504
		public static PgpPublicKeyRingBundle AddPublicKeyRing(PgpPublicKeyRingBundle bundle, PgpPublicKeyRing publicKeyRing)
		{
			long keyId = publicKeyRing.GetPublicKey().KeyId;
			if (bundle.pubRings.Contains(keyId))
			{
				throw new ArgumentException("Bundle already contains a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = Platform.CreateHashtable(bundle.pubRings);
			IList list = Platform.CreateArrayList(bundle.order);
			dictionary[keyId] = publicKeyRing;
			list.Add(keyId);
			return new PgpPublicKeyRingBundle(dictionary, list);
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x0013157C File Offset: 0x0013157C
		public static PgpPublicKeyRingBundle RemovePublicKeyRing(PgpPublicKeyRingBundle bundle, PgpPublicKeyRing publicKeyRing)
		{
			long keyId = publicKeyRing.GetPublicKey().KeyId;
			if (!bundle.pubRings.Contains(keyId))
			{
				throw new ArgumentException("Bundle does not contain a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = Platform.CreateHashtable(bundle.pubRings);
			IList list = Platform.CreateArrayList(bundle.order);
			dictionary.Remove(keyId);
			list.Remove(keyId);
			return new PgpPublicKeyRingBundle(dictionary, list);
		}

		// Token: 0x04001DE4 RID: 7652
		private readonly IDictionary pubRings;

		// Token: 0x04001DE5 RID: 7653
		private readonly IList order;
	}
}
