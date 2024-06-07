```bash
dotnet tool update --global dotnet-ef
```

```bash
dotnet tool update -g dotnet-aspnet-codegenerator
```

```bash
dotnet ef migrations --project App.DAL --startup-project WebApp add Initial
```

```bash
dotnet ef migrations --project App.DAL --startup-project WebApp remove
```

```bash
dotnet ef database --project App.DAL --startup-project WebApp update
```

```bash
dotnet ef database --project App.DAL --startup-project WebApp drop
```

```bash
cd WebApp
dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
cd ..
```

```bash
$WebApp = "WebApp"
$Domain = "App.Domain"
$DbContext = "AppDbContext"
$MvcOutput = "Areas/Admin/Controllers"
$ApiOutput = "ApiControllers"
$GenerateMvcControllers = $False
$GenerateApiControllers = $True

$Entities = Get-ChildItem -Path $Domain -Filter '*.cs' | ForEach-Object { $_.BaseName }

cd $WebApp

foreach ($Entity in $Entities) {
    $Controller = $Entity + "sController"
    $Model = "$Domain.$Entity"
  
    if ($GenerateMvcControllers) { 
        dotnet aspnet-codegenerator controller `
            --controllerName $Controller `
            --readWriteActions `
            --model $Model `
            --dataContext $DbContext `
            --useDefaultLayout `
            --relativeFolderPath $MvcOutput `
            --useAsyncActions `
            --referenceScriptLibraries `
            --force
    }
    
    if ($GenerateApiControllers) {
        dotnet aspnet-codegenerator controller `
            --controllerName $Controller `
            --model $Model `
            --dataContext $DbContext `
            --relativeFolderPath $ApiOutput `
            -api `
            --useAsyncActions `
            --force
    }
}

cd ..
```

* `--controllerName` or `-name` - Name of the controller.
* `--readWriteActions` or `-actions` - Generate controller with read/write actions without a model.
* `--model` or `-m` - Model class to use.
* `--dataContext` or `-dc` - The DbContext class to use or the name of the class to generate.
* `--useDefaultLayout` or `-udl` - Use the default layout for the views.
* `--relativeFolderPath` or `-outDir` - Specify the relative output folder path from project where the file needs to be generated, if not specified, file will be generated in the project folder
* `--restWithNoViews` or `-api` - Generate a Controller with REST style API. `noViews` is assumed and any view related options are ignored.
* `--useAsyncActions` or `-async` - Generate async controller actions.
* `--referenceScriptLibraries` or `-scripts` - Reference script libraries in the generated views. Adds _ValidationScriptsPartial to Edit and Create pages.
* `--force` or `-f` - Overwrite existing files.
