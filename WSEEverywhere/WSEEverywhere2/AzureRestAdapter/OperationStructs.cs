﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
}
