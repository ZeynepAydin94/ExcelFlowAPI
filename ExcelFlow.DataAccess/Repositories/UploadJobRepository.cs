using System;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ExcelFlow.DataAccess.Repositories;

public class UploadJobRepository : BaseRepository<UploadJob>, IUploadJobRepository
{
    public UploadJobRepository(AppDbContext context) : base(context)
    {
    }

}