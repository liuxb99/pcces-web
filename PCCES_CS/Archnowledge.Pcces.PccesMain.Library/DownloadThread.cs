using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading;

namespace Archnowledge.Pcces.PccesMain.Library;

public class DownloadThread
{
	private class RequestState
	{
		private const int bufferSize = 5120;

		public WebRequest request;

		public Stream responseStream;

		public byte[] bufferRead;

		public byte[] dataBufferFast;

		public ArrayList dataBufferSlow;

		public bool useFastBuffers;

		public int dataLength;

		public int bytesProcessed;

		public DownloadProgressHandler ProgressCallback;

		public RequestState()
		{
			request = null;
			bufferRead = new byte[5120];
			dataLength = -1;
			bytesProcessed = 0;
			useFastBuffers = true;
		}
	}

	private const int bufferSize = 5120;

	public string DownloadURL = string.Empty;

	public string savePath = string.Empty;

	public WebProxy proxy;

	private RequestState requestState = new RequestState();

	private ManualResetEvent allDone = new ManualResetEvent(initialState: false);

	public event DownloadCompleteHandler CompleteCallback;

	public event DownloadProgressHandler ProgressCallback;

	public event DownloadFailHandler FailCallback;

	public void Download()
	{
		allDone.Reset();
		if (this.CompleteCallback == null || !(DownloadURL != ""))
		{
			return;
		}
		try
		{
			WebRequest request = WebRequest.Create(DownloadURL);
			request.Proxy = proxy;
			requestState.request = request;
			string contentLength = request.GetResponse().Headers["Content-Length"];
			if (contentLength != null)
			{
				requestState.useFastBuffers = true;
				requestState.dataLength = Convert.ToInt32(contentLength);
				requestState.dataBufferFast = new byte[requestState.dataLength];
			}
			else
			{
				requestState.useFastBuffers = false;
				requestState.dataBufferSlow = new ArrayList(5120);
			}
			Stream respnoseStream = request.GetResponse().GetResponseStream();
			requestState.responseStream = respnoseStream;
			RequestState obj = requestState;
			obj.ProgressCallback = (DownloadProgressHandler)Delegate.Combine(obj.ProgressCallback, this.ProgressCallback);
			respnoseStream.BeginRead(requestState.bufferRead, 0, 5120, ReadCallBack, requestState);
			allDone.WaitOne();
			if (!requestState.useFastBuffers)
			{
				requestState.dataBufferFast = new byte[requestState.dataBufferSlow.Count];
				for (int i = 0; i < requestState.dataBufferSlow.Count; i++)
				{
					requestState.dataBufferFast[i] = (byte)requestState.dataBufferSlow[i];
				}
			}
			SaveFile();
			this.CompleteCallback(requestState.bytesProcessed, requestState.dataLength);
		}
		catch (Exception exception)
		{
			this.FailCallback(exception);
			Thread.Sleep(5000);
			Thread.CurrentThread.Abort();
		}
	}

	public void SaveFile()
	{
		FileStream file = new FileStream(savePath, FileMode.Create);
		file.Write(requestState.dataBufferFast, 0, requestState.dataBufferFast.Length);
		file.Close();
	}

	private void ReadCallBack(IAsyncResult asyncResult)
	{
		RequestState requestState = (RequestState)asyncResult.AsyncState;
		int bytesRead = requestState.responseStream.EndRead(asyncResult);
		if (bytesRead > 0)
		{
			if (requestState.useFastBuffers)
			{
				Array.Copy(requestState.bufferRead, 0, requestState.dataBufferFast, requestState.bytesProcessed, bytesRead);
			}
			else
			{
				for (int k = 0; k < bytesRead; k++)
				{
					requestState.dataBufferSlow.Add(requestState.bufferRead[k]);
				}
			}
			requestState.bytesProcessed += bytesRead;
			if (requestState.ProgressCallback != null)
			{
				requestState.ProgressCallback(requestState.bytesProcessed, requestState.dataLength);
			}
			requestState.responseStream.BeginRead(requestState.bufferRead, 0, 5120, ReadCallBack, requestState);
		}
		else
		{
			requestState.responseStream.Close();
			allDone.Set();
		}
	}
}
