using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;

namespace WPF_IRC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client client = new Client();
        private Dictionary<TreeViewItem, UserControl> windowsByItem = new Dictionary<TreeViewItem, UserControl>();
        private TreeViewItem previouslySelectedItem;

        public MainWindow()
        {
            InitializeComponent();
            client.onConnect += new EventHandler(handleNewNetwork);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            client.Connect(hostTextBox.Text, Convert.ToInt32(portTextBox.Text), userTextBox.Text, nickTextBox.Text);
        }

        private void handleNewNetwork(object sender, EventArgs e)
        {
            TreeViewItem item = new TreeViewItem();
            addContextMenu(item);
            item.Header = (e as IrcEventArgs).Network;
            var serverWindow = new ServerWindow((e as IrcEventArgs).Network);
            windowsByItem.Add(item, serverWindow);
            Grid.SetColumn(serverWindow, 2);
            if (previouslySelectedItem != null)
                mainGrid.Children.Remove(windowsByItem[previouslySelectedItem]);
            mainGrid.Children.Add(serverWindow);
            item.Selected += new RoutedEventHandler(treeViewItemSelected);
            previouslySelectedItem = item;
            connectionTreeView.Items.Add(item);
            item.IsSelected = true;
        }


        void treeViewItemSelected(object sender, RoutedEventArgs e)
        {
            if (previouslySelectedItem != null)
                mainGrid.Children.Remove(windowsByItem[previouslySelectedItem]);
            mainGrid.Children.Add(windowsByItem[connectionTreeView.SelectedItem as TreeViewItem]);
            previouslySelectedItem = connectionTreeView.SelectedItem as TreeViewItem;
            //mainGrid.Children[2] = windowsByItem[connectionTreeView.SelectedItem as TreeViewItem];
        }

        private void addContextMenu(TreeViewItem item)
        {
            item.ContextMenuOpening += new ContextMenuEventHandler(item_ContextMenuOpening);
            var menu = new ContextMenu();
            var closeMenuItem = new MenuItem();
            closeMenuItem.Header = "Close";
            closeMenuItem.Click += new RoutedEventHandler(deleteSelectedMenuItem);
            item.ContextMenu = new ContextMenu();
            item.ContextMenu.Items.Add(closeMenuItem);
        }

        void item_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            (sender as TreeViewItem).IsSelected = true;
        }

        void deleteSelectedMenuItem(object sender, RoutedEventArgs e)
        {
            connectionTreeView.Items.Remove(connectionTreeView.SelectedItem);
        }

    }
}
