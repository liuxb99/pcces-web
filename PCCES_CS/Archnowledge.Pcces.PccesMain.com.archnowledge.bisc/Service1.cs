using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace Archnowledge.Pcces.PccesMain.com.archnowledge.bisc;

[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[WebServiceBinding(Name = "Service1Soap", Namespace = "http://tempuri.org/")]
[DesignerCategory("code")]
[DebuggerStepThrough]
public class Service1 : SoapHttpClientProtocol
{
	private SendOrPostCallback GetCostKindOperationCompleted;

	private SendOrPostCallback GetCostListOperationCompleted;

	private bool useDefaultCredentialsSetExplicitly;

	public new string Url
	{
		get
		{
			return base.Url;
		}
		set
		{
			if (IsLocalFileSystemWebService(base.Url) && !useDefaultCredentialsSetExplicitly && !IsLocalFileSystemWebService(value))
			{
				base.UseDefaultCredentials = false;
			}
			base.Url = value;
		}
	}

	public new bool UseDefaultCredentials
	{
		get
		{
			return base.UseDefaultCredentials;
		}
		set
		{
			base.UseDefaultCredentials = value;
			useDefaultCredentialsSetExplicitly = true;
		}
	}

	public event GetCostKindCompletedEventHandler GetCostKindCompleted;

	public event GetCostListCompletedEventHandler GetCostListCompleted;

	public Service1()
	{
		Url = "http://bisc.archnowledge.com/arch_webservice/GetMrsCost.asmx";
		if (IsLocalFileSystemWebService(Url))
		{
			UseDefaultCredentials = true;
			useDefaultCredentialsSetExplicitly = false;
		}
		else
		{
			useDefaultCredentialsSetExplicitly = true;
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetCostKind", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetCostKind()
	{
		object[] results = Invoke("GetCostKind", new object[0]);
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetCostKind(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetCostKind", new object[0], callback, asyncState);
	}

	public DataSet EndGetCostKind(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetCostKindAsync()
	{
		GetCostKindAsync(null);
	}

	public void GetCostKindAsync(object userState)
	{
		if (GetCostKindOperationCompleted == null)
		{
			GetCostKindOperationCompleted = OnGetCostKindOperationCompleted;
		}
		InvokeAsync("GetCostKind", new object[0], GetCostKindOperationCompleted, userState);
	}

	private void OnGetCostKindOperationCompleted(object arg)
	{
		if (this.GetCostKindCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetCostKindCompleted(this, new GetCostKindCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetCostList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetCostList(string CostKindStr, string DownLoadKey)
	{
		object[] results = Invoke("GetCostList", new object[2] { CostKindStr, DownLoadKey });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetCostList(string CostKindStr, string DownLoadKey, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetCostList", new object[2] { CostKindStr, DownLoadKey }, callback, asyncState);
	}

	public DataSet EndGetCostList(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetCostListAsync(string CostKindStr, string DownLoadKey)
	{
		GetCostListAsync(CostKindStr, DownLoadKey, null);
	}

	public void GetCostListAsync(string CostKindStr, string DownLoadKey, object userState)
	{
		if (GetCostListOperationCompleted == null)
		{
			GetCostListOperationCompleted = OnGetCostListOperationCompleted;
		}
		InvokeAsync("GetCostList", new object[2] { CostKindStr, DownLoadKey }, GetCostListOperationCompleted, userState);
	}

	private void OnGetCostListOperationCompleted(object arg)
	{
		if (this.GetCostListCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetCostListCompleted(this, new GetCostListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	public new void CancelAsync(object userState)
	{
		base.CancelAsync(userState);
	}

	private bool IsLocalFileSystemWebService(string url)
	{
		if (url == null || url == string.Empty)
		{
			return false;
		}
		Uri wsUri = new Uri(url);
		if (wsUri.Port >= 1024 && string.Compare(wsUri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}
		return false;
	}
}
