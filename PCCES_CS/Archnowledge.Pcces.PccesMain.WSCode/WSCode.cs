using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Archnowledge.Pcces.PccesMain.Properties;

namespace Archnowledge.Pcces.PccesMain.WSCode;

[WebServiceBinding(Name = "WSCodeSoap", Namespace = "http://tempuri.org/")]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class WSCode : SoapHttpClientProtocol
{
	private SendOrPostCallback ReDataDocOperationCompleted;

	private SendOrPostCallback ReEditionOperationCompleted;

	private SendOrPostCallback ReEditionNameOperationCompleted;

	private SendOrPostCallback GetChapterInfoOperationCompleted;

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

	public event ReDataDocCompletedEventHandler ReDataDocCompleted;

	public event ReEditionCompletedEventHandler ReEditionCompleted;

	public event ReEditionNameCompletedEventHandler ReEditionNameCompleted;

	public event GetChapterInfoCompletedEventHandler GetChapterInfoCompleted;

	public WSCode()
	{
		Url = Settings.Default.PccesMain_WSCode_WSCode;
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

	[SoapDocumentMethod("http://tempuri.org/ReDataDoc", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	[return: XmlElement(DataType = "base64Binary")]
	public byte[] ReDataDoc(string PccesCode)
	{
		object[] results = Invoke("ReDataDoc", new object[1] { PccesCode });
		return (byte[])results[0];
	}

	public IAsyncResult BeginReDataDoc(string PccesCode, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("ReDataDoc", new object[1] { PccesCode }, callback, asyncState);
	}

	public byte[] EndReDataDoc(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (byte[])results[0];
	}

	public void ReDataDocAsync(string PccesCode)
	{
		ReDataDocAsync(PccesCode, null);
	}

	public void ReDataDocAsync(string PccesCode, object userState)
	{
		if (ReDataDocOperationCompleted == null)
		{
			ReDataDocOperationCompleted = OnReDataDocOperationCompleted;
		}
		InvokeAsync("ReDataDoc", new object[1] { PccesCode }, ReDataDocOperationCompleted, userState);
	}

	private void OnReDataDocOperationCompleted(object arg)
	{
		if (this.ReDataDocCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.ReDataDocCompleted(this, new ReDataDocCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/ReEdition", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string ReEdition(string PccesCode)
	{
		object[] results = Invoke("ReEdition", new object[1] { PccesCode });
		return (string)results[0];
	}

	public IAsyncResult BeginReEdition(string PccesCode, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("ReEdition", new object[1] { PccesCode }, callback, asyncState);
	}

	public string EndReEdition(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void ReEditionAsync(string PccesCode)
	{
		ReEditionAsync(PccesCode, null);
	}

	public void ReEditionAsync(string PccesCode, object userState)
	{
		if (ReEditionOperationCompleted == null)
		{
			ReEditionOperationCompleted = OnReEditionOperationCompleted;
		}
		InvokeAsync("ReEdition", new object[1] { PccesCode }, ReEditionOperationCompleted, userState);
	}

	private void OnReEditionOperationCompleted(object arg)
	{
		if (this.ReEditionCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.ReEditionCompleted(this, new ReEditionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/ReEditionName", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet ReEditionName(string sSQL)
	{
		object[] results = Invoke("ReEditionName", new object[1] { sSQL });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginReEditionName(string sSQL, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("ReEditionName", new object[1] { sSQL }, callback, asyncState);
	}

	public DataSet EndReEditionName(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void ReEditionNameAsync(string sSQL)
	{
		ReEditionNameAsync(sSQL, null);
	}

	public void ReEditionNameAsync(string sSQL, object userState)
	{
		if (ReEditionNameOperationCompleted == null)
		{
			ReEditionNameOperationCompleted = OnReEditionNameOperationCompleted;
		}
		InvokeAsync("ReEditionName", new object[1] { sSQL }, ReEditionNameOperationCompleted, userState);
	}

	private void OnReEditionNameOperationCompleted(object arg)
	{
		if (this.ReEditionNameCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.ReEditionNameCompleted(this, new ReEditionNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetChapterInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetChapterInfo(string ChapterNos)
	{
		object[] results = Invoke("GetChapterInfo", new object[1] { ChapterNos });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetChapterInfo(string ChapterNos, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetChapterInfo", new object[1] { ChapterNos }, callback, asyncState);
	}

	public DataSet EndGetChapterInfo(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetChapterInfoAsync(string ChapterNos)
	{
		GetChapterInfoAsync(ChapterNos, null);
	}

	public void GetChapterInfoAsync(string ChapterNos, object userState)
	{
		if (GetChapterInfoOperationCompleted == null)
		{
			GetChapterInfoOperationCompleted = OnGetChapterInfoOperationCompleted;
		}
		InvokeAsync("GetChapterInfo", new object[1] { ChapterNos }, GetChapterInfoOperationCompleted, userState);
	}

	private void OnGetChapterInfoOperationCompleted(object arg)
	{
		if (this.GetChapterInfoCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetChapterInfoCompleted(this, new GetChapterInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
