using LocativeApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LocativeApp.Services;

public class RentService
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;
    private readonly CurrentOwnerService _currentOwner;

    public RentService(
        IDbContextFactory<ApplicationDbContext> factory,
        CurrentOwnerService currentOwner)
    {
        _factory = factory;
        _currentOwner = currentOwner;
    }

    // ===================== MOIS =====================
    public List<(int Year, int Month)> GetMonths(DateTime startDate)
    {
        var months = new List<(int, int)>();

        var current = new DateTime(startDate.Year, startDate.Month, 1);
        var today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        while (current <= today)
        {
            months.Add((current.Year, current.Month));
            current = current.AddMonths(1);
        }

        return months;
    }

    // ===================== PAYMENTS =====================
    public async Task<List<RentPayment>> GetPaymentsAsync(int tenantId)
    {
        using var db = _factory.CreateDbContext();
        var ownerId = await _currentOwner.GetOwnerIdAsync();

        return await db.RentPayments
            .Where(p => p.TenantId == tenantId && p.OwnerId == ownerId)
            .ToListAsync();
    }

    // ===================== SUMMARY =====================
    public async Task<(int total, int paid, int unpaid)> GetSummaryAsync(Tenant tenant)
    {
        var months = GetMonths(tenant.CreatedAt);
        var payments = await GetPaymentsAsync(tenant.Id);

        int paid = payments.Count(p => p.IsPaid);
        int total = months.Count;

        return (total, paid, total - paid);
    }

    // ===================== PAY =====================
    public async Task MarkAsPaid(int tenantId, int propertyId, int year, int month, decimal amount)
    {
        using var db = _factory.CreateDbContext();

        var ownerId = await _currentOwner.GetOwnerIdAsync();

        if (string.IsNullOrEmpty(ownerId))
            throw new Exception("OwnerId introuvable (SaaS context)");

        var existing = await db.RentPayments.FirstOrDefaultAsync(p =>
            p.TenantId == tenantId &&
            p.Year == year &&
            p.Month == month &&
            p.OwnerId == ownerId);

        if (existing == null)
        {
            existing = new RentPayment
            {
                TenantId = tenantId,
                PropertyId = propertyId,
                Year = year,
                Month = month,
                OwnerId = ownerId, // 🔥 OBLIGATOIRE SaaS
                AmountPaid = amount,
                IsPaid = true,
                PaidDate = DateTime.Now
            };

            db.RentPayments.Add(existing);
        }
        else
        {
            existing.AmountPaid = amount;
            existing.IsPaid = true;
            existing.PaidDate = DateTime.Now;

            if (string.IsNullOrEmpty(existing.OwnerId))
                existing.OwnerId = ownerId;
        }

        await db.SaveChangesAsync();
    }

    // ===================== UNPAY =====================
    public async Task MarkAsUnpaid(int tenantId, int year, int month)
    {
        using var db = _factory.CreateDbContext();

        var ownerId = await _currentOwner.GetOwnerIdAsync();

        var existing = await db.RentPayments.FirstOrDefaultAsync(p =>
            p.TenantId == tenantId &&
            p.Year == year &&
            p.Month == month &&
            p.OwnerId == ownerId);

        if (existing == null) return;

        existing.IsPaid = false;
        existing.PaidDate = null;

        await db.SaveChangesAsync();
    }
}


//using LocativeApp.Data;
//using Microsoft.EntityFrameworkCore;

//namespace LocativeApp.Services;

//public class RentService
//{
//    private readonly IDbContextFactory<ApplicationDbContext> _factory;

//    public RentService(IDbContextFactory<ApplicationDbContext> factory)
//    {
//        _factory = factory;
//    }

//    // 🔥 Liste des mois attendus (inchangé)
//    public List<(int Year, int Month)> GetMonths(DateTime startDate)
//    {
//        var months = new List<(int, int)>();

//        var current = new DateTime(startDate.Year, startDate.Month, 1);
//        var today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

//        while (current <= today)
//        {
//            months.Add((current.Year, current.Month));
//            current = current.AddMonths(1);
//        }

//        return months;
//    }

//    // 🔥 Paiements existants
//    public async Task<List<RentPayment>> GetPaymentsAsync(int tenantId)
//    {
//        using var db = _factory.CreateDbContext();

//        return await db.RentPayments
//            .Where(p => p.TenantId == tenantId)
//            .ToListAsync();
//    }

//    // 🔥 Résumé global
//    public async Task<(int total, int paid, int unpaid)> GetSummaryAsync(Tenant tenant)
//    {
//        var months = GetMonths(tenant.CreatedAt);
//        var payments = await GetPaymentsAsync(tenant.Id);

//        int paid = payments.Count(p => p.IsPaid);
//        int total = months.Count;

//        return (total, paid, total - paid);
//    }

//    // 🔥 Marquer comme payé
//    public async Task MarkAsPaid(int tenantId, int propertyId, int year, int month, decimal amount)
//    {
//        using var db = _factory.CreateDbContext();

//        var existing = await db.RentPayments.FirstOrDefaultAsync(p =>
//            p.TenantId == tenantId &&
//            p.Year == year &&
//            p.Month == month);

//        if (existing == null)
//        {
//            existing = new RentPayment
//            {
//                TenantId = tenantId,
//                PropertyId = propertyId,
//                Year = year,
//                Month = month
//            };

//            db.RentPayments.Add(existing);
//        }

//        existing.AmountPaid = amount;
//        existing.IsPaid = true;
//        existing.PaidDate = DateTime.Now;

//        await db.SaveChangesAsync();
//    }

//    // 🔥 Marquer impayé
//    public async Task MarkAsUnpaid(int tenantId, int year, int month)
//    {
//        using var db = _factory.CreateDbContext();

//        var existing = await db.RentPayments.FirstOrDefaultAsync(p =>
//            p.TenantId == tenantId &&
//            p.Year == year &&
//            p.Month == month);

//        if (existing != null)
//        {
//            existing.IsPaid = false;
//            existing.PaidDate = null;

//            await db.SaveChangesAsync();
//        }
//    }
//}