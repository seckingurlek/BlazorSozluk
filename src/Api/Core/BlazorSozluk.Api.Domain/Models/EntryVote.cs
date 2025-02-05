﻿using BlazorSozluk.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class EntryVote:BaseEntity
    {
        public VoteType voteType {  get; set; }
        public Guid EntryId { get; set; }
        public Guid CreatedById { get; set; }
        public virtual Entry Entry { get; set; }
    }
}
