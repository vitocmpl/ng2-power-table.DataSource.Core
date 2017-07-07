ng2-ya-table DataSource Core
===============================

This library contains a collection of server-side helpers for the ng2-ya-table library (https://github.com/vitocmpl/ng2-ya-table). 


## Installation

You can add this library to your project using [NuGet](https://www.nuget.org/). This is the only method this library is currently distributed unless
you choose to build your own binaries using source code. Run the following command in the “Package Manager Console”:

    PM> Install-Package ng2-ya-table.DataSource.Core
    
Or right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘ng2-ya-table.DataSource.Core’ and click ‘Install’.
([see NuGet Gallery](https://www.nuget.org/packages/ng2-ya-table.DataSource.Core/).)


## Example 

```csharp
public async Task<IActionResult> GetUsers([FromBody]DataSourceRequest parameters)
{
    var query = from p in db.Users
                select new
                {
                    Id = p.Id,
                    Username = p.Username,
                    ...
                };

    return Ok(await query.ToDataSourceResult(parameters))
}
```


## Further Documentation

See the Demo project for further details; installation, customization and other useful articles will be available soon...


## License

[MIT](LICENSE) license.