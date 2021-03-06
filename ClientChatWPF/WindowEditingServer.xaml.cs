﻿using ClassesForServerClent.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClientChatWPF
{
	/// <summary>
	/// Логика взаимодействия для WindowEditingUser.xaml
	/// </summary>
	public partial class WindowEditingServer : Window
	{
		public event Action<List<ServerUser>> EventUpServerUser;
		public event Action<List<TextChat>> EventUpTextChat;
		public event Action<List<Opinion>>  EventUpOpinion;
		public event Action<List<EventLog>> EventUpEventLog;
		public event Action<List<Role>> EventUpRole;
		public event Action<Server> EventUpServer;
		public Server Server { get; private set; }

		private WindowEditingServer()
		{
			InitializeComponent();

			EventUpEventLog     += UpEventLog;
			EventUpTextChat     += UpTextChat;
			EventUpServerUser   += UpServerUser;
			EventUpOpinion      += UpOpinion;
			EventUpRole         += UpRole;
			EventUpServer       += UpServer;
		}

		public WindowEditingServer(Server server)
			: this()
			=> Server = server;

		public void StartEventOfObject(Object ob)
		{
			switch (ob)
			{
				case (Server S):
					EventUpServer?.Invoke(S);
					break;
				case (List<ServerUser> SU):
					EventUpServerUser?.Invoke(SU);
					break;
				case (List<Role> R):
					EventUpRole?.Invoke(R);
					break;
				case (List<TextChat> TC):
					EventUpTextChat?.Invoke(TC);
					break;
				case (List<EventLog> EL):
					EventUpEventLog?.Invoke(EL);
					break;
				case (List<Opinion> O):
					EventUpOpinion?.Invoke(O);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Твоя задача взять данные у объекта obj
		/// и передать их пользователю через поля
		/// </summary>
		/// <param name="obj"></param>
		private void UpServer(Server obj)
		{
			Name.Dispatcher?.Invoke(new Action(() => Name.Text = obj.Name));
			StatusServer.Dispatcher?.Invoke(new Action(() => StatusServer.IsChecked = obj.Status));
			Info.Dispatcher?.Invoke(new Action(() => Info.Text = obj.Info));
			Language.Dispatcher?.Invoke(new Action(() => Name.Text = obj.Language));
		}

		/// <summary>
		///	У роли есть заполненное своисво RightRole (ICollection)
		///	оно должно передаться на другое окно
		/// </summary>
		/// <param name="obj"></param>
		private void UpRole(List<Role> obj)
		{

		}

		/// <summary>
		/// У Opinion есть заполненное своисво Opinion (ICollection)
		/// </summary>
		/// <param name="obj"></param>
		private void UpOpinion(List<Opinion> obj)
		{
			ListTextChats.Dispatcher?
				.Invoke(new Action(() =>
				{
					var list1 = obj.Select(x => x).ToList();
					ListTextChats.ItemsSource = list1;
				}));
		}

		/// <summary>
		/// Просто список пользователей, у которых есть роль (1 роль)
		/// кидаешь их в ListBox
		/// </summary>
		/// <param name="obj"></param>
		private void UpServerUser(List<ServerUser> obj)
		{
			ListUserOnServer.Dispatcher?
				.Invoke(new Action(
					() =>
					{
						ListUserOnServer.ItemsSource = obj;
					}
				));
		}

		/// <summary>
		/// просто список текстовых чатов
		/// кидаешь их в ListBox
		/// </summary>
		/// <param name="obj"></param>
		private void UpTextChat(List<TextChat> obj)
		{
			/*		ListUserMessage.Dispatcher?
				.Invoke(new Action(
					() =>
					{
						var list1 = obj.Select(x => x).ToList();
						var list2 = (List<ServerUser>)obj.ToArray().Clone();
						ListUserMessage.ItemsSource = list1;
					}
					));*/
		}

		/// <summary>
		/// Список событий
		/// кидаешь его в ListBox
		/// </summary>
		/// <param name="obj"></param>
		private void UpEventLog(List<EventLog> obj)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabControlSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var s = new Server() { ID = Server.ID };

			switch (TabControlSet.SelectedIndex)
			{
				case (0):
					break;

				case (1):
					s.ActionForServer = ActionForServer.Loud;
					break;

				case (2):
					s.ActionForServer = ActionForServer.LoudServerUsers;
					break;

				case (3):
					s.ActionForServer = ActionForServer.LoudRole;
					break;

				case (4):
					s.ActionForServer = ActionForServer.LoudTextChat;
					break;

				case (5):
					s.ActionForServer = ActionForServer.LoudEventLog;
					break;

				case (6):
					break;

				default:
					MessageBox.Show("В дурку хочешь?\nТы как на это нажал?");
					break;
			}

			SendMessageToServer.SendMessageSerialize(s);
		}

        private void SaveEditingServerClick(object sender, RoutedEventArgs e)
        {

        }

        private void CancelEditingServerClick(object sender, RoutedEventArgs e)
        {

        }

        private void DelAddRoleClick(object sender, RoutedEventArgs e)
        {

        }
    }
}