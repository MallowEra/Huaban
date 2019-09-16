﻿using iHuaban.App.Models;
using iHuaban.App.Services;
using iHuaban.App.Views;
using iHuaban.Core.Commands;
using iHuaban.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using System.Net;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;

namespace iHuaban.App.ViewModels
{
    public class ShellViewModel : PageViewModel
    {
        private INavigationService navigationService;
        private IStorageService storageService;
        private IAuthService authService;
        public ObservableCollection<MenuItem> Menu { get; set; }
        public DataTemplateSelector DataTemplateSelector { get; private set; }
        public IValueConverter ValueConverter { get; set; }
        public Context Context { get; private set; }
        public ShellViewModel(
            INavigationService navigationService,
            IStorageService storageService,
            IAuthService authService,
            IValueConverter valueConverter,
            Context context)
        {
            this.navigationService = navigationService;
            this.storageService = storageService;
            this.authService = authService;
            this.Context = context;
            var list = new List<MenuItem>
            {
                new MenuItem
                {
                    Title = Constants.TextHome,
                    Icon = Constants.IconHome,
                    Type = typeof(HomePage)
                },
                new MenuItem
                {
                    Title = Constants.TextCategory,
                    Icon = Constants.IconCategory,
                    Type = typeof(CategoriesPage)
                },
                new MenuItem
                {
                    Title = Constants.TextFind,
                    Icon = Constants.IconFind,
                    Type = typeof(FindPage)
                },
                new MenuItem
                {
                    Title = Constants.TextMine,
                    Icon = Constants.IconMine,
                    Type = typeof(MinePage)
                },
                new MenuItem
                {
                    Title = Constants.TextDownload,
                    Icon = Constants.IconDownload,
                    Type = typeof(DownloadPage)
                },
                 new MenuItem
                {
                    Title = Constants.TextSetting,
                    Icon = Constants.IconSetting,
                    Type = typeof(SettingPage)
                },
            };
            Menu = new ObservableCollection<MenuItem>(list);
        }

        private MenuItem _SelectedMenu;
        public MenuItem SelectedMenu
        {
            get { return _SelectedMenu; }
            set { SetValue(ref _SelectedMenu, value); }
        }

        private DelegateCommand _NavigateCommand;
        public DelegateCommand NavigateCommand
        {
            get
            {
                return _NavigateCommand ?? (_NavigateCommand = new DelegateCommand(
                o =>
                {
                    try
                    {
                        if (SelectedMenu != null)
                        {
                            navigationService.Navigate(SelectedMenu.Type);
                        }
                    }
                    catch (Exception)
                    {

                    }

                }, o => true));
            }
        }

        public override void Init()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += async (s, e) =>
            {
                dispatcherTimer.Stop();
                string cookieJson = storageService.GetSetting("Cookies");
                var cookies = JsonConvert.DeserializeObject<List<Cookie>>(cookieJson);
                this.Context.SetCookie(cookies);
                var user = await this.authService.GetMeAsync();
                if (user != null && !string.IsNullOrWhiteSpace(user.user_id))
                {
                    this.Context.User = user;
                }
            };
            dispatcherTimer.Start();
        }
    }
}