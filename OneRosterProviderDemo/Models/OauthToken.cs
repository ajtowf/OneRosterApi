﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OneRosterProviderDemo.Models
{
    public class OauthToken
    {
        [Key, Required]
        public string Value { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public bool CanBeUsed()
        {
            long elapsedTicks = DateTime.Now.Ticks - CreatedAt.Ticks;
            var elapsedSpan = new TimeSpan(elapsedTicks);
            return elapsedSpan.TotalMinutes < 1440;
        }
    }
}
