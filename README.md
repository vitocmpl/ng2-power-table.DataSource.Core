ng2-power-table DataSource Core
===============================

This library contains a collection of server-side helpers for the ng2-power-table library (https://github.com/vitocmpl/ng2-power-table). 
The library is written for the .NET Core 1.1 applications.

## Installation
To install this library, add the reference in the project.json dependencies:

```json
"ng2-power-table.DataSource.Core": "0.1.0"
```

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

## License

MIT ©