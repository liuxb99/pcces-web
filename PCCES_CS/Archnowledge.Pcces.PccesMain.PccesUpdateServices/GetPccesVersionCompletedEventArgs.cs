using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Archnowledge.Pcces.PccesMain.PccesUpdateServices;

[DesignerCategory("code")]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DebuggerStepThrough]
public class GetPccesVersionCompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	public string Result
	{
		get
		{
			RaiseExceptionIfNecessary();
			return (string)results[0];
		}
	}

	internal GetPccesVersionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
