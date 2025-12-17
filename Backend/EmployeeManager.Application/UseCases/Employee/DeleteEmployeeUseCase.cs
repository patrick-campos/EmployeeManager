using EmployeeManager.Application.Abstractions.UseCases;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Application.UseCases
{
    public class DeleteEmployeeUseCase : IDeleteEmployeeUseCase
    {
        private readonly IRepository<Employee> _repository;

        public DeleteEmployeeUseCase(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task Execute(string idOfEmployeeWillBeDeleted, string positionOfCurrentUser)
        {
            Employee? empl = await _repository.Get(idOfEmployeeWillBeDeleted);

            if(empl is null)
                throw new ArgumentNullException("There's not any user with this argument");

            if (empl.CheckIfPositionOfCurrentUserIsAboveThanUserHowWillBeModified(positionOfCurrentUser, empl.PositionName))
                throw new UnauthorizedAccessException("You don't have the required level to perform this action");

            await _repository.Delete(idOfEmployeeWillBeDeleted);

        }
    }
}
