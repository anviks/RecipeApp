using System.Net;
using App.BLL;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using RecipeApp.ApiControllers;
using Xunit.Abstractions;

namespace App.Test.ApiControllers;

public class CategoriesControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private AppDbContext _ctx;
    private IAppBusinessLogic _bll;
    private IAppUnitOfWork _uow;
    private UserManager<AppUser> _userManager;

    // SUT (System Under Test)
    private readonly CategoriesController _controller;

    public CategoriesControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        23.Should().Be(23, because: "this is the literal value");
        
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _ctx = new AppDbContext(optionsBuilder.Options);

        var configUow = new MapperConfiguration(cfg => cfg.CreateMap<App.Domain.Category, DAL.DTO.Category>().ReverseMap());
        IMapper? mapperUow = configUow.CreateMapper();
        
        _uow = new AppUnitOfWork(_ctx, mapperUow);

        var configBll = new MapperConfiguration(cfg => cfg.CreateMap<DAL.DTO.Category, BLL.DTO.Category>().ReverseMap());
        IMapper? mapperBll = configBll.CreateMapper();
        _bll = new AppBusinessLogic(_uow, mapperBll);

        var configWeb = new MapperConfiguration(cfg => cfg.CreateMap<BLL.DTO.Category, DTO.v1_0.Category>().ReverseMap());
        IMapper? mapperWeb = configWeb.CreateMapper();
        
        var storeStub = Substitute.For<IUserStore<AppUser>>();
        var optionsStub = Substitute.For<IOptions<IdentityOptions>>();
        var hasherStub = Substitute.For<IPasswordHasher<AppUser>>();

        var validatorStub = Substitute.For<IEnumerable<IUserValidator<AppUser>>>();
        var passwordStub = Substitute.For<IEnumerable<IPasswordValidator<AppUser>>>();
        var lookupStub = Substitute.For<ILookupNormalizer>();
        var errorStub = Substitute.For<IdentityErrorDescriber>();
        var serviceStub = Substitute.For<IServiceProvider>();
        var loggerStub = Substitute.For<ILogger<UserManager<AppUser>>>();

        _userManager = Substitute.For<UserManager<AppUser>>(
            storeStub, optionsStub, hasherStub, validatorStub, 
            passwordStub, lookupStub, errorStub, serviceStub, loggerStub
        );
        
        _controller = new CategoriesController(_bll, mapperWeb);
        _userManager.GetUserId(_controller.User).Returns(Guid.NewGuid().ToString());
    }
    
    [Fact]
    public async Task GetTest()
    {
        var result = await _controller.GetCategories();
        var okRes = result.Result as OkObjectResult;
        var val = (List<App.DTO.v1_0.Category>) okRes!.Value!;
        val.Should().NotBeNull();
        val.Should().HaveCount(0);
    }
}
