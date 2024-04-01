using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkTracker.BusinessLogic;
using WorkTracker.Data.DAL;
using WorkTracker.DTO;
using WorkTracker.WPF.Utilities;

namespace WorkTracker.WPF.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public partial class WorkVM
    {
        private readonly WorkBusinessLogic _workBusinessLogic;
        public WorkDTO NewWork { get; set; } = new WorkDTO { WorkDetails = [new WorkDetailsDTO()] };
        public WorkDTO SelectedWork { get; set; } = new WorkDTO { WorkDetails = [new WorkDetailsDTO()] };
        public ObservableCollection<WorkDTO> Works { get; set; } = [];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TotalHours { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public static Func<string, string, Task<bool>> ConfirmDeletion { get; set; }

        public WorkVM(AutoMapper.IMapper mapper, IWorkRepository workRepository)
        {
            _workBusinessLogic = new WorkBusinessLogic(mapper, workRepository);

            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateTime.Now;
            AddCommand = new CustomCommand(Add, CanAdd);
            SearchCommand = new CustomCommand(Search, CanSearch);
            UpdateCommand = new CustomCommand(Update, CanUpdate);
            DeleteCommand = new CustomCommand(Delete, CanDelete);
            LoadData();
        }


        private async void Add(object obj)
        {
            await _workBusinessLogic.AddWork(NewWork);
            NewWork = new();
            LoadData();
        }

        private bool CanAdd(object obj)
        {
            var validated = NewWork.Date > new DateTime(2023, 05, 08)
                && NewWork.Date <= DateTime.Now
                && !string.IsNullOrWhiteSpace(NewWork.WorkDetails.FirstOrDefault()?.Description)
                && NewWork.WorkDetails.FirstOrDefault()?.Hours > 0;

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
            await _workBusinessLogic.UpdateWork(SelectedWork);
            LoadData();
        }

        private bool CanUpdate(object obj)
        {
            return SelectedWork?.Date > new DateTime(2023, 05, 08)
                && !string.IsNullOrWhiteSpace(SelectedWork?.WorkDetails?.FirstOrDefault()?.Description)
                && SelectedWork?.WorkDetails?.FirstOrDefault()?.Hours > 0;
        }
        private async void Delete(object obj)
        {
            if (await ConfirmDeletion?.Invoke("Warning", "Are you sure you want to delete this entry?")! == true)
                await _workBusinessLogic.DeleteWork(SelectedWork.Id);
            LoadData();
        }

        private bool CanDelete(object obj)
        {
            return SelectedWork != null;
        }
        private async void LoadData()
        {
            Works = (await _workBusinessLogic.GetWorks(StartDate, EndDate))
                .OrderBy(w => w.Date)
                .ToList()
                .ToObservableCollection();

            TotalHours = Works.Sum(w => w.WorkDetails.Sum(wd => wd.Hours));
        }
    }
}
