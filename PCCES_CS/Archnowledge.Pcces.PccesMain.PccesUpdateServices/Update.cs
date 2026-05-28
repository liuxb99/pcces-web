using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace Archnowledge.Pcces.PccesMain.PccesUpdateServices;

[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
[DesignerCategory("code")]
[WebServiceBinding(Name = "UpdateSoap", Namespace = "http://tempuri.org/")]
public class Update : SoapHttpClientProtocol
{
	private SendOrPostCallback HelloWorldOperationCompleted;

	private SendOrPostCallback GetVersionOperationCompleted;

	private SendOrPostCallback TimeOperationCompleted;

	private SendOrPostCallback AutoNumUpdOperationCompleted;

	private SendOrPostCallback AutoNumUpd2OperationCompleted;

	private SendOrPostCallback AutoNumCOperationCompleted;

	private SendOrPostCallback GetAutoNumABOperationCompleted;

	private SendOrPostCallback GetAutoNumAB_12OperationCompleted;

	private SendOrPostCallback GetAutoNumAOperationCompleted;

	private SendOrPostCallback RegisterOperationCompleted;

	private SendOrPostCallback RegisterWithVersionOperationCompleted;

	private SendOrPostCallback IsStillValidOperationCompleted;

	private SendOrPostCallback IsApprovedOperationCompleted;

	private SendOrPostCallback IsOK_forUpdate_ByRandomOperationCompleted;

	private SendOrPostCallback InvReportListOperationCompleted;

	private SendOrPostCallback GetPasswordOperationCompleted;

	private SendOrPostCallback GetDownloadRouteOperationCompleted;

	private SendOrPostCallback GetPccesVersionOperationCompleted;

	private SendOrPostCallback GetUpdateFileAddressOperationCompleted;

	private SendOrPostCallback GetUpdateFileAddressWithCurrentVersionOperationCompleted;

	private SendOrPostCallback AddNewspaperVisitorCountOperationCompleted;

	private SendOrPostCallback GetPubPriceVolumesOperationCompleted;

	private SendOrPostCallback GetPubPriceDatasetOperationCompleted;

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

	public event HelloWorldCompletedEventHandler HelloWorldCompleted;

	public event GetVersionCompletedEventHandler GetVersionCompleted;

	public event TimeCompletedEventHandler TimeCompleted;

	public event AutoNumUpdCompletedEventHandler AutoNumUpdCompleted;

	public event AutoNumUpd2CompletedEventHandler AutoNumUpd2Completed;

	public event AutoNumCCompletedEventHandler AutoNumCCompleted;

	public event GetAutoNumABCompletedEventHandler GetAutoNumABCompleted;

	public event GetAutoNumAB_12CompletedEventHandler GetAutoNumAB_12Completed;

	public event GetAutoNumACompletedEventHandler GetAutoNumACompleted;

	public event RegisterCompletedEventHandler RegisterCompleted;

	public event RegisterWithVersionCompletedEventHandler RegisterWithVersionCompleted;

	public event IsStillValidCompletedEventHandler IsStillValidCompleted;

	public event IsApprovedCompletedEventHandler IsApprovedCompleted;

	public event IsOK_forUpdate_ByRandomCompletedEventHandler IsOK_forUpdate_ByRandomCompleted;

	public event InvReportListCompletedEventHandler InvReportListCompleted;

	public event GetPasswordCompletedEventHandler GetPasswordCompleted;

	public event GetDownloadRouteCompletedEventHandler GetDownloadRouteCompleted;

	public event GetPccesVersionCompletedEventHandler GetPccesVersionCompleted;

	public event GetUpdateFileAddressCompletedEventHandler GetUpdateFileAddressCompleted;

	public event GetUpdateFileAddressWithCurrentVersionCompletedEventHandler GetUpdateFileAddressWithCurrentVersionCompleted;

	public event AddNewspaperVisitorCountCompletedEventHandler AddNewspaperVisitorCountCompleted;

	public event GetPubPriceVolumesCompletedEventHandler GetPubPriceVolumesCompleted;

	public event GetPubPriceDatasetCompletedEventHandler GetPubPriceDatasetCompleted;

	public Update()
	{
		Url = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
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

	[SoapDocumentMethod("http://tempuri.org/HelloWorld", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string HelloWorld()
	{
		object[] results = Invoke("HelloWorld", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginHelloWorld(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("HelloWorld", new object[0], callback, asyncState);
	}

	public string EndHelloWorld(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void HelloWorldAsync()
	{
		HelloWorldAsync(null);
	}

	public void HelloWorldAsync(object userState)
	{
		if (HelloWorldOperationCompleted == null)
		{
			HelloWorldOperationCompleted = OnHelloWorldOperationCompleted;
		}
		InvokeAsync("HelloWorld", new object[0], HelloWorldOperationCompleted, userState);
	}

	private void OnHelloWorldOperationCompleted(object arg)
	{
		if (this.HelloWorldCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetVersion", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string GetVersion()
	{
		object[] results = Invoke("GetVersion", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginGetVersion(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetVersion", new object[0], callback, asyncState);
	}

	public string EndGetVersion(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void GetVersionAsync()
	{
		GetVersionAsync(null);
	}

	public void GetVersionAsync(object userState)
	{
		if (GetVersionOperationCompleted == null)
		{
			GetVersionOperationCompleted = OnGetVersionOperationCompleted;
		}
		InvokeAsync("GetVersion", new object[0], GetVersionOperationCompleted, userState);
	}

	private void OnGetVersionOperationCompleted(object arg)
	{
		if (this.GetVersionCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetVersionCompleted(this, new GetVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/Time", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string Time()
	{
		object[] results = Invoke("Time", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginTime(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("Time", new object[0], callback, asyncState);
	}

	public string EndTime(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void TimeAsync()
	{
		TimeAsync(null);
	}

	public void TimeAsync(object userState)
	{
		if (TimeOperationCompleted == null)
		{
			TimeOperationCompleted = OnTimeOperationCompleted;
		}
		InvokeAsync("Time", new object[0], TimeOperationCompleted, userState);
	}

	private void OnTimeOperationCompleted(object arg)
	{
		if (this.TimeCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.TimeCompleted(this, new TimeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/AutoNumUpd", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet AutoNumUpd()
	{
		object[] results = Invoke("AutoNumUpd", new object[0]);
		return (DataSet)results[0];
	}

	public IAsyncResult BeginAutoNumUpd(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("AutoNumUpd", new object[0], callback, asyncState);
	}

	public DataSet EndAutoNumUpd(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void AutoNumUpdAsync()
	{
		AutoNumUpdAsync(null);
	}

	public void AutoNumUpdAsync(object userState)
	{
		if (AutoNumUpdOperationCompleted == null)
		{
			AutoNumUpdOperationCompleted = OnAutoNumUpdOperationCompleted;
		}
		InvokeAsync("AutoNumUpd", new object[0], AutoNumUpdOperationCompleted, userState);
	}

	private void OnAutoNumUpdOperationCompleted(object arg)
	{
		if (this.AutoNumUpdCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.AutoNumUpdCompleted(this, new AutoNumUpdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/AutoNumUpd2", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet AutoNumUpd2(string Version)
	{
		object[] results = Invoke("AutoNumUpd2", new object[1] { Version });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginAutoNumUpd2(string Version, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("AutoNumUpd2", new object[1] { Version }, callback, asyncState);
	}

	public DataSet EndAutoNumUpd2(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void AutoNumUpd2Async(string Version)
	{
		AutoNumUpd2Async(Version, null);
	}

	public void AutoNumUpd2Async(string Version, object userState)
	{
		if (AutoNumUpd2OperationCompleted == null)
		{
			AutoNumUpd2OperationCompleted = OnAutoNumUpd2OperationCompleted;
		}
		InvokeAsync("AutoNumUpd2", new object[1] { Version }, AutoNumUpd2OperationCompleted, userState);
	}

	private void OnAutoNumUpd2OperationCompleted(object arg)
	{
		if (this.AutoNumUpd2Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.AutoNumUpd2Completed(this, new AutoNumUpd2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/AutoNumC", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet AutoNumC()
	{
		object[] results = Invoke("AutoNumC", new object[0]);
		return (DataSet)results[0];
	}

	public IAsyncResult BeginAutoNumC(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("AutoNumC", new object[0], callback, asyncState);
	}

	public DataSet EndAutoNumC(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void AutoNumCAsync()
	{
		AutoNumCAsync(null);
	}

	public void AutoNumCAsync(object userState)
	{
		if (AutoNumCOperationCompleted == null)
		{
			AutoNumCOperationCompleted = OnAutoNumCOperationCompleted;
		}
		InvokeAsync("AutoNumC", new object[0], AutoNumCOperationCompleted, userState);
	}

	private void OnAutoNumCOperationCompleted(object arg)
	{
		if (this.AutoNumCCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.AutoNumCCompleted(this, new AutoNumCCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetAutoNumAB", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetAutoNumAB(string itemCode)
	{
		object[] results = Invoke("GetAutoNumAB", new object[1] { itemCode });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetAutoNumAB(string itemCode, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetAutoNumAB", new object[1] { itemCode }, callback, asyncState);
	}

	public DataSet EndGetAutoNumAB(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetAutoNumABAsync(string itemCode)
	{
		GetAutoNumABAsync(itemCode, null);
	}

	public void GetAutoNumABAsync(string itemCode, object userState)
	{
		if (GetAutoNumABOperationCompleted == null)
		{
			GetAutoNumABOperationCompleted = OnGetAutoNumABOperationCompleted;
		}
		InvokeAsync("GetAutoNumAB", new object[1] { itemCode }, GetAutoNumABOperationCompleted, userState);
	}

	private void OnGetAutoNumABOperationCompleted(object arg)
	{
		if (this.GetAutoNumABCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetAutoNumABCompleted(this, new GetAutoNumABCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetAutoNumAB_12", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetAutoNumAB_12(string itemCode)
	{
		object[] results = Invoke("GetAutoNumAB_12", new object[1] { itemCode });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetAutoNumAB_12(string itemCode, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetAutoNumAB_12", new object[1] { itemCode }, callback, asyncState);
	}

	public DataSet EndGetAutoNumAB_12(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetAutoNumAB_12Async(string itemCode)
	{
		GetAutoNumAB_12Async(itemCode, null);
	}

	public void GetAutoNumAB_12Async(string itemCode, object userState)
	{
		if (GetAutoNumAB_12OperationCompleted == null)
		{
			GetAutoNumAB_12OperationCompleted = OnGetAutoNumAB_12OperationCompleted;
		}
		InvokeAsync("GetAutoNumAB_12", new object[1] { itemCode }, GetAutoNumAB_12OperationCompleted, userState);
	}

	private void OnGetAutoNumAB_12OperationCompleted(object arg)
	{
		if (this.GetAutoNumAB_12Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetAutoNumAB_12Completed(this, new GetAutoNumAB_12CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetAutoNumA", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetAutoNumA()
	{
		object[] results = Invoke("GetAutoNumA", new object[0]);
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetAutoNumA(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetAutoNumA", new object[0], callback, asyncState);
	}

	public DataSet EndGetAutoNumA(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetAutoNumAAsync()
	{
		GetAutoNumAAsync(null);
	}

	public void GetAutoNumAAsync(object userState)
	{
		if (GetAutoNumAOperationCompleted == null)
		{
			GetAutoNumAOperationCompleted = OnGetAutoNumAOperationCompleted;
		}
		InvokeAsync("GetAutoNumA", new object[0], GetAutoNumAOperationCompleted, userState);
	}

	private void OnGetAutoNumAOperationCompleted(object arg)
	{
		if (this.GetAutoNumACompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetAutoNumACompleted(this, new GetAutoNumACompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/Register", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string Register(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP)
	{
		object[] results = Invoke("Register", new object[7] { UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP });
		return (string)results[0];
	}

	public IAsyncResult BeginRegister(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("Register", new object[7] { UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP }, callback, asyncState);
	}

	public string EndRegister(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void RegisterAsync(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP)
	{
		RegisterAsync(UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP, null);
	}

	public void RegisterAsync(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP, object userState)
	{
		if (RegisterOperationCompleted == null)
		{
			RegisterOperationCompleted = OnRegisterOperationCompleted;
		}
		InvokeAsync("Register", new object[7] { UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP }, RegisterOperationCompleted, userState);
	}

	private void OnRegisterOperationCompleted(object arg)
	{
		if (this.RegisterCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.RegisterCompleted(this, new RegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/RegisterWithVersion", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string RegisterWithVersion(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP, string CurrentVersion)
	{
		object[] results = Invoke("RegisterWithVersion", new object[8] { UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP, CurrentVersion });
		return (string)results[0];
	}

	public IAsyncResult BeginRegisterWithVersion(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP, string CurrentVersion, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("RegisterWithVersion", new object[8] { UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP, CurrentVersion }, callback, asyncState);
	}

	public string EndRegisterWithVersion(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void RegisterWithVersionAsync(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP, string CurrentVersion)
	{
		RegisterWithVersionAsync(UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP, CurrentVersion, null);
	}

	public void RegisterWithVersionAsync(string UserName, string EMail, string CompanyName, string DeptName, string TEL, string MAC, string InternalIP, string CurrentVersion, object userState)
	{
		if (RegisterWithVersionOperationCompleted == null)
		{
			RegisterWithVersionOperationCompleted = OnRegisterWithVersionOperationCompleted;
		}
		InvokeAsync("RegisterWithVersion", new object[8] { UserName, EMail, CompanyName, DeptName, TEL, MAC, InternalIP, CurrentVersion }, RegisterWithVersionOperationCompleted, userState);
	}

	private void OnRegisterWithVersionOperationCompleted(object arg)
	{
		if (this.RegisterWithVersionCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.RegisterWithVersionCompleted(this, new RegisterWithVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/IsStillValid", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public bool IsStillValid(string RegID, string UserName, string EMail, string MAC)
	{
		object[] results = Invoke("IsStillValid", new object[4] { RegID, UserName, EMail, MAC });
		return (bool)results[0];
	}

	public IAsyncResult BeginIsStillValid(string RegID, string UserName, string EMail, string MAC, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("IsStillValid", new object[4] { RegID, UserName, EMail, MAC }, callback, asyncState);
	}

	public bool EndIsStillValid(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (bool)results[0];
	}

	public void IsStillValidAsync(string RegID, string UserName, string EMail, string MAC)
	{
		IsStillValidAsync(RegID, UserName, EMail, MAC, null);
	}

	public void IsStillValidAsync(string RegID, string UserName, string EMail, string MAC, object userState)
	{
		if (IsStillValidOperationCompleted == null)
		{
			IsStillValidOperationCompleted = OnIsStillValidOperationCompleted;
		}
		InvokeAsync("IsStillValid", new object[4] { RegID, UserName, EMail, MAC }, IsStillValidOperationCompleted, userState);
	}

	private void OnIsStillValidOperationCompleted(object arg)
	{
		if (this.IsStillValidCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.IsStillValidCompleted(this, new IsStillValidCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/IsApproved", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public bool IsApproved(string RegID)
	{
		object[] results = Invoke("IsApproved", new object[1] { RegID });
		return (bool)results[0];
	}

	public IAsyncResult BeginIsApproved(string RegID, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("IsApproved", new object[1] { RegID }, callback, asyncState);
	}

	public bool EndIsApproved(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (bool)results[0];
	}

	public void IsApprovedAsync(string RegID)
	{
		IsApprovedAsync(RegID, null);
	}

	public void IsApprovedAsync(string RegID, object userState)
	{
		if (IsApprovedOperationCompleted == null)
		{
			IsApprovedOperationCompleted = OnIsApprovedOperationCompleted;
		}
		InvokeAsync("IsApproved", new object[1] { RegID }, IsApprovedOperationCompleted, userState);
	}

	private void OnIsApprovedOperationCompleted(object arg)
	{
		if (this.IsApprovedCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.IsApprovedCompleted(this, new IsApprovedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/IsOK_forUpdate_ByRandom", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public bool IsOK_forUpdate_ByRandom()
	{
		object[] results = Invoke("IsOK_forUpdate_ByRandom", new object[0]);
		return (bool)results[0];
	}

	public IAsyncResult BeginIsOK_forUpdate_ByRandom(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("IsOK_forUpdate_ByRandom", new object[0], callback, asyncState);
	}

	public bool EndIsOK_forUpdate_ByRandom(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (bool)results[0];
	}

	public void IsOK_forUpdate_ByRandomAsync()
	{
		IsOK_forUpdate_ByRandomAsync(null);
	}

	public void IsOK_forUpdate_ByRandomAsync(object userState)
	{
		if (IsOK_forUpdate_ByRandomOperationCompleted == null)
		{
			IsOK_forUpdate_ByRandomOperationCompleted = OnIsOK_forUpdate_ByRandomOperationCompleted;
		}
		InvokeAsync("IsOK_forUpdate_ByRandom", new object[0], IsOK_forUpdate_ByRandomOperationCompleted, userState);
	}

	private void OnIsOK_forUpdate_ByRandomOperationCompleted(object arg)
	{
		if (this.IsOK_forUpdate_ByRandomCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.IsOK_forUpdate_ByRandomCompleted(this, new IsOK_forUpdate_ByRandomCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/InvReportList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet InvReportList(string RepKind)
	{
		object[] results = Invoke("InvReportList", new object[1] { RepKind });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginInvReportList(string RepKind, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("InvReportList", new object[1] { RepKind }, callback, asyncState);
	}

	public DataSet EndInvReportList(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void InvReportListAsync(string RepKind)
	{
		InvReportListAsync(RepKind, null);
	}

	public void InvReportListAsync(string RepKind, object userState)
	{
		if (InvReportListOperationCompleted == null)
		{
			InvReportListOperationCompleted = OnInvReportListOperationCompleted;
		}
		InvokeAsync("InvReportList", new object[1] { RepKind }, InvReportListOperationCompleted, userState);
	}

	private void OnInvReportListOperationCompleted(object arg)
	{
		if (this.InvReportListCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.InvReportListCompleted(this, new InvReportListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetPassword", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string GetPassword()
	{
		object[] results = Invoke("GetPassword", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginGetPassword(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetPassword", new object[0], callback, asyncState);
	}

	public string EndGetPassword(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void GetPasswordAsync()
	{
		GetPasswordAsync(null);
	}

	public void GetPasswordAsync(object userState)
	{
		if (GetPasswordOperationCompleted == null)
		{
			GetPasswordOperationCompleted = OnGetPasswordOperationCompleted;
		}
		InvokeAsync("GetPassword", new object[0], GetPasswordOperationCompleted, userState);
	}

	private void OnGetPasswordOperationCompleted(object arg)
	{
		if (this.GetPasswordCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetPasswordCompleted(this, new GetPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetDownloadRoute", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string GetDownloadRoute()
	{
		object[] results = Invoke("GetDownloadRoute", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginGetDownloadRoute(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetDownloadRoute", new object[0], callback, asyncState);
	}

	public string EndGetDownloadRoute(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void GetDownloadRouteAsync()
	{
		GetDownloadRouteAsync(null);
	}

	public void GetDownloadRouteAsync(object userState)
	{
		if (GetDownloadRouteOperationCompleted == null)
		{
			GetDownloadRouteOperationCompleted = OnGetDownloadRouteOperationCompleted;
		}
		InvokeAsync("GetDownloadRoute", new object[0], GetDownloadRouteOperationCompleted, userState);
	}

	private void OnGetDownloadRouteOperationCompleted(object arg)
	{
		if (this.GetDownloadRouteCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetDownloadRouteCompleted(this, new GetDownloadRouteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetPccesVersion", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string GetPccesVersion()
	{
		object[] results = Invoke("GetPccesVersion", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginGetPccesVersion(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetPccesVersion", new object[0], callback, asyncState);
	}

	public string EndGetPccesVersion(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void GetPccesVersionAsync()
	{
		GetPccesVersionAsync(null);
	}

	public void GetPccesVersionAsync(object userState)
	{
		if (GetPccesVersionOperationCompleted == null)
		{
			GetPccesVersionOperationCompleted = OnGetPccesVersionOperationCompleted;
		}
		InvokeAsync("GetPccesVersion", new object[0], GetPccesVersionOperationCompleted, userState);
	}

	private void OnGetPccesVersionOperationCompleted(object arg)
	{
		if (this.GetPccesVersionCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetPccesVersionCompleted(this, new GetPccesVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetUpdateFileAddress", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string GetUpdateFileAddress()
	{
		object[] results = Invoke("GetUpdateFileAddress", new object[0]);
		return (string)results[0];
	}

	public IAsyncResult BeginGetUpdateFileAddress(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetUpdateFileAddress", new object[0], callback, asyncState);
	}

	public string EndGetUpdateFileAddress(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void GetUpdateFileAddressAsync()
	{
		GetUpdateFileAddressAsync(null);
	}

	public void GetUpdateFileAddressAsync(object userState)
	{
		if (GetUpdateFileAddressOperationCompleted == null)
		{
			GetUpdateFileAddressOperationCompleted = OnGetUpdateFileAddressOperationCompleted;
		}
		InvokeAsync("GetUpdateFileAddress", new object[0], GetUpdateFileAddressOperationCompleted, userState);
	}

	private void OnGetUpdateFileAddressOperationCompleted(object arg)
	{
		if (this.GetUpdateFileAddressCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetUpdateFileAddressCompleted(this, new GetUpdateFileAddressCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetUpdateFileAddressWithCurrentVersion", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public string GetUpdateFileAddressWithCurrentVersion(string CurrentVersion)
	{
		object[] results = Invoke("GetUpdateFileAddressWithCurrentVersion", new object[1] { CurrentVersion });
		return (string)results[0];
	}

	public IAsyncResult BeginGetUpdateFileAddressWithCurrentVersion(string CurrentVersion, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetUpdateFileAddressWithCurrentVersion", new object[1] { CurrentVersion }, callback, asyncState);
	}

	public string EndGetUpdateFileAddressWithCurrentVersion(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (string)results[0];
	}

	public void GetUpdateFileAddressWithCurrentVersionAsync(string CurrentVersion)
	{
		GetUpdateFileAddressWithCurrentVersionAsync(CurrentVersion, null);
	}

	public void GetUpdateFileAddressWithCurrentVersionAsync(string CurrentVersion, object userState)
	{
		if (GetUpdateFileAddressWithCurrentVersionOperationCompleted == null)
		{
			GetUpdateFileAddressWithCurrentVersionOperationCompleted = OnGetUpdateFileAddressWithCurrentVersionOperationCompleted;
		}
		InvokeAsync("GetUpdateFileAddressWithCurrentVersion", new object[1] { CurrentVersion }, GetUpdateFileAddressWithCurrentVersionOperationCompleted, userState);
	}

	private void OnGetUpdateFileAddressWithCurrentVersionOperationCompleted(object arg)
	{
		if (this.GetUpdateFileAddressWithCurrentVersionCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetUpdateFileAddressWithCurrentVersionCompleted(this, new GetUpdateFileAddressWithCurrentVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/AddNewspaperVisitorCount", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public void AddNewspaperVisitorCount()
	{
		Invoke("AddNewspaperVisitorCount", new object[0]);
	}

	public IAsyncResult BeginAddNewspaperVisitorCount(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("AddNewspaperVisitorCount", new object[0], callback, asyncState);
	}

	public void EndAddNewspaperVisitorCount(IAsyncResult asyncResult)
	{
		EndInvoke(asyncResult);
	}

	public void AddNewspaperVisitorCountAsync()
	{
		AddNewspaperVisitorCountAsync(null);
	}

	public void AddNewspaperVisitorCountAsync(object userState)
	{
		if (AddNewspaperVisitorCountOperationCompleted == null)
		{
			AddNewspaperVisitorCountOperationCompleted = OnAddNewspaperVisitorCountOperationCompleted;
		}
		InvokeAsync("AddNewspaperVisitorCount", new object[0], AddNewspaperVisitorCountOperationCompleted, userState);
	}

	private void OnAddNewspaperVisitorCountOperationCompleted(object arg)
	{
		if (this.AddNewspaperVisitorCountCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.AddNewspaperVisitorCountCompleted(this, new AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetPubPriceVolumes", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetPubPriceVolumes()
	{
		object[] results = Invoke("GetPubPriceVolumes", new object[0]);
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetPubPriceVolumes(AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetPubPriceVolumes", new object[0], callback, asyncState);
	}

	public DataSet EndGetPubPriceVolumes(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetPubPriceVolumesAsync()
	{
		GetPubPriceVolumesAsync(null);
	}

	public void GetPubPriceVolumesAsync(object userState)
	{
		if (GetPubPriceVolumesOperationCompleted == null)
		{
			GetPubPriceVolumesOperationCompleted = OnGetPubPriceVolumesOperationCompleted;
		}
		InvokeAsync("GetPubPriceVolumes", new object[0], GetPubPriceVolumesOperationCompleted, userState);
	}

	private void OnGetPubPriceVolumesOperationCompleted(object arg)
	{
		if (this.GetPubPriceVolumesCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetPubPriceVolumesCompleted(this, new GetPubPriceVolumesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapDocumentMethod("http://tempuri.org/GetPubPriceDataset", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
	public DataSet GetPubPriceDataset(string year, string month, string location)
	{
		object[] results = Invoke("GetPubPriceDataset", new object[3] { year, month, location });
		return (DataSet)results[0];
	}

	public IAsyncResult BeginGetPubPriceDataset(string year, string month, string location, AsyncCallback callback, object asyncState)
	{
		return BeginInvoke("GetPubPriceDataset", new object[3] { year, month, location }, callback, asyncState);
	}

	public DataSet EndGetPubPriceDataset(IAsyncResult asyncResult)
	{
		object[] results = EndInvoke(asyncResult);
		return (DataSet)results[0];
	}

	public void GetPubPriceDatasetAsync(string year, string month, string location)
	{
		GetPubPriceDatasetAsync(year, month, location, null);
	}

	public void GetPubPriceDatasetAsync(string year, string month, string location, object userState)
	{
		if (GetPubPriceDatasetOperationCompleted == null)
		{
			GetPubPriceDatasetOperationCompleted = OnGetPubPriceDatasetOperationCompleted;
		}
		InvokeAsync("GetPubPriceDataset", new object[3] { year, month, location }, GetPubPriceDatasetOperationCompleted, userState);
	}

	private void OnGetPubPriceDatasetOperationCompleted(object arg)
	{
		if (this.GetPubPriceDatasetCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.GetPubPriceDatasetCompleted(this, new GetPubPriceDatasetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
