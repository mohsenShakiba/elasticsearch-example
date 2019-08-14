FROM 5.9.211.60:5000/dotnet:2.2-sdk AS build
COPY . .
WORKDIR /ElasticSearchExample
RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o /app
COPY /ElasticSearchExample/Static/* /app/Static/

FROM 5.9.211.60:5000/dotnet:2.2-aspnetcore-runtime AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ElasticSearchExample.dll"]