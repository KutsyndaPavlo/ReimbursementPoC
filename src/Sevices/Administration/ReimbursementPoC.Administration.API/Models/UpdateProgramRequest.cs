﻿namespace ReimbursementPoC.Administration.API.Models
{
    public class UpdateProgramRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
