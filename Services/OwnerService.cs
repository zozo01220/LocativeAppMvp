using LocativeApp.Data;
using Microsoft.EntityFrameworkCore;
using System;

public class OwnerService
{
    private readonly ApplicationDbContext _db;

    public OwnerService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Owner>> GetAll()
    {
        return await _db.Owners
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<OwnerStatsDto>> GetAllStats()
    {
        var owners = await _db.Owners
            .Select(o => new OwnerStatsDto
            {
                OwnerId = o.Id,

                UsersCount = _db.Users.Count(u => u.OwnerId == o.Id),

                PropertiesCount = _db.Properties.Count(p => p.OwnerId == o.Id),

                TenantsCount = _db.Tenants.Count(t => t.OwnerId == o.Id)
            })
            .ToListAsync();

        return owners;
    }

    public async Task<Owner> GetById(string id)
    {
        return await _db.Owners
            .Include(o => o.Users)
            .Include(o => o.Properties)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task Create(Owner owner)
    {
        _db.Owners.Add(owner);
        await _db.SaveChangesAsync();
    }

    public async Task ToggleStatus(string id)
    {
        var owner = await _db.Owners.FindAsync(id);
        if (owner == null) return;

        owner.IsActive = !owner.IsActive;
        await _db.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        var owner = await _db.Owners.FindAsync(id);
        if (owner == null) return;

        _db.Owners.Remove(owner);
        await _db.SaveChangesAsync();
    }

    public async Task<OwnerStatsDto> GetStats(string ownerId)
    {
        return new OwnerStatsDto
        {
            OwnerId = ownerId,
            UsersCount = await _db.Users.CountAsync(u => u.OwnerId == ownerId),
            PropertiesCount = await _db.Properties.CountAsync(p => p.OwnerId == ownerId),
            TenantsCount = await _db.Tenants.CountAsync(t => t.OwnerId == ownerId)
        };
    }
}

//using LocativeApp.Data;

//namespace LocativeApp.Services
//{
//    public class OwnerService
//    {
//        private readonly ApplicationDbContext _db;

//        public OwnerService(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        public List<Owner> GetAll()
//        {
//            return _db.Owners.ToList();
//        }

//        public async Task<Owner> Create(Owner owner)
//        {
//            owner.Id = Guid.NewGuid().ToString();

//            _db.Owners.Add(owner);
//            await _db.SaveChangesAsync();

//            return owner;
//        }
//    }
//}