﻿using System;
using Socialite.Domain.Common;
using Socialite.Domain.Exceptions;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.StatusAggregate
{
    public class Status : BaseEntity, IAggregateRoot
    {
        public string Mood { get; private set; }

        public string Text { get; private set; }

        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } }

        private DateTime _updatedAt;
        public DateTime UpdatedAt { get { return _updatedAt; } }

        public Status(string mood, string text)
        {
            Mood = !String.IsNullOrEmpty(mood) ? mood : throw new StatusDomainException(nameof(mood));
            Text = !String.IsNullOrEmpty(text) ? text : throw new StackOverflowException(nameof(text));
        }
    }
}