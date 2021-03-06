﻿using Windows.UI.Xaml;

namespace iHuaban.Core.Models
{
    public class ViewModelBase : ObservableObject
    {
        public virtual string TemplateName
        {
            get
            {
                return "Template" + this.GetType().Name.Replace("ViewModel", "");
            }
        }
        public ViewModelBase()
        {
            this.NoMoreVisibility = Visibility.Collapsed;
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        private Visibility _NoMoreVisibility;
        public Visibility NoMoreVisibility
        {
            get { return _NoMoreVisibility; }
            set { SetValue(ref _NoMoreVisibility, value); }
        }
    }
}
