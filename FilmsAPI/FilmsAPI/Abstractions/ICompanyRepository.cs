using FilmsAPI.Entities;

namespace FilmsAPI.Abstractions;

public interface ICompanyRepository
{
    Task<Company> Add(Company company);

    Task<Company?> GetCompanyById(Guid companyId);
}