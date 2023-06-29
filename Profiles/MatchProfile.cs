using MatchInfo.API.Models;
using MatchInfo.API.Utilities;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Globalization;

namespace MatchInfo.API.Profiles
{
    /// <summary>
    /// A class for Match mapper.
    /// </summary>
    public class MatchProfile: Profile
    {
        /// <summary>
        /// Ctor for MatchProfile.
        /// </summary>
        public MatchProfile()
        {
            CreateMap<Entities.Match, Models.MatchDto>()
                .ForMember(dst => dst.MatchDate, opt => opt.MapFrom(src => src.MatchDateTime.Date))
                .ForMember(dst => dst.MatchTime, opt => opt.MapFrom(src => src.MatchDateTime.TimeOfDay))
                .ForMember(dst => dst.SportCategory, opt => opt.MapFrom(src => ((SportCategory)src.Sport).Category()))
                .ForMember(dst => dst.MatchOddDtos, opt => opt.MapFrom(src => src.MatchOdds))
                ;

            CreateMap<Models.MatchDto, Entities.Match>()
              .ForMember(dst => dst.MatchDateTime, opt => opt.MapFrom(src => src.MatchDate.Add(src.MatchTime)))
              .ForMember(dst => dst.Sport, opt => opt.MapFrom(src => (int)EnumUtils.EnumFromCategory<SportCategory>(src.SportCategory, SportCategory.Unknown)))
              .ForMember(dst => dst.MatchOdds, opt => opt.Ignore())
               ;
        }
    }
}
