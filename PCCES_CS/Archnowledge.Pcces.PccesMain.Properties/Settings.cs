using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Archnowledge.Pcces.PccesMain.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DefaultSettingValue("Data Source=(local);Initial Catalog=TEST8;Integrated Security=True")]
	[SpecialSetting(SpecialSetting.ConnectionString)]
	[DebuggerNonUserCode]
	public string TEST8ConnectionString => (string)this["TEST8ConnectionString"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[SpecialSetting(SpecialSetting.WebServiceUrl)]
	[DefaultSettingValue("https://pcces.pcc.gov.tw/csinew/WSCode.asmx")]
	public string PccesMain_WSCode_WSCode => (string)this["PccesMain_WSCode_WSCode"];
}
