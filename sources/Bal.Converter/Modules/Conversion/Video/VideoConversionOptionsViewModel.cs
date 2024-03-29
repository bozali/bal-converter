﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Bal.Converter.Modules.Conversion.Video;

public partial class VideoConversionOptionsViewModel : ObservableObject
{
    [ObservableProperty] private int quality;
    [ObservableProperty] private double minVideoLength;
    [ObservableProperty] private double maxVideoLength;
    [ObservableProperty] private TimeSpan minVideoLengthTime;
    [ObservableProperty] private TimeSpan maxVideoLengthTime;

    [ObservableProperty] private string minVideoText;
    [ObservableProperty] private string maxVideoText;

    public VideoConversionOptionsViewModel()
    {
    }

    public bool HasMinLengthTimeChanges()
    {
        return TimeSpan.Zero != this.MinVideoLengthTime;
    }

    public bool HasMaxLengthTimeChanges(TimeSpan max)
    {
        return this.MaxVideoLengthTime != max;
    }

    partial void OnMinVideoLengthChanging(double value)
    {
        this.MinVideoLengthTime = TimeSpan.FromSeconds(value);
        this.MinVideoText = this.MinVideoLengthTime.ToString(@"mm\:ss");
        this.OnPropertyChanged(nameof(this.MinVideoText));
    }

    partial void OnMaxVideoLengthChanging(double value)
    {
        this.MaxVideoLengthTime = TimeSpan.FromSeconds(value);
        this.MaxVideoText = this.MaxVideoLengthTime.ToString(@"mm\:ss");
        this.OnPropertyChanged(nameof(this.MaxVideoText));
    }
}