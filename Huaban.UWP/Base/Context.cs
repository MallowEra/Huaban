﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Huaban.UWP.Base
{
	using Models;
	using Services;
	public class Context : ObservableObject
	{
		public Context()
		{
			NavigationService = new NavigationService(this);
			CategoryList = new IncrementalLoadingList<Category>(GetCategoryList);
			Categories = new ObservableCollection<Category>();


		}
		public NavigationService NavigationService { get; private set; }
		public API API { private set; get; } = API.Current();

		private User _User;
		public User User
		{
			get { return _User; }
			set { SetValue(ref _User, value); }
		}

		private bool _IsLogin;
		public bool IsLogin
		{
			get
			{
				return _IsLogin;
			}
			set { SetValue(ref _IsLogin, value); }
		}

		public IncrementalLoadingList<Category> CategoryList { get; private set; }

		private ObservableCollection<Category> _Categories;
		public ObservableCollection<Category> Categories
		{
			get { return _Categories; }
			set { SetValue(ref _Categories, value); }
		}

		private IncrementalLoadingList<Board> _BoardList;
		public IncrementalLoadingList<Board> BoardList
		{
			get { return _BoardList; }
			set { SetValue(ref _BoardList, value); }
		}

		public async Task SetToken(AuthToken token)
		{
			BoardList = new IncrementalLoadingList<Board>(GetBoardList);
			API.SetToken(token);
			var user = await API.UserAPI.GetSelf();

			User = user;
			IsLogin = true;

			await StorageHelper.SaveLocal(token);
			await StorageHelper.SaveLocal(user);
		}

		private async Task<IEnumerable<Board>> GetBoardList(uint startIndex, int page)
		{
			BoardList.NoMore();

			List<Board> list = new List<Board>();
			try
			{
				list = await API.UserAPI.GetBoards(User.user_id, 0);
				if (list.Count < 20)
					BoardList.NoMore();
				else
					BoardList.HasMore();
				return list;
			}
			catch (Exception ex)
			{
			}

			return list;
		}

		private async Task<IEnumerable<Category>> GetCategoryList(uint startIndex, int page)
		{
			try
			{
				var list = await API.CategoryAPI.GetCategoryList();
				foreach (var item in list)
				{
					Categories.Add(item);
				}
				list.Insert(0, new Category() { name = "最热", nav_link = "/popular/" });
				list.Insert(0, new Category() { name = "首页", nav_link = "/all/" });

				CategoryList.NoMore();
				return list;
			}
			catch (Exception ex)
			{
			}

			return null;
		}
		public void ShowTip(string msg)
		{
			Message = msg;
		}

		private string _Message;
		public string Message
		{
			get { return _Message; }
			private set { SetValue(ref _Message, value); }
		}
		
	}
}
