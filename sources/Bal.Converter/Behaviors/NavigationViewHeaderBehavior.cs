﻿using Bal.Converter.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Xaml.Interactivity;

namespace Bal.Converter.Behaviors;

public class NavigationViewHeaderBehavior : Behavior<NavigationView>
{
    private static NavigationViewHeaderBehavior? Current;

    private Page? currentPage;

    public DataTemplate? DefaultHeaderTemplate
    {
        get; set;
    }

    public object DefaultHeader
    {
        get => GetValue(DefaultHeaderProperty);
        set => SetValue(DefaultHeaderProperty, value);
    }

    public static readonly DependencyProperty DefaultHeaderProperty =
        DependencyProperty.Register("DefaultHeader", typeof(object), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, (d, e) => Current!.UpdateHeader()));

    public static NavigationViewHeaderMode GetHeaderMode(Page item) => (NavigationViewHeaderMode)item.GetValue(HeaderModeProperty);

    public static void SetHeaderMode(Page item, NavigationViewHeaderMode value) => item.SetValue(HeaderModeProperty, value);

    public static readonly DependencyProperty HeaderModeProperty =
        DependencyProperty.RegisterAttached("HeaderMode", typeof(bool), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(NavigationViewHeaderMode.Always, (d, e) => Current!.UpdateHeader()));

    public static object GetHeaderContext(Page item) => item.GetValue(HeaderContextProperty);

    public static void SetHeaderContext(Page item, object value) => item.SetValue(HeaderContextProperty, value);

    public static readonly DependencyProperty HeaderContextProperty =
        DependencyProperty.RegisterAttached("HeaderContext", typeof(object), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, (d, e) => Current!.UpdateHeader()));

    public static DataTemplate GetHeaderTemplate(Page item) => (DataTemplate)item.GetValue(HeaderTemplateProperty);

    public static void SetHeaderTemplate(Page item, DataTemplate value) => item.SetValue(HeaderTemplateProperty, value);

    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.RegisterAttached("HeaderTemplate", typeof(DataTemplate), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, (d, e) => Current!.UpdateHeaderTemplate()));

    protected override void OnAttached()
    {
        base.OnAttached();

        var navigationService = App.GetService<INavigationService>();
        navigationService.Navigated += OnNavigated;

        Current = this;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        var navigationService = App.GetService<INavigationService>();
        navigationService.Navigated -= OnNavigated;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame && frame.Content is Page page)
        {
            currentPage = page;

            UpdateHeader();
            UpdateHeaderTemplate();
        }
    }

    private void UpdateHeader()
    {
        if (currentPage != null)
        {
            var headerMode = GetHeaderMode(currentPage);
            if (headerMode == NavigationViewHeaderMode.Never)
            {
                AssociatedObject.Header = null;
                AssociatedObject.AlwaysShowHeader = false;
            }
            else
            {
                var headerFromPage = GetHeaderContext(currentPage);
                if (headerFromPage != null)
                {
                    AssociatedObject.Header = headerFromPage;
                }
                else
                {
                    AssociatedObject.Header = DefaultHeader;
                }

                if (headerMode == NavigationViewHeaderMode.Always)
                {
                    AssociatedObject.AlwaysShowHeader = true;
                }
                else
                {
                    AssociatedObject.AlwaysShowHeader = false;
                }
            }
        }
    }

    private void UpdateHeaderTemplate()
    {
        if (currentPage != null)
        {
            var headerTemplate = GetHeaderTemplate(currentPage);
            AssociatedObject.HeaderTemplate = headerTemplate ?? DefaultHeaderTemplate;
        }
    }
}
