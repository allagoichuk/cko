FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

RUN mkdir Checkout.Payments.Api
RUN mkdir Checkout.Payments.Processor
RUN mkdir tests
RUN mkdir tests/Checkout.Payments.Api.UnitTests

# copy sln and csproj and restore as distinct layers
COPY Checkout.Payments.Api/*.csproj ./Checkout.Payments.Api/
COPY Checkout.Payments.Processor/*.csproj ./Checkout.Payments.Processor/
COPY tests/Checkout.Payments.Api.UnitTests/*.csproj ./tests/Checkout.Payments.Api.UnitTests/
COPY *.sln ./
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/Checkout.Payments.Api
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/Checkout.Payments.Api/out .
ENTRYPOINT ["dotnet", "Checkout.Payments.Api.dll"]