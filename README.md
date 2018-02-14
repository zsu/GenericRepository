# What is GenericRepository

GenericRepository is a data access library using repository pattern.

Some of the features of SessionMessage are:

  * Build-in paging feature
  * Implement Unit of Work pattern
  * Generic Entity key type

# NuGet
```xml
Install-Package GenericRepository.EntityFramework
```
# Getting started with GenericRepository

  * Call the followings in Startup:  
  ```xml
  * services.AddDbContext<AppContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));
  * services.AddDataAccess<AppContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));
  ```
  * Get the repository object and call functions:
  ```xml
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department,int>();

                foreach (var item in buildings)
                {
                    repository.Add(item);
                }

                await uow.SaveChangesAsync();
            }
  ```

# License
All source code is licensed under MIT license - http://www.opensource.org/licenses/mit-license.php
