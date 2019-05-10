FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/WebStudents/WebStudents.csproj", "src/WebStudents/"]
RUN dotnet restore "src/WebStudents/WebStudents.csproj"
COPY . .
WORKDIR "/src/src/WebStudents"
RUN dotnet build "WebStudents.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebStudents.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebStudents.dll"]