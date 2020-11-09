FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /test-app/

COPY /CRUD/*.csproj ./
RUN dotnet restore

COPY . ./test-app
RUN dotnet build -c Release
ENTRYPOINT ["dotnet", "run", "-c", "Release", "--no-build"]