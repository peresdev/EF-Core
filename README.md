# EF-Core
Entity Framework Core (EF Core)

# Comandos terminal / PM

```
dotnet new sln -n Curso
dotnet new console -n CursoEFCore -o Curso -f netcoreapp3.1
dotnet sln Curso.sln add Curso\CursoEFCore.csproj
```

```
dotnet add caminhocsproj package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.5
```

```
Package Manager Console -> Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.5
```

```
dotnet tool install --global dotnet-ef --version 3.1.5
```

```
dotnet add .\CursoEFCore.csproj package Microsoft.EntityFrameworkCore.Design --version 3.1.5
dotnet add .\CursoEFCore.csproj package Microsoft.EntityFrameworkCore.Tools --version 3.1.5
```

```
dotnet ef migrations add PrimeiraMigracao -p .\CursoEFCore.csproj 
```

```
dotnet ef migrations script -p .\CursoEFCore.csproj -o .\PrimeiraMigracao.sql
```

```
dotnet ef database update -p .\CursoEFCore.csproj -v
```

```
dotnet ef migrations script -p .\CursoEFCore.csproj -o .\Idempotente.sql --idempotent
```

```
dotnet ef database update PrimeiraMigracao -p .\CursoEFCore.csproj
```

```
dotnet ef migrations remove -p .\CursoEFCore.csproj
```

```
dotnet add package Microsoft.Extensions.Logging.Console --version 3.1.5
```
