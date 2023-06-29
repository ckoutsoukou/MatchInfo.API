using AutoMapper;

namespace MatchInfo.API.Profiles
{
    /// <summary>
    /// A class for MatchOdd Mapper.
    /// </summary>
    public class MatchOddProfile: Profile
    {
        /// <summary>
        /// Ctor for MatchOdd mapper.
        /// </summary>
        public MatchOddProfile() 
        {
            CreateMap<Entities.MatchOdd, Models.MatchOddDto>()
             .ForMember(dst => dst.Match, opt => opt.Ignore());

            CreateMap<Models.MatchOddDto, Entities.MatchOdd>()
            .ForMember(dst => dst.Match, opt => opt.Ignore());
        }
    }
}
