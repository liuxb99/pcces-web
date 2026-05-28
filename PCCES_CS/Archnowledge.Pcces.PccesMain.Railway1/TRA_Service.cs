using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace Archnowledge.Pcces.PccesMain.Railway1;

[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DesignerCategory("code")]
[WebServiceBinding(Name = "TRA_ServiceSoap", Namespace = "http://tempuri.org/")]
public class TRA_Service : SoapHttpClientProtocol
{
	private SendOrPostCallback GetProjectCodeOperationCompleted;

	private SendOrPostCallback OutputMrsOperationCompleted;

	private SendOrPostCallback InputMrsOperationCompleted;

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

	public event GetProjectCodeCompletedEventHandler GetProjectCodeCompleted;

	public event OutputMrsCompletedEventHandler OutputMrsCompleted;

	public event InputMrsCompletedEventHandler InputMrsCompleted;

	public TRA_Service()
	{
		Url = "http://dar-nb/ws_tra/tra_services.asmx";
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

	[SoapDocumentMethod("http://tempuri.org/GetProjectCode", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetProjectCode(string UseKey)
	{
		object[] results = Invoke("GetProjectCode", new object[1] { UseKey });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetProjectCode(string UseKey, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetProjectCode", new object[1] { UseKey }, callback, asyncState);
	}

	public DataSet EndGetProjectCode(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetProjectCodeAsync(string UseKey)
	{
		GetProjectCodeAsync(UseKey, null);
	}

	public void GetProjectCodeAsync(string UseKey, object userState)
	{
		if (GetProjectCodeOperationCompleted == null)
		{
			GetProjectCodeOperationCompleted = OnGetProjectCodeOperationCompleted;
		}
		InvokeAsync("GetProjectCode", new object[1] { UseKey }, GetProjectCodeOperationCompleted, userState);
	}

	private void OnGetProjectCodeOperationCompleted(object arg)
	{
		if (this.GetProjectCodeCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetProjectCodeCompleted(this, new GetProjectCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/OutputMrs", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet OutputMrs(string ConnStr, string UseKey)
	{
		object[] results = Invoke("OutputMrs", new object[2] { ConnStr, UseKey });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginOutputMrs(string ConnStr, string UseKey, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("OutputMrs", new object[2] { ConnStr, UseKey }, callback, asyncState);
	}

	public DataSet EndOutputMrs(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void OutputMrsAsync(string ConnStr, string UseKey)
	{
		OutputMrsAsync(ConnStr, UseKey, null);
	}

	public void OutputMrsAsync(string ConnStr, string UseKey, object userState)
	{
		if (OutputMrsOperationCompleted == null)
		{
			OutputMrsOperationCompleted = OnOutputMrsOperationCompleted;
		}
		InvokeAsync("OutputMrs", new object[2] { ConnStr, UseKey }, OutputMrsOperationCompleted, userState);
	}

	private void OnOutputMrsOperationCompleted(object arg)
	{
		if (this.OutputMrsCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.OutputMrsCompleted(this, new OutputMrsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/InputMrs", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public bool InputMrs(string ConnStr, DataSet ds, string UseKey)
	{
		object[] results = Invoke("InputMrs", new object[3] { ConnStr, ds, UseKey });
		return (bool)results[0];
	}

	public IAsyncResult BeginInputMrs(string ConnStr, DataSet ds, string UseKey, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("InputMrs", new object[3] { ConnStr, ds, UseKey }, callback, asyncState);
	}

	public bool EndInputMrs(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (bool)results[0];
	}

	public void InputMrsAsync(string ConnStr, DataSet ds, string UseKey)
	{
		InputMrsAsync(ConnStr, ds, UseKey, null);
	}

	public void InputMrsAsync(string ConnStr, DataSet ds, string UseKey, object userState)
	{
		if (InputMrsOperationCompleted == null)
		{
			InputMrsOperationCompleted = OnInputMrsOperationCompleted;
		}
		InvokeAsync("InputMrs", new object[3] { ConnStr, ds, UseKey }, InputMrsOperationCompleted, userState);
	}

	private void OnInputMrsOperationCompleted(object arg)
	{
		if (this.InputMrsCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.InputMrsCompleted(this, new InputMrsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
