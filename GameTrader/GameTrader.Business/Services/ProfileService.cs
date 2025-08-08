using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<bool> CreateProfileAsync(string userId, string profilePic, string bio)
        {
           return await _profileRepository.CreateProfileAsync(userId, profilePic, bio);
        }
    }
}
