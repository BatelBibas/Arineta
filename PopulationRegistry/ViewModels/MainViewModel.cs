using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopulationRegistry.Commands;
using PopulationRegistry.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Newtonsoft.Json;

namespace PopulationRegistry.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        #region Commands
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand JumpToCommand { get; private set; }
        public ICommand UpdatePersonCommand { get; private set; }
        #endregion

        #region Properties
        private int itemsPerPage = 15;
        private int itemsCount;

        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set 
            { 
                _currentPageIndex = value; 
                OnPropertyChanged("CurrentPage"); 
            }
        }
        public int CurrentPage
        {
            get { return _currentPageIndex + 1; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set 
            { 
                _totalPages = value; 
                OnPropertyChanged("TotalPage"); 
            }
        }

        public CollectionViewSource ViewList { get; set; }
        private ObservableCollection<Person> peopleList = new ObservableCollection<Person>();
        #endregion

        #region Commands Methods
        public void ShowNextPage()
        {
            CurrentPageIndex++;
            ViewList.View.Refresh();
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            ViewList.View.Refresh();
        }

        public void JumpToPage(int pageNumber)
        {
            if (pageNumber > 0 && pageNumber < _totalPages)
            {
                CurrentPageIndex = pageNumber - 1;
                ViewList.View.Refresh();
            }
        }

        private void view_Filter(object sender, FilterEventArgs e)
        {
            Person p = ((Person)e.Item);
            int index = peopleList.IndexOf(p);
            if (index >= itemsPerPage * CurrentPageIndex && index < itemsPerPage * (CurrentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void CalculateTotalPages()
        {
            if (itemsCount % itemsPerPage == 0)
            {
                TotalPages = (itemsCount / itemsPerPage);
            }
            else
            {
                TotalPages = (itemsCount / itemsPerPage) + 1;
            }
        }
        #endregion

        #region Constructor
        public MainViewModel(string jsonStream)
        {
            var streamList = JsonConvert.DeserializeObject<List<Person>>(jsonStream);
            peopleList = new ObservableCollection<Person>(streamList);

            ViewList = new CollectionViewSource();
            ViewList.Source = peopleList;
            ViewList.Filter += new FilterEventHandler(view_Filter);

            CurrentPageIndex = 0;
            itemsCount = peopleList.Count;
            CalculateTotalPages();

            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            JumpToCommand = new JumpToCommand(this);
        }
        #endregion
    }
}
