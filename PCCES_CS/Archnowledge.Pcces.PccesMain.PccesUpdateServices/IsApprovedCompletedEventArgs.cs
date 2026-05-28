using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Archnowledge.Pcces.PccesMain.PccesUpdateServices;

[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DesignerCategory("code")]
[DebuggerStepThrough]
public class IsApprovedCompletedEventArgs : AsyncCompletedEventArgs
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

	internal IsApprovedCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
