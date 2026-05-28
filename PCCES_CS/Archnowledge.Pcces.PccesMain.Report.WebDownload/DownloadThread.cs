using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading;
using Archnowledge.Pcces.CommonClass;

namespace Archnowledge.Pcces.PccesMain.Report.WebDownload;

public class DownloadThread
{
	private const int bufferSize = 5120;

	public string DownloadUrl = string.Empty;

	public string savePath = string.Empty;

	public string iniPath = string.Empty;

	private RequestState rState = new RequestState();

	private ManualResetEvent allDone = new ManualResetEvent(initialState: false);

	public event DownloadCompleteHandler CompleteCallback;

	public event DownloadProgressHandler ProgressCallback;

	public event DownloadFailHandler FailCallback;

	public void Download()
	{
		lock (this)
		{
			string proxyStatus = CommonMethods.IniReadValue(iniPath, "ProxyInfo", "usingProxy");
			string address = CommonMethods.IniReadValue(iniPath, "ProxyInfo", "address");
			string port = CommonMethods.IniReadValue(iniPath, "ProxyInfo", "port");
			string account = CommonMethods.IniReadValue(iniPath, "ProxyInfo", "account");
			string password = CommonMethods.IniReadValue(iniPath, "ProxyInfo", "password");
			WebProxy myProxy = new WebProxy();
			allDone.Reset();
			if (this.CompleteCallback == null || !(DownloadUrl != ""))
			{
				return;
			}
			try
			{
				WebRequest request = WebRequest.Create(DownloadUrl);
				if (proxyStatus.Trim().ToLower() == "true")
				{
					myProxy.Address = new Uri(address + ":" + port);
					if (account != "")
					{
						myProxy.Credentials = new NetworkCredential(account, password);
					}
					request.Proxy = myProxy;
				}
				rState.request = request;
				string strContentLength = request.GetResponse().Headers["Content-Length"];
				if (strContentLength != null)
				{
					rState.useFastBuffers = true;
					rState.dataLength = Convert.ToInt32(strContentLength);
					rState.dataBufferFast = new byte[rState.dataLength];
				}
				else
				{
					rState.useFastBuffers = false;
					rState.dataBufferSlow = new ArrayList(5120);
				}
				Stream respnoseStream = request.GetResponse().GetResponseStream();
				rState.responseStream = respnoseStream;
				RequestState requestState = rState;
				requestState.ProgressCallback = (DownloadProgressHandler)Delegate.Combine(requestState.ProgressCallback, this.ProgressCallback);
				IAsyncResult iRead = respnoseStream.BeginRead(rState.bufferRead, 0, 5120, ReadCallBack, rState);
				allDone.WaitOne();
				if (!rState.useFastBuffers)
				{
					rState.dataBufferFast = new byte[rState.dataBufferSlow.Count];
					for (int i = 0; i < rState.dataBufferSlow.Count; i++)
					{
						rState.dataBufferFast[i] = (byte)rState.dataBufferSlow[i];
					}
				}
				SaveFile();
				this.CompleteCallback(rState.bytesProcessed, rState.dataLength, savePath);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Report.WebDownload.cs" + ex.Message);
				this.FailCallback(ex);
				Thread.Sleep(5000);
				Thread.CurrentThread.Abort();
			}
		}
	}

	public void SaveFile()
	{
		FileStream file = new FileStream(savePath, FileMode.Create);
		file.Write(rState.dataBufferFast, 0, rState.dataBufferFast.Length);
		file.Close();
	}

	private void ReadCallBack(IAsyncResult asyncResult)
	{
		RequestState rState = (RequestState)asyncResult.AsyncState;
		int bytesRead = rState.responseStream.EndRead(asyncResult);
		if (bytesRead > 0)
		{
			if (rState.useFastBuffers)
			{
				Array.Copy(rState.bufferRead, 0, rState.dataBufferFast, rState.bytesProcessed, bytesRead);
			}
			else
			{
				for (int k = 0; k < bytesRead; k++)
				{
					rState.dataBufferSlow.Add(rState.bufferRead[k]);
				}
			}
			rState.bytesProcessed += bytesRead;
			if (rState.ProgressCallback != null)
			{
				rState.ProgressCallback(rState.bytesProcessed, rState.dataLength);
			}
			IAsyncResult iRead = rState.responseStream.BeginRead(rState.bufferRead, 0, 5120, ReadCallBack, rState);
		}
		else
		{
			rState.responseStream.Close();
			allDone.Set();
		}
	}
}
