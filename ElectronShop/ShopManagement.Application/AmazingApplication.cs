using _01_Framework.Application;
using ShopManagement.Application.Contracts.Amazing;
using ShopManagement.Domain.AmazingAgg;

namespace ShopManagement.Application
{
    public class AmazingApplication : IAmazingApplication
    {
        private readonly IAmazingRepository _amazingRepository;

        public AmazingApplication(IAmazingRepository amazingRepository)
        {
            _amazingRepository = amazingRepository;
        }

        public OperationResult Create(CreateAmazing command)
        {
            var operation = new OperationResult();
            if (_amazingRepository.Exists(x => x.ProductId == command.ProductId && x.Position == command.Position))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var amazing = new Amazing(startDate, endDate, command.Position, command.ProductId);
            _amazingRepository.Create(amazing);
            _amazingRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditAmazing command)
        {
            var operation = new OperationResult();
            var amazing = _amazingRepository.Get(command.Id);
            if (amazing == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_amazingRepository.Exists(x => x.ProductId == command.ProductId && x.Position == command.Position && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            amazing.Edit(startDate, endDate, command.Position, command.ProductId);
            _amazingRepository.SaveChanges();
            return operation.Success();
        }

        public EditAmazing GetDetails(int id)
        {
            return _amazingRepository.GetDetails(id);
        }

        public async Task<List<AmazingViewModel>> Search(AmazingSearchModel searchModel)
        {
            return await _amazingRepository.Search(searchModel);
        }
    }
}
