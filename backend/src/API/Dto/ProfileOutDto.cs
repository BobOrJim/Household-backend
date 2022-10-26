﻿namespace API.Dto
{
    public class ProfileOutDto
    {
        public Guid Id { get; set; }
        public string Alias { get; set; } = "";
        public string Avatar { get; set; } = "";
        public bool IsAdmin { get; set; } = false;
        public bool PendingRequest { get; set; } = false;
        public Guid AuthUserId { get; set; }
        public Guid HouseholdId { get; set; }
    }
}
