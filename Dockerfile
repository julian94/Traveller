FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnet-build
WORKDIR /build

COPY / .

RUN dotnet restore traveller.csproj
RUN dotnet build traveller.csproj --no-restore -c Release
RUN dotnet publish traveller.csproj --no-restore --no-build -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime-env
WORKDIR /app

COPY --from=dotnet-build /publish .

ENTRYPOINT [ "dotnet", "traveller.dll" ]
