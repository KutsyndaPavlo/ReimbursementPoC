﻿namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById
{
    public class VendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid ServiceFullName { get; set; }        

        public bool IsCanceled { get; set; }
    }
}
