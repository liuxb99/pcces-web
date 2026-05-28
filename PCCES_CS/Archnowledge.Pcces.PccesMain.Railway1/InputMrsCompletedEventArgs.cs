using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Archnowledge.Pcces.PccesMain.Railway1;

[DesignerCategory("code")]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DebuggerStepThrough]
public class InputMrsCompletedEventArgs : AsyncCompletedEventArgs
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

	internal InputMrsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
