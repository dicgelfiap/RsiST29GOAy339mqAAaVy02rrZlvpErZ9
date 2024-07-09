using System;
using System.Text;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200020B RID: 523
	public class RoleSyntax : Asn1Encodable
	{
		// Token: 0x060010CA RID: 4298 RVA: 0x0006127C File Offset: 0x0006127C
		public static RoleSyntax GetInstance(object obj)
		{
			if (obj is RoleSyntax)
			{
				return (RoleSyntax)obj;
			}
			if (obj != null)
			{
				return new RoleSyntax(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000612A4 File Offset: 0x000612A4
		public RoleSyntax(GeneralNames roleAuthority, GeneralName roleName)
		{
			if (roleName == null || roleName.TagNo != 6 || ((IAsn1String)roleName.Name).GetString().Equals(""))
			{
				throw new ArgumentException("the role name MUST be non empty and MUST use the URI option of GeneralName");
			}
			this.roleAuthority = roleAuthority;
			this.roleName = roleName;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00061308 File Offset: 0x00061308
		public RoleSyntax(GeneralName roleName) : this(null, roleName)
		{
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00061314 File Offset: 0x00061314
		public RoleSyntax(string roleName) : this(new GeneralName(6, (roleName == null) ? "" : roleName))
		{
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00061334 File Offset: 0x00061334
		private RoleSyntax(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.roleAuthority = GeneralNames.GetInstance(instance, false);
					break;
				case 1:
					this.roleName = GeneralName.GetInstance(instance, true);
					break;
				default:
					throw new ArgumentException("Unknown tag in RoleSyntax");
				}
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x000613E8 File Offset: 0x000613E8
		public GeneralNames RoleAuthority
		{
			get
			{
				return this.roleAuthority;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x000613F0 File Offset: 0x000613F0
		public GeneralName RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000613F8 File Offset: 0x000613F8
		public string GetRoleNameAsString()
		{
			return ((IAsn1String)this.roleName.Name).GetString();
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00061410 File Offset: 0x00061410
		public string[] GetRoleAuthorityAsString()
		{
			if (this.roleAuthority == null)
			{
				return new string[0];
			}
			GeneralName[] names = this.roleAuthority.GetNames();
			string[] array = new string[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				Asn1Encodable name = names[i].Name;
				if (name is IAsn1String)
				{
					array[i] = ((IAsn1String)name).GetString();
				}
				else
				{
					array[i] = name.ToString();
				}
			}
			return array;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00061498 File Offset: 0x00061498
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this.roleAuthority);
			asn1EncodableVector.Add(new DerTaggedObject(true, 1, this.roleName));
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000614D8 File Offset: 0x000614D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Name: " + this.GetRoleNameAsString() + " - Auth: ");
			if (this.roleAuthority == null || this.roleAuthority.GetNames().Length == 0)
			{
				stringBuilder.Append("N/A");
			}
			else
			{
				string[] roleAuthorityAsString = this.GetRoleAuthorityAsString();
				stringBuilder.Append('[').Append(roleAuthorityAsString[0]);
				for (int i = 1; i < roleAuthorityAsString.Length; i++)
				{
					stringBuilder.Append(", ").Append(roleAuthorityAsString[i]);
				}
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000C35 RID: 3125
		private readonly GeneralNames roleAuthority;

		// Token: 0x04000C36 RID: 3126
		private readonly GeneralName roleName;
	}
}
