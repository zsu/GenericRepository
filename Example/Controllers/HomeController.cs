using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenericRepository;
using Example.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUowProvider _uowProvider;
        private readonly IServiceProvider _serviceProder;
        private readonly IGenericService<AppContext> _genericService;   

        public HomeController(IUowProvider uowProvider,IGenericService<AppContext> genericService, IServiceProvider serviceProder)
        {
            _uowProvider = uowProvider;
            _serviceProder = serviceProder;
            _genericService = genericService;
        }

        public async Task<IActionResult> Index()
        {
            var result = _genericService.Query<Department>(null);
            using (var uow = _genericService.CreateUnitOfWork())
            {
                var result1 = _genericService.Query<Department>(null);
            }
            //var appContext = _serviceProder.GetService<IGenericService<AppContext>>();
            //var result1=appContext.Query<Department>(null);
            //await Seed();
            IEnumerable<Department> buildings = null;

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department>();

                //**************************************

                var includes = new Includes<Department>(query =>
                {
                    return query.Include(b => b.Employees)
                                    .ThenInclude(a => a.Addresses);
                });

                buildings = await repository.GetAllAsync(null, includes.Expression);

                //var building = await repository.GetAsync(1, includes.Expression);

                //**************************************

                //Func<IQueryable<Building>, IQueryable<Building>> func = query =>
                //{
                //    return query.Include(b => b.Appartments)
                //                    .ThenInclude(a => a.Rooms);
                //};

                //buildings = await repository.GetAllAsync(null, func);

                //**************************************

                //buildings = await repository.GetAllAsync(null, query =>
                //{
                //    return query.Include(b => b.Appartments)
                //                    .ThenInclude(a => a.Rooms);
                //});


            }

            return View(buildings);
        }

        public async Task<IActionResult> Seed()
        {
            var buildings = new List<Department>
            {
                new Department
                {
                    Name = "IT",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Age = 31,
                            Name = "Tom",
                            Addresses = new List<Address>
                            {
                                new Address
                                {
                                    Number = 123,
                                    Street ="Main Street",
                                    City= "Miami",
                                    State="FL",
                                    Zipcode="11031"
                                },
                                new Address
                                {
                                    Number = 124,
                                    Street ="Main Street",
                                    City= "Miami",
                                    State="FL",
                                    Zipcode="11031"
                                },
                                new Address
                                {
                                    Number = 125,
                                    Street ="Main Street",
                                    City= "Miami",
                                    State="FL",
                                    Zipcode="11031"
                                }
                            }
                        },
                        new Employee
                        {
                            Age = 50,
                            Name = "Bill",
                            Addresses = new List<Address>
                            {
                                new Address
                                {
                                    Number = 386,
                                    Street ="Gilber Street",
                                    City= "Miami",
                                    State="FL",
                                    Zipcode="15461"
                                },
                                new Address
                                {
                                    Number = 387,
                                    Street ="Gilber Street",
                                    City= "Miami",
                                    State="FL",
                                    Zipcode="15461"
                                }
                            }
                        }
                    }
                }
            };

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department>();

                foreach (var item in buildings)
                {
                    repository.Add(item);
                }

                await uow.SaveChangesAsync();
            }
            var Logs = new List<Log>
            {
                new Log{Error="Error1"},
                new Log{Error="Error2"}
            };
            using (var uow = _uowProvider.CreateUnitOfWork<SecondAppContext>())
            {
                var repository = uow.GetRepository<Log>();

                foreach (var item in Logs)
                {
                    repository.Add(item);
                }

                await uow.SaveChangesAsync();
            }

            using(var uow=_genericService.CreateUnitOfWork())
            {
                var item = _genericService.Query<Employee>(null).FirstOrDefault();
                if (item != null)
                    item.Name = item.Name + "1";
                _genericService.Update(item);
                var item2= _genericService.Query<Employee>(x=>x.Name=="Bill").FirstOrDefault();
                if (item2 != null)
                {
                    ChangeName(item2);
                    _genericService.Update(item2);
                }

                uow.SaveChanges();
            }
            var items = _genericService.Query<Employee>(null).ToList();
            using (var uow = _genericService.CreateUnitOfWork())
            {
                var item = _genericService.Query<Employee>(null).ToList();
            }
            return View();
        }
        private void ChangeName(Employee user)
        {
            using (var uow = _genericService.CreateUnitOfWork())
            {
                var item = _genericService.Query<Employee>(x=>x.Name==user.Name).FirstOrDefault();
                if (item != null)
                    item.Name = item.Name + "2";
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
