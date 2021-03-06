﻿using System;

namespace SerialLabs.Monitoring
{
    /// <summary>
    /// Status Check Item
    /// </summary>
    public class MonitoringResult
    {
        /// <summary>
        /// Title of the check
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// If check failed, this contains the error
        /// </summary>
        public Exception Error { get; set; }
        /// <summary>
        /// Duration of the check
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Additional infos
        /// </summary>
        public string AdditionalInfos { get; set; }
    }
}
