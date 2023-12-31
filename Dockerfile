FROM mcr.microsoft.com/dotnet/sdk:7.0 As build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT Production

ENTRYPOINT ["dotnet", "DockerComposeSample.dll"]