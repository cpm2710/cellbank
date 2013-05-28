using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzureRestAdapter
{
    /// <summary>
    /// The operation status values from PollGetOperationStatus.
    /// </summary>
    public enum OperationStatus
    {
        InProgress,
        Failed,
        Succeeded,
        TimedOut
    }

    /// <summary>
    /// The results from PollGetOperationStatus are passed in this struct.
    /// </summary>
    public struct OperationResult
    {
        // The status: InProgress, Failed, Succeeded, or TimedOut.
        public OperationStatus Status { get; set; }

        // The http status code of the requestId operation, if any.
        public HttpStatusCode StatusCode { get; set; }

        // The approximate running time for PollGetOperationStatus.
        public TimeSpan RunningTime { get; set; }

        // The error code for the failed operation.
        public string Code { get; set; }

        // The message for the failed operation.
        public string Message { get; set; }
    }

    public class OperationAdapter : RequestInvoker
    {
        public OperationAdapter(string publishSettings)
            : base(publishSettings)
        {

        }
        /// <summary>
        /// Calls the Get Operation Status operation in the Service 
        /// Management REST API for the specified subscription and requestId 
        /// and returns the Operation XML element from the response.
        /// </summary>
        /// <param name="requestId">The requestId of the operation to track.</param>
        /// <returns>The Operation XML element from the response.</returns>
        private XElement GetOperationStatus(
            string requestId)
        {
            string uriFormat = "https://management.core.windows.net/{0}" +
                "/operations/{1}";
            Uri uri = new Uri(String.Format(uriFormat, SubscriptionId, requestId));
            XDocument responseBody;
            InvokeRequest(uri, "GET", HttpStatusCode.OK, null, out responseBody);
            return responseBody.Element(AzureRestAdapterConstants.NameSpaceWA + "Operation");
        }

        /// <summary>
        /// Polls Get Operation Status for the operation specified by requestId
        /// every pollIntervalSeconds until timeoutSeconds have passed or the
        /// operation has returned a Failed or Succeeded status. 
        /// </summary>
        /// <param name="requestId">The requestId of the operation to get status for.</param>
        /// <param name="pollIntervalSeconds">The interval between calls to Get Operation Status.</param>
        /// <param name="timeoutSeconds">The maximum number of seconds to poll.</param>
        /// <returns>An OperationResult structure with status or error information.</returns>
        public OperationResult PollGetOperationStatus(
            string requestId,
            int pollIntervalSeconds,
            int timeoutSeconds)
        {
            OperationResult result = new OperationResult();
            DateTime beginPollTime = DateTime.UtcNow;
            TimeSpan pollInterval = new TimeSpan(0, 0, pollIntervalSeconds);
            DateTime endPollTime = beginPollTime + new TimeSpan(0, 0, timeoutSeconds);

            bool done = false;
            while (!done)
            {
                XElement operation = GetOperationStatus(requestId);
                result.RunningTime = DateTime.UtcNow - beginPollTime;
                try
                {
                    // Turn the Status string into an OperationStatus value
                    result.Status = (OperationStatus)Enum.Parse(
                        typeof(OperationStatus),
                        operation.Element(AzureRestAdapterConstants.NameSpaceWA + "Status").Value);
                }
                catch (Exception)
                {
                    throw new ApplicationException(string.Format(
                        "Get Operation Status {0} returned unexpected status: {1}{2}",
                        requestId,
                        Environment.NewLine,
                        operation.ToString(SaveOptions.OmitDuplicateNamespaces)));
                }

                switch (result.Status)
                {
                    case OperationStatus.InProgress:
                        Console.WriteLine(
                            "In progress for {0} seconds",
                            (int)result.RunningTime.TotalSeconds);
                        Thread.Sleep((int)pollInterval.TotalMilliseconds);
                        break;

                    case OperationStatus.Failed:
                        result.StatusCode = (HttpStatusCode)Convert.ToInt32(
                            operation.Element(AzureRestAdapterConstants.NameSpaceWA + "HttpStatusCode").Value);
                        XElement error = operation.Element(AzureRestAdapterConstants.NameSpaceWA + "Error");
                        result.Code = error.Element(AzureRestAdapterConstants.NameSpaceWA + "Code").Value;
                        result.Message = error.Element(AzureRestAdapterConstants.NameSpaceWA + "Message").Value;
                        done = true;
                        break;

                    case OperationStatus.Succeeded:
                        result.StatusCode = (HttpStatusCode)Convert.ToInt32(
                            operation.Element(AzureRestAdapterConstants.NameSpaceWA + "HttpStatusCode").Value);
                        done = true;
                        break;
                }

                if (!done && DateTime.UtcNow > endPollTime)
                {
                    result.Status = OperationStatus.TimedOut;
                    done = true;
                }
            }

            return result;
        }
    }
}
