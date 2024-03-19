using FilmsAPI.Abstractions;
using FilmsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Repositories;

public class CompanyRepository(AppDbContext appDbContext): ICompanyRepository
{
    public async Task<Company> Add(Company company)
    {
        var result = await appDbContext.Companies.AddAsync(company);
        return result.Entity;    }

    public async Task<Company?> GetCompanyById(Guid companyId)
    {
        var company = await appDbContext.Companies.FindAsync(companyId);

        return company;
    }
}