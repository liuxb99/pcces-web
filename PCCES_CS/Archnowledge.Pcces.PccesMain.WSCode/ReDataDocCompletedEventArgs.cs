using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Archnowledge.Pcces.PccesMain.WSCode;

[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DesignerCategory("code")]
public class ReDataDocCompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	public byte[] Result
	{
		get
		{
			RaiseExceptionIfNecessary();
			return (byte[])results[0];
		}
	}

	internal ReDataDocCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
