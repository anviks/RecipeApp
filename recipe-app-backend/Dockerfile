FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .

# Copy ALL the projects
COPY RecipeApp.Application/*.csproj ./RecipeApp.Application/
COPY RecipeApp.Base/*.csproj ./RecipeApp.Base/
COPY RecipeApp.Infrastructure/*.csproj ./RecipeApp.Infrastructure/
COPY RecipeApp.Resources/*.csproj ./RecipeApp.Resources/
COPY RecipeApp.Test/*.csproj ./RecipeApp.Test/
COPY RecipeApp.Web/*.csproj ./RecipeApp.Web/

RUN dotnet restore

# Copy everything else and build app
COPY RecipeApp.Application/. ./RecipeApp.Application/
COPY RecipeApp.Base/. ./RecipeApp.Base/
COPY RecipeApp.Infrastructure/. ./RecipeApp.Infrastructure/
COPY RecipeApp.Resources/. ./RecipeApp.Resources/
COPY RecipeApp.Test/. ./RecipeApp.Test/
COPY RecipeApp.Web/. ./RecipeApp.Web/

#RUN dotnet test Base.Test

WORKDIR /app/RecipeApp.Web
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
EXPOSE 80
EXPOSE 8080
EXPOSE 443
WORKDIR /app
COPY --from=build /app/RecipeApp.Web/out ./
COPY wait-for-it.sh /app
RUN chmod +x wait-for-it.sh
ENTRYPOINT ["./wait-for-it.sh", "postgres:5432", "-t", "7", "--", "dotnet", "RecipeApp.Web.dll"]
#ENTRYPOINT ["dotnet", "RecipeApp.dll"]
