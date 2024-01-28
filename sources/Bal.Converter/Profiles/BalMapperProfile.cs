﻿using AutoMapper;

using Bal.Converter.Common.Media;
using Bal.Converter.FFmpeg.Filters.Audio;
using Bal.Converter.FFmpeg.Filters.Video;
using Bal.Converter.Modules.Conversion.Filters.Unsharp;
using Bal.Converter.Modules.Conversion.Filters.Volume;
using Bal.Converter.Modules.MediaDownloader.ViewModels;
using Bal.Converter.YouTubeDl;

namespace Bal.Converter.Profiles;

public class BalMapperProfile : Profile
{
    public BalMapperProfile()
    {
        this.CreateMap<VideoViewModel, Video>()
            .ForMember(x => x.Tags, expression => expression.Ignore());

        this.CreateMap<Video, VideoViewModel>()
            .ForMember(x => x.Tags, expression => expression.Ignore());

        this.CreateMap<VolumeFilterViewModel, VolumeFilter>().ReverseMap();
        this.CreateMap<UnsharpFilterViewModel, UnsharpFilter>().ReverseMap();
        this.CreateMap<MediaTagsViewModel, MediaTags>().ReverseMap();
        this.CreateMap<FormatViewModel, Format>().ReverseMap();

        this.CreateMap<PlaylistViewModel, Playlist>()
            .ForMember(x => x.Videos, expression => expression.MapFrom(y => y.Videos));

        this.CreateMap<Playlist, PlaylistViewModel>()
            .ForMember(x => x.Videos, expression => expression.MapFrom(y => y.Videos));
    }
}