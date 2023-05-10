using AutoMapper;
using GlobalUtilities;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkTracker.DAL;
using WorkTracker.Domain;
using WorkTracker.DTO;
using WorkTracker.Utilities;

namespace WorkTracker.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public partial class WorkVM
    {
        private WorkManager _workManager;
        public WorkDTO NewWork { get; set; } = new();
        public WorkDTO SelectedWork { get; set; } = new();
        public ObservableCollection<WorkDTO> Works { get; set; } = new ObservableCollection<WorkDTO>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public static Func<string, string, Task<bool>> ConfirmDeletion { get; set; }

        public WorkVM(AutoMapper.IMapper mapper, IWorkRepository workRepository)
        {
            _workManager = new WorkManager(mapper, workRepository);

            StartDate = DateTime.Now.AddMonths(-3);
            EndDate = DateTime.Now;
            AddCommand = new CustomCommand(Add, CanAdd);
            SearchCommand = new CustomCommand(Search, CanSearch);
            UpdateCommand = new CustomCommand(Update, CanUpdate);
            DeleteCommand = new CustomCommand(Delete, CanDelete);
            LoadData();
        }


        private async void Add(object obj)
        {
            await _workManager.AddWork(NewWork);
            NewWork = new();
            LoadData();
        }

        private bool CanAdd(object obj)
        {
            var validated = NewWork.Date > new DateTime(2023, 05, 08)
                && !string.IsNullOrWhiteSpace(NewWork.Description)
                && NewWork.Hours > 0;
            return validated;
        }

        private bool CanSearch(object obj)
        {
            if (StartDate <= DateTime.MinValue || StartDate >= EndDate)
                return false;

            return true;
        }

        private void Search(object obj)
        {
            LoadData();
        }

        private async void Update(object obj)
        {
            await _workManager.UpdateWork(SelectedWork);
        }

        private bool CanUpdate(object obj)
        {
            return NewWork.Date > new DateTime(2023, 05, 08)
                && !string.IsNullOrWhiteSpace(NewWork.Description)
                && NewWork.Hours > 0;
        }
        private async void Delete(object obj)
        {
            if (await ConfirmDeletion?.Invoke("Warning", "Are you sure you want to delete this entry?")! == true)
                await _workManager.DeleteWork(SelectedWork.Id);
            LoadData();
        }

        private bool CanDelete(object obj)
        {
            return true;
        }
        private async void LoadData()
        {
            Works = (await _workManager.GetWorks(StartDate, EndDate))
                .OrderBy(w => w.Date)
                .ToList()
                .ToObservableCollection();
        }



    }
}
