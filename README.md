# What is GenericRepository

GenericRepository is a data access library using repository pattern with Entity Framework Core.

Some of the features of GenericRepository are:

  * Build-in paging with filter and sorting feature
  * Eager loading support
  * Unit of Work to support transaction control
  * Support different Entity key types

# NuGet
```xml
Install-Package GenericRepository.EntityFrameworkCore
```
# Getting started with GenericRepository

  * Implement IEntityContext in the application DbContext class
  * Add application DbContext in Startup: 
  ```xml
  * services.AddDbContext<AppContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));
  * services.AddDataAccess<AppContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));
  ```
  * Annotate key property in entity classes with [Key] attribute
  ```xml
           public class Department
           {
                [key]
                public virtual int Id {get; set;}
                public virtual string Name {get;set;}
                public virtual User Staff{get;set;}
           }
  ```
  * Get the repository object and call functions:  
  Unit of Work:
  ```xml
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department>();

                foreach (var item in departments)
                {
                    repository.Add(item);
                }

                await uow.SaveChangesAsync();
            }
  ```
  Filtering, sorting and paging:
  ```xml
            Func<IQueryable<Log>, IOrderedQueryable<Log>> orderBy=x=>x.OrderByDescending(y=>y.CreatedDate);
            var filter = PredicateBuilder.New<Log>(x => true);           
            if (!string.IsNullOrEmpty(sessionId))               
               filter = filter.And(x => x.SessionId == sessionId);
            if (!string.IsNullOrEmpty(logLevel))               
               filter = filter.And(x => x.LogLevel== logLevel);
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Log>();
                return repository.QueryPage(startRow, pageSize, filter, orderBy);           
            }
  ```
  Eager loading:
  ```xml
            Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy=x=>x.OrderBy(y=>y.Name);
            Func<IQueryable<Department>, IQueryable<Department>> include=x=>x.Include(y=>y.Staff);
            var filter = PredicateBuilder.New<Department>(x => true);           
            if (!string.IsNullOrEmpty(name))               
               filter = filter.And(x => x.Name == name);
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department>();
                return repository.QueryPage(startRow, pageSize, filter, orderBy,include);           
            }
  ```
# License
All source code is licensed under MIT license - http://www.opensource.org/licenses/mit-license.php
