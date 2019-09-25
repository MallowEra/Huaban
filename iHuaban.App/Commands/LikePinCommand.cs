﻿using iHuaban.App.Models;
using iHuaban.App.Services;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace iHuaban.App.Commands
{
    public class LikePinCommand : Command
    {
        private IAccountService accountService;
        public LikePinCommand(Context context, IAccountService accountService)
            : base(context)
        {
            this.accountService = accountService;
        }


        public async override void Execute(object parameter)
        {
            try
            {
                var Dispatcher = Window.Current.Dispatcher;
                await Task.Run(async () =>
                {
                    if (parameter is Pin pin)
                    {
                        var result = await accountService.LikePin(pin);
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            pin.like = true;
                            Context.ShowMessage("关注成功");
                        });
                    }
                });
            }
            catch
            {
            }
        }
    }
}

