```bash
dotnet tool update --global dotnet-ef
```

```bash
dotnet tool update -g dotnet-aspnet-codegenerator
```

```bash
dotnet ef migrations --project RecipeApp.Infrastructure --startup-project RecipeApp.Web add Initial
```

```bash
dotnet ef migrations --project RecipeApp.Infrastructure --startup-project RecipeApp.Web remove
```

```bash
dotnet ef database --project RecipeApp.Infrastructure --startup-project RecipeApp.Web update
```

```bash
dotnet ef database --project RecipeApp.Infrastructure --startup-project RecipeApp.Web drop
```

```bash
cd RecipeApp.Web
dotnet aspnet-codegenerator identity -dc RecipeApp.Infrastructure.Data.EntityFramework.AppDbContext -f
cd ..
```

```bash
$WebApp = "RecipeApp.Web"
$Domain = "RecipeApp.Infrastructure/Data/EntityFramework/Entities"
$DbContext = "AppDbContext"
$MvcOutput = "Areas/Admin/Controllers"
$ApiOutput = "ApiControllers"
$GenerateMvcControllers = $False
$GenerateApiControllers = $False

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


```bash
docker build -t recipeapp:latest .

docker compose -f docker-compose-tests.yml up --abort-on-container-exit --exit-code-from testapp
docker compose -f docker-compose-tests.yml up --abort-on-container-exit --exit-code-from testapp --build

# multiplatform build
docker buildx build --progress=plain --force-rm --push -t anviks/recipeapp:latest . 
```
