using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007B2 RID: 1970
	[ComVisible(true)]
	public static class FrameworkRedirect
	{
		// Token: 0x06004740 RID: 18240 RVA: 0x001716E4 File Offset: 0x001716E4
		static FrameworkRedirect()
		{
			FrameworkRedirect.InitFrameworkRedirectV2();
			FrameworkRedirect.InitFrameworkRedirectV4();
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00171710 File Offset: 0x00171710
		private static void InitFrameworkRedirectV2()
		{
			FrameworkRedirect.frmRedir2["Accessibility"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["cscompmgd"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["CustomMarshalers"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["IEExecRemote"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["IEHost"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["IIEHost"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["ISymWrapper"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.JScript"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.VisualBasic"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.VisualBasic.Compatibility"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.VisualBasic.Compatibility.Data"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.VisualBasic.Vsa"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.VisualC"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.Vsa"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft.Vsa.Vb.CodeDOMProcessor"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["Microsoft_VsaVb"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "8.0.0.0");
			FrameworkRedirect.frmRedir2["mscorcfg"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["mscorlib"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Configuration"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Configuration.Install"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Data"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Data.OracleClient"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Data.SqlXml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Deployment"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.DirectoryServices"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.DirectoryServices.Protocols"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Drawing"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Drawing.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.EnterpriseServices"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Management"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Messaging"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Runtime.Remoting"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Runtime.Serialization.Formatters.Soap"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Security"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.ServiceProcess"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Transactions"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Web"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Web.Mobile"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Web.RegularExpressions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Web.Services"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Windows.Forms"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["System.Xml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjscor"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["VJSharpCodeProvider"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjsJBC"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjslib"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjslibcw"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["Vjssupuilib"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjsvwaux"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjswfc"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["VJSWfcBrowserStubLib"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjswfccw"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir2["vjswfchtml"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x00171D94 File Offset: 0x00171D94
		private static void InitFrameworkRedirectV4()
		{
			FrameworkRedirect.frmRedir4["Accessibility"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["CustomMarshalers"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["ISymWrapper"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.JScript"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "10.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.VisualBasic"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "10.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.VisualBasic.Compatibility"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "10.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.VisualBasic.Compatibility.Data"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "10.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.VisualC"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "10.0.0.0");
			FrameworkRedirect.frmRedir4["mscorlib"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Configuration"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Configuration.Install"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.OracleClient"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.SqlXml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Deployment"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.DirectoryServices"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.DirectoryServices.Protocols"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Drawing"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Drawing.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.EnterpriseServices"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Management"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Messaging"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Remoting"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Serialization.Formatters.Soap"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Security"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceProcess"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Transactions"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Mobile"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.RegularExpressions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Services"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows.Forms"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["AspNetMMCExt"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["sysglobl"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Build.Engine"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Build.Framework"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationCFFRasterizer"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationCore"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework.Aero"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework.Classic"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework.Luna"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework.Royale"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationUI"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["ReachFramework"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Printing"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Speech"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["UIAutomationClient"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["UIAutomationClientsideProviders"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["UIAutomationProvider"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["UIAutomationTypes"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["WindowsBase"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["WindowsFormsIntegration"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["SMDiagnostics"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IdentityModel"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IdentityModel.Selectors"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IO.Log"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Serialization"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Install"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.WasHosting"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Workflow.Activities"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Workflow.ComponentModel"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Workflow.Runtime"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Transactions.Bridge"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Transactions.Bridge.Dtc"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.AddIn"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.AddIn.Contract"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ComponentModel.Composition"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Core"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.DataSetExtensions"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.Linq"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xml.Linq"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.DirectoryServices.AccountManagement"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Management.Instrumentation"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Web"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Extensions"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Extensions.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows.Presentation"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.WorkflowServices"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ComponentModel.DataAnnotations"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.Entity"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.Entity.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.Services"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.Services.Client"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Data.Services.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Abstractions"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.DynamicData"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.DynamicData.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Entity"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Entity.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.Routing"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Build"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.CSharp"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Dynamic"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Numerics"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xaml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Workflow.Compiler"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Activities.Build"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Build.Conversion.v4.0"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Build.Tasks.v4.0"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Build.Utilities.v4.0"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Internal.Tasks.Dataflow"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.VisualBasic.Activities.Compiler"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "10.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.VisualC.STLCLR"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "2.0.0.0");
			FrameworkRedirect.frmRedir4["Microsoft.Windows.ApplicationServer.Applications"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationBuildTasks"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework.Aero2"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework.AeroLite"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework-SystemCore"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework-SystemData"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework-SystemDrawing"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework-SystemXml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["PresentationFramework-SystemXmlLinq"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Activities"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Activities.Core.Presentation"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Activities.DurableInstancing"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Activities.Presentation"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ComponentModel.Composition.Registration"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Device"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IdentityModel.Services"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IO.Compression"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IO.Compression.FileSystem"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net.Http"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net.Http.WebRequest"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection.Context"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Caching"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.DurableInstancing"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.WindowsRuntime"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.WindowsRuntime.UI.Xaml"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Activation"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Activities"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Channels"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Discovery"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Internals"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Routing"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.ServiceMoniker40"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.ApplicationServices"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.DataVisualization"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Web.DataVisualization.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows.Controls.Ribbon"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows.Forms.DataVisualization"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows.Forms.DataVisualization.Design"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows.Input.Manipulations"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xaml.Hosting"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["XamlBuildTask"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["XsdBuildTask"] = new FrameworkRedirect.FrameworkRedirectInfo("31bf3856ad364e35", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Collections"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Collections.Concurrent"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ComponentModel"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ComponentModel.Annotations"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ComponentModel.EventBasedAsync"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Diagnostics.Contracts"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Diagnostics.Debug"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Diagnostics.Tools"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Diagnostics.Tracing"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Dynamic.Runtime"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Globalization"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.IO"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Linq"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Linq.Expressions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Linq.Parallel"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Linq.Queryable"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net.NetworkInformation"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net.Primitives"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net.Requests"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ObjectModel"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection.Emit"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection.Emit.ILGeneration"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection.Emit.Lightweight"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection.Extensions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Reflection.Primitives"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Resources.ResourceManager"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Extensions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.InteropServices"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.InteropServices.WindowsRuntime"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Numerics"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Serialization.Json"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Serialization.Primitives"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Runtime.Serialization.Xml"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Security.Principal"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Duplex"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Http"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.NetTcp"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Primitives"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.ServiceModel.Security"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Text.Encoding"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Text.Encoding.Extensions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Text.RegularExpressions"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Threading"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Threading.Timer"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Threading.Tasks"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Threading.Tasks.Parallel"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xml.ReaderWriter"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xml.XDocument"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xml.XmlSerializer"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Net.Http.Rtc"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Windows"] = new FrameworkRedirect.FrameworkRedirectInfo("b03f5f7f11d50a3a", "4.0.0.0");
			FrameworkRedirect.frmRedir4["System.Xml.Serialization"] = new FrameworkRedirect.FrameworkRedirectInfo("b77a5c561934e089", "4.0.0.0");
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x001735AC File Offset: 0x001735AC
		public static void ApplyFrameworkRedirect(ref IAssembly assembly, ModuleDef sourceModule)
		{
			IAssembly assembly2;
			if (FrameworkRedirect.TryApplyFrameworkRedirectCore(assembly, sourceModule, out assembly2))
			{
				assembly = assembly2;
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x001735D0 File Offset: 0x001735D0
		public static bool TryApplyFrameworkRedirect(IAssembly assembly, ModuleDef sourceModule, out IAssembly redirectedAssembly)
		{
			return FrameworkRedirect.TryApplyFrameworkRedirectCore(assembly, sourceModule, out redirectedAssembly);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x001735DC File Offset: 0x001735DC
		private static bool TryApplyFrameworkRedirectCore(IAssembly assembly, ModuleDef sourceModule, out IAssembly redirectedAssembly)
		{
			if (sourceModule != null)
			{
				if (sourceModule.IsClr40)
				{
					return FrameworkRedirect.TryApplyFrameworkRedirect(assembly, FrameworkRedirect.frmRedir4, out redirectedAssembly);
				}
				if (sourceModule.IsClr20)
				{
					return FrameworkRedirect.TryApplyFrameworkRedirect(assembly, FrameworkRedirect.frmRedir2, out redirectedAssembly);
				}
			}
			redirectedAssembly = null;
			return false;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00173618 File Offset: 0x00173618
		public static void ApplyFrameworkRedirectV2(ref IAssembly assembly)
		{
			IAssembly assembly2;
			if (FrameworkRedirect.TryApplyFrameworkRedirect(assembly, FrameworkRedirect.frmRedir2, out assembly2))
			{
				assembly = assembly2;
			}
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00173640 File Offset: 0x00173640
		public static void ApplyFrameworkRedirectV4(ref IAssembly assembly)
		{
			IAssembly assembly2;
			if (FrameworkRedirect.TryApplyFrameworkRedirect(assembly, FrameworkRedirect.frmRedir4, out assembly2))
			{
				assembly = assembly2;
			}
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00173668 File Offset: 0x00173668
		public static bool TryApplyFrameworkRedirectV2(IAssembly assembly, out IAssembly redirectedAssembly)
		{
			return FrameworkRedirect.TryApplyFrameworkRedirect(assembly, FrameworkRedirect.frmRedir2, out redirectedAssembly);
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00173678 File Offset: 0x00173678
		public static bool TryApplyFrameworkRedirectV4(IAssembly assembly, out IAssembly redirectedAssembly)
		{
			return FrameworkRedirect.TryApplyFrameworkRedirect(assembly, FrameworkRedirect.frmRedir4, out redirectedAssembly);
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00173688 File Offset: 0x00173688
		private static bool TryApplyFrameworkRedirect(IAssembly assembly, Dictionary<string, FrameworkRedirect.FrameworkRedirectInfo> frmRedir, out IAssembly redirectedAssembly)
		{
			redirectedAssembly = null;
			if (!Utils.LocaleEquals(assembly.Culture, ""))
			{
				return false;
			}
			FrameworkRedirect.FrameworkRedirectInfo frameworkRedirectInfo;
			if (!frmRedir.TryGetValue(assembly.Name, out frameworkRedirectInfo))
			{
				return false;
			}
			if (PublicKeyBase.TokenCompareTo(assembly.PublicKeyOrToken, frameworkRedirectInfo.publicKeyToken) != 0)
			{
				return false;
			}
			if (Utils.CompareTo(assembly.Version, frameworkRedirectInfo.redirectVersion) == 0)
			{
				redirectedAssembly = assembly;
			}
			else
			{
				redirectedAssembly = new AssemblyNameInfo(assembly);
				redirectedAssembly.Version = frameworkRedirectInfo.redirectVersion;
			}
			return true;
		}

		// Token: 0x040024DD RID: 9437
		private static readonly Dictionary<string, FrameworkRedirect.FrameworkRedirectInfo> frmRedir2 = new Dictionary<string, FrameworkRedirect.FrameworkRedirectInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040024DE RID: 9438
		private static readonly Dictionary<string, FrameworkRedirect.FrameworkRedirectInfo> frmRedir4 = new Dictionary<string, FrameworkRedirect.FrameworkRedirectInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000FE3 RID: 4067
		private readonly struct FrameworkRedirectInfo
		{
			// Token: 0x06008E5F RID: 36447 RVA: 0x002A9C2C File Offset: 0x002A9C2C
			public FrameworkRedirectInfo(string publicKeyToken, string redirectVersion)
			{
				this.publicKeyToken = new PublicKeyToken(publicKeyToken);
				this.redirectVersion = new Version(redirectVersion);
			}

			// Token: 0x040043C0 RID: 17344
			public readonly PublicKeyToken publicKeyToken;

			// Token: 0x040043C1 RID: 17345
			public readonly Version redirectVersion;
		}
	}
}
