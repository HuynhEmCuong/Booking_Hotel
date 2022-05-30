#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Booking_Hotel.Web/Booking_Hotel.Web.csproj", "Booking_Hotel.Web/"]
COPY ["Booking_Hotel.Application/Booking_Hotel.Application.csproj", "Booking_Hotel.Application/"]
COPY ["Booking_Hotel.Data/Booking_Hotel.Data.csproj", "Booking_Hotel.Data/"]
COPY ["Booking_Hotel.Data.EF/Booking_Hotel.Data.EF.csproj", "Booking_Hotel.Data.EF/"]
COPY ["Booking_Hotel.Utilities/Booking_Hotel.Utilities.csproj", "Booking_Hotel.Utilities/"]
RUN dotnet restore "Booking_Hotel.Web/Booking_Hotel.Web.csproj"
COPY . .
WORKDIR "/src/Booking_Hotel.Web"
RUN dotnet build "Booking_Hotel.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Booking_Hotel.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Booking_Hotel.Web.dll"]