﻿using System;
using System.Windows.Input;

namespace iHuaban.App.Commands
{
    public class FollowBoardCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
        }
    }
}
