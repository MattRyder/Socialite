using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles
{
    public class UserProfileViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Occupation { get; set; }

        public string Location { get; set; }

        public string Bio { get; set; }

        public string AvatarImageUrn { get; set; }

        public string HeaderImageUrn { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return UserId;
            yield return Name;
            yield return EmailAddress;
            yield return Occupation;
            yield return Location;
            yield return Bio;
            yield return AvatarImageUrn;
            yield return HeaderImageUrn;
            yield return CreatedAt;
            yield return UpdatedAt;
        }

        public static UserProfileViewModel FromModel(UserProfile userProfile)
        {
            return new UserProfileViewModel
            {
                Id = userProfile.Id,
                UserId = userProfile.UserId,
                Name = userProfile.Name,
                EmailAddress = userProfile.EmailAddress.Value,
                Occupation = userProfile.Occupation,
                Location = userProfile.Location,
                Bio = userProfile.Bio,
                AvatarImageUrn = userProfile.AvatarImageUrn,
                HeaderImageUrn = userProfile.HeaderImageUrn,
                CreatedAt = userProfile.CreatedAt,
                UpdatedAt = userProfile.UpdatedAt
            };
        }
    }
}
