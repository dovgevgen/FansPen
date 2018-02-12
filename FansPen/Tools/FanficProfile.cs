﻿using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class FanficProfile : Profile
    {
        public FanficProfile()
        {
            CreateMap<Fanfic, FanficViewModel>();
        }
    }
}
