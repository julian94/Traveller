FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnet-build
WORKDIR /build

COPY / .

RUN dotnet test
RUN dotnet publish Api/Api.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime-env
WORKDIR /app

COPY --from=dotnet-build /publish .

ENTRYPOINT [ "dotnet", "api.dll" ]
