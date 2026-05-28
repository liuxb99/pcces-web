using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Archnowledge.Pcces.PccesMain.PccesUpdateServices;

[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class IsOK_forUpdate_ByRandomCompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	public bool Result
	{
		get
		{
			RaiseExceptionIfNecessary();
			return (bool)results[0];
		}
	}

	internal IsOK_forUpdate_ByRandomCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
