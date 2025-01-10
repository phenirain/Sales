using AutoMapper;
using Bogus;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Entities.Mapping;
using Sales.Repositories;
using Sales.Repositories.Interfaces;
using Sales.Tests.Support;

namespace Sales.Tests.Integration.Repositories;


public class BuyerRepositoryTests
{
    private static IUnitOfWork _unitOfWork;
    
    [OneTimeSetUp]
    public static async Task GlobalSetUp()
    {
        _unitOfWork = await DatabaseTestHelper.CreateUnitOfWork();
        await DatabaseTestHelper.CreateFakeBuyers();
    }

    [Test]
    public async Task GetAllBuyers_WithLimitAndOffset()
    {
        var actual = await _unitOfWork.BuyerRepository.GetAll(7, 0);
        Assert.That(7, Is.EqualTo(actual.Count()));
    }

    [Test]
    public async Task GetAllBuyers_WithSalesIds()
    {
        await DatabaseTestHelper.CreateFakeSales(buyerId: new Faker().Random.Number(1, 20));
        var actual = await _unitOfWork.BuyerRepository.GetAll(7, 0);
        var nullCount = 0;
        var notExpected = 7;
        foreach (var buyer in actual)
        {
            if (buyer.SalesIds.Count == 0)
                nullCount++;
        }
        Assert.That(nullCount, Is.LessThan(notExpected));
    }

    [Test]
    public async Task GetBuyerById_WithSalesIds()
    {
        var id = new Faker().Random.Number(1, 20);
        await DatabaseTestHelper.CreateFakeSales(buyerId: id);
        var actual = await _unitOfWork.BuyerRepository.GetById(id);
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(id));
        Assert.That(actual.SalesIds.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task CreateBuyer_And_GetById()
    {
        int id = 21;
        var buyer = new Faker<Buyer>()
           .RuleFor(b => b.Name, f => f.Name.FullName())
           .Generate();
        var actual = await _unitOfWork.BuyerRepository.Create(buyer);
        await _unitOfWork.SaveChangesAsync();
        Assert.That(actual.Id, Is.GreaterThan(0));
        Assert.That(actual.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task UpdateBuyer()
    {
        var buyer = new Faker<Buyer>()
            .RuleFor(b => b.Id, f => f.Random.Number(1, 20))
            .RuleFor(b => b.Name, f => f.Name.FullName())
            .Generate();
        await _unitOfWork.BuyerRepository.Update(buyer);
        await _unitOfWork.SaveChangesAsync();
        var actual = await _unitOfWork.BuyerRepository.GetById(buyer.Id);
        Assert.IsNotNull(actual);
        Assert.That(actual.Name, Is.EqualTo(buyer.Name));
    }

    [Test]
    public async Task DeleteBuyer()
    {
        var id = new Faker().Random.Number(1, 20);
        var buyer = await _unitOfWork.BuyerRepository.GetById(id);
        Assert.IsNotNull(buyer);
        await _unitOfWork.BuyerRepository.Delete(buyer);
        await _unitOfWork.SaveChangesAsync();
        var actual = await _unitOfWork.BuyerRepository.GetById(id);
        Assert.IsNull(actual);
    }

    [OneTimeTearDown]
    public static async Task GlobalTearDown()
    {
        DatabaseTestHelper.ResetDatabase();
    }
}