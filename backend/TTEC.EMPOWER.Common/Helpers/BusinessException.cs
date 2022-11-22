// -----------------------------------------------------------------------
// <copyright file="BusinessException.cs" company="TTEC Holdings Inc.">
// Copyright © 2022 TTEC Holdings Inc.
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace TTEC.EMPOWER.Common.Helpers
{
    public class BusinessException
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }
    }
}
