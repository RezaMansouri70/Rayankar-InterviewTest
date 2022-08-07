#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/03.Presentation/Api/Api.csproj", "Src/03.Presentation/Api/"]
COPY ["Src/01.Core/ApplicationServices/ApplicationServices.csproj", "Src/01.Core/ApplicationServices/"]
COPY ["Src/02.Infrastructures/DataLayer.SqlServer/DataLayer.SqlServer.csproj", "Src/02.Infrastructures/DataLayer.SqlServer/"]
COPY ["Src/01.Core/DomainClass/DomainClass.csproj", "Src/01.Core/DomainClass/"]
RUN dotnet restore "Src/03.Presentation/Api/Api.csproj"
COPY . .
WORKDIR "/src/Src/03.Presentation/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]