#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RSoft.Mail.Web.Grpc/RSoft.Mail.Web.Grpc.Host.csproj", "RSoft.Mail.Web.Grpc/"]
COPY ["RSoft.Mail.Business/RSoft.Mail.Business.csproj", "RSoft.Mail.Business/"]
COPY ["RSoft.Mail.Contract/RSoft.Mail.Contract.csproj", "RSoft.Mail.Contract/"]
COPY [".nuget/nuget.config", ""]
RUN dotnet restore "RSoft.Mail.Web.Grpc/RSoft.Mail.Web.Grpc.Host.csproj"
COPY . .
WORKDIR "/src/RSoft.Mail.Web.Grpc"
RUN dotnet build "RSoft.Mail.Web.Grpc.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RSoft.Mail.Web.Grpc.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RSoft.Mail.Web.Grpc.Host.dll"]