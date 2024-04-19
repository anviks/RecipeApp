```bash
dotnet tool update --global dotnet-ef
```

```bash
dotnet tool update -g dotnet-aspnet-codegenerator
```

```bash
dotnet ef migrations --project App.DAL.EF --startup-project RecipeApp add initial
```

```bash
dotnet ef migrations --project App.DAL.EF --startup-project RecipeApp remove
```

```bash
dotnet ef database --project App.DAL.EF --startup-project RecipeApp update
```

```bash
dotnet ef database --project App.DAL.EF --startup-project RecipeApp drop
```

```bash
cd RecipeApp
dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
cd ..
```

```bash
$WebApp = "RecipeApp"
$Domain = "App.Domain"
$DbContext = "AppDbContext"
$Output = "Areas/Admin/Controllers"

$Entities = Get-ChildItem -Path $Domain -Filter '*.cs' | ForEach-Object { $_.BaseName }

cd $WebApp

foreach ($Entity in $Entities) {
    if ($Entity) { 
        dotnet aspnet-codegenerator controller `
            --controllerName ($Entity + "Controller") `
            --readWriteActions `
            --model "$Domain.$Entity" `
            --dataContext $DbContext `
            --useDefaultLayout `
            --relativeFolderPath $Output `
            --useAsyncActions `
            --referenceScriptLibraries `
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
* `--useAsyncActions` or `-async` - Generate async controller actions.
* `--referenceScriptLibraries` or `-scripts` - Reference script libraries in the generated views. Adds _ValidationScriptsPartial to Edit and Create pages.
* `--force` or `-f` - Overwrite existing files.
