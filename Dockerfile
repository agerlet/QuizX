# build and test
FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0 AS build-env
WORKDIR /build

COPY . ./

RUN dotnet restore
RUN dotnet test
RUN dotnet publish -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

COPY --from=build-env /build/out ./

CMD ["dotnet", "api.dll"]