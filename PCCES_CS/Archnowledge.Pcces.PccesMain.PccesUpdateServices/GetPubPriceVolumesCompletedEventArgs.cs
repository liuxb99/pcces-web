using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Archnowledge.Pcces.PccesMain.PccesUpdateServices;

[DesignerCategory("code")]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DebuggerStepThrough]
public class GetPubPriceVolumesCompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	public DataSet Result
	{
		get
		{
			RaiseExceptionIfNecessary();
			return (DataSet)results[0];
		}
	}

	internal GetPubPriceVolumesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
