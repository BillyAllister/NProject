﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NProject.Models.Domain
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ProjectStatus Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual User Responsible { get; set; } 
    }
}