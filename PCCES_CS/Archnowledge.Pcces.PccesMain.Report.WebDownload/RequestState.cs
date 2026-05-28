using System.Collections;
using System.IO;
using System.Net;

namespace Archnowledge.Pcces.PccesMain.Report.WebDownload;

public class RequestState
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
