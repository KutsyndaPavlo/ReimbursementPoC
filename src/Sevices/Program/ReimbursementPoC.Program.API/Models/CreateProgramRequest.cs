﻿namespace ReimbursementPoC.Program.API.Models
{
    public class CreateProgramRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string State { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
